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

        public PlaceModel(Place pDBPlace)
        {
            this.mId = pDBPlace.Id;
            this.mTitle = pDBPlace.Title;
            this.mAddress = pDBPlace.Address;

            if (pDBPlace.HeaderId != null)
                mHeader = pDBPlace.Pictures.Path;
            else
                mHeader = "";
        }

        public PlaceModel(String pAddress)
        {
            mAddress = pAddress;
        }
    }
}