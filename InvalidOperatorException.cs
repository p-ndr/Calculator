using System;

namespace Calculator
{
    public class InvalidOperatorException : Exception
    {
        public override string Message
        {
            get
            {
                return "Invalid Operator!";
            }
        }
    }
}