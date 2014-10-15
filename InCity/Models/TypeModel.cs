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

        }
    }
}