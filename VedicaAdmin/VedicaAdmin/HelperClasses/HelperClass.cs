using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;

namespace HelperClasses
{
    public class LoginCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class CloudiNarySetup
    {
        Account acc = null;
        public CloudiNarySetup()
        {
            acc = new Account
            {
                ApiKey = System.Configuration.ConfigurationManager.AppSettings["CloudinaryAPI"].ToString(),
                ApiSecret = System.Configuration.ConfigurationManager.AppSettings["CloudinaryAPISecret"].ToString(),
                Cloud = System.Configuration.ConfigurationManager.AppSettings["CluodinaryPath"].ToString()
            };
        }
        public string SaveToCloud(HttpPostedFileBase file)
        {
            string cloudPath = string.Empty;
            Cloudinary cld = new Cloudinary(acc);
            try
            {
                string dumpPath = Path.Combine(HttpContext.Current.Server.MapPath("/Dump"), file.FileName);
                string filePath = string.Empty;
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(dumpPath);
                }
                if (file != null && file.ContentLength > 0)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(dumpPath),
                    };
                    var uploadResult = cld.Upload(uploadParams);
                    cloudPath = uploadResult.Uri.ToString();
                }

                if (System.IO.File.Exists(dumpPath))
                {
                    System.IO.File.Delete(dumpPath);
                }
            }
            catch (Exception ex)
            {

            }
            return cloudPath;
        }
    }
}