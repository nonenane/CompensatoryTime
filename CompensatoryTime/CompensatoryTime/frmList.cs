using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompensatoryTime
{
    public partial class frmList : Form
    {
        public frmList()
        {
            InitializeComponent();
            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        }

        private void frmList_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getShop(true);
            task.Wait();
            if (task.Result != null)
            {
                cmbShop.DataSource = task.Result.Copy();
                cmbShop.DisplayMember = "cName";
                cmbShop.ValueMember = "id";
            }

            task = Config.hCntMain.getDateInv();
            task.Wait();
            if (task.Result != null)
            {
                cmbDateInvent.DataSource = task.Result.Copy();
                cmbDateInvent.DisplayMember = "DateInventory";
                cmbDateInvent.ValueMember = "id";
            }

            init_type();
        }

        private void init_type()
        {
            addCheckBox("isAll", "Все", true, 0);
            
            Task<DataTable> task = Config.hCntMain.getExceptionType();
            task.Wait();

            if (task.Result != null && task.Result.Rows.Count > 0)
            {
                foreach (DataRow row in task.Result.Rows)
                {
                    addCheckBox(row["cName"].ToString(), row["cName"].ToString(), false, (int)row["id"]);
                }
            }
        }

        private void addCheckBox(string name, string text, bool isChecked,int id)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = name;//.Replace("/"," ");
            checkBox.Text = text;//.Replace("/", " ");
            checkBox.Tag = id;
            checkBox.AutoSize = true;
            checkBox.Checked = isChecked;
            checkBox.CheckedChanged += checkBox_CheckedChanged;
            flowLayoutPanel1.Controls.Add(checkBox);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
        }

        private void frmList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        
    }
}
