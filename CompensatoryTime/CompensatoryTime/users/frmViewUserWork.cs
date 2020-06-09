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
    public partial class frmViewUserWork : Form
    {
        public DataRowView row { set; private get; }
        public DateTime dateInvent { set; private get; }

        public frmViewUserWork()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void frmViewUserWork_Load(object sender, EventArgs e)
        {
            int id_ttost = (int)row["id_ttost"];
            int id_kadr = (int)row["id_kadr"];
            tbFio.Text = (string)row["fio"];

            Task<DataTable> task = Config.hCntMain.getViewUserWork(id_ttost, id_kadr);
            task.Wait();
            dgvData.DataSource = task.Result;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
