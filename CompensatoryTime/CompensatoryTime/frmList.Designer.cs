namespace CompensatoryTime
{
    partial class frmList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cShop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeExeption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMoneyBonus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDayBonus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbFio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDateInvent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbShop = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btPrintDaysWork = new System.Windows.Forms.Button();
            this.btReportBonus = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btViewWorkUser = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btTransfer = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 575);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "доп.подсчет";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(15, 572);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(18, 18);
            this.panel1.TabIndex = 2;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cSelect,
            this.cShop,
            this.cFIO,
            this.cDeps,
            this.cTypeExeption,
            this.cMoneyBonus,
            this.cDayBonus});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.Location = new System.Drawing.Point(12, 132);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(887, 427);
            this.dgvData.TabIndex = 3;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            // 
            // cSelect
            // 
            this.cSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cSelect.DataPropertyName = "isSelect";
            this.cSelect.HeaderText = "V";
            this.cSelect.MinimumWidth = 35;
            this.cSelect.Name = "cSelect";
            this.cSelect.Width = 35;
            // 
            // cShop
            // 
            this.cShop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cShop.DataPropertyName = "nameShop";
            this.cShop.FillWeight = 259.2592F;
            this.cShop.HeaderText = "Магазин";
            this.cShop.MinimumWidth = 70;
            this.cShop.Name = "cShop";
            this.cShop.ReadOnly = true;
            this.cShop.Width = 70;
            // 
            // cFIO
            // 
            this.cFIO.DataPropertyName = "fio";
            this.cFIO.FillWeight = 68.14815F;
            this.cFIO.HeaderText = "ФИО";
            this.cFIO.Name = "cFIO";
            this.cFIO.ReadOnly = true;
            // 
            // cDeps
            // 
            this.cDeps.DataPropertyName = "nameDeps";
            this.cDeps.FillWeight = 68.14815F;
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            // 
            // cTypeExeption
            // 
            this.cTypeExeption.DataPropertyName = "nameExpType";
            this.cTypeExeption.FillWeight = 68.14815F;
            this.cTypeExeption.HeaderText = "Тип исключения";
            this.cTypeExeption.Name = "cTypeExeption";
            this.cTypeExeption.ReadOnly = true;
            // 
            // cMoneyBonus
            // 
            this.cMoneyBonus.DataPropertyName = "Summa";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.cMoneyBonus.DefaultCellStyle = dataGridViewCellStyle2;
            this.cMoneyBonus.FillWeight = 68.14815F;
            this.cMoneyBonus.HeaderText = "Вознаграждение денежное";
            this.cMoneyBonus.Name = "cMoneyBonus";
            this.cMoneyBonus.ReadOnly = true;
            // 
            // cDayBonus
            // 
            this.cDayBonus.DataPropertyName = "CountDays";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cDayBonus.DefaultCellStyle = dataGridViewCellStyle3;
            this.cDayBonus.FillWeight = 68.14815F;
            this.cDayBonus.HeaderText = "Вознаграждение отгулы";
            this.cDayBonus.Name = "cDayBonus";
            this.cDayBonus.ReadOnly = true;
            // 
            // tbFio
            // 
            this.tbFio.Location = new System.Drawing.Point(328, 106);
            this.tbFio.Name = "tbFio";
            this.tbFio.Size = new System.Drawing.Size(100, 20);
            this.tbFio.TabIndex = 4;
            this.tbFio.TextChanged += new System.EventHandler(this.tbFio_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Дата основной инв.";
            // 
            // cmbDateInvent
            // 
            this.cmbDateInvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateInvent.FormattingEnabled = true;
            this.cmbDateInvent.Location = new System.Drawing.Point(127, 13);
            this.cmbDateInvent.Name = "cmbDateInvent";
            this.cmbDateInvent.Size = new System.Drawing.Size(195, 21);
            this.cmbDateInvent.TabIndex = 6;
            this.cmbDateInvent.SelectionChangeCommitted += new System.EventHandler(this.cmbDateInvent_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Магазин";
            // 
            // cmbShop
            // 
            this.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShop.FormattingEnabled = true;
            this.cmbShop.Location = new System.Drawing.Point(127, 40);
            this.cmbShop.Name = "cmbShop";
            this.cmbShop.Size = new System.Drawing.Size(195, 21);
            this.cmbShop.TabIndex = 6;
            this.cmbShop.SelectionChangeCommitted += new System.EventHandler(this.cmbShop_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(328, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 86);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип исключения";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(457, 67);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btPrintDaysWork
            // 
            this.btPrintDaysWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrintDaysWork.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.Print;
            this.btPrintDaysWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btPrintDaysWork.Location = new System.Drawing.Point(436, 565);
            this.btPrintDaysWork.Name = "btPrintDaysWork";
            this.btPrintDaysWork.Size = new System.Drawing.Size(32, 32);
            this.btPrintDaysWork.TabIndex = 0;
            this.btPrintDaysWork.UseVisualStyleBackColor = true;
            this.btPrintDaysWork.Click += new System.EventHandler(this.btPrintDaysWork_Click);
            // 
            // btReportBonus
            // 
            this.btReportBonus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btReportBonus.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.honor;
            this.btReportBonus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btReportBonus.Location = new System.Drawing.Point(474, 565);
            this.btReportBonus.Name = "btReportBonus";
            this.btReportBonus.Size = new System.Drawing.Size(32, 32);
            this.btReportBonus.TabIndex = 0;
            this.btReportBonus.UseVisualStyleBackColor = true;
            this.btReportBonus.Click += new System.EventHandler(this.btReportBonus_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.microsoft_excel;
            this.btPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btPrint.Location = new System.Drawing.Point(512, 565);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 0;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btViewWorkUser
            // 
            this.btViewWorkUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btViewWorkUser.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.vision;
            this.btViewWorkUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btViewWorkUser.Location = new System.Drawing.Point(677, 565);
            this.btViewWorkUser.Name = "btViewWorkUser";
            this.btViewWorkUser.Size = new System.Drawing.Size(32, 32);
            this.btViewWorkUser.TabIndex = 0;
            this.btViewWorkUser.UseVisualStyleBackColor = true;
            this.btViewWorkUser.Click += new System.EventHandler(this.btViewWorkUser_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.Add;
            this.btAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btAdd.Location = new System.Drawing.Point(715, 565);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 0;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdit.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.Edit;
            this.btEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btEdit.Location = new System.Drawing.Point(753, 565);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(32, 32);
            this.btEdit.TabIndex = 0;
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btTransfer
            // 
            this.btTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTransfer.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.arrows;
            this.btTransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btTransfer.Location = new System.Drawing.Point(791, 565);
            this.btTransfer.Name = "btTransfer";
            this.btTransfer.Size = new System.Drawing.Size(32, 32);
            this.btTransfer.TabIndex = 0;
            this.btTransfer.UseVisualStyleBackColor = true;
            this.btTransfer.Click += new System.EventHandler(this.btTransfer_Click);
            // 
            // btDel
            // 
            this.btDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDel.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.Trash;
            this.btDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btDel.Location = new System.Drawing.Point(829, 565);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 0;
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.refresh;
            this.btUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btUpdate.Location = new System.Drawing.Point(867, 29);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(32, 32);
            this.btUpdate.TabIndex = 0;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackgroundImage = global::DicCompensatoryTime.Properties.Resources.Exit;
            this.btClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btClose.Location = new System.Drawing.Point(867, 565);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 0;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 609);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbShop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDateInvent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFio);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btPrintDaysWork);
            this.Controls.Add(this.btReportBonus);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btViewWorkUser);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btTransfer);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btClose);
            this.MinimizeBox = false;
            this.Name = "frmList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник исключений";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmList_FormClosing);
            this.Load += new System.EventHandler(this.frmList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.Button btTransfer;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btViewWorkUser;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btReportBonus;
        private System.Windows.Forms.Button btPrintDaysWork;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox tbFio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDateInvent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbShop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn cShop;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeExeption;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMoneyBonus;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDayBonus;
    }
}

