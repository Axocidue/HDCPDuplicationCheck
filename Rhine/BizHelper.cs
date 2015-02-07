using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

namespace Rhine
{
    public static class BizHelper
    {

        private static HPDDPRODEntities db;

        public static void InitializeDB()
        {
            DisposeDB();

            db = new HPDDPRODEntities();
        }

        public static void DisposeDB()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        public static string ReverseByteOrder(this string wrong)
        {
            //0123456789
            var wrongArray = wrong.ToCharArray();
            //8967452301
            var correctArray = new char [10] {
                wrongArray[8],
                wrongArray[9],
                wrongArray[6],
                wrongArray[7],
                wrongArray[4],
                wrongArray[5],
                wrongArray[2],
                wrongArray[3],
                wrongArray[0],
                wrongArray[1]
            };
            return new string(correctArray);
        }
        
        public static string GetHDCPFromSIAndVersion(string si, string version)
        {
            try
            {
                //InitializeDB();

                var query = from d in db.DELIVERies
                            join u in db.UNITs on d.DELIVERYID equals u.SUPPLYDELIVERYID
                            join p in db.PRODINFOes on u.PRODINFOID equals p.PRODINFOID
                            join i in db.INCLUDEs on u.UNITID equals i.UNITID
                            where p.DPYPRODNUM == si && p.DPYREVSTATE == version && i.INCNAMEID == 22
                            select new
                            {
                                p.DPYPRODNUM,
                                p.DPYREVSTATE,
                                u.MSN,
                                i.INCNUMBER,
                                d.DELIVERYNUMBER,
                                u.SUPPLYDELIVERYID,
                                u.DELIVERYID,
                                u.UNITID
                            };

                var count = 0;

                var line = "";

                var filename = @"C:\Lemon\Personal\Desktop\Rhine\" + si + "." + version.Replace(@"/","_") +".txt";

                var filenamec = @"C:\Lemon\Personal\Desktop\Rhinec.txt";

                using (var fsc = new FileStream(filenamec, FileMode.Append, FileAccess.Write))
                {
                    using (var swc = new StreamWriter(fsc, Encoding.Default))
                    {
                        using (var fs = new FileStream(filename, FileMode.Create))
                        {
                            using (var sw = new StreamWriter(fs, Encoding.Default))
                            {
                                foreach (var item in query)
                                {
                                    line = "";
                                    line = item.DPYPRODNUM + "\t" + item.DPYREVSTATE + "\t" +
                                        item.MSN + "\t" + item.INCNUMBER + "\t" + item.INCNUMBER.ReverseByteOrder() + "\t" +
                                        item.DELIVERYNUMBER + "\t" +
                                        item.SUPPLYDELIVERYID + "\t" + item.DELIVERYID + "\t" + item.UNITID;
                                    sw.WriteLine(line);
                                    swc.WriteLine(line);
                                    count++;
                                }
                            }
                        }
                    }
                }

                return filename + ": " + count.ToString();

            }
            catch(Exception ex)
            {
                return "Exception: " + ex.Message;
            }

            
            
            
        }
    }
}
