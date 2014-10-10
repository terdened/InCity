using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InCity.Models
{
    public class PlaceListViewModel
    {
        public List<PlaceModel> mPlaceList;
        public List<TypeModel> mChoosedTypeList;
        public List<TypeModel> mTypeList;

        public PlaceListViewModel()
        {
            InCityDBEntities1 db = new InCityDBEntities1();

            this.mPlaceList = new List<PlaceModel>();

            List<Place> places = db.Place.ToList();

            foreach(var place in places)
            {
                this.mPlaceList.Add(new PlaceModel(place));
            }

            this.mChoosedTypeList = new List<TypeModel>();
            this.mTypeList = new List<TypeModel>();

            List<Type> types = db.Type.ToList();

            foreach (var type in types)
                this.mTypeList.Add(new TypeModel(type)); 
        }

        public PlaceListViewModel(List<TypeModel> pChoosedTypeModel)
        {
            InCityDBEntities1 db = new InCityDBEntities1();

            mPlaceList = new List<PlaceModel>();
            this.mChoosedTypeList = pChoosedTypeModel;

            List<Place> places = db.Place.ToList();

            List<Place> filtredPlaces = new List<Place>();

            foreach (var type in this.mChoosedTypeList)
            {
                filtredPlaces.AddRange(places.Where(p=>p.Type.Where(t=>t.Id == type.mId).Count()>0));
            }

            filtredPlaces = filtredPlaces.Distinct().ToList();

            foreach (var place in filtredPlaces)
                mPlaceList.Add(new PlaceModel(place));

            this.mTypeList = new List<TypeModel>();

            List<Type> types = db.Type.ToList();

            foreach (var type in types)
                this.mTypeList.Add(new TypeModel(type)); 
        }

    }
}