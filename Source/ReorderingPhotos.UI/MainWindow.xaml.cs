using ReorderingPhotos.UI.ViewModel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace ReorderingPhotos.UI {

    public partial class MainWindow : Window,INotifyPropertyChanged
    {

        public ListPhotosViewModel ListPhotosVM { get; set; }
        public MainWindow()
        {
            ListPhotosVM = new ListPhotosViewModel();

            InitializeComponent();
            photosList.DataContext = ListPhotosVM;
            this.DataContext = this;
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings.Get("photos_dir"));
            ListPhotosVM.SetPhotos(files);
            SelectedDate=ListPhotosVM.GetLowestShootingTime();
            
        }


        private DateTime m_SelectedDate;
        public DateTime SelectedDate {
            get {
                return m_SelectedDate;
            }
            set {
                m_SelectedDate = value;
                NotifyPropertyChanged("SelectedDate");
                
            }
        }

        public string Time {
            get;set;
        }

        public string PhotosSize { get; set; }



        private void selectFolderButton_Click(object sender, RoutedEventArgs e) {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            if( ! string.IsNullOrEmpty(folderBrowserDialog.SelectedPath)) {
                string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                ListPhotosVM.SetPhotos(files);
                SelectedDate = ListPhotosVM.GetLowestShootingTime();
            }
        }

        private void setShootingTimeButton_Click(object sender, RoutedEventArgs e) {
            double inc = 0;
            int i = 0;
            foreach(PhotoViewModel photoViewModel in ListPhotosVM.PhotosCollection) {
                DateTime newDateTime = m_SelectedDate.AddMinutes(inc);
                photoViewModel.PhotoObj.ChangeShootingTime(newDateTime);
                photoViewModel.ShootingTime = m_SelectedDate;
                photoViewModel.RenameFileByShootingTime(i);

                inc += 1;
                i+= 1;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged( String propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
