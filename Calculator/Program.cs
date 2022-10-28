using Interface;
using System.ComponentModel.DataAnnotations;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char op;
            double x, y, z;
            Window<double> window = new Window<double>();
            Math<double> math = new Math<double>();
            bool end = false;
            ConsoleKey? choice;
            List<string> history = new List<string>();
            
            window.Welcome();
            window.Border();
            while (!end)
            {
                choice = window.Menu(); //TODO chage for input
                switch (choice)
                {
                    case ConsoleKey.D1:
                        x = window.GetNumber("Enter the first number: ", "Only use numbers! Use , for decimals!");
                        op = window.GetOperator("Enter the operator: ", "Accepted inputs: + - / *");
                        y = window.GetNumber("Enter the seccond number: ", "Only use numbers!");
                        if(op == '/' && y == 0){
                            window.PrintResult("Can't divide by 0!", true);
                        }
                        else
                        {
                            switch (op)
                            {
                                case '+':
                                    z = math.Addition(x, y);
                                    break;
                                case '-':
                                    z = math.Subtraction(x, y);
                                    break;
                                case '*':
                                    z = math.Multiplication(x, y);
                                    break;
                                case '/':
                                    z = math.Division(x, y);
                                    break;
                                default:
                                    z = 0;
                                    break;
                            }
                            window.PrintResult(x + " " + op + " " + y + " = " + z, false);
                            history.Add(x + " " + op + " " + y + " = " + z);
                        }
                        window.ClearInside();
                        break;
                    case ConsoleKey.D2:
                        window.History(history);
                        break;
                    case ConsoleKey.D3:
                        window.Exit();
                        end = true;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}