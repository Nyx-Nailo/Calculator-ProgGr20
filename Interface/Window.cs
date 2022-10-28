namespace Interface
{
    public class Window<T>
    {
        int width;
        int height;
        int borderStartW;
        int borderStartH;
        int borderWidth;
        int borderHeight;

        int currentWriteLine;
        int currentWriteCol;
        int startLine;
        int startCol;

        ConsoleColor textColor;
        ConsoleColor backgroundColor;
        Input<T> input;
        //Window konstruktor
        public Window(int Width = 80, int Height = 17, int bWidth = 70)
        {
            this.width = Width;
            this.height = Height;
            borderWidth = bWidth;
            borderStartH = 2;
            textColor = ConsoleColor.White;
            backgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(width, height);
            Console.BufferWidth = Width;
            Console.BufferHeight = Height;
            Console.CursorVisible = false;
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = textColor;
            borderStartW = (Console.WindowWidth - borderWidth) / 2;
            startLine = 4;
            startCol = borderStartW + 8;
            input = new Input<T>();
        }
        // Välkomst medelande.
        public void Welcome()
        {
            String[] txt = {
                @"  ______ __         ____ _               __        ",
                @" /_  __// /  ___   / __/(_)__ _   ___   / /___     ",
                @"  / /  / _ \/ -_) _\ \ / //  ' \ / _ \ / // -_)    ",
                @" /_/__/_//_/\__/_/___//_//_/_/_// .__//_/ \__/     ",
                @" / ___/___ _ / /____ __ __ / //_/ _ / /_ ___   ____",
                @"/ /__ / _ `// // __// // // // _ `// __// _ \ / __/",
                @"\___/ \_,_//_/ \__/ \_,_//_/ \_,_/ \__/ \___//_/   "};
            for(int i = 0; i < txt.Length; i++)
            {
                Console.SetCursorPosition((width - txt[i].Length) / 2, 4 + i);
                Console.WriteLine(txt[i]);
            }
            Console.SetCursorPosition(0, 12);
            for(int i = 0; i < width + 1; i++)
            {
                Console.Write("-");
                System.Threading.Thread.Sleep(40);
            }
            Console.Clear();
        }
        // Skriver ut en meny och väntar på knapp tryck
        public ConsoleKey Menu()
        {
            ConsoleKey choice;
            Console.SetCursorPosition(borderStartW + borderWidth / 2 - 2, startLine);
            Console.WriteLine("Menu");
            Console.SetCursorPosition(startCol, 6);
            Console.WriteLine("1. Make Calculations");
            Console.SetCursorPosition(startCol, 7);
            Console.WriteLine("2. Show Previous Results");
            Console.SetCursorPosition(startCol, 8);
            Console.WriteLine("3. Exit");
            Console.SetCursorPosition(startCol, 10);
            Console.WriteLine("Press the number corresponding to what you want to do.");
            choice = Console.ReadKey(true).Key;
            this.ClearInside();

            currentWriteLine = startLine;
            currentWriteCol = startCol;
            Console.SetCursorPosition(startCol, startLine);
            return choice;
        }
        // Skriver ut en lista av tidigare resultat
        public void History(List<string> History)
        {
            int i = History.Count;
            Console.Clear();
            Border(i + 8);
            Console.SetCursorPosition(borderStartW + borderWidth / 2 - 4, startLine);
            Console.WriteLine("History");
            currentWriteLine = startLine + 2;
            for (int j = 0; j < i; j++)
            {
                Console.SetCursorPosition(startCol, currentWriteLine + j);
                Console.Write(History[j]);
            }
            Console.SetCursorPosition(startCol, currentWriteLine + History.Count + 1);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
            Console.Clear();
            Border();
        }
        public void Exit()
        {
            Console.Clear();
            string txt = "Thank you for using this simple calculator.";
            Console.SetCursorPosition((width - txt.Length) / 2, 7);
            Console.WriteLine(txt);
            txt = "Press any key to exit.";
            Console.SetCursorPosition((width - txt.Length) / 2, 8);
            Console.WriteLine(txt);
            Console.ForegroundColor = ConsoleColor.Black;
        }
        // Tar input från användaren och kontrollerar att det är ett nummer
        public T GetNumber(string Text, string Error)
        {
            currentWriteCol = startCol;
            Console.SetCursorPosition(startCol, currentWriteLine);
            Console.WriteLine(Text);
            currentWriteCol += Text.Length;
            T number;
            while (true)
            {
                Console.SetCursorPosition(currentWriteCol, currentWriteLine);
                try
                {
                    number = input.ConsoleInputT();
                    ClearError();
                    currentWriteLine++;
                    currentWriteCol = startCol;
                    return number;
                }
                catch(Exception e)
                {
                    ErrorMsg(Error);
                    Console.SetCursorPosition(currentWriteCol, currentWriteLine);
                    for (int i = currentWriteCol; i < (borderStartW + borderWidth); i++)
                    {
                        if(i < (borderStartW + borderWidth -3))
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write("/");
                        }
                    }
                }
            }
        }
        // Tar input från användaren och kontrollerar att det accepteerad input
        public char GetOperator(string Text, string Error)
        {
            bool inputCheck;
            char[] operators = { '+', '-', '/', '*' };
            char c;

            Console.SetCursorPosition(startCol, currentWriteLine);
            Console.WriteLine(Text);
            currentWriteCol += Text.Length;
            do
            {
                Console.SetCursorPosition(currentWriteCol, currentWriteLine);
                c = input.ConsoleInputChar();
                if (!operators.Contains(c))
                {
                    ErrorMsg(Error);
                    inputCheck = false;
                    Console.SetCursorPosition(currentWriteCol, currentWriteLine);
                    for (int i = currentWriteCol; i < (borderStartW + borderWidth); i++)
                    {
                        if (i < (borderStartW + borderWidth - 3))
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write("/");
                        }
                    }
                }
                else { inputCheck = true; }

            } while (!inputCheck);
            ClearError();
            currentWriteLine++;
            currentWriteCol = startCol;
            return c;
        }
        //Skriver ut resultat i grönt eller vid fel i rött
        public void PrintResult(string Text, bool Error)
        {
            Console.SetCursorPosition(startCol, currentWriteLine + 1);
            if (Error) { Console.ForegroundColor = ConsoleColor.Red; }
            else { Console.ForegroundColor = ConsoleColor.Green; }
            Console.WriteLine(Text);
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(startCol, currentWriteLine + 3);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
        // Skriver ut invalid input och beskrivning på felet
        public void ErrorMsg(string Msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(startCol, Console.CursorTop);
            Console.WriteLine("Invalid Input");
            Console.SetCursorPosition(startCol, Console.CursorTop);
            Console.WriteLine(Msg);
            Console.ForegroundColor = textColor;
        }
        // Raderar fel medelanden
        public void ClearError()
        {
            Console.SetCursorPosition(startCol,currentWriteLine + 1);
            for (int i = 1; i < 3; i++)
            {
                Console.SetCursorPosition(startCol, currentWriteLine + i);
                for (int j = startCol; j < (borderStartW + borderWidth - 3); j++)
                {
                    Console.Write(" ");
                }
                
            }
        }
        // Skapar en ram.
        public void Border(int rows = 12)
        {
            borderHeight = rows;
            Console.SetCursorPosition(borderStartW, borderStartH);Console.Write("");
            for(int i = 0; i < (borderWidth - 10) / 2; i++)
            {
                Console.Write("/");
            }
            Console.Write("Calculator");
            for(int i = 0; i < (borderWidth - 10) / 2; i++)
            {
                Console.Write("/");
            }
            for (int i = 0; i < borderHeight - 1; i++)
            {
                Console.Write("\n");
                Console.SetCursorPosition(borderStartW, Console.CursorTop);
                if(i != rows - 2)
                {
                    for (int j = 0; j < borderWidth; j++)
                    {
                        if (j < 3 || j > borderWidth - 4)
                        {
                            Console.Write("/");
                        }
                        else { Console.Write(" "); }
                    }
                }
                else
                {
                    for(int j = 0; j < borderWidth; j++)
                    {
                        Console.Write("/");
                    }
                }
            }
            currentWriteLine = startLine;
            currentWriteCol = startCol;
            Console.SetCursorPosition(currentWriteCol, currentWriteLine);
        }
        // Tömmer allt inanför ramen.
        public void ClearInside()
        {
            Console.SetCursorPosition(borderStartW + 3, borderStartH + 1);
            for (int i = 0; i < borderHeight - 2; i++)
            {
                for(int j = 0; j < borderWidth - 6; j++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(borderStartW + 3, Console.CursorTop + 1);
            }
            currentWriteLine = startLine;
            currentWriteCol = startCol;
            Console.SetCursorPosition(startCol,startLine);
        }
    }
}