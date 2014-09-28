using InCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InCity.Controllers
{
    public class PlaceController : Controller
    {
        // GET: Place
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int pId)
        {
            InCityDBEntities dbEntity = new InCityDBEntities();
            Place pl = dbEntity.Place.First(p => p.Id == pId);
            PlaceModel place = new PlaceModel(pl);
            return View(place);
        }
    }
}