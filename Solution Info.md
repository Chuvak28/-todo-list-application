###ToDo List Application

##This project consist of three parts
#1-Business Logic
Provides a reusable library of classes that represents a domain model. Entity model
created using one of Entity approaches CODE FIRST.There are two domain classes
TODOList and TODOEntry which are describing business domain. TODOList is
responsible for storing list info, while TODOEntry is responsible for storing entry
information. To operate with data there are two classes ToDolistOperations and
TodoEntryOperations,and both of them derived from interfaces IToDoEntryDataProvider
and IToDoListDataProvider. Each of this classes have such methods like
Create, Update, Remove and Display to operate with data.
#2-Console Application
An application with a user interface. Demonstrates how to interact with application.
#3-Unit tests
The functionality of the domain model checks with the help of the unit tests.