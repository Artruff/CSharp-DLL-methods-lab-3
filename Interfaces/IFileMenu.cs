using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManageProgram.Interfaces
{
    interface IFileMenu
    {
        void CreateMenu<sourceCommandType>(string pathFile, object sourceCommand);
    }
}
