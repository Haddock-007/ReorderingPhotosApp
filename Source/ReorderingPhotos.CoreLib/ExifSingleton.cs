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
                DataTakenProperty_Default = propItems.Where(a => a.Id.ToString("x") == "9004").FirstOrDefault();
            }
        }

        public System.Drawing.Imaging.PropertyItem DataTakenProperty_Default { get; private set; }
    }
}