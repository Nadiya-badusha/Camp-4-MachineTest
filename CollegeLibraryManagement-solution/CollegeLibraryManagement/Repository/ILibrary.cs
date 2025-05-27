using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeLibraryManagement.Repository
{
    public interface ILibrary
    {
        //abstract methods
        public void ListAllBooks();
        public void AddBook();
        public void EditBook();
        public void RemoveBook();
        public void SearchByAuthor();
        public void SearchByTitle();
        public void SearchByISBN();
    }
}
