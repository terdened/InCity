using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class PlaceModel
    {
        public int mId;
        public string mTitle;
        public string mAddress;
        public string mHeader;
        public string mLatitude;
        public string mLongitude;

        public PlaceModel(Place pDBPlace)
        {
            this.mId = pDBPlace.Id;
            this.mTitle = pDBPlace.Title;
            this.mAddress = pDBPlace.Address;

            if (pDBPlace.HeaderId != null)
                mHeader = pDBPlace.Pictures.Path;
            else
                mHeader = "";

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
                this.mHeader = "";

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