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

namespace CompensatoryTime.users
{
    public partial class frmAdd : Form
    {

        public DataRowView row { set; private get; }
        public int id_ttost { set; private get; }
        public DateTime dateInvent { set; private get; }

        private int id = 0, id_kadr;
        private bool isEditData = false;
        private string oldName, oldNpp;
        private bool oldViewAdd, oldViewArchive;

        public frmAdd()
        {
            InitializeComponent();

            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSave, "Сохранить");            
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


            tbDateInvent.Text = dateInvent.ToShortDateString();


            if (row != null)
            {
                id = (int)row["id"];
                id_kadr = (int)row["id_kadr"];
                tbFio.Text = (string)row["fio"];
                cmbShop.SelectedValue = (int)row["id_shop"];
                cmbTypeExeptions.SelectedValue = (int)row["id_shop"];
                chbIsBonusValidate.Checked = (bool)row["isDop"];
                if (row["Summa"] != DBNull.Value)
                {
                    rbMoney.Checked = true; tbMoney.Text = row["Summa"].ToString();
                }
                else if (row["CountDays"] != DBNull.Value)
                {
                    rbDays.Checked = true; tbDays.Text = row["CountDays"].ToString();
                }


                //tbName.Text = (string)row["cName"];
                //tbNpp.Text = row["npp"].ToString();
                //chbViewAdd.Checked = (bool)row["ViewAdd"];
                //chbViewArchive.Checked = (bool)row["ViewArchive"];

                //oldName = tbName.Text.Trim();
                //oldNpp = tbNpp.Text.Trim();
                //oldViewAdd = chbViewAdd.Checked;
                //oldViewArchive = chbViewArchive.Checked;
            }

            isEditData = false;           
        }

        private void frmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void rbMoney_CheckedChanged(object sender, EventArgs e)
        {
            tbMoney.Enabled = rbMoney.Checked;
            tbDays.Enabled = rbDays.Checked;
            tbDays.Clear();
            tbMoney.Clear();
            isEditData = true;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (tbFio.Text.Trim().Length == 0 && id_kadr == 0)
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
                MessageBox.Show($"Необходимо заполнить  \"{groupBox1.Text} {rbMoney.Text}\"", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMoney.Focus();
                return;
            }


            if (rbDays.Checked && (tbDays.Text.Trim().Length == 0 || decimal.Parse(tbDays.Text) == 0))
            {
                MessageBox.Show($"Необходимо заполнить \"{groupBox1.Text} {rbDays.Text}\"", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDays.Focus();
                return;
            }

            decimal? summa= null;
            decimal? countDay = null;
            if (rbDays.Checked) countDay = decimal.Parse(tbDays.Text);
            if (rbMoney.Checked) summa = decimal.Parse(tbMoney.Text);

            Task<DataTable> task = Config.hCntMain.setException(id,id_kadr,(int)cmbShop.SelectedValue,id_ttost,(int)cmbTypeExeptions.SelectedValue,chbIsBonusValidate.Checked, summa, countDay, false, 0);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show(Config.centralText("В справочнике уже присутствует сотрудник\nназначенный на эту дату основной инвентаризации.\n"), "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -2)
            {
                MessageBox.Show("В справочнике уже занят указанный Вами номер по порядку.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая хрень.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (id == 0)
            {
                id = (int)dtResult.Rows[0]["id"];
                Logging.StartFirstLevel(1);
                Logging.Comment("Добавить Тип документа");
                //Logging.Comment($"ID: {id}");
                //Logging.Comment($"Наименование: {tbName.Text.Trim()}");
                //Logging.Comment($"Номер по порядку: {tbNpp.Text.Trim()}");
                //Logging.Comment($"Отображение архивных документов у руководителя: {(chbViewArchive.Checked ? "Да" : "Нет")}");
                //Logging.Comment($"Отображать при добавлении документа: {(chbViewAdd.Checked ? "Да" : "Нет")}");
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1);
                Logging.Comment("Редактировать Тип документа");
                Logging.Comment($"ID: {id}");
                //Logging.VariableChange("Наименование", tbName.Text.Trim(), oldName);
                //Logging.VariableChange("Номер по порядку", tbNpp.Text.Trim(), oldNpp);
                //Logging.VariableChange($"Отображение архивных документов у руководителя:", (chbViewArchive.Checked ? "Да" : "Нет"), (oldViewArchive ? "Да" : "Нет"));
                //Logging.VariableChange($"Отображать при добавлении документа:", (chbViewAdd.Checked ? "Да" : "Нет"), (oldViewAdd ? "Да" : "Нет"));

                Logging.StopFirstLevel();
            }


            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;


            this.DialogResult = DialogResult.OK;
        }

        private void cmbShop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void chbIsBonusValidate_Click(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void cmbTypeExeptions_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chbIsBonusValidate.Checked = false;
            chbIsBonusValidate.Enabled = (bool)(cmbTypeExeptions.DataSource as DataTable).AsEnumerable().Where(r => r.Field<int>("id") == (int)cmbTypeExeptions.SelectedValue).First()["isDop"];
            isEditData = true;
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

            isEditData = true;
        }

        private void btSelectUser_Click(object sender, EventArgs e)
        {
            id_kadr = 0;
            frmSelect frmSelect = new frmSelect() { dateInvent = dateInvent };
            if (DialogResult.OK == frmSelect.ShowDialog())
            {
                tbFio.Text = frmSelect.nameUser;
                id_kadr = frmSelect.id_kadr;
                isEditData = true;
            }
        }
    }
}
