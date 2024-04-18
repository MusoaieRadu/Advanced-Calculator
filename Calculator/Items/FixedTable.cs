using System;
using System.Windows.Forms;

namespace Calculator
{
    //Inherits from TableLayoutPanel
    //Since it is like it, but with dynamic properties
    public class FixedTable : TableLayoutPanel
    {
        //Set the RowCount and ColumnCount of the FixedTable
        public FixedTable(int row, int column)
        {
            //only allow rows and columns bigger than 0
            if (column <= 0) column = 1;
            if(row <= 0) row = 1;
            ColumnCount = column;
            RowCount = row;
            for (int i = 0; i < row; i++) //Adding rows evenly spaced
                RowStyles.Add(new RowStyle(SizeType.Percent, 100F / RowCount));
            for (int j = 0; j < column; j++) //Adding columns evenly spaced
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / ColumnCount));
        }
        //Method that add at [row, column] the Control obj
        public void Add(Control obj, int column, int row) {
                obj.Parent = this; //Set the obj parent to the table
                obj.Size = new System.Drawing.Size(Size.Width / ColumnCount, Size.Height / RowCount);
                //dynamically adjust the size
                this.Controls.Add(obj, column, row);
                //add to this controls the Control obj
        }
        //Indexer to retrieve object faster based on row and column
        public Control this[int i, int j]
        {
            get { return this.Controls[ColumnCount * i + j]; }
        }
        //Update the size of every object when resizing the table
        protected override void OnResize(EventArgs eventargs)
        {
            int width = this.Width / this.ColumnCount;
            int height = this.Height / this.RowCount;
            this.SuspendLayout();
            foreach(Control obj in this.Controls)
                obj.Size = new System.Drawing.Size(width, height);
            this.ResumeLayout();
            base.OnResize(eventargs);
        }
    }
}
