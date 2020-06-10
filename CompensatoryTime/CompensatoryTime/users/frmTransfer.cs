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
    public partial class frmTransfer : Form
    {
        public int id_ttost { set; private get; }
        public EnumerableRowCollection<DataRow> rowCollectIn { set; private get; }
        public frmTransfer()
        {
            InitializeComponent();

            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSave, "Перенести");
        }

        private void frmTransfer_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getDateInv();
            task.Wait();
            if (task.Result != null)
            {
                int cntDay = 270;

                EnumerableRowCollection<DataRow> rowCollect = task.Result.AsEnumerable()
                    .Where(r => r.Field<int>("id") != id_ttost && r.Field<DateTime>("DateInventory").AddDays(cntDay).Date >= DateTime.Now.Date)
                    .OrderByDescending(r => r.Field<DateTime>("DateInventory"));

                if (rowCollect.Count() > 0)
                    cmbDateInvent.DataSource = rowCollect.CopyToDataTable();

                cmbDateInvent.DisplayMember = "DateInventory";
                cmbDateInvent.ValueMember = "id";
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (cmbDateInvent.SelectedValue == null) return;

            int id_ttost = (int)cmbDateInvent.SelectedValue;
            bool isShowQuestion = false;


            foreach (DataRow row in rowCollectIn)
            {
                int id = (int)row["id"];
                int id_kadr = (int)row["id_kadr"];
                Task<DataTable> task = Config.hCntMain.transferKadrToNextInvent(id, id_kadr, id_ttost, 0);
                task.Wait();

                if (task.Result != null && task.Result.Rows.Count > 0 && (int)task.Result.Rows[0]["id"] == -1)
                {
                    isShowQuestion = true; break;
                }
            }

            if (isShowQuestion)
            {
                if (DialogResult.No == MessageBox.Show(Config.centralText("Существуют данные на выбранную дату.\nПерезаписать?\n"), "Копирование исключений", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) return;
            }

            List<int> listNotAdd = new List<int>();

            foreach (DataRow row in rowCollectIn)
            {
                int id = (int)row["id"];
                int id_kadr = (int)row["id_kadr"];
                Task<DataTable> task = Config.hCntMain.transferKadrToNextInvent(id, id_kadr, id_ttost, 1);
                task.Wait();

                if (task.Result != null && task.Result.Rows.Count > 0 && (int)task.Result.Rows[0]["id"] == -1)
                    listNotAdd.Add(id);
            }

            if (listNotAdd.Count() > 0)
            {
                foreach (int id in listNotAdd)
                {
                    EnumerableRowCollection<DataRow> rowCollect = rowCollectIn.Where(r => r.Field<int>("id") == id);

                }
            }

            MessageBox.Show("Данные скопированы.", "Копирование исключений", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }
    }
}
