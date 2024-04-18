using Calculator.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    class Pair<T, W>
    {
        public T X;
        public W Y;
        public Pair(T x, W y)
        {
            X = x;
            Y = y;
        }
        public Pair() { }
    }
    public partial class Graph : UserControl
    {
        private Point _downLeft, _upLeft, _downRight;
        private bool _drawing;
        private Pen _pen, _bluePen = new Pen(Color.Blue, 3);
        private double _xMax, _xMin, _yMax, _yMin;
        private Function _function;
        private List<Point> _plotPoints;
        private List<Pair<double, double>> _functionPoints;
        private int _leftOffset = 30, _topOffset = -70;
        public Graph(Function function, Pen drawPen, double xMin, double xMax)
        {
            _downLeft = new Point(_leftOffset, Height + _topOffset);
            _upLeft = new Point(_downLeft.X, 10);
            _downRight = new Point(Width - 10, _downLeft.Y);
            _plotPoints = new List<Point>();
            _functionPoints = new List<Pair<double, double>>();
            this.ResizeRedraw = true;
            this.DoubleBuffered = true;
            _function = function;
            _pen = drawPen;
            _function.SetExpressionToParametericExpression();
            _function.UpdateParameters();
            _xMin = xMin;
            _xMax = xMax;
            //Get yMin, yMax
            _yMin = Double.PositiveInfinity; _yMax = Double.NegativeInfinity;
            double x;
            for(double t = 0; t <= 1; t += 0.005) //Linearly interpolate
            {
                x = Lerp(xMin, xMax, t);
                function.SetExpressionToParametericExpression();
                function.UpdateParameters();
                function.UpdateVariable(x);
                function.FormatExpression();
                double y;
                if (double.TryParse(function.Parse(), out y) && y != Double.NaN && y != Double.PositiveInfinity && y != Double.NegativeInfinity)
                {
                    if (y < _yMin) _yMin = y;
                    if(y > _yMax) _yMax = y;
                }
            }
            if (_yMax == _yMin) { _yMin--; _yMax++; }
            InitializeComponent();
            GetPoints();
            ConvertPoints();
        }
        //Gets the function points
        public void GetPoints() {
            double x;
            for(double t = 0; t <= 1; t += 0.005)
            {
                x = Lerp(_xMin, _xMax, t);
                x = Math.Round(x, 3, MidpointRounding.AwayFromZero);
                _function.SetExpressionToParametericExpression();
                _function.UpdateParameters();
                _function.UpdateVariable(x);
                _function.FormatExpression();
                double y;
                if (double.TryParse(_function.Parse(), out y) && !double.IsInfinity(y) && !double.IsNaN(y))
                {
                    y = Math.Round(y, 3, MidpointRounding.AwayFromZero);
                    _functionPoints.Add(new Pair<double, double>(x, y));
                }
            }
        }
        //Converts the function points to screen Coordinates
        public void ConvertPoints()
        {
            _plotPoints.Clear();
            for(int i = 0;  i < _functionPoints.Count; i++)
            {
                int x_n = XNormalize(_functionPoints[i].X);
                int y_n = YNormalize(_functionPoints[i].Y);
                _plotPoints.Add(new Point(x_n, y_n));
            }
        }
        //Linearly interpolates between two values
        public double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }
        //Normalizes the x coordinate of the graph
        private int XNormalize(double x)
        {
            double frac = 1 / (_xMax - _xMin);
            double r = _downRight.X, l = _downLeft.X;
            return (int)(frac * ((r - l) * x + l * _xMax - r * _xMin));
        }
        //Normalizes the y coordinate of the graph
        private int  YNormalize(double y)
        {
            double frac = 1 / (_yMax - _yMin);
            double u = _upLeft.Y;
            double d = _downLeft.Y;
            return (int)(frac * ((u - d) * y + d * _yMax - u * _yMin));
        }
        //Overriding OnSizeChanged method
        protected override void OnSizeChanged(EventArgs e)
        {
            _downLeft = new Point(_leftOffset, Height + _topOffset);
            _upLeft = new Point(_downLeft.X, 10);
            _downRight = new Point(Width - 10, _downLeft.Y);
            _plotPoints.Clear();
            ConvertPoints();
            this.Invalidate();
            _drawing = true;
            base.OnSizeChanged(e);
        }
        //Overriding OnPain method
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!_drawing) return;
            Graphics g = e.Graphics;
            if (_plotPoints.Count > 0)
            {
                //Draw the function
                g.DrawCurve(_bluePen, _plotPoints.ToArray());
                //Draw the coordinate system
                g.DrawLine(_pen, _downLeft, _downRight);
                g.DrawLine(_pen, _upLeft, _downLeft);

                //Draw the x-Axis Ticks and labels
                Point bar1, bar2, textPoint;
                Brush textBrush = new SolidBrush(Color.Black);
                Font font = new Font("Arial", 10);
                int percentage = 5;
                for (double t = 0; t <= 1; t += 0.2)
                {
                    double x = Lerp(_xMin, _xMax, t);
                    int xNorm = XNormalize(x);
                    bar1 = new Point(xNorm, _downLeft.Y + percentage);
                    bar2 = new Point(xNorm, _downLeft.Y - percentage);
                    g.DrawLine(_pen, bar1, bar2);
                    textPoint = new Point(xNorm, _downLeft.Y - 4 * percentage);
                    g.DrawString(x.ToString("0.00"), font, textBrush, textPoint);
                }
                //Draw the y-Axis Ticks and labels
                for (double t = 0; t <= 1; t += 0.2)
                {
                    double y = Lerp(_yMin, _yMax, t);
                    int yNorm = YNormalize(y);
                    bar1 = new Point(_downLeft.X - percentage, yNorm);
                    bar2 = new Point(_downLeft.X + percentage, yNorm);
                    g.DrawLine(_pen, bar1, bar2);
                    textPoint = new Point(_downLeft.X - 6 * percentage, yNorm);
                    g.DrawString(y.ToString("0.00"), font, textBrush, textPoint);
                }
                _drawing = false;
            }
        }
    }
}
