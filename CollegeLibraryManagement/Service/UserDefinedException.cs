using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeLibraryManagement.Service
{
    public class UserDefinedException:Exception
    {
        //parameterazied Constructor
        public UserDefinedException(string isbn)
            : base($"Book with ISBN '{isbn}' was not found.")
        {
        }


    }
}
