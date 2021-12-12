using ReorderingPhotos.CoreLib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ReorderingPhotos.UI.ViewModel {


    public class PhotoViewModel : INotifyPropertyChanged {

        public double PhotoDimensionRatio { get; set; }

        public void SetPhoto(string fileName) {
            PhotoObj = new Photo();
            PhotoObj.FileFullPath = fileName;
            SetPhotoMetadata();
        }

        private static Regex r = new Regex(":");
        private void SetPhotoMetadata() {
            using (FileStream fs = new FileStream(PhotoObj.FileFullPath, FileMode.Open, FileAccess.Read)) {
                using (Image myImage = Image.FromStream(fs, false, false)) {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    PhotoObj.ShootingTime = DateTime.Parse(dateTaken);

                    double width = PhotoDimensionRatio * myImage.PhysicalDimension.Width;
                    double height = PhotoDimensionRatio * myImage.PhysicalDimension.Height;

                    PhotoDimensions = new Rect(0.0, 0.0, width, height);
                }
                fs.Close();
            }
        }

        public Photo PhotoObj { get; set; }

        public string PhotoPath {
            get {
                return PhotoObj.FileFullPath;
            }
        }


        public BitmapImage PhotoImage {
            get {
                //Uri uri = new Uri(PhotoPath);

                using (var stream = new FileStream(PhotoPath, FileMode.Open, FileAccess.Read)) {

                    BitmapImage bitmapImage = new BitmapImage();
                    
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    //bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    //bitmapImage.UriSource = uri;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    return bitmapImage;
                }
            }
        }


        public System.DateTime ShootingTime {
            get {
                return PhotoObj.ShootingTime;
            }
            set {
                PhotoObj.ShootingTime = value;
                NotifyPropertyChanged("ShootingTime");

            }
        }

        internal void RenameFileByShootingTime(int i) {
            PhotoObj.RenameFileByShootingTime(i);
            NotifyPropertyChanged("Filename");
            NotifyPropertyChanged("PhotoPath");
        }

        internal void RenameFile(string tempFilename) {
            PhotoObj.RenameFile(tempFilename);
        }

        public System.Windows.Rect PhotoDimensions {
            get; set;
        }

        public string Filename { get { return PhotoObj.FileName; } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
