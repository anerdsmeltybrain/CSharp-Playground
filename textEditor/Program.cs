using System;
using System.IO;
using System.Linq;

namespace Input
{
    public enum State
    {
        NORMAL,
        INSERT,
        REPLACE,
        DELETE,
        LOAD,
        SAVE
    }
    class Program
    {
        public static void showBanner(State state, string filePath)
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
                case State.LOAD:
                    mode = "LOAD";
                    break;
                case State.SAVE:
                    mode = "SAVE";
                    break;
            }
            Console.WriteLine($"Current Mode : {mode} | Current File Path : {filePath}");
            Console.WriteLine("| i = insert | r = replace | d = delete | s = save | l = load");
        }
        public static void updateBanner(State state, List<string> messages, string filePath)
        {
            int i = 0;
            Console.Clear();
            Program.showBanner(state, filePath);
            foreach(string message in messages)
            {
                Console.WriteLine($"{i++} : {message}");
            }
        }

        public static void loadFile(string[] args, ref List<string> messages, ref string filePath)
        {
           if(args.Length >= 1)
            {
                filePath = args[0];
                messages = File.ReadLines(filePath).ToList();
            } 
        }

        public static void saveFile(bool isSaved, ref List<string> messages, ref string filePath)
        {
            isSaved = true;
            File.WriteAllLines(filePath, messages);
        }
        public static void Main(string[] args)
        {
            bool isRunning = new bool();
            isRunning = true;

            bool isSaved = false;
            string filePath = "";
            List<string> messages = new List<string> {};
            string? message = "";
            int lineNum = 0;
            State state = State.NORMAL;

            loadFile(args, ref messages, ref filePath);

            while(isRunning)
            {
                state = State.NORMAL;
                updateBanner(state, messages, filePath);
                string? choice = Console.ReadLine();

                switch(choice)
                {
                    case "i":
                        state = State.INSERT;
                        updateBanner(state, messages, filePath);
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
                            if(lineNum <= messages.Count && lineNum >= 0)
                            {
                                messages[lineNum] = "";
                                updateBanner(state, messages, filePath);
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
                        updateBanner(state, messages, filePath);
                        Console.Write("LINE:");
                        if(int.TryParse(Console.ReadLine(), out lineNum))
                        {
                            if(lineNum <= messages.Count && lineNum >= 0)
                            {
                                messages.RemoveAt(lineNum);
                                updateBanner(state, messages, filePath);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a Valid Number");
                        }
                        Console.ReadKey();
                        break;
                    case "s":
                        state = State.SAVE;
                        updateBanner(state, messages, filePath);
                        saveFile(isSaved, ref messages, ref filePath);
                        Console.WriteLine("FILE SAVED");
                        Console.ReadKey();
                        break;
                    case "l":
                        state = State.LOAD;
                        updateBanner(state, messages, filePath);
                        Console.Write("FILE PATH:");
                        var input = Console.ReadLine();
                        if(string.IsNullOrWhiteSpace(input))
                        {
                            Console.WriteLine("Please enter a valid response");
                            break;
                        }
                        filePath = input;
                        messages = File.ReadLines(filePath).ToList();
                        updateBanner(state, messages, filePath);
                        Console.WriteLine("FILE LOADED");
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