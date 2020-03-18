using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class Form2
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
        protected void FillHoursComboBox()
        {
            for (int i = 0; i < 10; i++)
            {
                this.HoursComboBox.Items.Add("0"+i);
            }
            for(int i = 10; i < 24; i++)
            {
                this.HoursComboBox.Items.Add(i);
            }
            this.HoursComboBox.Text = "00";
        }
        protected void FillMinutesComboBox()
        {
            for(int i = 0; i < 10; i++)
            {
                this.MinutesComboBox.Items.Add("0"+i);
            }
            for (int i = 10; i < 60; i++)
            {
                this.MinutesComboBox.Items.Add(i);
            }
            this.MinutesComboBox.Text = "00";
        }
        protected void DisableSomeHourOptions()
        {
            int CurrentHour = DateTime.Now.Hour;
            for (int i = 0; i < 10 && i < CurrentHour; i++)
            {
                this.HoursComboBox.Items.Remove("0" + i);
            }
            for(int i = 10; i<24 && i < CurrentHour; i++)
            {
                this.HoursComboBox.Items.Remove(i);
            }
            this.HoursComboBox.Text = CurrentHour.ToString();
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TaskInputBox = new System.Windows.Forms.TextBox();
            this.AddTaskButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MinutesComboBox = new System.Windows.Forms.ComboBox();
            this.HoursComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TaskInputBox
            // 
            this.TaskInputBox.Location = new System.Drawing.Point(76, 64);
            this.TaskInputBox.Multiline = true;
            this.TaskInputBox.Name = "TaskInputBox";
            this.TaskInputBox.Size = new System.Drawing.Size(661, 85);
            this.TaskInputBox.TabIndex = 0;
            // 
            // AddTaskButton
            // 
            this.AddTaskButton.Location = new System.Drawing.Point(300, 309);
            this.AddTaskButton.Name = "AddTaskButton";
            this.AddTaskButton.Size = new System.Drawing.Size(126, 44);
            this.AddTaskButton.TabIndex = 2;
            this.AddTaskButton.Text = "ADD TASK";
            this.AddTaskButton.UseVisualStyleBackColor = true;
            this.AddTaskButton.Click += new System.EventHandler(this.AddTaskButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(76, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "ENTER THE TASK";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "SELECT THE TIME";
            // 
            // MinutesComboBox
            // 
            this.MinutesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MinutesComboBox.FormattingEnabled = true;
            this.MinutesComboBox.Location = new System.Drawing.Point(228, 213);
            this.MinutesComboBox.Name = "MinutesComboBox";
            this.MinutesComboBox.Size = new System.Drawing.Size(121, 24);
            this.MinutesComboBox.TabIndex = 6;
            // 
            // HoursComboBox
            // 
            this.HoursComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HoursComboBox.FormattingEnabled = true;
            this.HoursComboBox.Location = new System.Drawing.Point(82, 213);
            this.HoursComboBox.Name = "HoursComboBox";
            this.HoursComboBox.Size = new System.Drawing.Size(121, 24);
            this.HoursComboBox.TabIndex = 7;
            this.HoursComboBox.SelectedIndexChanged += new System.EventHandler(this.HoursComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(210, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = ":";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 441);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HoursComboBox);
            this.Controls.Add(this.MinutesComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddTaskButton);
            this.Controls.Add(this.TaskInputBox);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(EnteredTask == null)
            {
                this.Dispose();
            }
        }

        #endregion
        private System.Windows.Forms.TextBox TaskInputBox;
        private System.Windows.Forms.Button AddTaskButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox MinutesComboBox;
        private System.Windows.Forms.ComboBox HoursComboBox;
        private System.Windows.Forms.Label label4;
    }
}