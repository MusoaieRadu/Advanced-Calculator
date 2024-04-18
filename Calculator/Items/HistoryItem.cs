using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    //User control that defines a history item(expression followed by commmands)
    public partial class HistoryItem : UserControl
    {
        public Button DeleteButton { get { return btnDel; } }
        public HistoryItem(string text)
        {
            InitializeComponent();
            //Forcing the text box only to display something
            TextBox.Text = text;
            TextBox.Enabled = false;
            btnDel.Parent = this;
            btnCopy.Click += BtnCopyClick;
        }
        //We add in our main window textbox, the history item when copying
        private void BtnCopyClick(object sender, EventArgs e)
        {
            MainWindow main = Program.MainWindow; //getting the window instance
            int selectionStart = main.TextBox.SelectionStart;//from where to start pasting
            main.TextBox.SelectedText += "("; //we then add after a '('
            main.TextBox.SelectedText += TextBox.Text; //then the expression 
            main.TextBox.SelectedText += ")"; //followed by ')'
            //we move the caret after what we inserted after and give focus to the textbox
            main.TextBox.Select(selectionStart + TextBox.TextLength + 2, 0);
            main.TextBox.Focus();
        }
        //Overriding OnResize method
        //We dynamically scale the width of the textbox
        //and move the buttons accordingly
        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            int offset = 10;
            this.TextBox.Width = this.Width - btnCopy.Width - btnDel.Width - offset;
            btnCopy.Location = new Point(TextBox.Width + offset, btnCopy.Location.Y);
            btnDel.Location = new Point(TextBox.Width + btnCopy.Width + offset, btnDel.Location.Y);
            this.ResumeLayout();
            base.OnResize(e);
        }
    }
}
