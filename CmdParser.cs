using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJTalkInterfaceForYMM4
{
    public static class CmdParser
    {
        public static string[]? ParseCmdArgs(string[] args)
        {
            List<string> strs = new List<string>();

            StringBuilder strbuild = new StringBuilder();
            
            foreach(string s in args)
            {
                strbuild.Append(s);
                strbuild.Append(" ");
            }

            string str = strbuild.ToString();

            byte situation = 0;
            //0: none 1: single quotation 2: double quotation

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i != str.Length; i++)
            {
                switch (str.Substring(i))
                {
                    case "\'":
                        if (situation == 0)
                        {
                            situation = 1;
                        }

                        else if (situation == 1)
                        {
                            situation = 0;
                        }
                        else if (situation == 2)
                        {
                            sb.Append("\'");
                        }
                        break;
                    case "\"":
                        break;
                    case " ":
                        break;
                }
            }
        } 
    }
}
