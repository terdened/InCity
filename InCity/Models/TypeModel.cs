using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class TypeModel
    {
        public int mId { get; set; }
        public string mTitle { get; set; }

        public TypeModel(Type pType)
        {
            this.mId = pType.Id;
            this.mTitle = pType.Title;
        }

        public TypeModel(int pId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            Type type = db.Type.First(t => t.Id == pId);
            this.mId = type.Id;
            this.mTitle = type.Title;
        }

        public TypeModel()
        {
            this.mId = -1;
            this.mTitle = "";
        }

        public void RemoveFromDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();
            Type type = db.Type.First(t=>t.Id == this.mId);
            type.Place.Clear();

            db.Type.Remove(db.Type.First(t => t.Id == this.mId));
            db.SaveChanges();
        }

        public void SaveInDB()
        {
            InCityDBEntities1 db = new InCityDBEntities1();

            if(this.mId == -1)
                this.mId = db.Type.Max(t=>t.Id)+1;

            if(db.Type.Where(t=>t.Id == this.mId).Count()>0)
            {
                db.Type.First(t => t.Id == this.mId).Id = this.mId;
                db.Type.First(t => t.Id == this.mId).Title = this.mTitle;
            }
            else
            {
                Type type = new Type();
                type.Id = this.mId;
                type.Title = this.mTitle;
                db.Type.Add(type);
            }

            db.SaveChanges();
        }
    }
}