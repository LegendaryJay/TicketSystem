using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    static class Program
    {
        private static T GetResponseFromList<T>(string question, List<T> list)
        {
            do
            {
                Console.WriteLine(question);
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + list[i]);
                }

                int selection;
                String input = Console.ReadLine();
                try
                {
                    selection = int.Parse(input) - 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error, write a number");
                    continue;
                }

                if (selection < list.Count)
                {
                    return list[selection];
                }

                Console.WriteLine("Not an option");
            } while (true);
        }

        private static void Main(string[] args)
        {
            string file = "tickets.csv";
            List<Ticket> tickets = new List<Ticket>();
            string choice;
            do
            {
                // ask user a question
                Console.WriteLine("1) Read data from file.");
                Console.WriteLine("2) Create file from data.");
                Console.WriteLine("Enter any other key to exit.");
                // input response
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    // read data from file
                    if (File.Exists(file))
                    {
                        // read data from file
                        StreamReader sr = new StreamReader(file);
                        sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine() ?? throw new InvalidOperationException();
                            Ticket newTicket = new Ticket(line);
                            tickets.Add(newTicket);
                            Console.WriteLine(newTicket.ToString());
                        }

                        sr.Close();
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                    }
                }
                else if (choice == "2")
                {
                    // create file from data
                    StreamWriter sw = new(file);
                    sw.WriteLine();
                    bool continueLoop = true;
                    String input = "";
                    while (continueLoop)
                    {
                        // prompt for course name
                        Console.WriteLine("Enter the summary of the ticket");
                        string summary = Console.ReadLine();

                        Status status = GetResponseFromList("What is the status?",
                            Enum.GetValues(typeof(Status)).OfType<Status>().ToList<Status>());

                        Priority priority = (Priority) GetResponseFromList("What is the priority?",
                            Enum.GetValues(typeof(Priority)).OfType<Priority>().ToList<Priority>());

                        Console.WriteLine("Who submitted it?");
                        string submitter = Console.ReadLine();
                        Console.WriteLine("Who assigned it?");
                        string assigned = Console.ReadLine();
                        List<string> watching = new List<string>();
                        string watchingOne;
                        Console.WriteLine("Who is watching (say \"exit\" to exit)");
                        bool isValidName;
                        do
                        {
                            input = Console.ReadLine();
                            isValidName = input?.ToLower() is not (null or "" or "exit");
                            if (isValidName)
                            {
                                watching.Add(input);
                            }
                        } while (isValidName);

                        Ticket newTicket = new Ticket(summary, status, priority, submitter, assigned, watching);
                        sw.WriteLine(newTicket.ToPrintString());

                        //ask to repeat
                        Console.WriteLine("Continue? (y/n)");
                        input = Console.ReadLine();
                        continueLoop = input is "y";

                    }

                    sw.Close();
                }
            } while (choice is "1" or "2");
        }
    }
}