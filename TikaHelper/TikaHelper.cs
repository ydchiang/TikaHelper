using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace MXIC.EPR.TikaHelper
{
    public class TikaHelper
    {
        public string TikaJarFile { get; private set; }

        public TikaHelper(string tikaJarFile)
        {
            if (File.Exists(tikaJarFile))
            {
                TikaJarFile = tikaJarFile;
            }
            else
            {
                throw new FileNotFoundException("Tika jar file: " + tikaJarFile + " does not exist.");
            }
        }

        public DocumentInfo ParseDocument(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName + " does not exist.");
            }

            DocumentInfo documentInfo = null;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Parsing Document Metadata
            ProcessStartInfo psi = new ProcessStartInfo("java", "-jar " + TikaJarFile + " -j " + fileName);
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            using (Process p = Process.Start(psi))
            {
                string output = p.StandardOutput.ReadToEnd();
                documentInfo = JsonConvert.DeserializeObject<DocumentInfo>(output);
            }

            // Parsing Document Content
            psi = new ProcessStartInfo("java", "-jar " + TikaJarFile + " -t " + fileName);
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            using (Process p = Process.Start(psi))
            {
                string output = p.StandardOutput.ReadToEnd();
                documentInfo.Content = output;
            }

            sw.Stop();

            documentInfo.ParseCost = sw.ElapsedMilliseconds;

            return documentInfo;
        }
    }
}
