using System;
using System.Collections.Generic;
namespace Calculator.Input
{
    //class that allows functions(single input for now)
    //It evaluates input by using 2 expressions
    //The one which has all the parameters( eq : 3+b*x) <- _paramexp
    //And the one which has all parameters as number(eq : 3+0.1*2) <- _expression
    //It requires the _expression to be set to _paramexp before updating
    public class Function : Expression
    {
        private string _parametricExpression; //parameter expression(or function)
        public string ParameterExpression { get { return _parametricExpression; } }
        private Dictionary<string, double> _parameters; //the parameters of the function
        public Dictionary<string, double> Parameters{ //getter for the parameters
            get {  return _parameters; }
        }
        public Function(string expression, Dictionary<string, double> param) : base(expression)
        {
            _parameters = param;
            _parametricExpression = _expression;
        }
        public void AssignParameter(string name, double value) //method to assign values to parameters
        {
            _parameters[name] = value;
        }
        //sets _expression to _paramexp
        public void SetExpressionToParametericExpression()
        {
            _expression = _parametricExpression;
        }
        public void UpdateVariable(double x) //updates the function expression
        {
            string val;
            StringUtility util = new StringUtility();
            //Replaces the 'x' in the expression, with the parameter value
            for(int i = 0; i < _expression.Length; i++)
            {
                int start = i;
                val = util.GetWord(_expression, ref i);
                if(val == "x")
                {
                    if (x < 0)
                    {
                        val = "(";
                        val += x;
                        val += ")";
                    }
                    else val = Convert.ToString(x);
                    _expression = util.ReplaceFrom(_expression, start, i-1, val);
                }
            }
        }
        public void UpdateParameters()
        {
            string temp;
            StringUtility util = new StringUtility();
            foreach (var parameter in _parameters)
            {
                for(int i = 0; i < _expression.Length; i++)
                {
                    int start = i;
                    temp = util.GetWord(_expression, ref i);
                    if(temp == parameter.Key)
                    {
                        if (parameter.Value < 0)
                        {
                            temp = "(";
                            temp += parameter.Value;
                            temp += ")";
                        }
                        else temp = Convert.ToString(parameter.Value);
                        _expression = util.ReplaceFrom(_expression, start, i-1, temp);
                    }
                }
            }
        }
    }
}
