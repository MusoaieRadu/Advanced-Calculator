namespace Calculator.Forms
{
    partial class HelpForm
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
            this.helpText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // helpText
            // 
            this.helpText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpText.Location = new System.Drawing.Point(12, 9);
            this.helpText.Name = "helpText";
            this.helpText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.helpText.Size = new System.Drawing.Size(822, 728);
            this.helpText.TabIndex = 0;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 783);
            this.Controls.Add(this.helpText);
            this.MinimumSize = new System.Drawing.Size(864, 830);
            this.Name = "HelpForm";
            this.Text = "Help";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label helpText;
    }
}