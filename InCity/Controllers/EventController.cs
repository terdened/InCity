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
            InCityDBEntities dbEntity = new InCityDBEntities();
            List<EventModels> events= new List<EventModels>();

            foreach (var e in dbEntity.Event)
                events.Add(new EventModels(e));

            events.Sort((x, y) => DateTime.Compare(x.mDate, y.mDate));
            return View(events);
        }
    }
}