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
    public partial class frmSelect : Form
    {
        public int id_kadr { get; private set; }
        public string nameUser { get; private set; }
        public DateTime dateInvent { set; private get; }

        private DataTable dtData;
        public frmSelect()
        {
            InitializeComponent();

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSelect, "Выбрать");

            dgvData.AutoGenerateColumns = false;
        }

        private void frmSelect_Load(object sender, EventArgs e)
        {
            getDeps();
        }

        private void getDeps()
        {
            int id_PersonnelType = 1;
            if (rbUniversame.Checked)
                id_PersonnelType = 2;


            Task<DataTable> task = Config.hCntMain.getDeps(dateInvent, id_PersonnelType, true);
            task.Wait();

            if (task.Result != null)
            {
                cmbDeps.DataSource = task.Result.Copy();
                cmbDeps.DisplayMember = "cName";
                cmbDeps.ValueMember = "id";
            }

            task = Config.hCntMain.getListKadr(dateInvent, id_PersonnelType);
            task.Wait();
            dtData = task.Result;
            setFilter();
            dgvData.DataSource = dtData;
            task = null;
        }

        private void rbOffice_Click(object sender, EventArgs e)
        {
            getDeps();
        }

        private void tbFio_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btSelect.Enabled = false;
                return;
            }

            try
            {
                string filter = "";


                if ((int)cmbDeps.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_departments = {cmbDeps.SelectedValue}";

                if (tbFio.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"fio like '%{tbFio.Text.Trim()}%'";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btSelect.Enabled = dtData.DefaultView.Count != 0;                
            }
        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {            
            if (e.RowIndex == -1) return;
            if (e.Button == MouseButtons.Left) {
                btSelect_Click(sender, null);
            }
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            id_kadr = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
            nameUser = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["fio"];
            this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbFio.Location = new Point(dgvData.Location.X + cDeps.Width + 1, tbFio.Location.Y);
            tbFio.Size = new Size(cFIO.Width, tbFio.Height);
        }
    }
}
