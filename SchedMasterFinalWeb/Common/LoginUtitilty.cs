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

        public static int GenerateUniqueId()
        {
            // Get the maximum UserId in the database, or default to 1111 if there are no users yet
            int maxUserId = db.Users.Max(u => (int?)u.UserId) ?? 1111;

            // Return one more than the maximum UserId
            return maxUserId + 1;
        }
    }
}