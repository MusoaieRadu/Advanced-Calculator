using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class HistoryForm : Form
    {
        //List_offset - the list offset from the form
        //MaxSize - maximum number of elements allowed in the list
        private int _listOffset = 20;
        private int _maxSize = 10;
        private VerticalList _list;
        public VerticalList List { get { return _list; } }
        //Singleton
        private static HistoryForm _instance = null;
        public static HistoryForm Instance()
        {
            if (_instance == null)
            {
                _instance = new HistoryForm();
            }
            return _instance;
        }
        private HistoryForm()
        {
            InitializeComponent();
            //list
            _list = new VerticalList(_maxSize);
            _list.Location = new Point(0, 0);
            _list.Size = new Size(this.Width-_listOffset, this.Height);
            //form
            this.Controls.Add(_list);
            this.SizeChanged += SizeChangeHandler;
        }
        //Add a historyItem in the list with the parameter text as it's text
        //And add to it's btnDel the delete method on the click event
        public void Add(string text)
        {
            HistoryItem item = new HistoryItem(text);
            item.DeleteButton.Click += Delete;
            _list.Add(item);
        }
        //Deletes the corresponding object(HistoryItem) when the user presses it's
        //btnDel button(Delete Button)
        private void Delete(object sender, EventArgs e)
        {
            HistoryItem item = (HistoryItem)(((Control)sender).Parent);
            _list.Remove(item);
            item.Dispose();
        }
        //Don't delete this form when pressing the exit button(would lose info), just hide it
        //Resize the list dynamically when resizing this form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
        private void SizeChangeHandler(object sender, EventArgs e)
        {
            _list.Width = Width - _listOffset;
        }
    }
}
