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

        }

    }
}