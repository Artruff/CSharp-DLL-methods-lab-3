using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManageProgram.Interfaces
{
    /// <summary>
    /// Интерфейс класса, создающего меню из файла
    /// </summary>
    interface IFileMenu
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="sourceCommandType">Тип класса с командами меню</typeparam>
        /// <param name="pathFile">Адрес файла с меню</param>
        /// <param name="sourceCommand">Класс с командами меню</param>
        void CreateMenu<sourceCommandType>(string pathFile, object sourceCommand);
    }
}
