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
            try 
            {
                this.mId = db.Place.Max(e => e.Id) + 1;
            }
            catch
            {
                this.mId = 1;
            }
            
            this.mTitle = "";
            this.mAddress = "";
            this.mHeaderId = 0;

            this.mPlaceType = new List<TypeModel>();
            this.mTypesList = new List<TypeModel>();
            List<Type> types = db.Type.ToList();

            foreach (var type in types)
                this.mTypesList.Add(new TypeModel(type));
        }

        public CreatePlaceViewModel(int pId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            Place placeInDB = db.Place.First(p => p.Id == pId);

            this.mId = placeInDB.Id;
            this.mTitle = placeInDB.Title;
            this.mAddress = placeInDB.Address;
            this.mLatitude = placeInDB.Latitude;
            this.mLongitude = placeInDB.Longitude;
            if (placeInDB.HeaderId!=null)
                this.mHeaderId = (int) placeInDB.HeaderId;
            else
                this.mHeaderId = 0;

            this.mPlaceType = new List<TypeModel>();

            foreach (var type in placeInDB.Type)
                mPlaceType.Add(new TypeModel(type));

            this.mTypesList = new List<TypeModel>();
            List<Type> types = db.Type.ToList();

            foreach (var type in types)
                this.mTypesList.Add(new TypeModel(type));
        }

        public void RemoveFromDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            try
            {
                List<EventPlace> eventPlaces = db.EventPlace.Where(ep => ep.PlaceId == this.mId).ToList();
                db.EventPlace.RemoveRange(eventPlaces);

                db.Place.First(p => p.Id == this.mId).Type.Clear();

                db.Place.Remove(db.Place.First(p => p.Id == this.mId));
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public void SaveInDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            Place placeDB = new Place();

            if (db.Place.Where(p => p.Id == this.mId).Count() == 0)
            {
                this.mId = db.Place.Max(e => e.Id) + 1;
                placeDB.Id = this.mId;
                placeDB.Title = this.mTitle;
                placeDB.Address = this.mAddress;
                if (this.mHeaderId != 0)
                    placeDB.HeaderId = this.mHeaderId;
                else
                    placeDB.HeaderId = null;
                placeDB.Latitude = this.mLatitude;
                placeDB.Longitude = this.mLongitude;

                foreach (var t in this.mPlaceType)
                    placeDB.Type.Add(db.Type.First(typ => typ.Id == t.mId));

                db.Place.Add(placeDB);
            }
            else
            {
                db.Place.First(p => p.Id == this.mId).Title = this.mTitle;
                db.Place.First(p => p.Id == this.mId).Address = this.mAddress;
                if (this.mHeaderId != 0)
                    db.Place.First(p => p.Id == this.mId).HeaderId = this.mHeaderId;
                else
                    db.Place.First(p => p.Id == this.mId).HeaderId = null;
                db.Place.First(p => p.Id == this.mId).Latitude = this.mLatitude;
                db.Place.First(p => p.Id == this.mId).Longitude = this.mLongitude;
                db.Place.First(p => p.Id == this.mId).Type.Clear();

                foreach (var t in this.mPlaceType)
                    db.Place.First(p => p.Id == this.mId).Type.Add(db.Type.First(typ => typ.Id == t.mId));
            }
            db.SaveChanges();
        }
    }
}