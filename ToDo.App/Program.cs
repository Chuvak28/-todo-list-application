using System;
using System.Linq;
using ToDo.BLL.Operations;
using ToDo.BLL.Entity;

namespace ToDo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tWelcome to ToDoList Application!");

            Menu();

        }

        static void Menu()
        {
            Console.WriteLine("Menu Driven Program for ToDo List");
            while (true)
            {
                Console.WriteLine("  1. Create a list");
                Console.WriteLine("  2. Assign entity to a list");
                Console.WriteLine("  3. Show data");
                Console.WriteLine("  4. Modify a List");
                Console.WriteLine("  5. Modify an Entry");
                Console.WriteLine("  6. Set status of entry");
                Console.WriteLine("  7. Remove a List");
                Console.Write("Choose one (q to quit): ");

                string choice = Console.ReadLine();
                if (choice == "q")
                    break;
                switch (choice)
                {
                    case "1":
                        CreateList();
                        break;
                    case "2":
                        AssignEntityList();
                        break;
                    case "3":
                        ShowData();
                        break;
                    case "4":
                        ModifyList();
                        break;
                    case "5":
                        ModifyEntry();
                        break;
                    case "6":
                        SetStatusEntry();
                        break;
                    case "7":
                        RemoveList();
                        break;
                }
            }
        }

    }
}
