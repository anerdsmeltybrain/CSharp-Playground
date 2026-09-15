using System;
using System.IO;

namespace Input
{
    public enum State
    {
        NORMAL,
        INSERT,
        REPLACE,
        DELETE
    }
    class Program
    {
        public static void showBanner(State state)
        {
            string mode = "";
            switch(state)
            {
                case State.NORMAL:
                    mode = "IDLE";
                    break;
                case State.INSERT:
                    mode = "INSERT";
                    break;
                case State.REPLACE:
                    mode = "REPLACE";
                    break;
                case State.DELETE:
                    mode = "DELETE";
                    break;
            }
            Console.WriteLine($"Current Mode : {mode}");
            Console.WriteLine("| i = insert |");
        }
        public static void updateBanner(State state, List<string> messages)
        {
            int i = 0;
            Console.Clear();
            Program.showBanner(state);
            foreach(string message in messages)
            {
                Console.WriteLine($"{i++} : {message}");
            }
        }
        public static void Main()
        {
            bool isRunning = new bool();
            isRunning = true;

            List<string> messages = new List<string> {};
            string? message = "";
            int lineNum = 0;
            State state = State.NORMAL;

            while(isRunning)
            {
                state = State.NORMAL;
                updateBanner(state, messages);
                string? choice = Console.ReadLine();

                switch(choice)
                {
                    case "i":
                        state = State.INSERT;
                        updateBanner(state, messages);
                        message = Console.ReadLine();
                        if(message is not null)
                        {
                            messages.Add(message);
                        }
                        break;
                    case "r":
                        //update state, make choice of which line, empty line, insert at choice a message
                        state = State.REPLACE;
                        Console.Write("LINE:");
                        if(int.TryParse(Console.ReadLine(), out lineNum))
                        {
                            if(lineNum <= messages.Count)
                            {
                                messages[lineNum] = "";
                                updateBanner(state, messages);
                                message = Console.ReadLine();
                                if(message is not null)
                                {
                                    messages[lineNum] = message;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a Valid Number");
                        }
                        Console.ReadKey();
                        break;
                    case "d":
                        state = State.DELETE;
                        updateBanner(state, messages);
                        Console.Write("LINE:");
                        if(int.TryParse(Console.ReadLine(), out lineNum))
                        {
                            if(lineNum <= messages.Count)
                            {
                                messages.RemoveAt(lineNum);
                                updateBanner(state, messages);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a Valid Number");
                        }
                        Console.ReadKey();
                        break;
                    case "q":
                        isRunning = false;
                        break;
                }

            }

            Console.WriteLine("Program Exited");
        }
    }
}