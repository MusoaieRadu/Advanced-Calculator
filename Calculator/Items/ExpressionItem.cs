using System.Windows.Forms;

namespace Calculator
{
    using Calculator.Forms;
    using Input;
    using System;
    using System.Drawing;

    //User Control that "formats" user defined functions
    public partial class ExpressionItem : UserControl
    {
        private Function _function; //coresponding function refrence
        public Button DeleteButton { get { return btnDel; }  } //getter for Delete Button
        public string Id { get { return id_name.Text; } } //getter for Id
        public ExpressionItem(Function function, string id)
        {
            //this
            InitializeComponent();
            this.SizeChanged += ResizeItem;
            //Function
            _function = function;
            //Parameter list(ComboBox)
            foreach (string D in _function.Parameters.Keys)
                this.paramList.Items.Add(D);
            if (paramList.Items.Count != 0) //We select the first parameter added if we have one at least
                this.paramList.SelectedIndex = 0;
            //Id_name
            this.id_name.Text = id;
            //Function text
            this.functionText.Location = new System.Drawing.Point(id_name.Left + id_name.Size.Width + 20, functionText.Top);
            this.functionText.Text = _function.ParameterExpression;
            //Value Textbox event handlers
            this.valueBox.LostFocus += ValueBoxOufOfFocus;
            this.valueBox.KeyPress += OnKeyboardPress;
            //Parameter List event handler
            this.paramList.SelectedIndexChanged += ParametersSelectionChanged;
            //XMinBox-XMaxBox event handlers
            this.xMinBox.KeyPress += OnKeyboardPress;
            this.xMaxBox.KeyPress += OnKeyboardPress;
            //Update the parameters 
            _function.UpdateParameters();
            //Set the "real" function to the expression of the function
            this.realText.Text = _function.ExpressionValue;
            //Set the button plot click method
            this.btnPlot.Click += InstantiatePlot;
        }

        //Get the corresponding value for the parameter when changing selections
        private void ParametersSelectionChanged(object sender, System.EventArgs e)
        {
            this.valueBox.Text = _function.Parameters[(string)paramList.SelectedItem].ToString();
        }
        //Method to allow only for numbers to be entered and '-', '.'
        private void OnKeyboardPress(object sender, KeyPressEventArgs e)
        {
            char ch = Convert.ToChar(e.KeyChar);
            if (!char.IsDigit(ch) && !char.IsControl(ch) && (ch != '.') && (ch != '-'))
                e.Handled = true;
            if (ch == '-' && ((TextBox)sender).TextLength != 0)
                e.Handled = true;
            else if (ch == '.' && ((TextBox)sender).Text.IndexOf('.') != -1)
                e.Handled = true;
        }
        //If we have an empty valuebox and we leave the box, we set it to "0"
        //Then we update the "real" function
        private void ValueBoxOufOfFocus(object sender, EventArgs e)
        {
            if (valueBox.Text == "")
                valueBox.Text = "0";
            if (paramList.Items.Count > 0)
            {
                _function.AssignParameter((string)paramList.SelectedItem, Convert.ToDouble(valueBox.Text));
                _function.SetExpressionToParametericExpression();
                _function.UpdateParameters();
                this.realText.Text = _function.ExpressionValue;
            }
        }
        //Method that is responsible for dynamically resizing the item
        private void ResizeItem(object sender, EventArgs e)
        {
            int offset = 20;
            functionText.Width = this.Width - id_name.Width - offset;
            functionText.Left = id_name.Left + id_name.Width + offset/5;
            realText.Width = this.Width - result.Width - offset;
            realText.Left = result.Left + result.Width + offset/5;
            btnDel.Location = new System.Drawing.Point(this.Width - btnDel.Width - offset, btnDel.Top);
            btnPlot.Location = new System.Drawing.Point(this.Width - btnDel.Width - btnPlot.Width - offset, btnPlot.Top);
            xMaxBox.Location = new Point(btnPlot.Left - xMaxBox.Width - offset, xMaxBox.Top);
            xMaxLabel.Location = new Point(xMaxBox.Left, xMaxLabel.Top);
            xMinBox.Location = new Point(xMaxBox.Left - xMinBox.Width - offset, xMinBox.Top);
            xMinLabel.Location = new Point(xMinBox.Left, xMinLabel.Top);
        }
        //Method that instantiates a plot form for the function
          private void InstantiatePlot(object sender, EventArgs e)
        {
            Pen drawPen = new Pen(Color.Black, 3);
            double xMin, xMax;
            if (xMinBox.Text != "" && xMaxBox.Text != "")
            {
                xMin = Double.Parse(xMinBox.Text);
                xMax = Double.Parse(xMaxBox.Text);
                if (xMin < xMax)
                {
                    Graph plotGraph = new Graph(this._function, drawPen, xMin, xMax);
                    PlotForm plot = new PlotForm(plotGraph);
                    plot.Show();
                }
            }
        }
    }
}