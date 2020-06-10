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

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btPrint, "Печать");

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

        private void btPrint_Click(object sender, EventArgs e)
        {
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;
            int indexCol = 0;
            int maxCol = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible) maxCol++;

            report.SetColumnWidth(indexRow, 1, indexRow, 1, 28);
            report.SetColumnWidth(indexRow, 2, indexRow, 2, 10);
            report.SetColumnWidth(indexRow, 3, indexRow, 3, 10);
            report.SetColumnWidth(indexRow, 4, indexRow, 4, 10);

            report.Merge(indexRow, 1, indexRow, maxCol);
            report.SetWrapText(indexRow, 1, indexRow, 1);
            report.AddSingleValue($"{this.Text}", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1,16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            report.SetRowHeight(indexRow, 1, indexRow, 1, 45);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxCol);
            report.AddSingleValue($"{lUsers.Text}: {tbFio.Text}", indexRow, 1);            
            indexRow++;
            indexRow++;

            foreach (DataGridViewColumn col in dgvData.Columns)            
                if (col.Visible)
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }
            
            report.SetFontBold(indexRow, 1, indexRow, indexCol);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, indexCol);
            report.SetBorders(indexRow, 1, indexRow, indexCol);
            indexRow++;


            foreach (DataGridViewRow row in dgvData.Rows)
            {
                indexCol = 0;
                foreach (DataGridViewColumn col in dgvData.Columns)
                    if (col.Visible)
                    {
                        indexCol++;
                        report.AddSingleValue(row.Cells[col.Name].Value.ToString(), indexRow, indexCol);
                    }
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, indexCol);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, indexCol);
                report.SetBorders(indexRow, 1, indexRow, indexCol);
                report.SetWrapText(indexRow, 1, indexRow, 1);
                indexRow++;
            }

            report.Show();
        }
    }
}
