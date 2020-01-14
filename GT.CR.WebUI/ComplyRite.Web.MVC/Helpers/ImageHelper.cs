using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ComplyRite.Web.MVC.Helpers
{
    public class ImageHelper
    {
        public static string LoadImage(string imageId)
        {
            byte[] imageBytes =  File.ReadAllBytes(ConfigurationManager.AppSettings["ImagePath"] +imageId);
            var base64 = Convert.ToBase64String(imageBytes);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return imgSrc;
        }        
        public static string ImageData(string filename)
        {
            string url = "http://172.21.10.133:8253/api/Audit/Download?filename=" + filename;            
            WebClient wc = new WebClient();
            byte[] data = wc.DownloadData(url);
            //MemoryStream memstream = new MemoryStream(data);
            //Image img = Image.FromStream(memstream);
            //context.Response.Clear();
            //context.Response.ClearHeaders();
            //img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            //context.Response.ContentType = "image/jpeg";
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            var base64 = Convert.ToBase64String(data);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return imgSrc;
        }
    }
}