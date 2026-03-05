using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models.Common;

namespace TravelPortal.web.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        public JsonResult CityAirportSearch(string term)
        {
            var categories = PreloadApplicationData.Cities;
            var filtered = categories
         .Where(c => c.CityAirport.StartsWith(term, StringComparison.OrdinalIgnoreCase) || c.IATACode.StartsWith(term, StringComparison.OrdinalIgnoreCase))
         .Select(c => new
         {
             label = $"{c.CityAirport} - {c.IATACode}",
             value = $"{c.CityAirport} - {c.IATACode}",
             id = c.CityAirportId
         })
         .ToList();

            return Json(filtered, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AirlineSearch(string term)
        {
            var db = new EDMX.db_silviEntities();
            var objlist = db.Airlines.ToList();
            var filtered = objlist
         .Where(c => c.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase) || c.IATA_Code.StartsWith(term, StringComparison.OrdinalIgnoreCase))
         .Select(c => new
         {
             label = $"{c.Name} - {c.IATA_Code}",
             value = $"{c.Name} - {c.IATA_Code}",
             id = c.IATA_Code
         })
         .ToList();

            return Json(filtered, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VerifyPANCard(string panNumber, HttpPostedFileBase panImage)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                if (panImage != null && panImage.ContentLength > 0)
                {
                    string folderpath = SessionHelper.UserDetail.AspNetID;
                    response = FileUploadHelper.FileUpload(panImage, folderpath, panNumber);

                    return Json(response);
                }
                else
                {
                    response.status = 0;
                    response.message = "No file Uploaded";
                    return Json(response);
                }

            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        public JsonResult UploadKycDocument(HttpPostedFileBase DocFile)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                if (DocFile != null && DocFile.ContentLength > 0)
                {
                    string folderpath = SessionHelper.UserDetail.AspNetID;
                    response = FileUploadHelper.FileUpload(DocFile, folderpath);
                    //response.status = 1;
                    //response.message = "Uploaded Successfully";
                    return Json(response);
                }
                else
                {
                    response.status = 0;
                    response.message = "No file Uploaded";
                    return Json(response);
                }

            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = ex.Message;
                return Json(response);
            }
        }
    }
}