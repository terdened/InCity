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
            PlaceListViewModel model = new PlaceListViewModel();
            return View(model);
        }

        public ActionResult Show(int pId)
        {
            PlaceViewModel model = new PlaceViewModel(pId);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddType(String pModel, String pType)
        {
            PlaceListViewModel model = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<PlaceListViewModel>(pModel);

            TypeModel type = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<TypeModel>(pType);

            model.mChoosedTypeList.Add(type);
            model = new PlaceListViewModel(model.mChoosedTypeList);
            return PartialView("Index", model);
        }

        [HttpPost]
        public ActionResult RemoveType(String pModel, String pType)
        {
            PlaceListViewModel model = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<PlaceListViewModel>(pModel);

            TypeModel type = new System.Web.Script.Serialization.JavaScriptSerializer().
                Deserialize<TypeModel>(pType);

            model.mChoosedTypeList.Remove(model.mChoosedTypeList.First(t=>t.mId==type.mId));

            if (model.mChoosedTypeList.Count > 0)
                model = new PlaceListViewModel(model.mChoosedTypeList);
            else
                model = new PlaceListViewModel();

            return PartialView("Index", model);
        }
    }
}