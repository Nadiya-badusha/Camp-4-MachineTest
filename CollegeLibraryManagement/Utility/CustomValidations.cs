using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CollegeLibraryManagement.Utility
{
    public class CustomValidations
    {

        //validate title and author inputs
        #region Validate name
        public static bool IsValidTitleOrAuthor(string input)
        {
            return !string.IsNullOrWhiteSpace(input) &&
                   input.Length <= 20 &&
                   Regex.IsMatch(input, @"^[A-Za-z][A-Za-z ]*$") &&
                   !input.StartsWith(" ");
        }
        #endregion
        //validate published date do not accept future date
        #region validate date
        public static bool IsValidPublishedDate(string input, out DateTime publishedDate)
        {
            return DateTime.TryParse(input, out publishedDate) && publishedDate <= DateTime.Today;
        }
        #endregion
    }
}
