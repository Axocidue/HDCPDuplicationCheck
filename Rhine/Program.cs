using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Rhine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            try
            {
                BizHelper.InitializeDB();

                var lines = File.ReadAllLines(@"C:\Lemon\Personal\Desktop\Rhine_Source.txt", Encoding.Default);

                var monitor = @"C:\Lemon\Personal\Desktop\Rhine_Monitor.txt";

                using (var fs = new FileStream(monitor, FileMode.Create))
                {
                    using (var sw = new StreamWriter(fs, Encoding.Default))
                    {

                        foreach (var line in lines)
                        {
                            var elements = Regex.Split(line, @"\.");
                            var si = elements[0];
                            var version = elements[1];
                            sw.WriteLine(BizHelper.GetHDCPFromSIAndVersion(si, version));

                        }
                    }
                }
            }
            catch(Exception ex_main)
            {
                Console.WriteLine(ex_main.Message);
            }
            finally
            {
                BizHelper.DisposeDB();
            }


            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
