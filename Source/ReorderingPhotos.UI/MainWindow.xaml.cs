using ReorderingPhotos.UI.ViewModel;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace ReorderingPhotos.UI {

    public partial class MainWindow : Window, INotifyPropertyChanged {
        public ListPhotosViewModel ListPhotosVM { get; set; }

        public MainWindow() {
            ListPhotosVM = new ListPhotosViewModel();
            ListPhotosVM.PhotosDimensionRatio = 0.05;

            InitializeComponent();
            photosList.DataContext = ListPhotosVM;
            this.DataContext = this;
            photoSizeTextBox.DataContext = ListPhotosVM;
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
            get; set;
        }

        public string PhotosSize { get; set; }

        private void selectFolderButton_Click(object sender, RoutedEventArgs e) {
            ListPhotosVM.PhotosCollection.Clear();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (Directory.Exists(Properties.Settings.Default.LastSelectedFolder)) {
                folderBrowserDialog.SelectedPath = Properties.Settings.Default.LastSelectedFolder;
            }
            folderBrowserDialog.ShowDialog();

            if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath)) {
                string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath,"*.*").Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("jpeg")).ToArray(); 
                ListPhotosVM.SetPhotos(files);
                SelectedDate = ListPhotosVM.GetLowestShootingTime();
            }
            Properties.Settings.Default.LastSelectedFolder = folderBrowserDialog.SelectedPath;
            Properties.Settings.Default.Save();
        }

        private void setShootingTimeButton_Click(object sender, RoutedEventArgs e) {
            double inc = 0;

            foreach (PhotoViewModel photoViewModel in ListPhotosVM.PhotosCollection) {
                DateTime newDateTime = m_SelectedDate.AddMinutes(inc);
                photoViewModel.PhotoObj.ChangeShootingTime(newDateTime);
                photoViewModel.ShootingTime = m_SelectedDate;
                photoViewModel.RenameFile(Path.GetRandomFileName());
                inc += 1;
            }

            int i = 0;
            foreach (PhotoViewModel photoViewModel in ListPhotosVM.PhotosCollection) {
                photoViewModel.RenameFileByShootingTime(i);
                i += 1;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}