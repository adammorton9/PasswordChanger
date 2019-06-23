using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChanger.Domain
{
    public class GenType
    {
        public int NumberOfSpecialCharacters { get; set; }
        public List<int> StringKeys { get; set; }


        public GenType(int numOfSpecialCharacters, List<int> stringKeys)
        {
            NumberOfSpecialCharacters = numOfSpecialCharacters;
            StringKeys = stringKeys;
        }
    }
}
