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

namespace CompensatoryTime.users
{
    public partial class frmAdd : Form
    {

        private int id;
        public int id_ttost { set; private get; }
        public DateTime dttost { set; private get; }

        public frmAdd()
        {
            InitializeComponent();

            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getShop();
            task.Wait();
            if (task.Result != null)
            {
                cmbShop.DataSource = task.Result.Copy();
                cmbShop.DisplayMember = "cName";
                cmbShop.ValueMember = "id";
                cmbShop.SelectedIndex = -1;
            }

            task = Config.hCntMain.getExceptionType();
            task.Wait();

            if (task.Result != null && task.Result.Rows.Count > 0)
            {
                cmbTypeExeptions.DataSource = task.Result.Copy();
                cmbTypeExeptions.DisplayMember = "cName";
                cmbTypeExeptions.ValueMember = "id";
                cmbTypeExeptions.SelectedIndex = -1;
            }


            tbDateInvent.Text = dttost.ToShortDateString();
        }

        private void frmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void rbMoney_CheckedChanged(object sender, EventArgs e)
        {
            tbMoney.Enabled = rbMoney.Checked;
            tbDays.Enabled = rbDays.Checked;
            tbDays.Clear();
            tbMoney.Clear();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (tbFio.Text.Trim().Length == 0)
            {
                MessageBox.Show($"Необходимо заполнить \"{lUsers.Text}\"", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btSelectUser.Focus();
                return;
            }

            if (cmbShop.SelectedIndex == -1)
            {
                MessageBox.Show($"Необходимо выбрать \"{lShop.Text}\"", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbShop.Focus();
                return;
            }

            if (cmbTypeExeptions.SelectedIndex == -1)
            {
                MessageBox.Show($"Необходимо выбрать \"{lTypeExeptions.Text}\"", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTypeExeptions.Focus();
                return;
            }

            if (rbMoney.Checked && (tbMoney.Text.Trim().Length == 0 || decimal.Parse(tbMoney.Text) == 0))
            {
                MessageBox.Show($"Необходимо выбрать \"{rbMoney.Text}\"", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMoney.Focus();
                return;
            }


            if (rbDays.Checked && (tbDays.Text.Trim().Length == 0 || decimal.Parse(tbDays.Text) == 0))
            {
                MessageBox.Show($"Необходимо выбрать \"{rbDays.Text}\"", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDays.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void cmbTypeExeptions_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chbIsBonusValidate.Checked = false;
            chbIsBonusValidate.Enabled = (bool)(cmbTypeExeptions.DataSource as DataTable).AsEnumerable().Where(r => r.Field<int>("id") == (int)cmbTypeExeptions.SelectedValue).First()["isDop"];
        }

        private void tbMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
            {
                if (e.KeyChar != '\b')
                { e.Handled = true; }
            }
        }
    }
}
