using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace InCity.Models
{
    public class TagModel
    {
        public int mId;
        public string mTitle;
        [ScriptIgnore]
        public Tag mTag;

        public TagModel(Tag pTag)
        {
            this.mId = pTag.Id;
            this.mTitle = pTag.Title;
            this.mTag = pTag;
        }

        public TagModel()
        {

        }

    }
}