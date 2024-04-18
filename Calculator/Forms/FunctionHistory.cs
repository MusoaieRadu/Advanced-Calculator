using Calculator.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator.Forms
{
    public partial class FunctionHistory : Form
    {
        //The list of user function
        private VerticalList _userList = null;
        //default function names
        private string[] _functionName = { "f", "g", "h", "p", "q", "r", "u", "v", "w" };
        private bool[] _usedNames;
        //maximum number of functions
        private int _maxFunctions = 9;
        //dictionary with all the user defined functions based on the function name
        private Dictionary<string, Function> _userFunctions;
        //list offset
        private int _offset = 25;
        //Singleton
        private static FunctionHistory _instance = null;
        public static FunctionHistory Instance()
        {
            if (_instance == null)
                _instance = new FunctionHistory();
            return _instance;
        }
        private FunctionHistory()
        {
            InitializeComponent();
            //list
            _userList = new VerticalList(_maxFunctions);
            _userList.Location = new System.Drawing.Point(2, 0);
            _userList.Size = new Size(this.Width - _offset, this.Height);
            //dictionary
            _userFunctions = new Dictionary<string, Function>();
            //this
            this.Controls.Add(_userList);
            this.SizeChanged += FormResize;
            _usedNames = new bool[_maxFunctions];
            for (int i = 0; i < _maxFunctions; i++) _usedNames[i] = false;
        }
        //Checks if function "name" exists in the function list
        public bool Find(string name)
        {
            for (int i = 0; i < _maxFunctions; i++)
                if (_usedNames[i])
                  if (name == _functionName[i])
                    return true;
            return false;           
        }
        //Adds a "Function"
        public void Add(Function function)
        {
            //Find the name of the function first
            int index = 0;
            for (index = 0; index < _maxFunctions && _usedNames[index] != false; index++);
            if (index < _maxFunctions)
            {
                _userFunctions.Add(_functionName[index], function);
                _usedNames[index] = true;
                //Instantiate ExpressionItem with the function parameter
                ExpressionItem item = new ExpressionItem(function, _functionName[index]);
                //Add to it's delete button the RemoveItem method on click
                item.DeleteButton.Click += RemoveItem;
                //add item to the list and the user functions list
                _userList.Add(item);
            }
        }
        //Removes a "Function"
        private void RemoveItem(object sender, EventArgs e)
        {
            //Gets the parent of the button pressed from the ExpressedItem object
            ExpressionItem parent = (ExpressionItem)((Control)sender).Parent;
            //deletes the used name for the function
            //removes it from the list and user functions
            int index = _userList.Items.IndexOf(parent);
            _usedNames[index] = false;
            _userList.Remove(parent);
            _userFunctions.Remove(parent.Id);
            //then disposes it
            parent.Dispose();
            
        }
        //Calls the function "name" with the variable "x"
        public double Call(string name, double x)
        {
            //If the searched user function exists
            if (_userFunctions.ContainsKey(name))
            {
                //Clear the expression to the parametric one
                _userFunctions[name].SetExpressionToParametericExpression();
                //then updates the parameters
                _userFunctions[name].UpdateParameters();
                //then updates the variable
                _userFunctions[name].UpdateVariable(x);
                _userFunctions[name].FormatExpression();
                //the it parses the input and returns it
                return System.Convert.ToDouble((_userFunctions[name].Parse()));
            }//Otherwise, return NaN
            return Double.NaN;
        }
        //Don't exit this window when pressing exit, just hide it(would lose info otherwise)
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
        //Dynamically resize the list when resizing this form
        private void FormResize(object sender, EventArgs e)
        {
            _userList.Size = new Size(this.Width - _offset, this.Height);
        }
    }
}
