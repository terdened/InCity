using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class TagModel
    {
        public int mId;
        public string mTitle;

        public TagModel(Tag pTag)
        {
            this.mId = pTag.Id;
            this.mTitle = pTag.Title;
        }

        public TagModel()
        {

        }

    }
}