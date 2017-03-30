using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LargeAddress
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Large Address Aware CLI\n");

            int errorLevel = 0;
            bool stop = false;
            bool largeAwareMode = false;
            string filename = "";

            foreach (string eachArg in args)
            {
                switch(eachArg.ToUpper())
                {

                    case "/AWARE":
                        largeAwareMode = true;
                        break;

                    case "/UNAWARE":
                        largeAwareMode = false;
                        break;

                    default:
                        if(eachArg.ToUpper().EndsWith(".EXE"))
                        {
                            filename = eachArg;
                        }
                        else
                        {
                            Console.WriteLine("Original code and GUI project by FordGT90Concept @ techpowerup forums\nCLI by mute55 & meesterturner\n\n" +
                                        "USAGE:\n/AWARE [PathToEXE] - Set EXE LAA flag true\n/UNAWARE [PathToEXE] - Set EXE LAA flag false");
                            stop = true;
                        }
                        break;

                }
            }


            if(stop == false)
            {
                if(filename != "")
                {
                    Console.WriteLine("Setting LAA to: " + largeAwareMode.ToString() + "\n" +
                                      "on            : " + filename);
                    LaaFile laa = new LaaFile(filename);
                    if(laa.WriteCharacteristics(largeAwareMode) == false)
                    {
                        Console.WriteLine("Error setting LAA flag");
                        errorLevel = 1;
                    }
                    else
                    {
                        Console.WriteLine("Success!");
                    }
                }
            }

            return errorLevel;
        }
    }
}
