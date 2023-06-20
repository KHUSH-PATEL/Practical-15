using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BankingSystem.Helper
{
    public static class AcountUtility
    {
        public static string GenerateAccountNumber()
        {
            Random random = new Random();
            int length = random.Next(9, 17); // Generate a random length between 9 and 16
            StringBuilder accountNumber = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                accountNumber.Append(random.Next(0, 10)); // Generate a random digit between 0 and 9
            }

            return accountNumber.ToString();
        }
        public static string GenerateIFSCCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Define the characters allowed in IFSC code

            StringBuilder ifscCode = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                ifscCode.Append(chars[random.Next(chars.Length)]); // Generate a random character from the allowed characters
            }
            ifscCode.Append("0"); // Append a zero as the fifth character
            for (int i = 0; i < 6; i++)
            {
                ifscCode.Append(random.Next(0, 10)); // Generate a random digit between 0 and 9
            }

            return ifscCode.ToString();
        }
    }
}