using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJTalkInterfaceForYMM4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }
            else
            {
                if (args[0].ToLower() == "help")
                {
                    System.Reflection.Assembly myAssembly =   System.Reflection.Assembly.GetExecutingAssembly();
                    System.IO.StreamReader sr =
                        new System.IO.StreamReader(
                        myAssembly.GetManifestResourceStream("OpenJTalkInterfaceForYMM4.Help.txt"),
                            System.Text.Encoding.UTF8);
                    Console.WriteLine(sr.ReadToEnd());
                    return;
                }

                StringBuilder sb = new StringBuilder();
                
                for(int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                    }
                }

            }
        }
    }
}
