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
        public DateTime mDate;
        public PlaceModel mPlace;

        public EventModels(Event pDBEvent)
        {
            this.mId = pDBEvent.Id;
            this.mTitle = pDBEvent.Title;
            this.mDescription = pDBEvent.Description;
            this.mDate = (DateTime)pDBEvent.Date;

            if (pDBEvent.Place1!=null)
                this.mPlace = new PlaceModel(pDBEvent.Place1);
            else
                this.mPlace = new PlaceModel(pDBEvent.Place);
        }
    }
}