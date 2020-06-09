using Nwuram.Framework.Logging;
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
        private DataTable dtData;
        public frmList()
        {
            InitializeComponent();
            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;
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
            getData();
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

        private void getData()
        {
            if (cmbDateInvent.SelectedValue == null) return;

            Task<DataTable> task = Config.hCntMain.getException((int)cmbDateInvent.SelectedValue);
            task.Wait();
            dtData = task.Result;

            dgvData.DataSource = dtData;
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                //btSelect.Enabled = false;
                return;
            }

            try
            {
                string filter = "";


                if ((int)cmbShop.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_shop = {cmbShop.SelectedValue}";

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
                //btSelect.Enabled = dtData.DefaultView.Count != 0;
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new users.frmAdd() { Text = "Добавить сотрудника", id_ttost = (int)cmbDateInvent.SelectedValue, dateInvent = DateTime.Parse(cmbDateInvent.Text) }.ShowDialog())
                getData();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                if (DialogResult.OK == new users.frmAdd() { Text = "Редактировать сотрудника", row = row, id_ttost = (int)cmbDateInvent.SelectedValue, dateInvent = DateTime.Parse(cmbDateInvent.Text) }.ShowDialog())
                    getData();
            }
        }

        private void btViewWorkUser_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                new users.frmViewUserWork() { Text =$"Просмотр мест подсчета сотрудника за дату инвентаризации {cmbDateInvent.Text}", row = row, dateInvent = DateTime.Parse(cmbDateInvent.Text) }.ShowDialog();
            }
        }

        private void cmbShop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void tbFio_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void cmbDateInvent_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getData();
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                int id = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
                int id_kadr = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_kadr"];
                int id_shop = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_shop"];
                int id_ttost = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_ttost"];
                int id_ExceptionType = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id_ExceptionType"];
                bool isDop = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isDop"];
                decimal? summa = null;
                if (dtData.DefaultView[dgvData.CurrentRow.Index]["Summa"] != DBNull.Value)
                {
                    summa = decimal.Parse(dtData.DefaultView[dgvData.CurrentRow.Index]["Summa"].ToString());
                }
                decimal? CountDays = null;
                if (dtData.DefaultView[dgvData.CurrentRow.Index]["CountDays"] != DBNull.Value)
                {
                    CountDays = decimal.Parse(dtData.DefaultView[dgvData.CurrentRow.Index]["CountDays"].ToString());
                }                
                bool isActive = true;

                Task<DataTable> task = Config.hCntMain.setException(id, id_kadr, id_shop, id_ttost, id_ExceptionType, isDop, summa, CountDays,true,0);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Config.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                    return;
                }


                if (result == -2 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show(Config.centralText("Выбранная для удаления запись используется в программе.\nСделать запись недействующей?\n"), "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 1520);
                        task = Config.hCntMain.setException(id, id_kadr, id_shop, id_ttost, id_ExceptionType, isDop, summa, CountDays, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        getData();
                        return;
                    }
                }
                else
                if (result == 0 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 1519);
                        task = Config.hCntMain.setException(id, id_kadr, id_shop, id_ttost, id_ExceptionType, isDop, summa, CountDays, true, 1);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        getData();
                        return;
                    }
                }
                else if (!isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Сделать выбранную запись действующей?", "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 1521);
                        task = Config.hCntMain.setException(id, id_kadr, id_shop, id_ttost, id_ExceptionType, isDop, summa, CountDays, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        getData();
                        return;
                    }
                }
            }
        }

        private void setLog(int id, int id_log)
        {
            Logging.StartFirstLevel(id_log);
            switch (id_log)
            {
                case 2: Logging.Comment("Удаление Типа документа"); break;
                case 3: Logging.Comment("Тип документа переведён в недействующие "); break;
                case 4: Logging.Comment("Тип документа переведён  в действующие"); break;
                default: break;
            }

            Logging.Comment($"ID:{id}");
            //Logging.Comment($"Наименование: {(string)dtData.DefaultView[dgvData.CurrentRow.Index]["cName"]}");
            //Logging.Comment($"Номер по порядку: {(int)dtData.DefaultView[dgvData.CurrentRow.Index]["npp"]}");
            //Logging.Comment($"Отображение архивных документов у руководителя: {((bool)dtData.DefaultView[dgvData.CurrentRow.Index]["ViewArchive"] ? "Да" : "Нет")}");
            //Logging.Comment($"Отображать при добавлении документа: {((bool)dtData.DefaultView[dgvData.CurrentRow.Index]["ViewAdd"] ? "Да" : "Нет")}");

            Logging.StopFirstLevel();
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbFio.Location = new Point(dgvData.Location.X + cSelect.Width + 1 + cShop.Width + 1, tbFio.Location.Y);
            tbFio.Size = new Size(cFIO.Width, tbFio.Height);
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                if ((bool)dtData.DefaultView[e.RowIndex]["isDop"])
                    rColor = panel1.BackColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            }
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

       
    }
}
