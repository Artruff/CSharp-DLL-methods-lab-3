using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManageProgram.Access;

namespace FileManageProgram.Menu
{
    /// <summary>
    /// Меню, которое считывает элементы из файла
    /// </summary>
    public class AutomaticMenuStrip : MenuStrip, Interfaces.IFileMenu
    {
        /// <summary>
        /// Инициализирует создание меню
        /// </summary>
        /// <typeparam name="sourceCommandType">Тип класса с командами меню</typeparam>
        /// <param name="pathFile">Адрес файла с меню</param>
        /// <param name="sourceCommand">Класс с командами меню</param>
        public void CreateMenu<sourceCommandType>(string pathFile, object sourceCommand)
        {
            FileInfo file = new FileInfo(pathFile);
            StreamReader sr = new StreamReader(file.OpenRead(), Encoding.Default);
            CreateMenuItem<sourceCommandType>(sourceCommand, sr, null);

            sr.Close();
        }
        /// <summary>
        /// Функция рекурсивного создания элемента меню
        /// </summary>
        /// <typeparam name="sourceCommandType">Тип класса с командами меню</typeparam>
        /// <param name="sourceCommand">Класс с командами меню</param>
        /// <param name="sr">Поток чтения файла меню</param>
        /// <param name="topItem">Предыдущий элемент</param>
        /// <param name="prevLevel">Уровень предыдущего элемента</param>
        private void CreateMenuItem<sourceCommandType>(object sourceCommand, StreamReader sr, ToolStripMenuItem topItem = null, int prevLevel = 0)
        {
            if (sr.EndOfStream)
                return;
            //Считываем элемент
            string[] dataItem = sr.ReadLine().Split(new char[] { ' ' });
            ToolStripMenuItem downItem = new ToolStripMenuItem();
            int level = Convert.ToInt32(dataItem[0]);

            //Определяем вложенность элемента в меню
            if (level != 0)
            {
                ToolStripMenuItem tmpItem = topItem == null ? (ToolStripMenuItem)this.Items[this.Items.Count - 1] : topItem;
                while (level != prevLevel-- + 1)
                    tmpItem = tmpItem.OwnerItem == null ? (ToolStripMenuItem)this.Items[this.Items.Count - 1] : (ToolStripMenuItem)tmpItem.OwnerItem;
                tmpItem.DropDownItems.Add(downItem);
            }
            else
                this.Items.Add(downItem);
            
            //Задаём имя элемента
            downItem.Name = dataItem[1].Replace('_', ' ');
            downItem.Text = dataItem[1];

            //Определяем доступность элемента
            switch (Convert.ToInt32(dataItem[2]))
            {
                case (int)AccessEnum.VisibleAndNotAccessible:
                    downItem.Enabled = false;
                    break;
                case (int)AccessEnum.NotVisible:
                    downItem.Visible = false;
                    break;
            }

            //Создаём для элемента команду
            if (dataItem.Length == 4)
            {
                sourceCommandType commands = (sourceCommandType)sourceCommand;
                EventHandler command = (EventHandler)commands.GetType().GetMethod(dataItem[3]).CreateDelegate(typeof(EventHandler), commands);
                downItem.Click += command;
            }

            CreateMenuItem<sourceCommandType>(sourceCommand, sr, downItem, level);
        }
    }
}
