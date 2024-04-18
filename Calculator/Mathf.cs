using System;
using System.Collections.Generic;

namespace Calculator
{
    //Operator class
    class Operator
    {
        public int Precedence;
        public Operator(int prec, Func<double, double, double> operation)
        {
            Precedence = prec;
            Operation = operation;
        }
        public Func<double, double, double> Operation;
    }
    //Singleton type class(only allows one instance)
    public class Mathf
    {
        private readonly Dictionary<string, Func<double, double>> _functions;
        public  Dictionary<string, Func<double, double>> Functions { get { return _functions; } }
        private Dictionary<char, Operator> _operators;
        private static Mathf _instance = null;
        public static Mathf Instance()
        {
            if(_instance == null)
            {
                _instance = new Mathf();
            }
            return _instance;
        }
        private Mathf()
        {
            _operators = new Dictionary<char, Operator>()
            {
                {'+', new Operator(1, (x, y)=>{return x + y; }) },
                {'-', new Operator(1, (x, y)=>{return x-y; }) },
                {'*', new Operator(2, (x, y)=>{return x*y; }) },
                {'/', new Operator(2, (x, y)=>{return x/y; }) },
                {'^', new Operator(3, (x, y)=>{return Math.Pow(x, y); }) },
                {'(', new Operator(0, (x, y)=>{return 0; }) } //Shouldn't be called when parsing
            };
            //Dictionary with most-known math functions(each string has assigned a delegate)
            _functions = new Dictionary<string, Func<double, double>>()
            {
                {"abs", (double x)=>{return Math.Abs(x); } },
                {"floor", (double x)=>{return Math.Floor(x); } },
                {"ceil", (double x)=>{return Math.Ceiling(x); } },
                {"sign", (double x)=>{return Math.Sign(x); } },
                {"frac", (double x)=>{return (x-Math.Floor(x)); } },
                {"exp", (double x)=>{return Math.Exp(x); } },
                {"ln", (double x)=>{return Math.Log(x); } },
                {"log", (double x)=>{return Math.Log10(x);} },
                {"sqrt", (double x)=>{return Math.Sqrt(x); } },
                {"cbrt", (double x)=>{return Cbrt(x); } },
                {"sin", (double x)=>{return Math.Sin(x); } },
                {"cos", (double x)=>{return Math.Cos(x); } },
                {"tan", (double x)=>{return Math.Tan(x); } },
                {"cot", (double x)=>{return 1/Math.Tan(x); } },
                {"sinh", (double x)=>{return Math.Sinh(x); } },
                {"cosh", (double x)=>{return Math.Cosh(x); } },
                {"tanh", (double x)=>{return Math.Tanh(x); } },
                {"coth", (double x)=>{return 1/Math.Tanh(x); } },
                {"asin", (double x)=>{return Math.Asin(x); } },
                {"acos", (double x)=>{return Math.Acos(x); } },
                {"atan", (double x)=>{return Math.Atan(x); } },
                {"acot", (double x)=>{return Math.Atan(1/x); } }
            };
        }
        public int PrecedenceCall(char op)
        {
            return _operators[op].Precedence;
        }
        public double OperatorCall(char operation, double x, double y)
        {
            return _operators[operation].Operation(x, y);
        }
        public double FunctionCall(string operation, double x)
        {
            return _functions[operation](x);
        }
        //Approximate cbrt(x) via Newton's Method
        public double Cbrt(double x)
        {
            double xn = 1;
            for (int i = 0; i < 20; i++)
            {
                xn = 0.66667 * xn + x / (3 * xn * xn);
            }
            return xn;
        }
     }
}
