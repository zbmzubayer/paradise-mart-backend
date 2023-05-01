using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Helpers
{
    public class FileHandle
    {
        public static string CustomerUploadPhoto(HttpRequest httpRequest, string guid)
        {
            string uniqueFileName = "";
            foreach (string file in httpRequest.Files)
            {
                var posteFile = httpRequest.Files[file];
                var rootPath = HttpContext.Current.Server.MapPath("~/Uploads/CustomerPhotos");
                if(!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var fileExt = System.IO.Path.GetExtension(posteFile.FileName);
                var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                uniqueFileName = unixTimestamp + fileExt;
                var filePath = System.IO.Path.Combine(rootPath, uniqueFileName);
                posteFile.SaveAs(filePath);
            }
            return uniqueFileName;
        }
        public static string SellerUploadPhoto(HttpRequest httpRequest, string guid)
        {
            string uniqueFileName = "";
            foreach (string file in httpRequest.Files)
            {
                var posteFile = httpRequest.Files[file];
                var rootPath = HttpContext.Current.Server.MapPath("~/Uploads/SellerPhotos");
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var fileExt = System.IO.Path.GetExtension(posteFile.FileName);
                var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                uniqueFileName = unixTimestamp + fileExt;
                var filePath = System.IO.Path.Combine(rootPath, uniqueFileName);
                posteFile.SaveAs(filePath);
            }
            return uniqueFileName;
        }
        public static string CompanyUploadLogo(HttpRequest httpRequest, string guid)
        {
            string uniqueFileName = "";
            foreach (string file in httpRequest.Files)
            {
                var posteFile = httpRequest.Files[file];
                var rootPath = HttpContext.Current.Server.MapPath("~/Uploads/CompanyLogo");
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var fileExt = System.IO.Path.GetExtension(posteFile.FileName);
                var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                uniqueFileName = unixTimestamp + fileExt;
                var filePath = System.IO.Path.Combine(rootPath, uniqueFileName);
                posteFile.SaveAs(filePath);
            }
            return uniqueFileName;
        }
        public static string AdminUploadPhoto(HttpRequest httpRequest, string guid)
        {
            string uniqueFileName = "";
            foreach (string file in httpRequest.Files)
            {
                var posteFile = httpRequest.Files[file];
                var rootPath = HttpContext.Current.Server.MapPath("~/Uploads/AdminPhotos");
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var fileExt = System.IO.Path.GetExtension(posteFile.FileName);
                var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                uniqueFileName = unixTimestamp + fileExt;
                var filePath = System.IO.Path.Combine(rootPath, uniqueFileName);
                posteFile.SaveAs(filePath);
            }
            return uniqueFileName;
        }
    }
}
