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
        public string mPoster;
        public List<PlaceModel> mPlaces;
        public List<string> mPrices;
        public List<TagModel> mTags;

        public EventListItem()
        {

        }

        public EventListItem(Event pEvendDB, DateTime pDate, List<EventPlace> pEP)
        {
            InCityDBEntities1 db = new InCityDBEntities1();

            //List<EventPlace> eventPlaces = db.EventPlace.Where(ep => ep.EventId == pEvendDB.Id).ToList();
            List<EventPlace> eventPlacesInDate = pEP.Where(ep => ep.EventId == pEvendDB.Id && ep.StartDate <= pDate && ep.EndDate >= pDate).ToList();
            mPlaces = new List<PlaceModel>();
            mPrices = new List<string>();

            foreach (var place in eventPlacesInDate)
            {
                mPlaces.Add(new PlaceModel(place.Place));
                if(place.Price!=null)
                    mPrices.Add(place.Price);
                else
                    mPrices.Add("-");
            }

            mTags = new List<TagModel>();

            foreach (var tag in pEvendDB.EventTag)
            {
                mTags.Add(new TagModel(db.Tag.First(t=>t.Id==tag.TagId)));
            }
                

            this.mDescription = pEvendDB.Description;
            this.mId = pEvendDB.Id;
            this.mTitle = pEvendDB.Title;
            this.mDate = pDate.ToString();

            if (pEvendDB.PosterId != null)
            {
                this.mPoster = pEvendDB.Pictures.Path;
            }
            else
            {
                this.mPoster = "Content/Img/Posters/noposter.jpg";
            }
        }
    }
}