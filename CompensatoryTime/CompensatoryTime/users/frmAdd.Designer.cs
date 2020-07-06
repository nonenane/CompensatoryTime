namespace CompensatoryTime.users
{
    partial class frmAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btClose = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDateInvent = new System.Windows.Forms.TextBox();
            this.lUsers = new System.Windows.Forms.Label();
            this.tbFio = new System.Windows.Forms.TextBox();
            this.btSelectUser = new System.Windows.Forms.Button();
            this.cmbShop = new System.Windows.Forms.ComboBox();
            this.lShop = new System.Windows.Forms.Label();
            this.lTypeExeptions = new System.Windows.Forms.Label();
            this.cmbTypeExeptions = new System.Windows.Forms.ComboBox();
            this.chbIsBonusValidate = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMoney = new System.Windows.Forms.RadioButton();
            this.rbDays = new System.Windows.Forms.RadioButton();
            this.tbDays = new System.Windows.Forms.TextBox();
            this.tbMoney = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.Exit;
            this.btClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btClose.Location = new System.Drawing.Point(351, 240);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 1;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.Save;
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btSave.Location = new System.Drawing.Point(313, 240);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 1;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дата основной инв.";
            // 
            // tbDateInvent
            // 
            this.tbDateInvent.Location = new System.Drawing.Point(130, 3);
            this.tbDateInvent.MaxLength = 12;
            this.tbDateInvent.Name = "tbDateInvent";
            this.tbDateInvent.ReadOnly = true;
            this.tbDateInvent.Size = new System.Drawing.Size(213, 20);
            this.tbDateInvent.TabIndex = 3;
            // 
            // lUsers
            // 
            this.lUsers.AutoSize = true;
            this.lUsers.Location = new System.Drawing.Point(16, 36);
            this.lUsers.Name = "lUsers";
            this.lUsers.Size = new System.Drawing.Size(60, 13);
            this.lUsers.TabIndex = 2;
            this.lUsers.Text = "Сотрудник";
            // 
            // tbFio
            // 
            this.tbFio.Location = new System.Drawing.Point(130, 29);
            this.tbFio.MaxLength = 150;
            this.tbFio.Name = "tbFio";
            this.tbFio.ReadOnly = true;
            this.tbFio.Size = new System.Drawing.Size(213, 20);
            this.tbFio.TabIndex = 3;
            // 
            // btSelectUser
            // 
            this.btSelectUser.Location = new System.Drawing.Point(349, 29);
            this.btSelectUser.Name = "btSelectUser";
            this.btSelectUser.Size = new System.Drawing.Size(38, 20);
            this.btSelectUser.TabIndex = 4;
            this.btSelectUser.Text = "...";
            this.btSelectUser.UseVisualStyleBackColor = true;
            this.btSelectUser.Click += new System.EventHandler(this.btSelectUser_Click);
            // 
            // cmbShop
            // 
            this.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShop.FormattingEnabled = true;
            this.cmbShop.Location = new System.Drawing.Point(130, 58);
            this.cmbShop.Name = "cmbShop";
            this.cmbShop.Size = new System.Drawing.Size(213, 21);
            this.cmbShop.TabIndex = 8;
            this.cmbShop.SelectionChangeCommitted += new System.EventHandler(this.cmbShop_SelectionChangeCommitted);
            // 
            // lShop
            // 
            this.lShop.AutoSize = true;
            this.lShop.Location = new System.Drawing.Point(16, 62);
            this.lShop.Name = "lShop";
            this.lShop.Size = new System.Drawing.Size(51, 13);
            this.lShop.TabIndex = 7;
            this.lShop.Text = "Магазин";
            // 
            // lTypeExeptions
            // 
            this.lTypeExeptions.AutoSize = true;
            this.lTypeExeptions.Location = new System.Drawing.Point(16, 89);
            this.lTypeExeptions.Name = "lTypeExeptions";
            this.lTypeExeptions.Size = new System.Drawing.Size(90, 13);
            this.lTypeExeptions.TabIndex = 7;
            this.lTypeExeptions.Text = "Тип исключения";
            // 
            // cmbTypeExeptions
            // 
            this.cmbTypeExeptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeExeptions.FormattingEnabled = true;
            this.cmbTypeExeptions.Location = new System.Drawing.Point(130, 85);
            this.cmbTypeExeptions.Name = "cmbTypeExeptions";
            this.cmbTypeExeptions.Size = new System.Drawing.Size(213, 21);
            this.cmbTypeExeptions.TabIndex = 8;
            this.cmbTypeExeptions.SelectionChangeCommitted += new System.EventHandler(this.cmbTypeExeptions_SelectionChangeCommitted);
            // 
            // chbIsBonusValidate
            // 
            this.chbIsBonusValidate.AutoSize = true;
            this.chbIsBonusValidate.Location = new System.Drawing.Point(130, 112);
            this.chbIsBonusValidate.Name = "chbIsBonusValidate";
            this.chbIsBonusValidate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbIsBonusValidate.Size = new System.Drawing.Size(157, 17);
            this.chbIsBonusValidate.TabIndex = 9;
            this.chbIsBonusValidate.Text = "Дополнительный подсчет";
            this.chbIsBonusValidate.UseVisualStyleBackColor = true;
            this.chbIsBonusValidate.Click += new System.EventHandler(this.chbIsBonusValidate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbMoney);
            this.groupBox1.Controls.Add(this.rbDays);
            this.groupBox1.Controls.Add(this.tbDays);
            this.groupBox1.Controls.Add(this.tbMoney);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(130, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 94);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вид компенсации";
            // 
            // rbMoney
            // 
            this.rbMoney.AutoSize = true;
            this.rbMoney.Checked = true;
            this.rbMoney.Location = new System.Drawing.Point(19, 20);
            this.rbMoney.Name = "rbMoney";
            this.rbMoney.Size = new System.Drawing.Size(75, 17);
            this.rbMoney.TabIndex = 0;
            this.rbMoney.TabStop = true;
            this.rbMoney.Text = "денежная";
            this.rbMoney.UseVisualStyleBackColor = true;
            this.rbMoney.CheckedChanged += new System.EventHandler(this.rbMoney_CheckedChanged);
            // 
            // rbDays
            // 
            this.rbDays.AutoSize = true;
            this.rbDays.Location = new System.Drawing.Point(165, 20);
            this.rbDays.Name = "rbDays";
            this.rbDays.Size = new System.Drawing.Size(60, 17);
            this.rbDays.TabIndex = 0;
            this.rbDays.Text = "отгулы";
            this.rbDays.UseVisualStyleBackColor = true;
            this.rbDays.CheckedChanged += new System.EventHandler(this.rbMoney_CheckedChanged);
            // 
            // tbDays
            // 
            this.tbDays.Enabled = false;
            this.tbDays.Location = new System.Drawing.Point(165, 43);
            this.tbDays.MaxLength = 4;
            this.tbDays.Name = "tbDays";
            this.tbDays.Size = new System.Drawing.Size(45, 20);
            this.tbDays.TabIndex = 3;
            this.tbDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            // 
            // tbMoney
            // 
            this.tbMoney.Location = new System.Drawing.Point(19, 43);
            this.tbMoney.MaxLength = 13;
            this.tbMoney.Name = "tbMoney";
            this.tbMoney.Size = new System.Drawing.Size(94, 20);
            this.tbMoney.TabIndex = 3;
            this.tbMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "дней.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "руб.";
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 284);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chbIsBonusValidate);
            this.Controls.Add(this.cmbTypeExeptions);
            this.Controls.Add(this.lTypeExeptions);
            this.Controls.Add(this.cmbShop);
            this.Controls.Add(this.lShop);
            this.Controls.Add(this.btSelectUser);
            this.Controls.Add(this.tbFio);
            this.Controls.Add(this.lUsers);
            this.Controls.Add(this.tbDateInvent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDateInvent;
        private System.Windows.Forms.Label lUsers;
        private System.Windows.Forms.TextBox tbFio;
        private System.Windows.Forms.Button btSelectUser;
        private System.Windows.Forms.ComboBox cmbShop;
        private System.Windows.Forms.Label lShop;
        private System.Windows.Forms.Label lTypeExeptions;
        private System.Windows.Forms.ComboBox cmbTypeExeptions;
        private System.Windows.Forms.CheckBox chbIsBonusValidate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbMoney;
        private System.Windows.Forms.RadioButton rbDays;
        private System.Windows.Forms.TextBox tbDays;
        private System.Windows.Forms.TextBox tbMoney;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}