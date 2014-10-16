using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace InCity.Models
{
    public class TagModel
    {
        public int mId { get; set; }
        public string mTitle { get; set; }

        public TagModel(Tag pTag)
        {
            this.mId = pTag.Id;
            this.mTitle = pTag.Title;
        }

        public TagModel()
        {
            this.mId = -1;
            this.mTitle = "";
        }

        public TagModel(int pId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            this.mId = pId;
            this.mTitle = db.Tag.First(t=>t.Id == pId).Title;
        }

        public void RemoveFromDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            List<EventTag> eventTags = db.EventTag.Where(et => et.EventId == this.mId).ToList();

            foreach(var et in eventTags)
            {
                db.EventTag.Remove(db.EventTag.First(e => e.EventId == this.mId && e.TagId == et.TagId));
            }

            db.Tag.Remove(db.Tag.First(t => t.Id == this.mId));
            db.SaveChanges();
        }

        public void SaveInDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();

            if(this.mId == -1)
                this.mId = db.Tag.Max(t=>t.Id)+1;

            if(db.Tag.Where(t=>t.Id == this.mId).Count()>0)
            {
                db.Tag.First(t => t.Id == this.mId).Id = this.mId;
                db.Tag.First(t => t.Id == this.mId).Title = this.mTitle;
            }
            else
            {
                Tag tag = new Tag();
                tag.Id = this.mId;
                tag.Title = this.mTitle;
                db.Tag.Add(tag);
            }

            db.SaveChanges();
        }

    }
}