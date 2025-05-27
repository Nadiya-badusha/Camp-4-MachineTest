using CollegeLibraryManagement.Repository;

namespace CollegeLibraryManagement
{
    public class LibraryManagementApp
    {
        static void Main(string[] args)
        {
            //library object instanstiation
            ILibrary library = new Library();

            //Menu Driven

            while (true)
            {
                Console.WriteLine("\n--- Library Management Menu ---");
                Console.WriteLine("1. List All Books");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Edit Book");
                Console.WriteLine("4. Remove Book");
                Console.WriteLine("5. Search by Author");
                Console.WriteLine("6. Search by Title");
                Console.WriteLine("7. Search by ISBN id");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");


                //check choice
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Invaild choice.please try again");

                    continue;

                }
                //go to respective choice
                try
                {
                    switch (choice)
                    {
                        case 1:
                            library.ListAllBooks();
                            break;

                        case 2:
                            library.AddBook();
                            break;

                        case 3:
                            library.EditBook();
                            break;

                        case 4:
                            library.RemoveBook();
                            break;

                        case 5:
                            library.SearchByAuthor();
                            break;

                        case 6:
                            library.SearchByTitle();
                            break;

                        case 7:
                            library.SearchByISBN();
                            break;

                        case 8:
                            Console.WriteLine("Exitting...");
                            return;


                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"An Unexpected error occured :{ex.Message}");
                }

                //pause screen
                Console.WriteLine("\nPress any key to continue......");
                Console.ReadKey();

            }
        }
    }
}


