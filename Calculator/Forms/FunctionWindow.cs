using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FunctionWindow : Form
    {
        //Table with it's data
        private FixedTable _functionTable;
        string[,] data =
        {
            {"floor", "frac", "ceil", "sign" },
            {"exp", "ln", "log", "cbrt" },
            {"sin", "cos", "tan", "cot" },
            {"sinh", "cosh", "tanh", "coth" },
            {"asin", "acos", "atan", "acot" },
            {"sqrt", "abs", "", ""}
        };
        //Singleton
        private static FunctionWindow _instance = null;
        public static FunctionWindow Instance()
        {
            if(_instance == null)
            {
                _instance = new FunctionWindow();
                _instance.FormClosed += delegate { _instance = null; };
            }
            return _instance;
        }
        private FunctionWindow()
        {
            //Initiate this window
            InitializeComponent();
            //Get a refrence of the onCharClick event handler
            MainWindow main = Program.MainWindow;
            //Create table
            Font temp = new Font("Tahoma", 13F);
            _functionTable = new FixedTable(data.GetLength(0), data.GetLength(1));
            _functionTable.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            _functionTable.Location = new Point(0, 0);
            for (int i = 0; i < _functionTable.RowCount - 1; i++)
                for (int j = 0; j < _functionTable.ColumnCount; j++)
                {
                    _functionTable.Add(new Button() { Text = data[i, j], Font = temp }, j, i);
                    _functionTable[i, j].Click += main.OnCharClick;
                }
            _functionTable.Add(new Button() { Text = data[5, 0], Font = temp }, 0, 5);
            _functionTable.Add(new Button() { Text = data[5, 1], Font = temp }, 1, 5);
            _functionTable[5, 0].Click += main.OnCharClick;
            _functionTable[5, 1].Click += main.OnCharClick;
            this.Controls.Add(_functionTable);
            this.SizeChanged += ResizeWindow;
        }
        //Dynamically adjust the table size when resizing this form
        private void ResizeWindow(object sender, EventArgs e)
        {
            _functionTable.Size = ClientSize;
        }
    }
}
