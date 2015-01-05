namespace 隐患点主表导入工具
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.resultLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.baseDataComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.baseFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.baseSelect = new System.Windows.Forms.Button();
            this.baseComFile = new System.Windows.Forms.TextBox();
            this.baseComSelect = new System.Windows.Forms.Button();
            this.loadBaseBtn = new System.Windows.Forms.Button();
            this.baseTip = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.componentComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.componentFile = new System.Windows.Forms.TextBox();
            this.componentFileSelect = new System.Windows.Forms.Button();
            this.componentTip = new System.Windows.Forms.Label();
            this.componentFileBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.otherFile = new System.Windows.Forms.TextBox();
            this.otherFileSelect = new System.Windows.Forms.Button();
            this.otherFileBtn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.otherTip = new System.Windows.Forms.Label();
            this.otherComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(100, 215);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 12);
            this.resultLabel.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "基础数据导入";
            // 
            // baseDataComboBox
            // 
            this.baseDataComboBox.FormattingEnabled = true;
            this.baseDataComboBox.Items.AddRange(new object[] {
            "十万调查",
            "移民搬迁",
            "五万详查"});
            this.baseDataComboBox.Location = new System.Drawing.Point(114, 37);
            this.baseDataComboBox.Name = "baseDataComboBox";
            this.baseDataComboBox.Size = new System.Drawing.Size(121, 20);
            this.baseDataComboBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "文件路径";
            // 
            // baseFile
            // 
            this.baseFile.Location = new System.Drawing.Point(114, 74);
            this.baseFile.Name = "baseFile";
            this.baseFile.Size = new System.Drawing.Size(278, 21);
            this.baseFile.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "综合表路径";
            // 
            // baseSelect
            // 
            this.baseSelect.Location = new System.Drawing.Point(398, 72);
            this.baseSelect.Name = "baseSelect";
            this.baseSelect.Size = new System.Drawing.Size(75, 23);
            this.baseSelect.TabIndex = 16;
            this.baseSelect.Text = "…";
            this.baseSelect.UseVisualStyleBackColor = true;
            this.baseSelect.Click += new System.EventHandler(this.baseSelect_Click);
            // 
            // baseComFile
            // 
            this.baseComFile.Location = new System.Drawing.Point(114, 106);
            this.baseComFile.Name = "baseComFile";
            this.baseComFile.Size = new System.Drawing.Size(278, 21);
            this.baseComFile.TabIndex = 17;
            // 
            // baseComSelect
            // 
            this.baseComSelect.Location = new System.Drawing.Point(398, 104);
            this.baseComSelect.Name = "baseComSelect";
            this.baseComSelect.Size = new System.Drawing.Size(75, 23);
            this.baseComSelect.TabIndex = 18;
            this.baseComSelect.Text = "…";
            this.baseComSelect.UseVisualStyleBackColor = true;
            this.baseComSelect.Click += new System.EventHandler(this.baseComSelect_Click);
            // 
            // loadBaseBtn
            // 
            this.loadBaseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadBaseBtn.Location = new System.Drawing.Point(499, 72);
            this.loadBaseBtn.Name = "loadBaseBtn";
            this.loadBaseBtn.Size = new System.Drawing.Size(75, 53);
            this.loadBaseBtn.TabIndex = 19;
            this.loadBaseBtn.Text = "导入";
            this.loadBaseBtn.UseVisualStyleBackColor = true;
            this.loadBaseBtn.Click += new System.EventHandler(this.loadBaseBtn_Click);
            // 
            // baseTip
            // 
            this.baseTip.AutoSize = true;
            this.baseTip.Location = new System.Drawing.Point(112, 140);
            this.baseTip.Name = "baseTip";
            this.baseTip.Size = new System.Drawing.Size(0, 12);
            this.baseTip.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "属性数据导入";
            // 
            // componentComboBox
            // 
            this.componentComboBox.FormattingEnabled = true;
            this.componentComboBox.Items.AddRange(new object[] {
            "隐患点主表",
            "应急调查",
            "月报速报",
            "防灾预案表",
            "工作明白卡",
            "避险明白卡"});
            this.componentComboBox.Location = new System.Drawing.Point(114, 190);
            this.componentComboBox.Name = "componentComboBox";
            this.componentComboBox.Size = new System.Drawing.Size(121, 20);
            this.componentComboBox.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(55, 227);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "文件路径";
            // 
            // componentFile
            // 
            this.componentFile.Location = new System.Drawing.Point(114, 224);
            this.componentFile.Name = "componentFile";
            this.componentFile.Size = new System.Drawing.Size(278, 21);
            this.componentFile.TabIndex = 24;
            // 
            // componentFileSelect
            // 
            this.componentFileSelect.Location = new System.Drawing.Point(398, 222);
            this.componentFileSelect.Name = "componentFileSelect";
            this.componentFileSelect.Size = new System.Drawing.Size(75, 23);
            this.componentFileSelect.TabIndex = 25;
            this.componentFileSelect.Text = "…";
            this.componentFileSelect.UseVisualStyleBackColor = true;
            this.componentFileSelect.Click += new System.EventHandler(this.componentFileSelect_Click);
            // 
            // componentTip
            // 
            this.componentTip.AutoSize = true;
            this.componentTip.Location = new System.Drawing.Point(112, 258);
            this.componentTip.Name = "componentTip";
            this.componentTip.Size = new System.Drawing.Size(0, 12);
            this.componentTip.TabIndex = 26;
            // 
            // componentFileBtn
            // 
            this.componentFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.componentFileBtn.Location = new System.Drawing.Point(317, 280);
            this.componentFileBtn.Name = "componentFileBtn";
            this.componentFileBtn.Size = new System.Drawing.Size(75, 23);
            this.componentFileBtn.TabIndex = 27;
            this.componentFileBtn.Text = "导入";
            this.componentFileBtn.UseVisualStyleBackColor = true;
            this.componentFileBtn.Click += new System.EventHandler(this.componentFileBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "矿山复绿，移民搬迁，防治规划";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(55, 363);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "数据类型";
            // 
            // otherFile
            // 
            this.otherFile.Location = new System.Drawing.Point(114, 389);
            this.otherFile.Name = "otherFile";
            this.otherFile.Size = new System.Drawing.Size(277, 21);
            this.otherFile.TabIndex = 30;
            // 
            // otherFileSelect
            // 
            this.otherFileSelect.Location = new System.Drawing.Point(397, 387);
            this.otherFileSelect.Name = "otherFileSelect";
            this.otherFileSelect.Size = new System.Drawing.Size(75, 23);
            this.otherFileSelect.TabIndex = 31;
            this.otherFileSelect.Text = "…";
            this.otherFileSelect.UseVisualStyleBackColor = true;
            this.otherFileSelect.Click += new System.EventHandler(this.otherFileSelect_Click);
            // 
            // otherFileBtn
            // 
            this.otherFileBtn.Location = new System.Drawing.Point(317, 454);
            this.otherFileBtn.Name = "otherFileBtn";
            this.otherFileBtn.Size = new System.Drawing.Size(75, 23);
            this.otherFileBtn.TabIndex = 32;
            this.otherFileBtn.Text = "导入";
            this.otherFileBtn.UseVisualStyleBackColor = true;
            this.otherFileBtn.Click += new System.EventHandler(this.otherFileBtn_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(55, 394);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "文件路径";
            // 
            // otherTip
            // 
            this.otherTip.AutoSize = true;
            this.otherTip.Location = new System.Drawing.Point(112, 424);
            this.otherTip.Name = "otherTip";
            this.otherTip.Size = new System.Drawing.Size(0, 12);
            this.otherTip.TabIndex = 34;
            // 
            // otherComboBox
            // 
            this.otherComboBox.FormattingEnabled = true;
            this.otherComboBox.Items.AddRange(new object[] {
            "矿山复绿基础档案表",
            "矿山复绿环境调查表",
            "矿山复绿遥感解译卡",
            "移民搬迁泥石流核查表",
            "移民搬迁崩塌核查表",
            "移民搬迁滑坡核查表",
            "移民搬迁斜坡核查表",
            "移民搬迁地面塌陷核查表",
            "移民搬迁安置地评价表",
            "防治规划"});
            this.otherComboBox.Location = new System.Drawing.Point(114, 360);
            this.otherComboBox.Name = "otherComboBox";
            this.otherComboBox.Size = new System.Drawing.Size(148, 20);
            this.otherComboBox.TabIndex = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 553);
            this.Controls.Add(this.otherComboBox);
            this.Controls.Add(this.otherTip);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.otherFileBtn);
            this.Controls.Add(this.otherFileSelect);
            this.Controls.Add(this.otherFile);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.componentFileBtn);
            this.Controls.Add(this.componentTip);
            this.Controls.Add(this.componentFileSelect);
            this.Controls.Add(this.componentFile);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.componentComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.baseTip);
            this.Controls.Add(this.loadBaseBtn);
            this.Controls.Add(this.baseComSelect);
            this.Controls.Add(this.baseComFile);
            this.Controls.Add(this.baseSelect);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.baseFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.baseDataComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.resultLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "江西省数据导入工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox baseDataComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox baseFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button baseSelect;
        private System.Windows.Forms.TextBox baseComFile;
        private System.Windows.Forms.Button baseComSelect;
        private System.Windows.Forms.Button loadBaseBtn;
        private System.Windows.Forms.Label baseTip;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox componentComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox componentFile;
        private System.Windows.Forms.Button componentFileSelect;
        private System.Windows.Forms.Label componentTip;
        private System.Windows.Forms.Button componentFileBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox otherFile;
        private System.Windows.Forms.Button otherFileSelect;
        private System.Windows.Forms.Button otherFileBtn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label otherTip;
        private System.Windows.Forms.ComboBox otherComboBox;
    }
}

