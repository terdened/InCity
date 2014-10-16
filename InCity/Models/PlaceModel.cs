using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class PlaceModel
    {
        public int mId { get; set; }
        public string mTitle { get; set; }
        public string mAddress { get; set; }
        public string mHeader { get; set; }
        public string mLatitude { get; set; }
        public string mLongitude { get; set; }

        public PlaceModel(Place pDBPlace)
        {
            this.mId = pDBPlace.Id;
            this.mTitle = pDBPlace.Title;
            this.mAddress = pDBPlace.Address;

            if (pDBPlace.HeaderId != null)
                mHeader = pDBPlace.Pictures.Path;
            else
                mHeader = "Content/Img/Headers/notitle.jpg";

            this.mLatitude = pDBPlace.Latitude;
            this.mLongitude = pDBPlace.Longitude;
        }

        public PlaceModel(int pId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            Place place = db.Place.First(p => p.Id == pId);
            this.mId = place.Id;
            this.mTitle = place.Title;
            this.mAddress = place.Address;

            if (place.HeaderId != null)
                this.mHeader = place.Pictures.Path;
            else
                this.mHeader = "Content/Img/Headers/notitle.jpg";

            this.mLatitude = place.Latitude;
            this.mLongitude = place.Longitude;

        }

        public PlaceModel(String pAddress)
        {
            mAddress = pAddress;
        }

        public PlaceModel()
        {

        }
    }
}