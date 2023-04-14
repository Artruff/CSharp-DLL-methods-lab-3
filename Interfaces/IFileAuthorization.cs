using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManageProgram.Interfaces
{
    /// <summary>
    /// Интерфейс объекта выполняющую инициализацию пользователя через файл
    /// </summary>
    interface IFileAuthorization
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="menuFilePath">Адрес файла с меню данного пользователя</param>
        /// <returns>Результат авторизации</returns>
        bool Authorization(string name, string password, out string menuFilePath);
    }
}
