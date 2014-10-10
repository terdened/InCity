using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class PlaceViewModel
    {
        public PlaceModel mPlace;
        public EventsListViewModel mEventList;
        public List<TypeModel> mTypes;

        public PlaceViewModel(int pId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();

            Place place = db.Place.First(p=>p.Id==pId);

            mPlace = new PlaceModel(place);
            mTypes = new List<TypeModel>();

            foreach (var type in place.Type)
                this.mTypes.Add(new TypeModel(type));

            List<EventPlace> eventsInPlace = place.EventPlace.Where(ep=>ep.EndDate >= DateTime.Today).ToList();
            List<Event> eventsList = new List<Event>();

            foreach(var ep in eventsInPlace)
                eventsList.Add(ep.Event);

            eventsList = eventsList.Distinct().ToList();

            this.mEventList = new EventsListViewModel(eventsList, place);
        }
    }
}