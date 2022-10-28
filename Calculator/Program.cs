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
            
            //Startar programmet med ett välkomst medelande
            window.Welcome();
            //Välkomst medelandet tas bort och ersätts av en ram
            window.Border();
            //En loop som körs till användaren vill avsluta programmet
            while (!end)
            {
                //Skriver ut menyn och väntar på svar från användaren
                choice = window.Menu();
                switch (choice)
                {
                    //Om användaren tryckte 1 kommer användaren få mata in tal och operator för att göra en uträkning
                    case ConsoleKey.D1:
                        // Ber om ett nummer och sickar med en förklaring som visas vid felaktig inmatning
                        x = window.GetNumber("Enter the first number: ", "Only use numbers! Use , for decimals!");
                        // Ber om en matematisk operator och sickar med en förklaring som visas vid felaktig inmatning
                        op = window.GetOperator("Enter the operator: ", "Accepted inputs: + - / *");
                        // Ber om ett nummer och sickar med en förklaring som visas vid felaktig inmatning
                        y = window.GetNumber("Enter the seccond number: ", "Only use numbers! Use , for decimals!");
                        //kollar om användaren försöker dela med 0 och skriver då ut att det inte går i röd text
                        if(op == '/' && y == 0){
                            window.PrintResult("Can't divide by 0!", true);
                        }
                        //om användaren inte försöker dela med 0 görs matematiken beroend på operand
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
                            //skriver ut resultatet i grön text
                            window.PrintResult(x + " " + op + " " + y + " = " + z);
                            //sparar resultatet så att det kan visas senare
                            history.Add(x + " " + op + " " + y + " = " + z);
                        }
                        window.ClearInside();
                        break;
                    //Om användaren tryckte 2 visas tidigare resultat
                    case ConsoleKey.D2:
                        window.History(history);
                        break;
                    //Om användaren tryckte 3 avslutas programmet
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