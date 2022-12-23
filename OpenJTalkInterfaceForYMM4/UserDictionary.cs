 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJTalkInterfaceForYMM4
{
    public class UserDictionary
    {
        public string Word { get; set; }
        public string Replace { get; set; }
        public bool IgnoreCase { get; set; }
        public UserDictionary(string word, string replace, bool ignoreCase)
        {
            Word = word;
            Replace = replace;
            IgnoreCase = ignoreCase;
        }
        public UserDictionary(string word, string replace)
        {
            Word = word;
            Replace = replace;
            IgnoreCase = false;
        }

        public UserDictionary()
        {
            Word = "";
            Replace = "";
            IgnoreCase = false;
        }
    }
}
