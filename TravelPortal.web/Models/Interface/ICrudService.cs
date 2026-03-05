using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Common;

namespace TravelPortal.web.Models.Interface
{
    public interface ICrudService<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        JsonResponse AddEdit(T entity);
        JsonResponse Delete(int id);
    }
}