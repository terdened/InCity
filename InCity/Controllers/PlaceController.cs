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
            InCityDBEntities dbEntity = new InCityDBEntities();
            List<PlaceModel> places = new List<PlaceModel>();

            foreach (var p in dbEntity.Place)
                places.Add(new PlaceModel(p));

            return View(places);
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