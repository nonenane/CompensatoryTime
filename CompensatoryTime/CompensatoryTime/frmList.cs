using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
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
        private bool isLoadData = true;
        private int countDayForEdit = 0;
        public frmList()
        {
            InitializeComponent();
            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            validateSettings("mkot","N", "макс.кол-во отгулов","2","",true);
            validateSettings("vpdo", "N", "ввод первичных данных отгулов", "0", "", true);
            validateSettings("dpzn", "N", "Время для объединения", "30", "", true);
            validateSettings("dkor", "N", "Количество дней", "3", "", true);


            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btDel, "Удалить");
            tp.SetToolTip(btEdit, "Редактировать");
            tp.SetToolTip(btAdd, "Добавить");
            tp.SetToolTip(btViewWorkUser, "Просмотр мест подсчета сотрудника по ПЛАНУ");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btTransfer, "Копировать выделенных сотрудников");
            tp.SetToolTip(btReportBonus, "Отчет по премиям за подсчет холодильных камер");
            tp.SetToolTip(btPrintDaysWork, "Отчет по местам подсчета суточников");
            tp.SetToolTip(btUpdate, "Обновить");

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

            if (UserSettings.User.StatusCode.ToLower().Equals("оп"))
            {
                btAdd.Visible = btEdit.Visible = btDel.Visible = btTransfer.Visible = btReportBonus.Visible = false;
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
            checkBox.Click += checkBox_CheckedChanged;
            flowLayoutPanel1.Controls.Add(checkBox);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int idTag = (int)checkBox.Tag;
            if (idTag == 0 && checkBox.Checked)
            {
                foreach (Control cnt in flowLayoutPanel1.Controls)
                {
                    if (cnt is CheckBox)
                    {
                        checkBox = (CheckBox)cnt;
                        if ((int)checkBox.Tag != idTag) checkBox.Checked = false;
                    }
                }
            }
            else if (idTag != 0 && checkBox.Checked)
            {
                foreach (Control cnt in flowLayoutPanel1.Controls)
                {
                    if (cnt is CheckBox)
                    {
                        checkBox = (CheckBox)cnt;
                        if ((int)checkBox.Tag == 0) { checkBox.Checked = false; break; }
                    }
                }
            }
            else if (idTag != 0 && !checkBox.Checked)
            {
                bool isChecked = false;
                foreach (Control cnt in flowLayoutPanel1.Controls)
                {
                    if (cnt is CheckBox)
                    {
                        checkBox = (CheckBox)cnt;
                        if ((int)checkBox.Tag != 0 && checkBox.Checked) { isChecked = true; break; }
                    }
                }

                if (!isChecked)
                    foreach (Control cnt in flowLayoutPanel1.Controls)
                    {
                        if (cnt is CheckBox)
                        {
                            checkBox = (CheckBox)cnt;
                            if ((int)checkBox.Tag == 0) { checkBox.Checked = true; break; }
                        }
                    }
            }

            setFilter();
        }

        private void frmList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void getData()
        {
            isLoadData = true;
            if (cmbDateInvent.SelectedValue == null) return;

            string value = getSettings("dkor");
            countDayForEdit = int.Parse(value);

            Task<DataTable> task = Config.hCntMain.getException((int)cmbDateInvent.SelectedValue);
            task.Wait();
            dtData = task.Result;
            setFilter();
            dgvData.DataSource = dtData;
            isLoadData = false;
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btAdd.Enabled = btViewWorkUser.Enabled = btEdit.Enabled = btDel.Enabled = btTransfer.Enabled = false;
                btPrintDaysWork.Enabled =
                    btReportBonus.Enabled =
                    btPrint.Enabled = true;
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

                string strTypeException = "";
                foreach (Control cnt in flowLayoutPanel1.Controls)
                {
                    if (cnt is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)cnt;
                        if (checkBox.Checked && (int)checkBox.Tag != 0)
                        {
                            strTypeException += (strTypeException.Length == 0 ? "" : " or ")+ $"id_ExceptionType = {checkBox.Tag}";
                        }
                    }
                }

                if (strTypeException.Trim().Length > 0) filter += (filter.Length == 0 ? "" : " and ") + strTypeException;

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {

                btAdd.Enabled = btEdit.Enabled = btDel.Enabled = DateTime.Parse(cmbDateInvent.Text).AddDays(countDayForEdit).Date <= DateTime.Now.Date;


                btViewWorkUser.Enabled = dtData.DefaultView.Count != 0;
                btPrintDaysWork.Enabled =
                   btReportBonus.Enabled =
                   btPrint.Enabled = true;
                setActiveTransferButton();
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

        private void btTransfer_Click(object sender, EventArgs e)
        {
            EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable()
                .Where(r => r.Field<bool>("isSelect"));

            if (rowCollect.Count() == 0) return;

            int id_ttost = (int)cmbDateInvent.SelectedValue;
            new users.frmTransfer() { id_ttost = id_ttost, rowCollectIn = rowCollect }.ShowDialog();
        }

        private void setActiveTransferButton()
        {
            if (isLoadData || dtData == null)
            {
                btTransfer.Enabled = false;
                return;
            }

            dtData.AcceptChanges();

            EnumerableRowCollection<DataRow> rowCollect = dtData.DefaultView.ToTable().AsEnumerable().Where(r => r.Field<bool>("isSelect"));
            btTransfer.Enabled = rowCollect.Count() != 0;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoadData) return;
            if (e.ColumnIndex == cSelect.Index)
            {
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "True":
                        ch1.Value = false;
                        break;
                    case "False":
                        ch1.Value = true;
                        break;
                }

                //bool isSelect = (bool)dtData.DefaultView[e.RowIndex][e.ColumnIndex];
                setActiveTransferButton();
            }

        }

        private string getSettings(string id_value)
        {
            Task<DataTable> task = Config.hCntMain.getSettings(id_value);
            task.Wait();
            if (task != null && task.Result.Rows.Count > 0)
                return task.Result.Rows[0]["value"].ToString();
            return "";
        }

        private void validateSettings(string id_value,string type_value,string value_name, string value, string comment, bool isInsertData)
        {
            Task<DataTable> task = Config.hCntMain.getSettings(id_value);
            task.Wait();
            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                if (isInsertData)
                {
                    task = Config.hCntMain.setSettings(id_value, type_value, value_name, value, comment);
                    task.Wait();
                }
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для формирования отчёта.","Печать",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }


            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;
            int indexCol = 0;
            int maxCol = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible && !col.DataPropertyName.Equals("isSelect") ) maxCol++;

            report.SetColumnWidth(indexRow, 1, indexRow, 1, 10);
            report.SetColumnWidth(indexRow, 2, indexRow, 2, 32);
            report.SetColumnWidth(indexRow, 3, indexRow, 3, 21);
            report.SetColumnWidth(indexRow, 4, indexRow, 4, 24);
            report.SetColumnWidth(indexRow, 5, indexRow, 5, 17);
            report.SetColumnWidth(indexRow, 6, indexRow, 6, 17);

            report.Merge(indexRow, 1, indexRow, maxCol);
            //report.SetWrapText(indexRow, 1, indexRow, 1);
            report.AddSingleValue($"Справочник исключений", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            //report.SetRowHeight(indexRow, 1, indexRow, 1, 45);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Дата основной инвентаризации: {cmbDateInvent.Text}", indexRow, 1); indexRow++;
            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Магазин: {cmbShop.Text}", indexRow, 1); indexRow++;

            string strTypeException = "";
            foreach (Control cnt in flowLayoutPanel1.Controls)
            {
                if (cnt is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)cnt;
                    if (checkBox.Checked )
                    {
                        strTypeException += (strTypeException.Length == 0 ? "" : ", ") + $"{checkBox.Text}";
                    }
                }
            }

            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Типы исключений: {strTypeException}", indexRow, 1); indexRow++;
            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Поиск по ФИО: {tbFio.Text}", indexRow, 1); indexRow++;
            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Выгрузил: {Nwuram.Framework.Settings.User.UserSettings.User.FullUsername}", indexRow, 1); indexRow++;
            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Дата выгрузки: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}", indexRow, 1); indexRow++;indexRow++;



            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible && !col.DataPropertyName.Equals("isSelect"))
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }

            report.SetFontBold(indexRow, 1, indexRow, indexCol);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, indexCol);
            report.SetBorders(indexRow, 1, indexRow, indexCol);
            report.SetWrapText(indexRow, 1, indexRow, indexCol);
            report.SetRowHeight(indexRow, 1, indexRow, indexCol, 36);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, indexCol);
            indexRow++;


            foreach (DataGridViewRow row in dgvData.Rows)
            {
                indexCol = 0;
                foreach (DataGridViewColumn col in dgvData.Columns)
                    if (col.Visible && !col.DataPropertyName.Equals("isSelect"))
                    {
                        indexCol++;
                        report.AddSingleValue(row.Cells[col.Name].Value.ToString(), indexRow, indexCol);                        
                    }
                report.SetCellColor(indexRow, 1, indexRow, indexCol, row.DefaultCellStyle.BackColor);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, indexCol);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, indexCol);
                report.SetBorders(indexRow, 1, indexRow, indexCol);
                report.SetWrapText(indexRow, 1, indexRow, indexCol);
                indexRow++;
            }

            report.Show();
        }

        private void btReportBonus_Click(object sender, EventArgs e)
        {
            if (cmbDateInvent.SelectedValue == null) return;

            Task<DataTable> task = Config.hCntMain.getReportBonusPayment((int)cmbDateInvent.SelectedValue);
            task.Wait();

            if(task.Result==null || task.Result.Rows.Count==0)
            {
                MessageBox.Show("Нет данных для формирования отчёта.", "Печать", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;            
            int maxCol = 6;
           
            report.SetColumnWidth(indexRow, 1, indexRow, 1, 6);
            report.SetColumnWidth(indexRow, 2, indexRow, 2, 32);
            report.SetColumnWidth(indexRow, 3, indexRow, 3, 25);
            report.SetColumnWidth(indexRow, 4, indexRow, 4, 50);
            report.SetColumnWidth(indexRow, 5, indexRow, 5, 17);
            report.SetColumnWidth(indexRow, 6, indexRow, 6, 17);

            report.Merge(indexRow, 1, indexRow, maxCol);
            //report.SetWrapText(indexRow, 1, indexRow, 1);
            report.AddSingleValue($"Премиям за подсчет холодильных камер", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            //report.SetRowHeight(indexRow, 1, indexRow, 1, 45);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Дата основной инвентаризации: {cmbDateInvent.Text}", indexRow, 1); indexRow++;            
            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Выгрузил: {Nwuram.Framework.Settings.User.UserSettings.User.FullUsername}", indexRow, 1); indexRow++;
            report.Merge(indexRow, 1, indexRow, maxCol); report.AddSingleValue($"Дата выгрузки: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}", indexRow, 1); indexRow++; indexRow++;


            report.AddSingleValue("№", indexRow, 1);
            report.AddSingleValue("ФИО сотрудника", indexRow, 2);
            report.AddSingleValue("Отдел", indexRow, 3);
            report.AddSingleValue("№ камеры", indexRow, 4);
            report.AddSingleValue("Сумма", indexRow, 5);
            report.AddSingleValue("Подпись", indexRow, 6);

            report.SetFontBold(indexRow, 1, indexRow, maxCol);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxCol);
            report.SetBorders(indexRow, 1, indexRow, maxCol);
            report.SetWrapText(indexRow, 1, indexRow, maxCol);
            report.SetRowHeight(indexRow, 1, indexRow, maxCol, 36);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxCol);
            indexRow++;


            int npp = 1;

            foreach (DataRow row in task.Result.Rows)
            {
                report.AddSingleValue($"{npp}", indexRow, 1);
                report.AddSingleValue($"{row["fio"]}", indexRow, 2);
                report.AddSingleValue($"{row["nameDeps"]}", indexRow, 3);
                report.AddSingleValue($"{row["place"]}", indexRow, 4);
                report.AddSingleValue($"{row["payment"]}", indexRow, 5);

                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxCol);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxCol);
                report.SetBorders(indexRow, 1, indexRow, maxCol);
                report.SetWrapText(indexRow, 1, indexRow, maxCol);
                indexRow++;
                npp++;
            }

            report.Show();
        }

        private void btPrintDaysWork_Click(object sender, EventArgs e)
        {
            if (cmbDateInvent.SelectedValue == null) return;

            Task<DataTable> task = Config.hCntMain.getReportDayWorkingUser((int)cmbDateInvent.SelectedValue);
            task.Wait();

            if (task.Result == null || task.Result.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для формирования отчёта.", "Печать", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;
            int maxCol = 5;

            report.SetColumnWidth(indexRow, 1, indexRow, 1, 11);
            report.SetColumnWidth(indexRow, 2, indexRow, 2, 35);
            report.SetColumnWidth(indexRow, 3, indexRow, 3, 26);
            report.SetColumnWidth(indexRow, 4, indexRow, 4, 27);
            report.SetColumnWidth(indexRow, 5, indexRow, 5, 16);
            

            report.Merge(indexRow, 1, indexRow, maxCol);
            //report.SetWrapText(indexRow, 1, indexRow, 1);
            report.AddSingleValue($"Отчет по местам подсчета суточников за {cmbDateInvent.Text}", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            //report.SetRowHeight(indexRow, 1, indexRow, 1, 45);
            indexRow++;
            indexRow++;

            report.AddSingleValue("Магазин", indexRow, 1);
            report.AddSingleValue("ФИО", indexRow, 2);
            report.AddSingleValue("Всего участков", indexRow, 3);
            report.AddSingleValue("Общее время подсчёта", indexRow, 4);
            report.AddSingleValue("Подпись", indexRow, 5);

            report.SetFontBold(indexRow, 1, indexRow, maxCol);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxCol);
            report.SetBorders(indexRow, 1, indexRow, maxCol);
            report.SetWrapText(indexRow, 1, indexRow, maxCol);
            report.SetRowHeight(indexRow, 1, indexRow, maxCol, 36);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxCol);
            indexRow++;

            var groupShop = task.Result.AsEnumerable().GroupBy(r => new { id_Shop = r.Field<int>("id_Shop"), nameShop = r.Field<string>("nameShop") })
                .Select(s => new { s.Key.id_Shop, s.Key.nameShop })
                .OrderBy(r => r.id_Shop);

            foreach (var gShop in groupShop)
            {
                var groupKadr = task.Result.AsEnumerable()
                    .Where(r => r.Field<int>("id_Shop") == gShop.id_Shop)
                    .GroupBy(r => new { id_kadr = r.Field<int>("id_kadr"), fio = r.Field<string>("fio") })
                    .Select(s => new { s.Key.id_kadr, s.Key.fio })
                    .OrderBy(r => r.fio);

                int startIndexShop = indexRow;
                foreach (var gKadr in groupKadr)
                {
                    EnumerableRowCollection<DataRow> rowCollect = task.Result.AsEnumerable()
                        .Where(r => r.Field<int>("id_Shop") == gShop.id_Shop && r.Field<int>("id_kadr") == gKadr.id_kadr)
                        .OrderBy(r => r.Field<DateTime>("timeStart"));

                    int startIndexKadr = indexRow;
                    TimeSpan sumTs =new TimeSpan();
                    foreach (DataRow row in rowCollect)
                    {
                        TimeSpan ts = ((DateTime)row["timeEnd"]) - ((DateTime)row["timeStart"]);
                        sumTs += ts;
                        string info = $"{((DateTime)row["timeStart"]).ToShortTimeString()} - {((DateTime)row["timeEnd"]).ToShortTimeString()} = {ts.Hours}ч.{ts.Minutes}мин. ";

                        report.AddSingleValue($"{row["nameDeps"]}", indexRow, 3);
                        report.AddSingleValue($"{info}", indexRow, 4);
                        report.SetBorders(indexRow, 1, indexRow, 5);
                        indexRow++;
                    }

                    report.AddSingleValue($"Итого:", indexRow, 3);
                    report.SetCellAlignmentToRight(indexRow, 3, indexRow, 3);
                    report.SetFontBold(indexRow, 3, indexRow, 4);
                    report.AddSingleValue($"{sumTs.Hours}ч.{sumTs.Minutes}мин.", indexRow, 4);
                    report.Merge(startIndexKadr, 2, indexRow, 2);                    
                    report.AddSingleValue($"{gKadr.fio}", startIndexKadr, 2);
                    report.SetCellAlignmentToCenter(startIndexKadr, 2, startIndexKadr, 2);
                    report.SetCellAlignmentToJustify(startIndexKadr, 2, startIndexKadr, 2);
                    report.Merge(startIndexKadr, 5, indexRow, 5);
                    report.SetBorders(indexRow, 1, indexRow, 5);
                    indexRow++;
                }
                report.Merge(startIndexShop, 1, indexRow-1, 1);
                report.AddSingleValue($"{gShop.nameShop}", startIndexShop, 1);
                report.SetCellAlignmentToCenter(startIndexShop, 1, startIndexShop, 1);
                report.SetCellAlignmentToJustify(startIndexShop, 1, startIndexShop, 1);

            }
            report.Show();
        }


    }
}
