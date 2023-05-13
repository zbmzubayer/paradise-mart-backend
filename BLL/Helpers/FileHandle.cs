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
        static string rootPath = HttpContext.Current.Server.MapPath("~/Uploads");
        public static string UploadPhoto(HttpRequest httpRequest, string path, int id)
        {
            string uniqueFileName = "";
            foreach (string file in httpRequest.Files)
            {
                var posteFile = httpRequest.Files[file];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileExt = System.IO.Path.GetExtension(posteFile.FileName);
                var unixTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                uniqueFileName = id + unixTimestamp + fileExt;
                var filePath = System.IO.Path.Combine(path, uniqueFileName);
                posteFile.SaveAs(filePath);
            }
            return uniqueFileName;
        }
        public static string CustomerUploadPhoto(HttpRequest httpRequest, int id)
        {
            var customerPhotoPath = rootPath + "/CustomerPhotos";
            return UploadPhoto(httpRequest, customerPhotoPath, id);
        }
        public static string SellerUploadPhoto(HttpRequest httpRequest, int id)
        {
            var sellerPhotoPath = rootPath + "/SellerPhotos";
            return UploadPhoto(httpRequest, sellerPhotoPath, id);
        }
        public static string SellerUploadCompanyLogo(HttpRequest httpRequest, int id)
        {
            var sellerPhotoPath = rootPath + "/CompanyLogos";
            return UploadPhoto(httpRequest, sellerPhotoPath, id);
        }
        public static string AdminUploadPhoto(HttpRequest httpRequest, int id)
        {
            var adminPhotoPath = rootPath + "/AdminPhotos";
            return UploadPhoto(httpRequest, adminPhotoPath, id);
        }
        public static bool DeletePhoto(string path, string photo)
        {
            string fullPath = rootPath + path + photo;
            FileInfo fileInfo = new FileInfo(fullPath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                return true;
            }
            return false;
        }
    }
}
