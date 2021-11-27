using ReorderingPhotos.CoreLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ReorderingPhotos.UI.ViewModel {


    public class PhotoViewModel {

        public void SetPhoto(string fileName) {
            PhotoObj = new Photo();
            PhotoObj.FileFullPath = fileName;
            PhotoObj.ShootingTime = GetDateTakenFromImage(fileName);
    }
        public Photo PhotoObj { get; set; }

        public string PhotoPath {
            get {
                return PhotoObj.FileFullPath;
            }
        }
        public System.DateTime ShootingTime {
            get {
                return PhotoObj.ShootingTime;
            }
        }

        public string Filename { get { return PhotoObj.FileName; } }

        private static Regex r = new Regex(":");

        //retrieves the datetime WITHOUT loading the whole image
        public static DateTime GetDateTakenFromImage(string path) {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false)) {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

    }
}
