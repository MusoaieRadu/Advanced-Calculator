using Calculator.Forms;
using System;
using System.Collections.Generic;

namespace Calculator.Input
{
    //Handles input entered by user
    class InputHandler
    {
        private string _input; //user input
        private Dictionary<string, double> _parameters; //custom parameters
        private int _length; //length of input
        private bool _isFunction = false; //checks if we have a function or not
        private Mathf _math; //pointer to Mathf class which contains all the math functions
        private FunctionHistory _functionHistory; //pointer to user defined custom functions
        public InputHandler(string input) {
         _input = input;
         _length = _input.Length;
         _math = Mathf.Instance();
         _parameters = new Dictionary<string, double>();
         _functionHistory = FunctionHistory.Instance();
        }
        public bool ValidateInput() //Verifies the given input, returns false for invalid data
        {
            StringUtility util = new StringUtility();
            string word; 
            for (int index = 0; index < _length; index++)
            {
                word = util.GetWord(_input, ref index); //gets word from position index
                if (word == "x") { _isFunction = true; continue; } //f(x)
                if (Double.TryParse(word, out double d)) { //we have found a number
                    if (index != _length)
                        if (_input[index] == '(') return false; // disallowed -> number(...
                    continue;
                }
                if (word == "e" || word == "pi") {
                    if (index != _length)
                        if (_input[index] == '(') return false; //disallowed -> e/pi(...)
                    continue;
                }
                if (_functionHistory.Find(word)) //we have found a user function
                    continue;
                if (!(_math.Functions.ContainsKey(word))){ //we have found a parameter
                    if (word != "") //if it isn't empty
                    {
                        if (!_parameters.ContainsKey(word)) //if we haven't found it already
                        {
                            _parameters.Add(word, 0f); //we add
                            if (index != _length)
                                if (_input[index] == '(') return false; //disallow -> param(...
                        }
                    }
                }
                else //we have found a math function otherwise
                {
                    if(index != _length)
                        if (_input[index] != '(') return false; //only allow -> function(...
                }
            }
            return true; 
        }
        public Expression GetExpression() //Returns using polymorphism the type of input entered
        {
            if(_parameters.Count == 0 && !_isFunction)
                return new Expression(_input);
            return new Function(_input, _parameters);
        }
    }
}
