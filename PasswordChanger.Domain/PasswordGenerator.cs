using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChanger.Domain
{
    public static class PasswordGenerator
    {
        private static char[] _alphaCharacters = { 'a','A','b','B','c','C','d','D','e','E','f','F','g','G','h','H','i','I','j','J','k','K','l','L',
            'm','M','n','N','o','O','p','P','q','Q','r','R','s','S','t','T','u','U','v','V','w','W','x','X','y','Y','z','Z' };

        private static char[] _numCharacters = { '2', '5', '8', '7', '1', '9', '4', '3', '6', '0' };

        private static char[] _specialCharacters = { '*', '_', '[', ']', '!', '#' };

        public static string GeneratePassword(GenType genType)
        {
            char nextCharToAdd;
            string newPassword = "";

            bool shouldBeNumChar = false;
            bool shouldBeSpecialChar = false;

            int specialCharacterCount = 0;

            int counter = 0;
            foreach(int j in genType.StringKeys)
            {
                if (j%2 == 0 && counter == 0)
                {
                    shouldBeNumChar = true;
                }
                else if (j%3 == 0 && counter != 0)
                {
                    shouldBeSpecialChar = true;
                }

                if (shouldBeNumChar)
                {
                    nextCharToAdd = _numCharacters[(_numCharacters.Length - 1) % j];
                }
                else if (shouldBeSpecialChar && specialCharacterCount < 2)
                {
                    nextCharToAdd = _specialCharacters[(_specialCharacters.Length - 1) % j];
                    specialCharacterCount++;
                }
                else
                {
                    nextCharToAdd = _alphaCharacters[(_alphaCharacters.Length - 1) % j];
                }
                newPassword = string.Format("{0}{1}", newPassword, nextCharToAdd.ToString());
            }
            return newPassword;
        }
    }
}
