using InCity.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InCity.Controllers
{
    public class ContentFillerController : Controller
    {
        string mPass = "Amaterassu11071959";

        public ActionResult Index(string pPass)
        {
            if (mPass != pPass)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NewEvent(string pPass)
        {
            if (mPass != pPass)
                return RedirectToAction("Index", "Home");
            CreateEventViewModel model = new CreateEventViewModel();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NewPlace(string pPass)
        {
            if (mPass != pPass)
                return RedirectToAction("Index", "Home");
            CreatePlaceViewModel model = new CreatePlaceViewModel();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewEvent(CreateEventViewModel model)
        {
            return View("NewEvent",model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewPlace(CreatePlaceViewModel model)
        {
            return View("NewPlace", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewEventAddPlace(CreateEventViewModel pModel)
        {
            pModel.mEventPlaces.Add(new EventPlaceModel(pModel.mId));
            return NewEvent(pModel);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewEventRemovePlace(CreateEventViewModel pModel)
        {
            if(pModel.mEventPlaces.Count>0)
                pModel.mEventPlaces.RemoveAt(pModel.mEventPlaces.Count-1);
            return NewEvent(pModel);
        }

        public ActionResult NewPlaceAddType(CreatePlaceViewModel pModel)
        {
            pModel.mPlaceType.Add(new TypeModel());
            return NewPlace(pModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewPlaceRemoveType(CreatePlaceViewModel pModel)
        {
            if (pModel.mPlaceType.Count > 0)
                pModel.mPlaceType.RemoveAt(pModel.mPlaceType.Count - 1);
            return NewPlace(pModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewEventAddTag(CreateEventViewModel pModel)
        {
            pModel.mEventTag.Add(new TagModel());
            return NewEvent(pModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewEventRemoveTag(CreateEventViewModel pModel)
        {
            if (pModel.mEventTag.Count > 0)
                pModel.mEventTag.RemoveAt(pModel.mEventTag.Count - 1);
            return NewEvent(pModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewEventSave(CreateEventViewModel pModel)
        {
            pModel.SaveInDB();
            return RedirectToAction("Index", new { pPass = mPass});
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewPlaceSave(CreatePlaceViewModel pModel)
        {
            pModel.SaveInDB();
            return RedirectToAction("Index", new { pPass = mPass });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewEventDownloadImage(CreateEventViewModel pModel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                InCityDBEntities1 db = new InCityDBEntities1();
                string pic = (db.Pictures.Max(p=>p.Id)+1).ToString()+".jpg";
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Img/Posters"), pic);
                file.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                Pictures picture = new Pictures();
                picture.Id = db.Pictures.Max(p => p.Id)+1;
                picture.Path = "../../Content/Img/Posters/" + picture.Id.ToString() + ".jpg";
                db.Pictures.Add(picture);
                db.SaveChanges();

                pModel.mPosterId = picture.Id;
            }    

            return NewEvent(pModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewPlaceDownloadImage(CreatePlaceViewModel pModel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                InCityDBEntities1 db = new InCityDBEntities1();
                string pic = (db.Pictures.Max(p => p.Id) + 1).ToString() + ".jpg";
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Img/Headers"), pic);
                file.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                Pictures picture = new Pictures();
                picture.Id = db.Pictures.Max(p => p.Id) + 1;
                picture.Path = "../../Content/Img/Headers/" + picture.Id.ToString() + ".jpg";
                db.Pictures.Add(picture);
                db.SaveChanges();

                pModel.mHeaderId = picture.Id;
            }

            return NewPlace(pModel);
        }

        public ActionResult EditEvent(int pId, string pPass)
        {
            if (mPass != pPass)
                return RedirectToAction("Index", "Home");

            CreateEventViewModel model = new CreateEventViewModel(pId);
            return NewEvent(model);
        }

        public ActionResult RemoveEvent(int pId, string pPass)
        {
            if (mPass != pPass)
                return RedirectToAction("Index", "Home");

            CreateEventViewModel model = new CreateEventViewModel(pId);
            model.RemoveFromDB();

            return RedirectToAction("EventsList", new { pPass = mPass });
        }

        public ActionResult EventsList(string pPass)
        {
            if (mPass != pPass)
                return RedirectToAction("Index", "Home");

            InCityDBEntities1 db = new InCityDBEntities1();
            List<Event> events = db.Event.ToList();
            List<EventModels> model = new List<EventModels>();

            foreach (var e in events)
            {
                model.Add(new EventModels(e));
            }

            return View(model);
        }
    }
}