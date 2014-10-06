using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class TypeModel
    {
        public int mId;
        public string mTitle;

        public TypeModel(Type pType)
        {
            this.mId = pType.Id;
            this.mTitle = pType.Title;
        }

        public TypeModel()
        {

        }
    }
}