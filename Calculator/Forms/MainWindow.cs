using Calculator.Forms;
using Calculator.Input;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    //Main window, where the program begins from
    public partial class MainWindow : Form
    {
        //Calculator string data
        private string[,] _data =
        {
            {"f(x)", "abc", "C", "<-" }, 
            {"<", ">", "e", "pi" },
            { "(", ")", "^", "/"},
            { "7", "8", "9", "*"},
            { "4", "5", "6", "-" },
            { "1", "2", "3", "+" },
            { "0", ".", "+/-", "="}
        };
        //Main window needs to have refrence to all the other windows in
        //order to control them
        KeysWindow _keysWin = null; //Keys window
        FunctionWindow _functionWin = null; //Function window
        HistoryForm _historyWin = null; //Expression history window
        FunctionHistory _functionHistory; //User function history window
        HelpForm _helpWin = null;
        FixedTable _table;
        private int _tableOffset; //table offset
        public RichTextBox TextBox
        { //Getter and setter for the user input textbox
            get { return textBox; }
            set { textBox = value; }
        }
        public MainWindow()
        {
            //Initialize main window
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            //Define Fixed Table
            _table = new FixedTable(7, 4);
            Font temp = new Font("Tahoma", 13F);
            _tableOffset = this.textBox.Height + this.textBox.Location.Y;
            _table.SuspendLayout();
            _table.Location = new Point(this.Location.X, this.Location.Y + _tableOffset);
            _table.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - _tableOffset);
            this.Controls.Add(_table);
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 4; j++)
                    _table.Add(new Button() { Text = _data[i, j], Font = temp }, j, i);
            _table[0, 0].Click += OnClickFunc;
            _table[0, 1].Click += OnClickKeys;
            _table[0, 2].Click += OnClearClick;
            _table[0, 3].Click += OnBackClick;
            _table[1, 0].Click += OnCaretBack;
            _table[1, 1].Click += OnCaretForward;
            for (int i = 6; i <= 25; i++)
                _table[i / 4, i % 4].Click += OnCharClick;
            _table[6, 2].Click += OnNegateClick;
            _table[6, 3].Click += OnEqualClick;
            //Color table
            _table[6, 3].BackColor = Color.CornflowerBlue;
            _table[6, 3].ForeColor = Color.White;
            for (int i = 3; i < 7; i++)
                for (int j = 0; j < 3; j++)
                    _table[i, j].BackColor = SystemColors.Control;
            _table.ResumeLayout();
            textBox.ReadOnly= true;
            textBox.ForeColor = Color.Black;
            //Add handlers
            this.SizeChanged += ResizeMain;
            //Instances
            _functionHistory = FunctionHistory.Instance();
            _historyWin = HistoryForm.Instance();
        }
        public void OnCharClick(object sender, EventArgs e)
        {
            Control btn = sender as Control;
            int selectionStart = TextBox.SelectionStart;
            TextBox.SelectedText += btn.Text;
            TextBox.Select(selectionStart + btn.Text.Length, 0);
            TextBox.Focus();
        }
        //Ignore any try to select text from the TextBox
        private void OnSelectionChanged(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        //Bring up the Function Window if we press "f(x)" button
        private void OnClickFunc(object sender, EventArgs e)
        {
            _functionWin = FunctionWindow.Instance();
            if(_functionWin.Visible)
            { _functionWin.Hide(); return; }
            _functionWin.StartPosition = FormStartPosition.Manual;
            _functionWin.Location = new Point(this.Location.X - _functionWin.Width, this.Location.Y);
            _functionWin.Show();
            textBox.Focus();
        }
        //Bring up the Keys Window if we press "abc" button
        private void OnClickKeys(object sender, EventArgs e)
        {
            _keysWin = KeysWindow.Instance();
            if (_keysWin.Visible) 
                { _keysWin.Hide(); return; }
            _keysWin.StartPosition = FormStartPosition.Manual;
            _keysWin.Location = new Point(this.Location.X + this.Width, this.Location.Y);
            _keysWin.Show();
            textBox.Focus();
        }
        //Clear the text if we press "C" button
        private void OnClearClick(object sender, EventArgs e)
        {
            this.textBox.Text = "";
            textBox.Select(0, 0);
            textBox.Focus();
        }
        //Move back if we press "<-" button
        private void OnBackClick(object sender, EventArgs e)
        {
            if (textBox.Text.Length >= 1)
            {
                int charToRemove = this.textBox.SelectionStart - 1;
                this.textBox.Text = this.textBox.Text.Remove(charToRemove, 1);
                textBox.Select(charToRemove, 0);
                textBox.Focus();
            }
        }
        //Negate everything if we use "+/-" button
        private void OnNegateClick(object sender, EventArgs e)
        {
            string temp = "-(";
            temp += textBox.Text;
            temp += ")";
            this.textBox.Text = temp;
            textBox.Select(textBox.TextLength, 0);
            textBox.Focus();
        }
        //Handle input if we press the "=" button
        private void OnEqualClick(object sender, EventArgs e)
        {
            //Instanting an InputHandler with the user input text
            InputHandler handler = new InputHandler(textBox.Text);
            if (handler.ValidateInput() == true) //If the input is alright
            {
                Expression exp = handler.GetExpression(); //We get the expression
                if (exp.GetType() == typeof(Expression)) //If it isn't a function
                {
                    string textCopy = textBox.Text; //We hold a copy of the text
                    textBox.Text = exp.Parse(); //Parse the expression
                    if (textBox.Text != "Invalid input") //If the parsed expression is alright
                        _historyWin.Add(textCopy); //We add it to the history
                    textBox.Select(textBox.TextLength, 0); //We move the caret to the most right
                    return; //returning from this function
                }
                    _functionHistory = FunctionHistory.Instance();
                    _functionHistory.Add((Function)exp); //otherwise, we consider it a user function
                    textBox.Text = "";
                return; //and then we return 
            }
            textBox.Text = "Invalid input"; //Failed the validation from the get-go
        }
        //Method for moving the caret back
        private void OnCaretBack(object sender, EventArgs e)
        {
            if(textBox.SelectionStart != 0)
               textBox.Select(textBox.SelectionStart - 1, 0);
            textBox.Focus();
        }
        //Method for moving the caret forward
        private void OnCaretForward(object sender, EventArgs e)
        {
            if (textBox.SelectionStart != textBox.Text.Length)
                textBox.Select(textBox.SelectionStart + 1, 0);
            textBox.Focus();
        }
        //Dynamically change the size of the table when resizing the main form
        private void ResizeMain(object sender, EventArgs e)
        {
            _table.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - _tableOffset);
        }
        //Make the History Form appear when clicking on the history menu item
        private void HistoryToolStripMenuItemClick(object sender, EventArgs e)
        {
            _historyWin = HistoryForm.Instance();
            _historyWin.Show();
            textBox.Focus();
        }
        //Make the Function History Form appear when clicking on the functions menu item
        private void FunctionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            _functionHistory = FunctionHistory.Instance();
            _functionHistory.Show();
            textBox.Focus();
        }
        private void HelpToolStripMenuItemClick(object sender, EventArgs e)
        {
            _helpWin = new HelpForm();
            _helpWin.Show();
        }
    }
}
