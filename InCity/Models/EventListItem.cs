using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class EventListItem
    {
        public int mId;
        public string mDate;
        public string mTitle;
        public string mDescription;
        public List<PlaceModel> mPlaces;

        public EventListItem()
        {

        }

        public EventListItem(Event pEvendDB, DateTime pDate, List<EventPlace> pEP)
        {
            InCityDBEntities db = new InCityDBEntities();

            //List<EventPlace> eventPlaces = db.EventPlace.Where(ep => ep.EventId == pEvendDB.Id).ToList();
            List<EventPlace> eventPlacesInDate = pEP.Where(ep => ep.EventId == pEvendDB.Id && ep.StartDate <= pDate && ep.EndDate >= pDate).ToList();
            mPlaces = new List<PlaceModel>();

            foreach (var place in eventPlacesInDate)
                mPlaces.Add(new PlaceModel(place.Place));

            this.mDescription = pEvendDB.Description;
            this.mId = pEvendDB.Id;
            this.mTitle = pEvendDB.Title;
        }
    }
}