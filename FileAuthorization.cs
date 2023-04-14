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
    /// <summary>
    /// Класс авторизации через файл
    /// </summary>
    public class FileAuthorization
    {
        //Адресс файла
        string _filePath;
        /// <summary>
        /// Конструктор класса с файлом пользователей
        /// </summary>
        /// <param name="filePath">Адрес файла пользователей</param>
        public FileAuthorization(string filePath)
        {
            _filePath = filePath;
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="menuFilePath">Адрес файла с меню данного пользователя</param>
        /// <returns>Результат авторизации</returns>
        public bool Authorization(string name, string password, out string menuFilePath)
        {
            FileInfo f = new FileInfo(_filePath);
            StreamReader sr = f.OpenText();
            while (!sr.EndOfStream)
            {
                //Проверяем введённые данные
                string[] data = sr.ReadLine().Split(new char[1] { ' ' });
                if (name != data[0] || password != data[1])
                    continue;
                //Сохраняем адресс меню
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
