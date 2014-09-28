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

        public PlaceModel(Place pDBPlace)
        {
            this.mId = pDBPlace.Id;
            this.mTitle = pDBPlace.Title;
            this.mAddress = pDBPlace.Address;
        }

        public PlaceModel(String pAddress)
        {
            mAddress = pAddress;
        }
    }
}