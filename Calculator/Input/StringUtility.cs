using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Input
{
    public class StringUtility
    {
        public StringUtility() { }
        public string GetWord(string src, ref int index)
        {
            string temp = "", dels = "+-/*^()"; //Keeping the word in a variable
            for (; index < src.Length; index++)
            {
                if (dels.IndexOf(src[index]) == -1)
                    temp += src[index]; //appending the character if itsn't a delimiter
                else break;
            }
            return temp;
        }
        public string ReplaceFrom(string src, int start, int end, string newstr)
        {
            string temp = "";
            if (start > end)
                return temp;
            if (start >= 0)
            {
                for (int i = 0; i < start; i++)
                    temp += src[i];
            }
            for (int j = 0; j < newstr.Length; j++)
                temp += newstr[j];
            for (int i = end + 1; i < src.Length; i++)
                temp += src[i];
            return temp;
        }
    }
}
