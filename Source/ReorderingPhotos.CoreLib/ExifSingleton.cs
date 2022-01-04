using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace ReorderingPhotos.CoreLib {

    public class ExifSingleton {
        private static ExifSingleton m_ExifSingleton;

        public static ExifSingleton Instance {
            get {
                if (m_ExifSingleton == null) {
                    m_ExifSingleton = new ExifSingleton();
                }

                return m_ExifSingleton;
            }
        }

        public ExifSingleton() {
            string localFileWithDateTakenExifProp = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\resource\photo\photoWithDateTakenExifProp.jpg";
            using (Image image = new Bitmap(localFileWithDateTakenExifProp)) {
                PropertyItem[] propItems = image.PropertyItems;
                Encoding _Encoding = Encoding.UTF8;
                DataTakenProperty1_Default = propItems.Where(a => a.Id.ToString("x") == "9004").FirstOrDefault();
                DataTakenProperty2_Default = propItems.Where(a => a.Id.ToString("x") == "9003").FirstOrDefault();
            }
        }

        public System.Drawing.Imaging.PropertyItem DataTakenProperty1_Default { get; private set; }
        public System.Drawing.Imaging.PropertyItem DataTakenProperty2_Default { get; private set; }
    }
}