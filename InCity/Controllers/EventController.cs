using InCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InCity.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            EventsListViewModel model = new EventsListViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTag(String pModel, String pTag)
        {
            EventsListViewModel model = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<EventsListViewModel>(pModel);

            TagModel tag = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<TagModel>(pTag);

            model.mChoosedTagsList.Add(tag);
            model = new EventsListViewModel(model.mChoosedTagsList);
            return PartialView("Index", model);
        }

        [HttpPost]
        public ActionResult RemoveTag(String pModel, String pTag)
        {
            EventsListViewModel model = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<EventsListViewModel>(pModel);

            TagModel tag = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<TagModel>(pTag);

            model.mChoosedTagsList.Remove(model.mChoosedTagsList.First(t=>t.mId==tag.mId));
            model = new EventsListViewModel(model.mChoosedTagsList);
            return PartialView("Index", model);
        }

        [HttpPost]
        public ActionResult ChooseDate(String pModel, String pDate)
        {
            EventsListViewModel model = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<EventsListViewModel>(pModel);

            DateTime date = DateTime.Parse(pDate);
            model = new EventsListViewModel(date);
            return PartialView("Index", model);
        }
    }
}