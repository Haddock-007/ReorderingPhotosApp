using ReorderingPhotos.CoreLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderingPhotos.UI.ViewModel {
    public class ListPhotosViewModel {

        public ListPhotosViewModel() {
            PhotosCollection = new ObservableCollection<PhotoViewModel>();
        }

        public ObservableCollection<PhotoViewModel> PhotosCollection {
            get; set;
        }

        internal void SetPhotos(string[] files) {

            List<PhotoViewModel> photosList = new List<PhotoViewModel>();

            foreach (string photoFilename in files) {
                PhotoViewModel photoViewModel = new PhotoViewModel();
                photoViewModel.SetPhoto(photoFilename);
                photosList.Add(photoViewModel);
            }
            photosList=photosList.OrderBy(x => x.ShootingTime).ToList();
            PhotosCollection.Clear();
            foreach (PhotoViewModel photoVM in photosList) {
                PhotosCollection.Add(photoVM);
            }
            

        }
    }
}
