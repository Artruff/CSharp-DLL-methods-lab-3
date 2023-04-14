using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManageProgram.FileAuthorization
{
    public class FileAuthorization
    {
        string _filePath;
        public FileAuthorization(string filePath)
        {
            _filePath = filePath;
        }
        public bool Authorization(string name, string password, out string menuFilePath)
        {
            FileInfo f = new FileInfo(_filePath);
            StreamReader sr = f.OpenText();
            while (!sr.EndOfStream)
            {
                string[] data = sr.ReadLine().Split(new char[1] { ' ' });
                if (name != data[0] || password != data[1])
                    continue;
                menuFilePath = data[2];
                sr.Close();
                return true;
            }
            sr.Close();
            menuFilePath = "";
            return false;
        }
    }
}
