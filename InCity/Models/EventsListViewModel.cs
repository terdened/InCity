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
        public List<EventModels> mEventsList;
        public string mChoosedData;
        public string mTitleDate;

        public EventsListViewModel()
        {
            InCityDBEntities db = new InCityDBEntities();

            this.mChoosedData = DateTime.Today.ToString();
            this.mTitleDate = "all";
 
            this.mTagsList = new List<TagModel>();
            foreach(var tag in db.Tag)
                mTagsList.Add(new TagModel(tag));

            this.mChoosedTagsList = new List<TagModel>();

            this.mEventsList = new List<EventModels>();
            foreach (var e in db.Event)
                mEventsList.Add(new EventModels(e));
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

            this.mEventsList = new List<EventModels>();

            List<EventPlace> ep = db.EventPlace.Where(ev => ev.StartDate <= pDate && ev.EndDate >= pDate).ToList();
            foreach (var e in ep)
                mEventsList.Add(new EventModels(e.Event));
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

            this.mEventsList = new List<EventModels>();

            List<EventPlace> ep = db.EventPlace.Where(ev => (ev.StartDate <= pStartDate && ev.EndDate >= pStartDate)||
                                                            (ev.StartDate <= pEndDate && ev.EndDate >= pEndDate) ||
                                                            (ev.StartDate >= pStartDate && ev.StartDate <= pEndDate) ||
                                                            (ev.EndDate <= pStartDate && ev.EndDate >= pEndDate)).ToList();
            foreach (var e in ep)
                mEventsList.Add(new EventModels(e.Event));
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

                this.mEventsList = new List<EventModels>();

                List<Event> eventList = new List<Event>();
                foreach (var ct in mChoosedTagsList)
                {
                    Tag t = db.Tag.First(tag => tag.Id == ct.mId);
                    List<Event> events = new List<Event>();

                    foreach (var e in db.Event)
                    {
                        if (e.Tag.Contains(t))
                            events.Add(e);
                    }
                    eventList.AddRange(events);
                }

                foreach (var e in eventList)
                    this.mEventsList.Add(new EventModels(e));
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

                this.mEventsList = new List<EventModels>();
                foreach (var e in db.Event)
                    mEventsList.Add(new EventModels(e));   
            }
        }

    }
}