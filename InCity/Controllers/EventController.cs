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

        public ActionResult Show(int pId)
        {
            InCityDBEntities1 db = new InCityDBEntities1();

            Event eventDB = db.Event.First(e=>e.Id == pId);
            EventModels model = new EventModels(eventDB);
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

            

            if (pDate=="today")
            {
                model = new EventsListViewModel(DateTime.Today);
            }
            else
            if (pDate == "tomorrow")
            {
                model = new EventsListViewModel(DateTime.Today.AddDays(1));
            }
            else
            if (pDate == "week")
            {
                int start = (int)DateTime.Today.DayOfWeek;
                int target = (int)7;
                if (target <= start)
                    target += 7;
                model = new EventsListViewModel(DateTime.Today, DateTime.Today.AddDays(target - start));
            }
            else
            if (pDate == "all")
            {
                model = new EventsListViewModel();
            }
            else
            {
                DateTime date = DateTime.Parse(pDate);
                model = new EventsListViewModel(date);
            }

            return PartialView("Index", model);
        }
    }
}