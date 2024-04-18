using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Calculator
{
    //Class that behaves like a Panel, but add the functionality
    //of resizing it's items dynamically
    public class VerticalList : Panel
    {
        private List<Control> _items; //list with the items of the list
        private int _index = 0; //the current item added index
        private int _threshold; //the maximum capacity of the list
        public int Count { get { return _index; } } //returns the number of elements
        public List<Control> Items { get { return _items; } }
        public VerticalList(int threshold)
        {
            _items = new List<Control>();
            this.AutoScroll = true; //enable autoscrolling
            _threshold = threshold;
        }
        public void Add(Control item)
        {
            this.SuspendLayout();
            if (_index >= _threshold) //remove the first element, if exceeding threshold
                Remove(_items[0]);
            item.Parent = this; //set the added item parent to this object
            item.Size = new Size(this.Width, item.Height);//set it's width to this object's width
            int offset = 0;
            for (int i = 0; i < _items.Count; i++)
                 offset += _items[i].Height;
            Point p = new Point(this.Left, this.Top + offset);
            //Loop through the existing items to find it's location in the list
            item.Location = p;
            _items.Add(item); //add the item in the list
            this.Controls.Add(_items[_index++]); //add this item to this object controls
            this.ResumeLayout();
        }
        public void Remove(Control obj)
        {
            this.SuspendLayout();
            this.Controls.Remove(obj); //remove from the object controls
            _items.Remove(obj); //remove from the items
            _index--; //decrease the count
            int offset = 0; 
            //reposition remaining items
            for(int i = 0; i < this.Controls.Count; i++) {
                Point p = new Point(this.Left, this.Top + offset);
                this.Controls[i].Location = p;
                offset += this.Controls[i].Height;
            }
            this.ResumeLayout();
        }
        //Indexer for easier access to an item
        public Control this[int i]
        {
            get { return _items[i]; }
        }
        //Overriding the OnResize method
        //In order to resize all elements as well 
        protected override void OnResize(EventArgs eventargs)
        {
            foreach (Control obj in _items)
                obj.Width = this.Width;
            base.OnResize(eventargs);
        }
    }
}
