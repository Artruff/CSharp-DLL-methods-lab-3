using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManageProgram.Interfaces
{
    interface IFileAuthorization
    {
        bool Authorization(string name, string password, out string menuFilePath);
    }
}
