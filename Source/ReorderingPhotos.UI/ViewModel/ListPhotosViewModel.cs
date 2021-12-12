using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ReorderingPhotos.UI.ViewModel {
    public class ListPhotosViewModel {


        
        public ListPhotosViewModel() {
            PhotosCollection = new ObservableCollection<PhotoViewModel>();
        }

        public ObservableCollection<PhotoViewModel> PhotosCollection {
            get; set;
        }

        internal void SetPhotos(string[] files) {
            PhotosCollection.Clear();
            List<PhotoViewModel> photosList = new List<PhotoViewModel>();

            foreach (string photoFilename in files) {
                PhotoViewModel photoViewModel = new PhotoViewModel();
                photoViewModel.PhotoDimensionRatio = PhotosDimensionRatio;
                photoViewModel.SetPhoto(photoFilename);
                photosList.Add(photoViewModel);
            }
            photosList=photosList.OrderBy(x => x.ShootingTime).ToList();
            
            foreach (PhotoViewModel photoVM in photosList) {
                PhotosCollection.Add(photoVM);
            }
            

        }

        public double PhotosDimensionRatio { get; set; }

        internal DateTime GetLowestShootingTime() {
            if (PhotosCollection.Count > 0) {
                return PhotosCollection[0].ShootingTime;
            }
            else {
                return DateTime.Now;
            }
            
        }
    }
}
