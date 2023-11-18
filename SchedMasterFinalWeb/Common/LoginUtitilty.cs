using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedMasterFinalWeb.Common
{
    public class LoginUtitilty
    {
        private static SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();

        //public static string GenerateDefaultPassword(int length)
        //{

        //    const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        //    Random random = new Random();

        //    // Select random characters from the validChars string.
        //    var chars = Enumerable.Repeat(validChars, length)
        //                          .Select(s => s[random.Next(s.Length)]).ToArray();

        //    // Create a string from the character array and return it as the password.
        //    return new String(chars);
        //}
        public static string GenerateDefaultPassword()
        {
            return "Welcome123";
        }

        public static int GenerateRandomUniqueId()
        {
            Random rnd = new Random();
            int uniqueId;
            do
            {
                uniqueId = rnd.Next(1000000, 10000000); // Generates a number between 1000000 and 9999999
            }
            while (db.Users.Any(u => u.UserId == uniqueId)); // This checks the generated ID against the database

            return uniqueId;
        }
    }
}