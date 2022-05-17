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

        static bool isSelected(string str)
        {
            if (str == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool ParseInteger(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;

            }

            if (str.Length > 6)
            {
                return false;
            }

            bool canConvert = int.TryParse(str.Trim(), out int number);
            if (canConvert == false)
            {
                return false;
            }

            return true;
        }

        static int VerifyParse(string str)
        {
            bool check = ParseInteger(str);
            if (check == true)
            {
                return Convert.ToInt32(str);
            }
            else
            {
                Console.WriteLine("You entered wrong number");
                Console.WriteLine("Please try again:");
                string input = Console.ReadLine();
                return VerifyParse(input);
            }
        }
        static string GetName()
        {
            Console.WriteLine("Enter a name:");
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
                listName = GetName();
                TODOList newList = new ToDoListOperations().Create(listName);
                DisplayList(newList);
                Console.Write("Do you want enter more lists (y/n)");
                choice = Console.ReadLine();
            }
        }

        static void RemoveList()
        {
                Console.WriteLine("Enter ID of a list:");
                string input = Console.ReadLine();
                int id = VerifyParse(input);

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
            Console.WriteLine("Enter a list ID to modify");

            string input = Console.ReadLine();
            int id = VerifyParse(input);

            string modifyName = GetName();
            Console.WriteLine("Do you want to hide a list from the list view(y/n)");
            string resultHide = Console.ReadLine();
            bool isVisible = isSelected(resultHide);

            TODOList modifyList = new TODOList()
            {
                id = id,
                name = modifyName,
                isVisible = !isVisible
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
            Console.WriteLine("Enter ID of a list:");
            string input = Console.ReadLine();
            int id = VerifyParse(input);
            TODOList list = new ToDoListOperations().Get(id);
            DisplayList(list);
        }

        static void CreateEntry()
        {
            Console.WriteLine("Enter an list ID");

            string input = Console.ReadLine();
            int id = VerifyParse(input);

            string title = GetName();

            Console.WriteLine("Enter description for entry :");
            string description = Console.ReadLine();

            Console.WriteLine("Enter due date for entry(example “MM/dd/yyyy hh:mm”):");
            
            bool checkDateTime = DateTime.TryParse(Console.ReadLine(), out DateTime date);
            if(checkDateTime == false)
            {
                Console.WriteLine("DaterTime is not in the correct format");
            }

            Console.WriteLine("Does the task is finished(y/n):");
            string statusChar = Console.ReadLine();
            bool statusString = isSelected(statusChar);

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
                Console.WriteLine("Enter ID of a entry:");
                string input = Console.ReadLine();
                int id = VerifyParse(input);

                var removeList = new ToDoEntryOperations().Remove(id);
                if (removeList == true)
                {
                    Console.WriteLine("List with ID: {0} successfully removed ", id);
                }
                else
                {
                    Console.WriteLine("Sorry, list with ID: {0} unsuccessfully removed ", id);
                }

                Console.Write("Do you want to remove more entries (y/n)");
                choice = Console.ReadLine();
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
            Console.WriteLine("Enter ID of a list:");
            string input = Console.ReadLine();
            int id = VerifyParse(input);

            TODOEntry entry = new ToDoEntryOperations().Get(id);
            Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                entry.id, entry.title, entry.description, entry.dueDate, entry.isDone);
        }

        static void UpdateEntry()
        {
            Console.WriteLine("Please enter id of entry to modify: ");

            string input = Console.ReadLine();
            int id = VerifyParse(input);

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
                    Console.WriteLine("Enter a new name for the title");
                    string titleName = Console.ReadLine();
                    entry.title = titleName;
                    break;
                case 2:
                    Console.WriteLine("Enter a new name for the description");
                    string descriptionName = Console.ReadLine();
                    entry.description = descriptionName;
                    break;
                case 3:
                    Console.WriteLine("Enter due date for entry(example “MM/dd/yyyy”):");
                    bool checkDateTime = DateTime.TryParse(Console.ReadLine(), out DateTime date);
                    if (checkDateTime == true)
                    {
                        entry.dueDate = date;
                    }
                    break;
                case 4:
                    Console.WriteLine("Does the task is finished(true/false):");
                    bool status = Convert.ToBoolean(Console.ReadLine());
                    entry.isDone = status;
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
