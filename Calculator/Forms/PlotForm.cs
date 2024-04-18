using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator.Forms
{
    public partial class PlotForm : Form
    {
        private Graph _plotGraph;
        private int _offset = 20;
        public PlotForm(Graph plotGraph)
        {
            InitializeComponent();
            _plotGraph = plotGraph;
            _plotGraph.Size = new Size(Width-2*_offset, Height - _offset);
            _plotGraph.Location = new Point(_offset, 0);
            this.Controls.Add(plotGraph);
            this.SizeChanged += UpdateSize;
        }
        public void UpdateSize(object sender, EventArgs e) {
            _plotGraph.Size = new Size(Width - 2*_offset, Height - _offset);
        }
    }
}
