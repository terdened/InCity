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
        public DateTime mChoosedData;

        public EventsListViewModel()
        {
            InCityDBEntities db = new InCityDBEntities();

            this.mChoosedData = DateTime.Today;

            this.mTagsList = new List<TagModel>();
            foreach(var tag in db.Tag)
                mTagsList.Add(new TagModel(tag));

            this.mChoosedTagsList = new List<TagModel>();

            this.mEventsList = new List<EventModels>();
            foreach (var e in db.Event)
                mEventsList.Add(new EventModels(e));
        }

        public EventsListViewModel(List<TagModel> pChoosedTagsList)
        {
            if (pChoosedTagsList.Count > 0)
            {
                InCityDBEntities db = new InCityDBEntities();

                this.mChoosedData = DateTime.Today;

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

                this.mChoosedData = DateTime.Today;

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