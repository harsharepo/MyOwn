using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EducationWebAdmin
{
    /// <summary>
    /// Summary description for LargeFileUploadHandler
    /// </summary>
    public class LargeFileUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Stream inputStream = context.Request.InputStream;
            byte[] bytesToRead = new byte[inputStream.Length];
            string FileName = context.Request.QueryString["fileName"];
            inputStream.Read(bytesToRead, 0, bytesToRead.Length);
            string filePath = context.Server.MapPath("~/GalleryImages/" + FileName);
            System.IO.FileStream fs = null;
            using (fs = !File.Exists(filePath) ? System.IO.File.Create(filePath) : File.Open(filePath, FileMode.Append))
            {
                fs.Write(bytesToRead, 0, bytesToRead.Length);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}