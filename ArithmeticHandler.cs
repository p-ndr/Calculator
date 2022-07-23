using System;
using System.IO;
using System.Diagnostics;

namespace Calculator
{
    public class ArithmeticHandler
    {
        /*
         * This class evaluates a postfix string of operations and numbers.
         * Support for multidigit and floats is available.
         * 
         * Fields:
         * CustomParser parser: For retrieving parsers (Shunting Yard in this case)
         * A stack of doubles to handle the operands
         * A string holding the directory of history files.
         * 
         * Psuedocode and some ideas:
         * https://www.geeksforgeeks.org/stack-set-4-evaluation-postfix-expression/
         * https://github.com/rachelvansciver/ShuntingYardCalculator/blob/main/Arithmetic.java
         * 
         */

        private CustomParser parser;
        private CustomStack<double> operands;
        private string historyAddress = @"C:\\Users\\Parnian\\source\\repos\\Calculator\\Histories\\hist.txt";

        public ArithmeticHandler()
        {
            this.parser = new CustomParser();
            this.operands = new CustomStack<double>();
        }

        public void evaluatePostfixString(string originalInfix, string infix)
        {

            /*
             * This method handles the evaluation of a postfix expression, in Reversed Polish Notation.
             * Throws InvalidOperatorException() if operation is invalid.
             * 
             * @params: string infix (to be turned into postfix later)
             * @return: none - only writes the result to the history file.
             * 
             */

            bool isDecimal = false; // to check whether we have a floating point number
            bool isNegative = false; // to check whether we have a negative number
            double divisor = 0.1D; // divisor to put digits in their right places according to their value
            double a = 0;
            double b = 0;
            char operand = ' ';
            string postfix = parser.parseWithShuntingYard(infix);

            try
            {
                for (int i = 0; i < postfix.Length; i++)
                {
                    operand = postfix[i];

                    if (operand == '_') // go one step forward, we have a negative number.
                    {
                        isNegative = true;
                        continue;
                    }
                    if (operand == ' ') // skipping the space between characters.
                    {
                        continue;
                    }
                    else if (Char.IsDigit(operand) || operand == '.' || operand == '_') // extracting operands.
                    {
                        a = 0;
                        isDecimal = false;
                        divisor = 1;
                        while (Char.IsDigit(operand) || operand == '.' || operand == '_')
                        {
                            if (operand == '.') // checking for floating point
                            {
                                isDecimal = true;
                            }
                            else if (isDecimal) // retrieving the floating point
                            {
                                a += ((double)(operand - '0') / (divisor * 10));
                                divisor *= 10;
                            }
                            else // retrieving the integer
                            {
                                a = a * 10 + (double)(operand - '0');
                            }
                            i++; // goes forward in the string to find the consecutive digits... and...
                            operand = postfix[i];
                        }

                        if (isNegative)
                        {
                            a *= -1;
                            isNegative = false;
                        }
                        i--; // (cont. from above) ...takes a step back as it has reached the end of the number.
                        operands.push(a);
                    }
                    else
                    {
                        // Reached an operator. Start calculating.
                        a = operands.pop();
                        b = operands.pop();

                        switch (operand)
                        {
                            case '+':
                                operands.push(b + a);
                                break;

                            case '-':
                                operands.push(b - a);
                                break;

                            case '*':
                                operands.push(b * a);
                                break;

                            case '/':
                                Debug.Assert(a != 0);
                                operands.push(b / a);
                                break;

                            case '^':
                                // we should check whether a and b are simultaneously zero or not.
                                operands.push((double)Math.Pow(b, a));
                                break;

                            default:
                                Console.WriteLine("Wait, what?");
                                break;
                        }
                    }
                }
                saveToFile(originalInfix + " = " + operands.pop()); // save to file. Note that the original infix - without inspection - is passed.
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught:\n" + e.Message);
                System.Environment.Exit(-1);
            }
        }

        private void saveToFile(string str)
        {
            /*
             * Writes an string to the file, with time and date.
             * 
             * @param string
             * 
             */
            if (File.Exists(historyAddress)){ // if the file exists, append results

                using (StreamWriter sw = File.AppendText(historyAddress))
                {
                    string line = DateTime.Now.ToString() + ": " + str;
                    sw.WriteLine(line);
                }
            }
            else // if not, create the file.
            {
                using (StreamWriter sw = File.CreateText(historyAddress))
                {
                    string line = DateTime.Now.ToString() + ": " + str;
                    sw.WriteLine(line);
                }
            }
        }

        public void openHistoryFile()
        {
            /*
             * Opens the history file with respective software.
             * 
             * @param none
             * @return void
             * 
             */

            try
            {
                Process.Start("Notepad.exe", historyAddress);
            } 
            catch(Exception e) when (e is ObjectDisposedException || e is System.ComponentModel.Win32Exception || e is FileNotFoundException)
            {
                Console.WriteLine("We have a problem:\n" + e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }

        public void clearHistory()
        {
            /* clearing file history*/

            File.WriteAllText(historyAddress, string.Empty);
        }
    }
}