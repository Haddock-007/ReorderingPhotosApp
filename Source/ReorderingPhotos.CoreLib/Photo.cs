using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;


namespace ReorderingPhotos.CoreLib {
    public class Photo {
        public string FileFullPath { get; set; }

        public string FileName {
            get {
                return Path.GetFileName(FileFullPath);
            }
        }


        public void RenameFileByShootingTime(int increment) {
            string incStr = String.Format("{0:0000}", increment);
            string dayStr = String.Format("{0:00}", ShootingTime.Day);
            string monthStr = String.Format("{0:00}", ShootingTime.Month);

            string fileName = "img" + dayStr + monthStr + ShootingTime.Year + "_" + incStr + ".jpg";
            RenameFile(fileName);
        }

        public void RenameFile(string newFileName) {
            string oldFilename = FileName;
            string newFileFullPath = FileFullPath.Replace(oldFilename, newFileName);
            File.Move(FileFullPath, newFileFullPath);
            FileFullPath = newFileFullPath;
        }


        public int OrderIndex { get; set; }

        public DateTime ShootingTime { get; set; }

        public void ChangeShootingTime(DateTime newDateTime) {

            string tempFileName = Path.GetTempPath() + "ReorderingPhotos.UI" + Path.GetRandomFileName() + ".jpg";
            File.Copy(FileFullPath, tempFileName);

            using (Image image = new Bitmap(tempFileName)) {

                PropertyItem[] propItems = image.PropertyItems;
                Encoding _Encoding = Encoding.UTF8;
                var DataTakenProperty1 = propItems.Where(a => a.Id.ToString("x") == "9004").FirstOrDefault();
                //var DataTakenProperty2 = propItems.Where(a => a.Id.ToString("x") == "9003").FirstOrDefault();

                if (DataTakenProperty1 == null) {  //undefined
                    DataTakenProperty1 = ExifSingleton.Instance.DataTakenProperty_Default;
                }

                string originalDateString = _Encoding.GetString(DataTakenProperty1.Value);
                originalDateString = originalDateString.Remove(originalDateString.Length - 1);


                DataTakenProperty1.Value = _Encoding.GetBytes(newDateTime.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
               // DataTakenProperty2.Value = _Encoding.GetBytes(newDateTime.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
                image.SetPropertyItem(DataTakenProperty1);
                //image.SetPropertyItem(DataTakenProperty2);
                image.Save(FileFullPath);

                image.Dispose();
            }
        }
    }
}
