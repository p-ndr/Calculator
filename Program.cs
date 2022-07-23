using System;
using System.Text.RegularExpressions;
using System.Text;

namespace Calculator
{
    class Program
    {
        static string Inspect(string exp)
        {

            /*
             * This method receives user input and prepares it for parsing and evaluation.
             * If the string has the character '|', meaning the operands and operators are separated,
             * this method orders it into a proper mathematical expression (It relies a little bit on the
             * user - he/she should not separate negative sign of a number or the parantheses from the operand, or 
             * he/she should separate the other entities with space),and then, before passing it to the Arithmetic handler,
             * the negative numbers are flagged with flagForNegative().
             * 
             * If it does not contain the character mentioned, its negative numbers get flagged, and the result is
             * returned.
             * 
             * @param string exp
             * @return result as a string.
             * @throw IndexOutOfRangeException
             * 
             */
            try
            {
                if (!exp.Contains('|'))
                {
                    return flagForNegative(exp);
                }
                else
                {
                    string[] separated = exp.Split('|');
                    string[] operands = separated[0].Split(' ');
                    string[] operators = separated[1].Split(' ');
                    StringBuilder sb = new StringBuilder(operands[0]);
                    int i;
                    for (i = 0; i < operators.Length; i++)
                    {
                        sb.Append(" " + operators[i] + " " + operands[i + 1]);
                    }
                    if (i - 1 < operators.Length - 1 || i > operators.Length)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else
                    {
                        return sb.ToString();
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        static string flagForNegative(string exp)
        {
            /*
             * This method receives a mathematical expression, searches for negative numbers, and flags the
             * negative signs with '_' so that the parser won't mistake them for an operator.
             * 
             * @params string exp
             * @returns result as a string.
             * 
             */

            StringBuilder sb = new StringBuilder(exp);
            Regex rx = new Regex(@"-\d"); // search for -1, -2, ... \d is for decimal character.
            MatchCollection mc = rx.Matches(exp);

            foreach (Match m in mc)
            {
                sb.Replace(sb[m.Index], '_');
            }
            return sb.ToString();
        }
        static void Main(string[] args)
        {

            string startMsg = "Enter input in one of the following forms:\n1. Expression (e. g. -3 * 4 + 2)\n" +
                "2. Operands|Operators (e. g. 2 3 4|+ *)\nOr you can type 'finish' to close the program.";

            ArithmeticHandler handler = new ArithmeticHandler();


            while (true)
            {
                Console.WriteLine(startMsg);
                string ins = Console.ReadLine();

                if (ins.ToLower().Equals("finish"))
                {
                    Console.WriteLine("\nGoodbye!");
                    break;
                }

                string inspected = Inspect(ins);

                if (inspected.Length <= 0)
                {
                    Console.WriteLine("Inspection problem");
                    Environment.Exit(-1);
                }

                if (ins.Contains('|')) // for more orderly output, if operands and operators are separated, the inspected string is passed to write in the file.
                {
                    handler.evaluatePostfixString(inspected, flagForNegative(inspected));
                }
                else
                {
                    handler.evaluatePostfixString(ins, inspected);
                }
                Console.WriteLine();
                Console.WriteLine($"\nResult saved to hist.txt, Now what?\n1. Type 'history' to open the text file" +
                    "\n2. Type 'finish' for ending the program" + "\n3. Type 'clear' to clear the console and start over.");

                string command = Console.ReadLine();

                if (command.ToLower().Equals("history"))
                {
                    handler.openHistoryFile();
                    Console.WriteLine("Wanna clear history?(Y/N)");
                    string answer = Console.ReadLine();

                    if (answer.ToLower().Equals("y"))
                    {
                        handler.clearHistory();
                        handler.openHistoryFile();
                    }
                    Console.Clear();
                }
                else if (command.ToLower().Equals("finish"))
                {
                    Console.WriteLine("\nSo you are about to leave. Wanna clear history?(Y/N)");
                    string answer = Console.ReadLine();

                    if (answer.ToLower().Equals("y"))
                    {
                        handler.clearHistory();
                        handler.openHistoryFile();
                    }

                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                {
                    Console.Clear();
                }
            }
        }
    }
}
