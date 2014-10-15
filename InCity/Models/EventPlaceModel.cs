using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class EventPlaceModel
    {
        public int mEventId { get; set; }
        public int mPlaceId { get; set; }
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
        public string mPrice { get; set; }

        public EventPlaceModel()
        {

        }

        public EventPlaceModel(int pEventId)
        {
            this.mEventId = pEventId;
            this.mPlaceId = 0;
            this.mStartDate = DateTime.Today;
            this.mEndDate = DateTime.Today;
            this.mPrice = "";
        }

        public EventPlaceModel(int pEventId, int pPlaceId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            EventPlace eventPlace = db.EventPlace.First(ep => ep.EventId == pEventId && ep.PlaceId == pPlaceId);
            this.mEventId = eventPlace.EventId;
            this.mPlaceId = eventPlace.PlaceId;
            this.mStartDate = eventPlace.StartDate;
            this.mEndDate = eventPlace.EndDate;
            this.mPrice = eventPlace.Price;
        }
    }
}