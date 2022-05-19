using System;
using System.Linq;
using System.Collections.Generic;
using ToDo.BLL.Operations;
using ToDo.BLL.Entity;

namespace ToDo.App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("\tWelcome to ToDoList Application!");

            Menu();
        }

        static DateTime ParseDateTime(Func<string> getNameFunc)
        {
            string result = getNameFunc();
            if (!string.IsNullOrEmpty(result))
            {
                if (DateTime.TryParse(result.Trim(), out DateTime value))
                {
                    return value;
                }
            }

            Console.WriteLine("Error message");
            return ParseDateTime(getNameFunc);
        }

        static bool ParseBool(Func<string> getNameFunc)
        {
            string result = getNameFunc();
            if (!string.IsNullOrEmpty(result))
            {
                if (bool.TryParse(result.Trim(), out bool value))
                {
                    return value;
                }
            }

            Console.WriteLine("Error message");
            return ParseBool(getNameFunc);
        }

        static int ParseInteger(Func<string> getNameFunc)
        {
            string result = getNameFunc();
            if (!string.IsNullOrEmpty(result))
            {
                if (int.TryParse(result.Trim(), out int value))
                {
                    return value;
                }
            }

            Console.WriteLine("Error message");
            return ParseInteger(getNameFunc);
        }
        static string GetInput(string inputText)
        {
            Console.WriteLine(inputText);
            string name = Console.ReadLine();
            return name;
        }

        static void DisplayList(TODOList item)
        {
            Console.WriteLine();
            Console.WriteLine("List:\n\t ID: {0}\tName: {1}\tVisible: {2}", item.id, item.name, item.isVisible);
            Console.WriteLine();
        }

        static void CreateList()
        {
            string choice = "y";
            string listName;
            while (choice != "n")
            {
                listName = GetInput("Enter list name");
                TODOList newList = new ToDoListOperations().Create(listName);
                DisplayList(newList);
                Console.Write("Do you want enter more lists (y/n)");
                choice = Console.ReadLine();
            }
        }

        static void RemoveList()
        {
            int id = ParseInteger(() => GetInput("Enter ID of a list:"));

            var removeList = new ToDoListOperations().Remove(id);
            if (removeList == true)
            {
                Console.WriteLine("List with ID: {0} successfully removed ", id);
            }
            else
            {
                Console.WriteLine("Sorry, list with ID: {0} unsuccessfully removed ", id);
            }
        }

        static void UpdateList()
        {
            int id = ParseInteger(() => GetInput("Enter a list ID to modify"));
            string modifyName = GetInput("Enter a new name for list");
            bool isVisible = ParseBool(() => GetInput("Do you want to hide a list from the list view(true/false)"));

            TODOList modifyList = new TODOList()
            {
                id = id,
                name = modifyName,
                isVisible = isVisible
            };

            modifyList = new ToDoListOperations().Update(modifyList);
            DisplayList(modifyList);
        }

        static void GetAllListData()
        {
            Console.WriteLine();
            List<TODOList> lists = new ToDoListOperations().GetAll();

            foreach (var list in lists)
            {
                DisplayList(list);
            }
        }

        static void GetList()
        {
            int id = ParseInteger(() => GetInput("Enter ID of a list: "));
            TODOList list = new ToDoListOperations().Get(id);
            DisplayList(list);
        }

        static void CreateEntry()
        {
            int id = ParseInteger(() => GetInput("Enter ID of a entry: "));
            string title = GetInput("Enter a title for entry: ");
            string description = GetInput("Enter description for entry: ");
            DateTime date = ParseDateTime(() => GetInput("Enter due date for entry(example “MM/dd/yyyy hh:mm”):"));
            bool statusString = ParseBool(() => GetInput("Do the task is finished(true/false)"));

            TODOEntry newentity = new TODOEntry()
            {
                title = title,
                description = description,
                dueDate = date,
                isDone = statusString,
                listid = id
            };

            var result = new ToDoEntryOperations().Create(newentity);
            Console.WriteLine("You added entry:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                result.id, result.title,
                result.description, result.dueDate, result.isDone);
        }

        static void RemoveEntry()
        {
            string choice = "y";

            while (choice != "n")
            {
                Console.WriteLine();
                int id = ParseInteger(() => GetInput("Enter ID of a entry: "));

                var removeList = new ToDoEntryOperations().Remove(id);
                if (removeList == true)
                {
                    Console.WriteLine("List with ID: {0} successfully removed ", id);
                }
                else
                {
                    Console.WriteLine("Sorry, list with ID: {0} unsuccessfully removed ", id);
                }

                choice = GetInput("Do you want to remove more entries (y/n)");
                Console.WriteLine();
            }
        }

        static void GetAllEntryData()
        {
            Console.WriteLine();
            List<TODOEntry> entries = new ToDoEntryOperations().GetAll();

            foreach (var entry in entries)
            {
                Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                entry.id, entry.title, entry.description, entry.dueDate, entry.isDone);
            }
        }

        static void GetEntry()
        {
            int id = ParseInteger(() => GetInput("Enter ID of a entry: "));

            TODOEntry entry = new ToDoEntryOperations().Get(id);
            Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                entry.id, entry.title, entry.description, entry.dueDate, entry.isDone);
        }

        static void UpdateEntry()
        {
            int id = ParseInteger(() => GetInput("Please enter id of entry to modify: "));

            TODOEntry entry = new ToDoEntryOperations().Get(id);

            Console.WriteLine("1-Title");
            Console.WriteLine("2-Description");
            Console.WriteLine("3-Due date");
            Console.WriteLine("4-Completed");
            Console.Write("What you want to modify choose 1-4:");
            int x = Convert.ToInt32(Console.ReadLine());

            switch (x)
            {
                case 1:
                    entry.title = GetInput("Enter a new name for the title");
                    break;
                case 2:
                    entry.description = GetInput("Enter a new name for the description");
                    break;
                case 3:
                    entry.dueDate = ParseDateTime(() => GetInput("Enter due date for entry(example “MM/dd/yyyy hh:mm”):")); ;
                    break;
                case 4:
                    entry.isDone = ParseBool(() => GetInput("Do the task is finished(true/false)"));
                    break;
                default:
                    Console.WriteLine("Please enter from 1 to 4");
                    break;
            }

            var result = new ToDoEntryOperations().Update(entry);

            Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                result.id, result.title, result.description, result.dueDate, result.isDone);
        }

        static void Menu()
        {
            Console.WriteLine("Menu Driven Program for ToDo List");
            while (true)
            {
                Console.WriteLine("  1. Create a list");
                Console.WriteLine("  2. Get all list data");
                Console.WriteLine("  3. Get one list");
                Console.WriteLine("  4. Modify a ist");
                Console.WriteLine("  5. Remove a list");
                Console.WriteLine("  6. Create an entry");
                Console.WriteLine("  7. Remove an entry");
                Console.WriteLine("  8. Get one entry");
                Console.WriteLine("  9. Get all entry data");
                Console.WriteLine("  10. Modify an entry");
                Console.Write("Choose one (q to quit): ");

                string choice = Console.ReadLine();
                if (choice == "q")
                    break;
                switch (choice)
                {
                    case "1":
                        Console.WriteLine();
                        CreateList();
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine();
                        GetAllListData();
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        GetList();
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        UpdateList();
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        RemoveList();
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine();
                        CreateEntry();
                        Console.WriteLine();
                        break;
                    case "7":
                        Console.WriteLine();
                        RemoveEntry();
                        Console.WriteLine();
                        break;
                    case "8":
                        Console.WriteLine();
                        GetEntry();
                        Console.WriteLine();
                        break;
                    case "9":
                        Console.WriteLine();
                        GetAllEntryData();
                        Console.WriteLine();
                        break;
                    case "10":
                        Console.WriteLine();
                        UpdateEntry();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Please enter from 1 to 10 or q to quit from application");
                        break;
                }
            }
        }

    }
}
