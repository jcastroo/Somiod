namespace SOMIODPUB
{
    partial class FormPub
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBoxNomeContainer = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.buttonCreateContainer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.brokerDomainTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNameApp = new System.Windows.Forms.TextBox();
            this.buttonCreateApp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControlCreate = new System.Windows.Forms.TabControl();
            this.tabPageApp = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUpdateApplication = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxType2 = new System.Windows.Forms.ComboBox();
            this.buttonUpdateApp = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxType7 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxUpdateContainer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxType3 = new System.Windows.Forms.ComboBox();
            this.buttonUpdateContainer = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxType4 = new System.Windows.Forms.ComboBox();
            this.buttonRemoveAplication = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxType6 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxType5 = new System.Windows.Forms.ComboBox();
            this.buttonRemoveContainer = new System.Windows.Forms.Button();
            this.tabControlCreate.SuspendLayout();
            this.tabPageApp.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxNomeContainer
            // 
            this.textBoxNomeContainer.Enabled = false;
            this.textBoxNomeContainer.Location = new System.Drawing.Point(274, 151);
            this.textBoxNomeContainer.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNomeContainer.Name = "textBoxNomeContainer";
            this.textBoxNomeContainer.Size = new System.Drawing.Size(159, 22);
            this.textBoxNomeContainer.TabIndex = 0;
            this.textBoxNomeContainer.Visible = false;
            this.textBoxNomeContainer.TextChanged += new System.EventHandler(this.textBoxNome_TextChanged);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(274, 118);
            this.comboBoxType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(159, 24);
            this.comboBoxType.TabIndex = 2;
            this.comboBoxType.Visible = false;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // buttonCreateContainer
            // 
            this.buttonCreateContainer.Location = new System.Drawing.Point(133, 219);
            this.buttonCreateContainer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCreateContainer.Name = "buttonCreateContainer";
            this.buttonCreateContainer.Size = new System.Drawing.Size(301, 28);
            this.buttonCreateContainer.TabIndex = 3;
            this.buttonCreateContainer.Text = "Criar Container";
            this.buttonCreateContainer.UseVisualStyleBackColor = true;
            this.buttonCreateContainer.Visible = false;
            this.buttonCreateContainer.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 155);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nome do Container";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Aplicação";
            this.label3.Visible = false;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(285, 15);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(103, 28);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // brokerDomainTextBox
            // 
            this.brokerDomainTextBox.Location = new System.Drawing.Point(144, 18);
            this.brokerDomainTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.brokerDomainTextBox.Name = "brokerDomainTextBox";
            this.brokerDomainTextBox.Size = new System.Drawing.Size(132, 22);
            this.brokerDomainTextBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "IP Address: ";
            // 
            // textBoxNameApp
            // 
            this.textBoxNameApp.Location = new System.Drawing.Point(288, 134);
            this.textBoxNameApp.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNameApp.Name = "textBoxNameApp";
            this.textBoxNameApp.Size = new System.Drawing.Size(155, 22);
            this.textBoxNameApp.TabIndex = 0;
            this.textBoxNameApp.Visible = false;
            // 
            // buttonCreateApp
            // 
            this.buttonCreateApp.Location = new System.Drawing.Point(146, 182);
            this.buttonCreateApp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCreateApp.Name = "buttonCreateApp";
            this.buttonCreateApp.Size = new System.Drawing.Size(297, 28);
            this.buttonCreateApp.TabIndex = 3;
            this.buttonCreateApp.Text = "Create App";
            this.buttonCreateApp.UseVisualStyleBackColor = true;
            this.buttonCreateApp.Visible = false;
            this.buttonCreateApp.Click += new System.EventHandler(this.buttonCreateApp_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 138);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Nome da App";
            this.label5.Visible = false;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // tabControlCreate
            // 
            this.tabControlCreate.Controls.Add(this.tabPageApp);
            this.tabControlCreate.Controls.Add(this.tabPage2);
            this.tabControlCreate.Controls.Add(this.tabPage1);
            this.tabControlCreate.Controls.Add(this.tabPage3);
            this.tabControlCreate.Controls.Add(this.tabPage4);
            this.tabControlCreate.Controls.Add(this.tabPage5);
            this.tabControlCreate.Location = new System.Drawing.Point(100, 81);
            this.tabControlCreate.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlCreate.Name = "tabControlCreate";
            this.tabControlCreate.SelectedIndex = 0;
            this.tabControlCreate.Size = new System.Drawing.Size(659, 424);
            this.tabControlCreate.TabIndex = 14;
            this.tabControlCreate.Visible = false;
            // 
            // tabPageApp
            // 
            this.tabPageApp.Controls.Add(this.textBoxNameApp);
            this.tabPageApp.Controls.Add(this.label5);
            this.tabPageApp.Controls.Add(this.buttonCreateApp);
            this.tabPageApp.Location = new System.Drawing.Point(4, 25);
            this.tabPageApp.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageApp.Name = "tabPageApp";
            this.tabPageApp.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageApp.Size = new System.Drawing.Size(651, 395);
            this.tabPageApp.TabIndex = 0;
            this.tabPageApp.Text = "Create App";
            this.tabPageApp.UseVisualStyleBackColor = true;
            this.tabPageApp.Click += new System.EventHandler(this.tabPageApp_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.textBoxNomeContainer);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.comboBoxType);
            this.tabPage2.Controls.Add(this.buttonCreateContainer);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(651, 395);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Create Container";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxUpdateApplication);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.comboBoxType2);
            this.tabPage1.Controls.Add(this.buttonUpdateApp);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(651, 395);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "UpdateApp";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 133);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Aplicação";
            this.label4.Visible = false;
            // 
            // textBoxUpdateApplication
            // 
            this.textBoxUpdateApplication.Enabled = false;
            this.textBoxUpdateApplication.Location = new System.Drawing.Point(296, 166);
            this.textBoxUpdateApplication.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUpdateApplication.Name = "textBoxUpdateApplication";
            this.textBoxUpdateApplication.Size = new System.Drawing.Size(159, 22);
            this.textBoxUpdateApplication.TabIndex = 7;
            this.textBoxUpdateApplication.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 170);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Nome da Aplicação";
            this.label6.Visible = false;
            // 
            // comboBoxType2
            // 
            this.comboBoxType2.FormattingEnabled = true;
            this.comboBoxType2.Location = new System.Drawing.Point(296, 133);
            this.comboBoxType2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType2.Name = "comboBoxType2";
            this.comboBoxType2.Size = new System.Drawing.Size(159, 24);
            this.comboBoxType2.TabIndex = 8;
            this.comboBoxType2.Visible = false;
            this.comboBoxType2.SelectedIndexChanged += new System.EventHandler(this.comboBoxType2_SelectedIndexChanged);
            // 
            // buttonUpdateApp
            // 
            this.buttonUpdateApp.Location = new System.Drawing.Point(155, 234);
            this.buttonUpdateApp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUpdateApp.Name = "buttonUpdateApp";
            this.buttonUpdateApp.Size = new System.Drawing.Size(301, 28);
            this.buttonUpdateApp.TabIndex = 9;
            this.buttonUpdateApp.Text = "Update Aplicação";
            this.buttonUpdateApp.UseVisualStyleBackColor = true;
            this.buttonUpdateApp.Visible = false;
            this.buttonUpdateApp.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.comboBoxType7);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.textBoxUpdateContainer);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.comboBoxType3);
            this.tabPage3.Controls.Add(this.buttonUpdateContainer);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(651, 395);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "UpdateContainer";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(151, 160);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 16);
            this.label13.TabIndex = 13;
            this.label13.Text = "Container";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label13.Visible = false;
            // 
            // comboBoxType7
            // 
            this.comboBoxType7.Enabled = false;
            this.comboBoxType7.FormattingEnabled = true;
            this.comboBoxType7.Location = new System.Drawing.Point(296, 160);
            this.comboBoxType7.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType7.Name = "comboBoxType7";
            this.comboBoxType7.Size = new System.Drawing.Size(159, 24);
            this.comboBoxType7.TabIndex = 12;
            this.comboBoxType7.Visible = false;
            this.comboBoxType7.SelectedIndexChanged += new System.EventHandler(this.comboBoxType7_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(151, 121);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Aplicação";
            this.label7.Visible = false;
            // 
            // textBoxUpdateContainer
            // 
            this.textBoxUpdateContainer.Enabled = false;
            this.textBoxUpdateContainer.Location = new System.Drawing.Point(296, 192);
            this.textBoxUpdateContainer.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUpdateContainer.Name = "textBoxUpdateContainer";
            this.textBoxUpdateContainer.Size = new System.Drawing.Size(159, 22);
            this.textBoxUpdateContainer.TabIndex = 7;
            this.textBoxUpdateContainer.Visible = false;
            this.textBoxUpdateContainer.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(151, 196);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Nome do Container";
            this.label8.Visible = false;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // comboBoxType3
            // 
            this.comboBoxType3.FormattingEnabled = true;
            this.comboBoxType3.Location = new System.Drawing.Point(296, 121);
            this.comboBoxType3.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType3.Name = "comboBoxType3";
            this.comboBoxType3.Size = new System.Drawing.Size(159, 24);
            this.comboBoxType3.TabIndex = 8;
            this.comboBoxType3.Visible = false;
            this.comboBoxType3.SelectedIndexChanged += new System.EventHandler(this.comboBoxType3_SelectedIndexChanged);
            // 
            // buttonUpdateContainer
            // 
            this.buttonUpdateContainer.Location = new System.Drawing.Point(155, 234);
            this.buttonUpdateContainer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUpdateContainer.Name = "buttonUpdateContainer";
            this.buttonUpdateContainer.Size = new System.Drawing.Size(301, 28);
            this.buttonUpdateContainer.TabIndex = 9;
            this.buttonUpdateContainer.Text = "Update Container";
            this.buttonUpdateContainer.UseVisualStyleBackColor = true;
            this.buttonUpdateContainer.Visible = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.comboBoxType4);
            this.tabPage4.Controls.Add(this.buttonRemoveAplication);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(651, 395);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "RemoveApp";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(152, 167);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Aplicação";
            this.label9.Visible = false;
            // 
            // comboBoxType4
            // 
            this.comboBoxType4.FormattingEnabled = true;
            this.comboBoxType4.Location = new System.Drawing.Point(297, 167);
            this.comboBoxType4.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType4.Name = "comboBoxType4";
            this.comboBoxType4.Size = new System.Drawing.Size(159, 24);
            this.comboBoxType4.TabIndex = 8;
            this.comboBoxType4.Visible = false;
            this.comboBoxType4.SelectedIndexChanged += new System.EventHandler(this.comboBoxType4_SelectedIndexChanged);
            // 
            // buttonRemoveAplication
            // 
            this.buttonRemoveAplication.Location = new System.Drawing.Point(155, 234);
            this.buttonRemoveAplication.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveAplication.Name = "buttonRemoveAplication";
            this.buttonRemoveAplication.Size = new System.Drawing.Size(301, 28);
            this.buttonRemoveAplication.TabIndex = 9;
            this.buttonRemoveAplication.Text = "Remove Aplicação";
            this.buttonRemoveAplication.UseVisualStyleBackColor = true;
            this.buttonRemoveAplication.Visible = false;
            this.buttonRemoveAplication.Click += new System.EventHandler(this.buttonRemoveAplication_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label10);
            this.tabPage5.Controls.Add(this.comboBoxType6);
            this.tabPage5.Controls.Add(this.label11);
            this.tabPage5.Controls.Add(this.comboBoxType5);
            this.tabPage5.Controls.Add(this.buttonRemoveContainer);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(651, 395);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "RemoveContainer";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(151, 185);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "Container";
            this.label10.Visible = false;
            // 
            // comboBoxType6
            // 
            this.comboBoxType6.Enabled = false;
            this.comboBoxType6.FormattingEnabled = true;
            this.comboBoxType6.Location = new System.Drawing.Point(296, 185);
            this.comboBoxType6.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType6.Name = "comboBoxType6";
            this.comboBoxType6.Size = new System.Drawing.Size(159, 24);
            this.comboBoxType6.TabIndex = 12;
            this.comboBoxType6.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(151, 133);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Aplicação";
            this.label11.Visible = false;
            // 
            // comboBoxType5
            // 
            this.comboBoxType5.FormattingEnabled = true;
            this.comboBoxType5.Location = new System.Drawing.Point(296, 133);
            this.comboBoxType5.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType5.Name = "comboBoxType5";
            this.comboBoxType5.Size = new System.Drawing.Size(159, 24);
            this.comboBoxType5.TabIndex = 8;
            this.comboBoxType5.Visible = false;
            this.comboBoxType5.SelectedIndexChanged += new System.EventHandler(this.comboBoxType5_SelectedIndexChanged);
            // 
            // buttonRemoveContainer
            // 
            this.buttonRemoveContainer.Location = new System.Drawing.Point(155, 234);
            this.buttonRemoveContainer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveContainer.Name = "buttonRemoveContainer";
            this.buttonRemoveContainer.Size = new System.Drawing.Size(301, 28);
            this.buttonRemoveContainer.TabIndex = 9;
            this.buttonRemoveContainer.Text = "Remove Container";
            this.buttonRemoveContainer.UseVisualStyleBackColor = true;
            this.buttonRemoveContainer.Visible = false;
            // 
            // FormPub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 566);
            this.Controls.Add(this.tabControlCreate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.brokerDomainTextBox);
            this.Controls.Add(this.buttonConnect);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormPub";
            this.Text = "FormPub";
            this.Load += new System.EventHandler(this.FormPub_Load);
            this.tabControlCreate.ResumeLayout(false);
            this.tabPageApp.ResumeLayout(false);
            this.tabPageApp.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBoxNomeContainer;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonCreateContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox brokerDomainTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNameApp;
        private System.Windows.Forms.Button buttonCreateApp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControlCreate;
        private System.Windows.Forms.TabPage tabPageApp;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUpdateApplication;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxType2;
        private System.Windows.Forms.Button buttonUpdateApp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxUpdateContainer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxType3;
        private System.Windows.Forms.Button buttonUpdateContainer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxType4;
        private System.Windows.Forms.Button buttonRemoveAplication;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxType5;
        private System.Windows.Forms.Button buttonRemoveContainer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxType7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxType6;
    }
}

