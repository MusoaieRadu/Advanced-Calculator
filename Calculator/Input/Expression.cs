using Calculator.Forms;
using System;
using System.Collections.Generic;

namespace Calculator.Input
{
    //object that holds an expression and parses it when needed
    public class Expression : IParser
    {
        protected string _expression, _formatedExpression;
        Mathf _math; //math instance for math functions
        FunctionHistory _userFunctions; //user functions
        public string ExpressionValue { get { return _expression; } }
        public Expression(string expression) {
            _expression = expression;
            FormatExpression();
            _math = Mathf.Instance();
            _userFunctions = FunctionHistory.Instance();
        }
        //Makes it such that -f(..) becomes 0-f(...), -(...) becomes 0-(...)
        public void FormatExpression() {
            _formatedExpression = "";
            if (_expression[0] == '-') _formatedExpression += "0";
            _formatedExpression += _expression[0];
            for(int i = 1; i < _expression.Length; i++)
            {
                if (_expression[i] == '-')
                    if (!char.IsDigit(_expression[i-1]) && _expression[i-1] != ')')
                        _formatedExpression += "0";
                _formatedExpression += _expression[i];
            }
        }
        //Expression parser
        //Based on tweaked infix to postfix evaluation algorithm 
        public virtual string Parse()
        {
            StringUtility util = new StringUtility();
            //temp will hold "words"
            string temp = "", copy = _expression;
            _expression = _formatedExpression;
            //append at the end ")" 
            _expression += ")";
            Stack<double> values = new Stack<double>(); //stores the values in the expression
            Stack<char> operators = new Stack<char>();
            Stack<string> functions = new Stack<string>();
            int l = _expression.Length;
            operators.Push('('); //push "(" for the appended ")" in the operator stack
            double value1, value2; //operands for operation calls(eq: a + b)
            try //Put all the code for safety measures in a try catch(If any of the stack pop fails, wrong input)
            { 
              for (int i = 0; i < l; i++)
              {
                    if (char.IsLetter(_expression[i])) //If we have found a letter, we search a word
                    {
                        temp = util.GetWord(_expression, ref i); //Method to obtain the word
                        if (temp == "e") values.Push(Math.E); //if we find the constant e
                        else if (temp == "pi") values.Push(Math.PI); //if we find the constant pi
                        else functions.Push(temp); //otherwise, we assume it is a function and push it
                        temp = ""; //reset the word
                    }
                    else if (char.IsDigit(_expression[i])  || _expression[i] == '.') //We have found a digit or '.'
                    {
                        if (_expression[i] == '.') temp += "0"; //eg -> .1 will be the same as 0.1
                        temp += util.GetWord(_expression, ref i); //we search the number
                        values.Push(Convert.ToDouble(temp)); //We push it on the values stack
                        temp = ""; //reset word
                    }
                    if (_expression[i] == '(')
                    {
                        operators.Push('('); //We always push '('
                        if (i != 0)
                            if (!char.IsLetter(_expression[i - 1]))
                                functions.Push("");
                        //We push nothing on the function stack, since 
                        //before (, we don't have a letter(a function name
                    }
                    else if (_expression[i] == ')') //We start popping until we find an opening bracket
                    {
                        while (operators.Peek() != '(') //loop until opening bracket
                        {
                            value1 = values.Pop(); //Popping the first operand
                            value2 = values.Pop(); //Popping the second operand
                            values.Push(_math.OperatorCall(operators.Pop(), value2, value1));
                            //We apply the operator with the 2 operands and push the value
                        }
                        if (functions.Count != 0) //If we have a function as well, we have to apply it
                        {
                            if (functions.Peek() == "")
                                functions.Pop(); //Do nothing if we have no function to call yet
                            else
                            {
                                value1 = values.Pop(); //Popping the value we pushed
                                if (_math.Functions.ContainsKey(functions.Peek())) //If we have a math function
                                    values.Push(_math.FunctionCall(functions.Pop(), value1));
                                else if (_userFunctions.Find(functions.Peek())) //if we have a user defined function
                                    values.Push(_userFunctions.Call(functions.Pop(), value1));
                                else throw new InvalidOperationException();
                            }
                        }
                        operators.Pop(); //pop the operator -> '('
                    }
                    else //We have found an operand
                    {
                        int precTop = _math.PrecedenceCall(operators.Peek()); //Get the precedence of the top of the stack
                        int precCurrent = _math.PrecedenceCall(_expression[i]); //get the precedence of the operator
                                                                               //We will pop operators until we have found one with precedence lesser than the operator to be inserted
                        while (operators.Count != 0 && precCurrent <= precTop)
                        {
                            if (precCurrent == precTop && operators.Peek() == '^') break;//Right associativity
                            value1 = values.Pop();
                            value2 = values.Pop();
                            values.Push(_math.OperatorCall(operators.Pop(), value2, value1));
                            precTop = _math.PrecedenceCall(operators.Peek()); //getting next operator
                        }
                        operators.Push(_expression[i]); //push the operator after
                    }
            }
            while (operators.Count != 0) //After scanning the input, empty all the stacks
            {
                value1 = values.Pop(); //pop a value
                    if (functions.Count != 0) //if we have a function, apply it
                    {
                        //Checking if the function is defined
                        if (_math.Functions.ContainsKey(functions.Peek()))
                            values.Push(_math.FunctionCall(functions.Pop(), value1));
                        else if (_userFunctions.Find(functions.Peek()))
                            values.Push(_userFunctions.Call(functions.Pop(), value1));
                        else throw new InvalidOperationException();
                    }
                    else //we only have an operator, we apply it
                    {
                        value2 = values.Pop();
                        values.Push(_math.OperatorCall(operators.Pop(), value2, value1));
                    }
            }
            _expression = copy; //reutilize the initial string
            return Convert.ToString(values.Pop()); //return the result
            }
            catch(InvalidOperationException) //Exception for invalid input
            {
                return "Invalid input";
            }
        }
    }
}
