using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class CreateEventViewModel
    {
        public int mId { get; set; }
        public string mTitle { get; set; }
        public string mDescription { get; set; }
        public int mPosterId { get; set; }
        public List<EventPlaceModel> mEventPlaces { get; set; }
        public List<TagModel> mEventTag { get; set; }
        public List<PlaceModel> mPlacesList { get; set; }
        public List<TagModel> mTagsList { get; set; }
        public HttpPostedFileBase mPosterPath { get; set; }

        public CreateEventViewModel()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            try
            {
                this.mId = db.Event.Max(e => e.Id) + 1;
            }
            catch
            {
                this.mId = 1;
            }
            this.mTitle = "Введите заголовок";
            this.mDescription = "Введите описание";
            this.mPosterId = 0;

            this.mEventPlaces = new List<EventPlaceModel>();
            this.mEventTag = new List<TagModel>();
            this.mPlacesList = new List<PlaceModel>();
            List<Place> places = db.Place.ToList();

            foreach(var place in places)
                this.mPlacesList.Add(new PlaceModel(place));

            this.mTagsList = new List<TagModel>();
            List<Tag> tags = db.Tag.ToList();

            foreach (var tag in tags)
                this.mTagsList.Add(new TagModel(tag));
        }

        public CreateEventViewModel(int pId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            Event eventInDB = db.Event.First(e => e.Id == pId);

            this.mId = eventInDB.Id;
            this.mTitle = eventInDB.Title;
            this.mDescription = eventInDB.Description;
            this.mPosterId = (int) eventInDB.PosterId;

            List<EventPlace> eventPlaces = db.EventPlace.Where(ep => ep.EventId == this.mId).ToList();
            this.mEventPlaces = new List<EventPlaceModel>();

            foreach (var ep in eventPlaces)
                this.mEventPlaces.Add(new EventPlaceModel(ep.EventId, ep.PlaceId));
            
            this.mEventTag = new List<TagModel>();
            List<EventTag> tagsList = db.EventTag.Where(tag => tag.EventId == this.mId).ToList();

            foreach (var tag in tagsList)
                mEventTag.Add(new TagModel(db.Tag.First(t=>t.Id == tag.TagId)));

            this.mPlacesList = new List<PlaceModel>();
            List<Place> places = db.Place.ToList();

            foreach (var place in places)
                this.mPlacesList.Add(new PlaceModel(place));

            this.mTagsList = new List<TagModel>();
            List<Tag> tags = db.Tag.ToList();

            foreach (var tag in tags)
                this.mTagsList.Add(new TagModel(tag));
        }

        public void RemoveFromDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            try
            {
                List<EventPlace> eventPlaces = db.EventPlace.Where(ep => ep.EventId == this.mId).ToList();
                db.EventPlace.RemoveRange(eventPlaces);

                List<EventTag> eventTags = db.EventTag.Where(ep => ep.EventId == this.mId).ToList();
                db.EventTag.RemoveRange(eventTags);

                db.Event.Remove(db.Event.First(e => e.Id == this.mId));
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public void SaveInDB()
        {
            RemoveFromDB();

            InCityDBEntities1 db = new InCityDBEntities1();
            Event eventDB = new Event();

            try
            {
                this.mId = db.Event.Max(e => e.Id) + 1;
            }
            catch
            {
                this.mId = 1;
            }

            eventDB.Id = this.mId;
            eventDB.Title = this.mTitle;
            eventDB.Description = this.mDescription;
            eventDB.PosterId = this.mPosterId;

            foreach(var t in this.mEventTag)
            {
                EventTag et = new EventTag();
                et.EventId = this.mId;
                et.TagId = t.mId;
                eventDB.EventTag.Add(et);
            }

            db.Event.Add(eventDB);
            db.SaveChanges();

            List<EventPlace> eventPlaces = new List<EventPlace>();

            foreach(var eventPlace in this.mEventPlaces)
            {
                EventPlace ep = new EventPlace();
                ep.EventId = this.mId;
                ep.PlaceId = eventPlace.mPlaceId;
                ep.Price = eventPlace.mPrice;
                ep.StartDate = eventPlace.mStartDate;
                ep.EndDate = eventPlace.mEndDate;
                eventPlaces.Add(ep);
            }

            db.EventPlace.AddRange(eventPlaces);
            db.SaveChanges();
        }
    }
}