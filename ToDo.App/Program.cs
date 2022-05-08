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

        /// <summary>
        /// Create list
        /// </summary>
        static void CreateList()
        {
            string choice = "y";
            string listName;
            while (choice != "n")
            {
                Console.WriteLine();
                Console.WriteLine("Enter a list name:");
                listName = Console.ReadLine();
                TODOList newList = new ToDoListOperations().Create(listName);
                Console.WriteLine();
                Console.WriteLine("You created list: {0}", newList.name);
                Console.WriteLine();
                Console.Write("Do you want enter more lists (y/n)");
                choice = Console.ReadLine();
            }
        }

        /// <summary>
        /// Remove list by ID
        /// </summary>
        static void RemoveList()
        {
            string choice = "y";
            int listId;
            while (choice != "n")
            {
                Console.WriteLine();
                Console.WriteLine("Enter ID of a list:");
                listId = Convert.ToInt32(Console.ReadLine());
                var removeList = new ToDoListOperations().Remove(listId);
                if (removeList > 0)
                {
                    Console.WriteLine("List with ID: {0} successfully removed ", listId);
                }
                else
                {
                    Console.WriteLine("Sorry, list with ID: {0} unsuccessfully removed ", listId);
                }

                Console.Write("Do you want to remove more lists (y/n)");
                choice = Console.ReadLine();
                Console.WriteLine();
            }
        }

        static void UpdateList()
        {
            Console.WriteLine("Enter a list ID to modify");
            int listId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter a new name for the list");
            string modifyName = Console.ReadLine();
            Console.WriteLine("Do you want to hide a list from the list view(y/n)");
            string resultHide = Console.ReadLine();
            bool isVisible;

            if (resultHide == "y")
            {
                isVisible = true;
            }
            else
            {
                isVisible = false;
            }

            TODOList modifyList = new TODOList()
            {
                id = listId,
                name = modifyName,
                isVisible = isVisible
            };

            modifyList = new ToDoListOperations().Update(modifyList);

            Console.WriteLine("List with ID {0} have new values {1}  {2}", modifyList.id,
                modifyList.name, modifyList.isVisible);
            Console.WriteLine();
        }

        static void GetAllListData()
        {
            Console.WriteLine();
            List<TODOList> lists = new ToDoListOperations().GetAll();

            foreach (var list in lists)
            {
                Console.WriteLine("List:\n\t ID: {0}\tName: {1}\tVisible: {2}", list.id, list.name, list.isVisible);
            }
        }

        static void GetList()
        {
            Console.WriteLine("Enter ID of a list:");
            int listId = Convert.ToInt32(Console.ReadLine());

            TODOList list = new ToDoListOperations().Get(listId);
            Console.WriteLine("List:\n\t ID: {0}\tName: {1}\tVisible: {2}", list.id, list.name, list.isVisible);
        }

        static void SetStatusEntry()
        {
            Console.WriteLine("Enter ID of an entry:");
            int entryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter status of an entry(true/false):");
            bool entryStatus = Convert.ToBoolean(Console.ReadLine());
            var result = new ToDoEntryOperations().SetStatus(entryId, entryStatus);
            if (result > 0)
            {
                Console.WriteLine("Status changed successfully");
            }
            else
            {
                Console.WriteLine("Something goes wrong");
            }    
        }

        /// <summary>
        /// Create entry
        /// </summary>
        static void CreateEntry()
        {
            Console.WriteLine("Enter an list ID");
            int listId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter title for entry:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter description for entry :");
            string description = Console.ReadLine();

            Console.WriteLine("Enter due date for entry(example “MM/dd/yyyy”):");
            string pattern = "MM/dd/yyyy";
            DateTime date = DateTime.ParseExact(Console.ReadLine(), pattern, null);

            Console.WriteLine("Does the task is finished(y/n):");
            string statusChar = Console.ReadLine();
            bool statusString;
            if (statusChar == "y")
            {
                statusString = true;
            }
            else
            {
                statusString = false;
            }

            TODOEntry newentity = new TODOEntry()
            {
                title = title,
                description = description,
                dueDate = date,
                isDone = statusString,
                listid = listId
            };

            var result = new ToDoEntryOperations().Create(newentity);
            Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3}",
                newentity.id, newentity.title,
                newentity.description, newentity.dueDate, newentity.isDone);
        }

        /// <summary>
        /// Remove Entry
        /// </summary>
        static void RemoveEntry()
        {
            string choice = "y";
            int entryId;
            while (choice != "n")
            {
                Console.WriteLine();
                Console.WriteLine("Enter ID of a entry:");
                entryId = Convert.ToInt32(Console.ReadLine());
                var removeList = new ToDoEntryOperations().Remove(entryId);
                if (removeList > 0)
                {
                    Console.WriteLine("List with ID: {0} successfully removed ", entryId);
                }
                else
                {
                    Console.WriteLine("Sorry, list with ID: {0} unsuccessfully removed ", entryId);
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
                Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3}",
                entry.id, entry.title, entry.description, entry.dueDate, entry.isDone);
            }
        }

        static void GetEntry()
        {
            Console.WriteLine("Enter ID of a list:");
            int entryId = Convert.ToInt32(Console.ReadLine());

            TODOEntry entry = new ToDoEntryOperations().Get(entryId);
            Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3}",
                entry.id, entry.title, entry.description, entry.dueDate, entry.isDone);
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
                        Console.WriteLine();
                        break;
                    case "2":
                        GetAllListData();
                        Console.WriteLine();
                        break;
                    case "3":
                        GetList();
                        Console.WriteLine();
                        break;
                    case "4":
                        UpdateList();
                        break;
                    case "5":
                        RemoveList();
                        Console.WriteLine();
                        break;
                    case "6":
                        CreateEntry();
                        break;
                    case "7":
                        RemoveEntry();
                        break;
                    case "8":
                        GetEntry();
                        break;
                    case "9":
                        GetAllEntryData();
                        break;
                    case "10":
                        SetStatusEntry();
                        break;
                    
                }
            }
        }

    }
}
