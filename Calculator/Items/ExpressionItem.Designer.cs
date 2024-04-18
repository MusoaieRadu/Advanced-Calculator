namespace Calculator
{
    partial class ExpressionItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.paramList = new System.Windows.Forms.ComboBox();
            this.param = new System.Windows.Forms.Label();
            this.val = new System.Windows.Forms.Label();
            this.valueBox = new System.Windows.Forms.TextBox();
            this.id_name = new System.Windows.Forms.Label();
            this.functionText = new System.Windows.Forms.RichTextBox();
            this.realText = new System.Windows.Forms.RichTextBox();
            this.result = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnPlot = new System.Windows.Forms.Button();
            this.xMaxBox = new System.Windows.Forms.TextBox();
            this.xMinBox = new System.Windows.Forms.TextBox();
            this.xMinLabel = new System.Windows.Forms.Label();
            this.xMaxLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // paramList
            // 
            this.paramList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramList.FormattingEnabled = true;
            this.paramList.Location = new System.Drawing.Point(161, 66);
            this.paramList.Name = "paramList";
            this.paramList.Size = new System.Drawing.Size(52, 33);
            this.paramList.TabIndex = 1;
            // 
            // param
            // 
            this.param.AutoSize = true;
            this.param.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.param.Location = new System.Drawing.Point(11, 69);
            this.param.Name = "param";
            this.param.Size = new System.Drawing.Size(113, 25);
            this.param.TabIndex = 2;
            this.param.Text = "Parameter :";
            // 
            // val
            // 
            this.val.AutoSize = true;
            this.val.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.val.Location = new System.Drawing.Point(11, 114);
            this.val.Name = "val";
            this.val.Size = new System.Drawing.Size(79, 25);
            this.val.TabIndex = 3;
            this.val.Text = "Value : ";
            // 
            // valueBox
            // 
            this.valueBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueBox.Location = new System.Drawing.Point(96, 114);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(117, 30);
            this.valueBox.TabIndex = 4;
            this.valueBox.Text = "0";
            // 
            // id_name
            // 
            this.id_name.AutoSize = true;
            this.id_name.BackColor = System.Drawing.SystemColors.Window;
            this.id_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.id_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_name.Location = new System.Drawing.Point(16, 21);
            this.id_name.Name = "id_name";
            this.id_name.Size = new System.Drawing.Size(2, 27);
            this.id_name.TabIndex = 6;
            // 
            // functionText
            // 
            this.functionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.functionText.Location = new System.Drawing.Point(161, 21);
            this.functionText.Multiline = false;
            this.functionText.Name = "functionText";
            this.functionText.ReadOnly = true;
            this.functionText.Size = new System.Drawing.Size(518, 31);
            this.functionText.TabIndex = 7;
            this.functionText.Text = "";
            // 
            // realText
            // 
            this.realText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.realText.Location = new System.Drawing.Point(91, 161);
            this.realText.Multiline = false;
            this.realText.Name = "realText";
            this.realText.ReadOnly = true;
            this.realText.Size = new System.Drawing.Size(588, 31);
            this.realText.TabIndex = 7;
            this.realText.Text = "";
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result.Location = new System.Drawing.Point(3, 164);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(82, 25);
            this.result.TabIndex = 8;
            this.result.Text = "Result : ";
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(584, 66);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(95, 78);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // btnPlot
            // 
            this.btnPlot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlot.Location = new System.Drawing.Point(473, 66);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(95, 78);
            this.btnPlot.TabIndex = 9;
            this.btnPlot.Text = "Plot";
            this.btnPlot.UseVisualStyleBackColor = true;
            // 
            // xMaxBox
            // 
            this.xMaxBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMaxBox.Location = new System.Drawing.Point(396, 114);
            this.xMaxBox.Name = "xMaxBox";
            this.xMaxBox.Size = new System.Drawing.Size(54, 30);
            this.xMaxBox.TabIndex = 4;
            this.xMaxBox.Text = "0";
            // 
            // xMinBox
            // 
            this.xMinBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMinBox.Location = new System.Drawing.Point(321, 114);
            this.xMinBox.Name = "xMinBox";
            this.xMinBox.Size = new System.Drawing.Size(54, 30);
            this.xMinBox.TabIndex = 4;
            this.xMinBox.Text = "0";
            // 
            // xMinLabel
            // 
            this.xMinLabel.AutoSize = true;
            this.xMinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMinLabel.Location = new System.Drawing.Point(316, 86);
            this.xMinLabel.Name = "xMinLabel";
            this.xMinLabel.Size = new System.Drawing.Size(59, 25);
            this.xMinLabel.TabIndex = 2;
            this.xMinLabel.Text = "xMin ";
            // 
            // xMaxLabel
            // 
            this.xMaxLabel.AutoSize = true;
            this.xMaxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMaxLabel.Location = new System.Drawing.Point(391, 86);
            this.xMaxLabel.Name = "xMaxLabel";
            this.xMaxLabel.Size = new System.Drawing.Size(60, 25);
            this.xMaxLabel.TabIndex = 2;
            this.xMaxLabel.Text = "xMax";
            // 
            // ExpressionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnPlot);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.result);
            this.Controls.Add(this.realText);
            this.Controls.Add(this.functionText);
            this.Controls.Add(this.id_name);
            this.Controls.Add(this.xMinBox);
            this.Controls.Add(this.xMaxBox);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.val);
            this.Controls.Add(this.xMaxLabel);
            this.Controls.Add(this.xMinLabel);
            this.Controls.Add(this.param);
            this.Controls.Add(this.paramList);
            this.MinimumSize = new System.Drawing.Size(270, 205);
            this.Name = "ExpressionItem";
            this.Size = new System.Drawing.Size(689, 203);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox paramList;
        private System.Windows.Forms.Label param;
        private System.Windows.Forms.Label val;
        private System.Windows.Forms.TextBox valueBox;
        private System.Windows.Forms.Label id_name;
        private System.Windows.Forms.RichTextBox functionText;
        private System.Windows.Forms.RichTextBox realText;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.TextBox xMaxBox;
        private System.Windows.Forms.TextBox xMinBox;
        private System.Windows.Forms.Label xMinLabel;
        private System.Windows.Forms.Label xMaxLabel;
    }
}
