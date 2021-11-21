using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderingPhotos.CoreLib
{
    public class Photo
    {
        public string FileFullPath { get; set; }

        public string FileName { get
            {
                return Path.GetFileName(FileFullPath);
            }
        }

        public void RenameFile(string newFileName)
        {
            string oldFilename = FileName;
            string newFileFullPath = FileFullPath.Replace(oldFilename, newFileName);
            File.Move(FileFullPath, newFileFullPath);
            FileFullPath = newFileFullPath;
        }

        public void SetNewShootingTime(DateTime newTime)
        {
            throw new NotImplementedException();
        }

        public int OrderIndex { get; set; }

        public DateTime ShootingTime { get; set; }
    }
}
