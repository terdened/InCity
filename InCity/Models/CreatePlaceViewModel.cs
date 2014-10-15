using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class CreatePlaceViewModel
    {
        public int mId { get; set; }
        public string mTitle { get; set; }
        public string mAddress { get; set; }
        public string mLongitude { get; set; }
        public string mLatitude { get; set; }
        public int mHeaderId { get; set; }
        public List<TypeModel> mPlaceType { get; set; }
        public List<TypeModel> mTypesList { get; set; }
        public HttpPostedFileBase mHeaderPath { get; set; }

        public CreatePlaceViewModel()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            this.mId = db.Event.Max(e => e.Id) + 1;
            this.mTitle = "Введите заголовок";
            this.mAddress = "Введите описание";
            this.mHeaderId = 0;

            this.mPlaceType = new List<TypeModel>();
            this.mTypesList = new List<TypeModel>();
            List<Type> types = db.Type.ToList();

            foreach (var type in types)
                this.mTypesList.Add(new TypeModel(type));
        }

        public void SaveInDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            Place placeDB = new Place();
            this.mId = db.Place.Max(e => e.Id) + 1;
            placeDB.Id = this.mId;
            placeDB.Title = this.mTitle;
            placeDB.Address = this.mAddress;
            placeDB.HeaderId = this.mHeaderId;
            placeDB.Latitude = this.mLatitude;
            placeDB.Longitude = this.mLongitude;

            foreach (var t in this.mPlaceType)
                placeDB.Type.Add(db.Type.First(typ=>typ.Id==t.mId));

            db.Place.Add(placeDB);
            db.SaveChanges();
        }
    }
}