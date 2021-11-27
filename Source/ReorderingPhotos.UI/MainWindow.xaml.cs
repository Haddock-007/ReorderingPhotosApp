using ReorderingPhotos.UI.ViewModel;
using System.Configuration;
using System.IO;
using System.Windows;

namespace ReorderingPhotos.UI {

    public partial class MainWindow : Window
    {

        ListPhotosViewModel listPhotosVM;
        public MainWindow()
        {
            listPhotosVM = new ListPhotosViewModel();
            
            
            InitializeComponent();
            photosList.DataContext = listPhotosVM;
            photoSizeTextBox.DataContext = this;
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings.Get("photos_dir"));
            listPhotosVM.SetPhotos(files);
        }

        public string PhotosSize { get; set; }

    }
}
