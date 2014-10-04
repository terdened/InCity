using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class EventModels
    {
        public int mId;
        public string mTitle;
        public string mDescription;
        public string mStartDate;
        public string mEndDate;
        public string mPoster;
        public List<PlaceModel> mPlace;

        public EventModels(Event pDBEvent)
        {
            this.mId = pDBEvent.Id;
            this.mTitle = pDBEvent.Title;
            this.mDescription = pDBEvent.Description;

            this.mStartDate = pDBEvent.EventPlace.Min(ep => ep.StartDate).ToString();
            this.mEndDate = pDBEvent.EventPlace.Max(ep => ep.EndDate).ToString();

            this.mPlace = new List<PlaceModel>();

            foreach(var ep in pDBEvent.EventPlace)
                this.mPlace.Add(new PlaceModel(ep.Place));

            if (pDBEvent.PosterId != null)
            {
                this.mPoster = pDBEvent.Pictures.Path;
            }
            else
            {
                this.mPoster = "Content/Img/Posters/noposter.jpg";
            }
        }

        public EventModels()
        {

        }

        private string getValue(string pIn)
        {
            return pIn.Split(' ')[0];
        }
    }
}