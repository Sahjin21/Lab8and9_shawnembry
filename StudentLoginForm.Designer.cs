namespace FallLab2
{
    partial class StudentLoginForm
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
            this.StudentIdLabel = new System.Windows.Forms.Label();
            this.StudentIDTextBox = new System.Windows.Forms.TextBox();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtbxAdminID = new System.Windows.Forms.TextBox();
            this.txtbxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // StudentIdLabel
            // 
            this.StudentIdLabel.AutoSize = true;
            this.StudentIdLabel.Location = new System.Drawing.Point(16, 29);
            this.StudentIdLabel.Name = "StudentIdLabel";
            this.StudentIdLabel.Size = new System.Drawing.Size(49, 13);
            this.StudentIdLabel.TabIndex = 0;
            this.StudentIdLabel.Text = "Enter ID:";
            // 
            // StudentIDTextBox
            // 
            this.StudentIDTextBox.Location = new System.Drawing.Point(71, 26);
            this.StudentIDTextBox.Name = "StudentIDTextBox";
            this.StudentIDTextBox.Size = new System.Drawing.Size(100, 20);
            this.StudentIDTextBox.TabIndex = 1;
            this.StudentIDTextBox.TextChanged += new System.EventHandler(this.StudentIDTextBox_TextChanged);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(156, 179);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 23);
            this.ContinueButton.TabIndex = 2;
            this.ContinueButton.Text = "Login";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StudentIDTextBox);
            this.groupBox1.Controls.Add(this.StudentIdLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 57);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Student Login";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtbxPassword);
            this.groupBox2.Controls.Add(this.txtbxAdminID);
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 98);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Admin Login";
            // 
            // txtbxAdminID
            // 
            this.txtbxAdminID.Location = new System.Drawing.Point(71, 26);
            this.txtbxAdminID.Name = "txtbxAdminID";
            this.txtbxAdminID.Size = new System.Drawing.Size(100, 20);
            this.txtbxAdminID.TabIndex = 0;
            // 
            // txtbxPassword
            // 
            this.txtbxPassword.Location = new System.Drawing.Point(71, 62);
            this.txtbxPassword.Name = "txtbxPassword";
            this.txtbxPassword.Size = new System.Drawing.Size(100, 20);
            this.txtbxPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // StudentLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 211);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "StudentLoginForm";
            this.Text = "Login";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label StudentIdLabel;
        private System.Windows.Forms.TextBox StudentIDTextBox;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxPassword;
        private System.Windows.Forms.TextBox txtbxAdminID;
    }
}