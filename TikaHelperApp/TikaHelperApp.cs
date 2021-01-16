using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MXIC.EPR.TikaHelper;

namespace TikaHelperApp
{
    public class TikaHelperApp
    {
        public static void Main()
        {

            TikaHelper tika = new TikaHelper("tika-app-1.25.jar");

            DocumentInfo docInfo = tika.ParseDocument(@"Example\ch3.pdf");

            Console.WriteLine("Author: " + docInfo.Author);
            Console.WriteLine("CreationDate: " + docInfo.CreationDate);
            Console.WriteLine("LastModifiedDate: " + docInfo.LastModifiedDate);
            Console.WriteLine("LastSavedDate: " + docInfo.LastSavedDate);
            Console.WriteLine("WordCount: " + docInfo.WordCount);
            Console.WriteLine(new string('-',60));
            Console.WriteLine(docInfo.Content);
            Console.WriteLine();
            Console.WriteLine("ParseCost: " + docInfo.ParseCost);

            Console.ReadLine();
        }
    }
}
