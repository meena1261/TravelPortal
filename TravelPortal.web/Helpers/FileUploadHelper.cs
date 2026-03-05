using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Common;

namespace TravelPortal.web.Helpers
{
    public class FileUploadHelper
    {
        public static JsonResponse FileUpload(HttpPostedFileBase file, string folder, string fileName = "")
        {
            JsonResponse response = new JsonResponse();
            try
            {

                // 1. Check if file is provided
                if (file == null || file.ContentLength == 0)
                {
                    response.status = 0;
                    response.message = "No file selected.";
                    return response;
                }

                // 2. Validate file extension
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                string ext = Path.GetExtension(file.FileName).ToLower();

                if (Array.IndexOf(allowedExtensions, ext) < 0)
                {
                    response.status = 0;
                    response.message = "Only PDF, JPG, JPEG, and PNG are allowed.";
                    return response;
                }

                // 3. Validate file size (10 MB limit)
                if (file.ContentLength > 10 * 1024 * 1024)
                {
                    response.status = 0;
                    response.message = "File size must be below 10MB.";
                    return response;
                }

                // 4. Define upload path
                string uploadDir = HttpContext.Current.Server.MapPath($"~/Uploads/{folder}");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // 5. Create safe file name
                if (string.IsNullOrEmpty(fileName))
                    fileName = Path.GetFileName(file.FileName);
                else
                    fileName = fileName + ext;
                string filePath = Path.Combine(uploadDir, fileName);

                // 6. Check if file already exists
                if (System.IO.File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // 7. Save file
                file.SaveAs(filePath);
                response.status = 1;
                response.message = fileName;
                return response;
            }
            catch (Exception ex)
            {
                // 8. Handle exceptions
                response.status = 0;
                response.message = "Error: " + ex.Message;
                return response;
            }
        }

    }
}