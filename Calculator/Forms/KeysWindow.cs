using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{

    public partial class KeysWindow : Form
    {
        //The table with it's data
        FixedTable _table;
        string[,] data =
            {
            { "q", "w", "e" },
            { "r", "t", "y" },
            { "u", "i", "o" },
            { "p" , "a", "s" },
            { "d", "f", "g" },
            { "h", "j", "k" },
            { "l" ,"z", "x" },
            { "c", "v", "b" },
            { "n", "m", "" }
            };
        private static KeysWindow _instance = null;
        public static KeysWindow Instance()
        {
            if (_instance == null)
            {
                _instance = new KeysWindow();
                _instance.FormClosed += delegate { _instance = null; };
            }
            return _instance;
        }
        private KeysWindow()
        {
            //Initiate this window
            InitializeComponent();
            //Get a refrence of the OnCharClick event handler
            MainWindow main = Program.MainWindow;
            //Create table
            Font temp = new Font("Tahoma", 13F);
            _table = new FixedTable(9, 3);
            this.Controls.Add(_table);
            _table.Location = new Point(0, 0);
            _table.Size = this.ClientSize;
            for (int i = 0; i < _table.RowCount - 1; i++)
                for (int j = 0; j < _table.ColumnCount; j++)
                {
                    _table.Add(new Button() { Text = data[i, j], Font = temp }, j, i);
                    _table[i, j].Click += main.OnCharClick;
                }
            _table.Add(new Button() { Text = data[8, 0] , Font = temp}, 0, 8);
            _table[8, 0].Click += main.OnCharClick;
            _table.Add(new Button() { Text = data[8, 1] , Font = temp}, 1, 8);
            _table[8, 1].Click += main.OnCharClick;
            this.SizeChanged += ResizeForm;
        }
        //Resizing the table when resizing the form
        private void ResizeForm(object sender, EventArgs e)
        {
            _table.Size = ClientSize;
        }
    }
}
