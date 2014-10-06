using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class EventsListViewModel
    {
        public List<TagModel> mTagsList;
        public List<TagModel> mChoosedTagsList;
        public List<EventListItem> mEventsList;
        public string mChoosedData;
        public string mTitleDate;


        public DateTime getLastEventDate()
        {
            InCityDBEntities db = new InCityDBEntities();
            return db.EventPlace.Max(ep => ep.EndDate);
        }

        public void getEvents(DateTime pStartDate, DateTime pEndDate)
        {
            InCityDBEntities db = new InCityDBEntities();
            mEventsList = new List<EventListItem>();

            DateTime startDate = pStartDate;
            DateTime stopDate = pEndDate;
            DateTime currentDate = startDate;

            List<Event> eventsWithDate = (from e in db.Event
                                          join ep in db.EventPlace on e.Id equals ep.EventId
                                          where ((ep.StartDate <= startDate && ep.EndDate >= startDate) ||
                                                  (ep.StartDate <= stopDate && ep.EndDate >= stopDate) ||
                                               (ep.StartDate >= startDate && ep.StartDate <= stopDate) ||
                                                   (ep.EndDate <= startDate && ep.EndDate >= stopDate))&&
                                                   (ep.EventId == e.Id)
                                          select e).ToList();

            List<EventPlace> eventPlacesWithDate = db.EventPlace.Where(ev => (ev.StartDate <= startDate && ev.EndDate >= startDate) ||
                                                                               (ev.StartDate <= stopDate && ev.EndDate >= stopDate) ||
                                                                            (ev.StartDate >= startDate && ev.StartDate <= stopDate) ||
                                                                               (ev.EndDate <= startDate && ev.EndDate >= stopDate)).ToList();

            while (currentDate <= stopDate)
            {
                List<Event> eventsInCurrentDate = eventsWithDate.Where(e => e.EventPlace.Where(ep => (ep.StartDate <= currentDate && ep.EndDate >= currentDate)).Count()>0).ToList();
                eventsInCurrentDate = eventsInCurrentDate.Distinct().ToList();

                if (this.mChoosedTagsList.Count > 0)
                {
                    List<Event> filteredEvents = new List<Event>();

                    foreach (var tag in mChoosedTagsList)
                        foreach (var e in eventsInCurrentDate)
                            if (e.Tag.Where(t => t.Id == tag.mId).Count() > 0)
                                filteredEvents.Add(e);

                 
                    foreach (var e in filteredEvents)
                        mEventsList.Add(new EventListItem(e, currentDate, eventPlacesWithDate));
                    
                }
                else
                {
                    foreach (var e in eventsInCurrentDate)
                        mEventsList.Add(new EventListItem(e, currentDate, eventPlacesWithDate));
                }

                currentDate = currentDate.AddDays(1);
            }
        }

        public void getEvents(List<Event> pEvents, Place pPlace, DateTime pStartDate, DateTime pEndDate)
        {
            mEventsList = new List<EventListItem>();

            DateTime startDate = pStartDate;
            DateTime stopDate = pEndDate;
            DateTime currentDate = startDate;
            

            while (currentDate <= stopDate)
            {
                List<Event> eventsInCurrentDate = pEvents.Where(e => e.EventPlace.Where(ep => (ep.StartDate <= currentDate && ep.EndDate >= currentDate)).Count() > 0).ToList();
                eventsInCurrentDate = eventsInCurrentDate.Distinct().ToList();

                foreach (var e in eventsInCurrentDate)
                {
                    List<EventPlace> eventPlace = new List<EventPlace>();
                    eventPlace.Add(pPlace.EventPlace.First(ep => ep.EventId == e.Id));
                    mEventsList.Add(new EventListItem(e, currentDate, eventPlace));
                }
                   
                currentDate = currentDate.AddDays(1);
            }
        }


        public EventsListViewModel(List<Event> pEventList, Place pPlace)
        {
            InCityDBEntities db = new InCityDBEntities();

            mEventsList = new List<EventListItem>();
            DateTime startDate = DateTime.Today;
            DateTime endDate = db.EventPlace.Where(ep=>ep.PlaceId==pPlace.Id).Max(ep=>ep.EndDate);

            getEvents(pEventList, pPlace, startDate, endDate);

            this.mTagsList = new List<TagModel>();
            foreach (var tag in db.Tag)
                mTagsList.Add(new TagModel(tag));

            this.mChoosedTagsList = new List<TagModel>();
        }
        public EventsListViewModel()
        {
            InCityDBEntities db = new InCityDBEntities();

            this.mChoosedData = DateTime.Today.ToString();
            this.mTitleDate = "all";
 
            this.mTagsList = new List<TagModel>();
            foreach(var tag in db.Tag)
                mTagsList.Add(new TagModel(tag));

            this.mChoosedTagsList = new List<TagModel>();

            getEvents(DateTime.Today, getLastEventDate());
        }

        public EventsListViewModel(DateTime pDate)
        {
            InCityDBEntities db = new InCityDBEntities();

            this.mChoosedData = pDate.ToString();
            
            if(pDate == DateTime.Today)
                this.mTitleDate = "today";
            else
            if (pDate == DateTime.Today.AddDays(1))
                this.mTitleDate = "tomorrow";
            else
                this.mTitleDate = "other";

            this.mTagsList = new List<TagModel>();
            foreach (var tag in db.Tag)
                mTagsList.Add(new TagModel(tag));

            this.mChoosedTagsList = new List<TagModel>();

            getEvents(DateTime.Parse(this.mChoosedData), DateTime.Parse(this.mChoosedData));
        }

        public EventsListViewModel(DateTime pStartDate, DateTime pEndDate)
        {
            InCityDBEntities db = new InCityDBEntities();

            this.mChoosedData = DateTime.Today.ToString();
            this.mTitleDate = "week";

            this.mTagsList = new List<TagModel>();
            foreach (var tag in db.Tag)
                mTagsList.Add(new TagModel(tag));

            this.mChoosedTagsList = new List<TagModel>();

            getEvents(pStartDate, pEndDate);

            /*this.mEventsList = new List<EventModels>();

            List<EventPlace> ep = db.EventPlace.Where(ev => (ev.StartDate <= pStartDate && ev.EndDate >= pStartDate)||
                                                            (ev.StartDate <= pEndDate && ev.EndDate >= pEndDate) ||
                                                            (ev.StartDate >= pStartDate && ev.StartDate <= pEndDate) ||
                                                            (ev.EndDate <= pStartDate && ev.EndDate >= pEndDate)).ToList();
            foreach (var e in ep)
                mEventsList.Add(new EventModels(e.Event));*/
        }

        public EventsListViewModel(List<TagModel> pChoosedTagsList)
        {
            if (pChoosedTagsList.Count > 0)
            {
                InCityDBEntities db = new InCityDBEntities();

                this.mChoosedData = DateTime.Today.ToString();
                this.mTitleDate = "all";

                this.mTagsList = new List<TagModel>();
                foreach (var tag in db.Tag)
                    mTagsList.Add(new TagModel(tag));

                this.mChoosedTagsList = pChoosedTagsList;

                getEvents(DateTime.Today, getLastEventDate());
            }
            else
            {
                InCityDBEntities db = new InCityDBEntities();

                this.mChoosedData = DateTime.Today.ToString();
                this.mTitleDate = "all";

                this.mTagsList = new List<TagModel>();
                foreach (var tag in db.Tag)
                    mTagsList.Add(new TagModel(tag));

                this.mChoosedTagsList = new List<TagModel>();

                getEvents(DateTime.Today, getLastEventDate()); 
            }
        }

    }
}