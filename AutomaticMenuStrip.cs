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
    public class AutomaticMenuStrip : MenuStrip, Interfaces.IFileMenu
    {
        public void CreateMenu<sourceCommandType>(string pathFile, object sourceCommand)
        {
            FileInfo file = new FileInfo(pathFile);
            StreamReader sr = new StreamReader(file.OpenRead(), Encoding.Default);
            CreateMenuItem<sourceCommandType>(sourceCommand, sr, null);

            sr.Close();
        }
        private void CreateMenuItem<sourceCommandType>(object sourceCommand, StreamReader sr, ToolStripMenuItem topItem = null, int prevLevel = 0)
        {
            if (sr.EndOfStream)
                return;
            string[] dataItem = sr.ReadLine().Split(new char[] { ' ' });
            ToolStripMenuItem downItem = new ToolStripMenuItem();
            int level = Convert.ToInt32(dataItem[0]);

            if (level != 0)
            {
                ToolStripMenuItem tmpItem = topItem == null ? (ToolStripMenuItem)this.Items[this.Items.Count - 1] : topItem;
                while (level != prevLevel-- + 1)
                    tmpItem = tmpItem.OwnerItem == null ? (ToolStripMenuItem)this.Items[this.Items.Count - 1] : (ToolStripMenuItem)tmpItem.OwnerItem;
                tmpItem.DropDownItems.Add(downItem);
            }
            else
                this.Items.Add(downItem);

            downItem.Name = dataItem[1];
            downItem.Text = dataItem[1];

            switch (Convert.ToInt32(dataItem[2]))
            {
                case (int)AccessEnum.VisibleAndNotAccessible:
                    downItem.Enabled = false;
                    break;
                case (int)AccessEnum.NotVisible:
                    downItem.Visible = false;
                    break;
            }

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
