using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text;

//////using Aspose.Pdf;
//////using Aspose.Pdf.Facades;

namespace Utilities
{
    public class PrintPDF
    {
        public class PrintPDFArgs
        {
            public string filename;
            public int ncopies;
            public string printername;
            public bool duplex;
            public bool tumble;

        }
        public static void doitThread(string filename, int ncopies, string printername, bool duplex, bool tumble)
        {
            PrintPDFArgs args = new PrintPDFArgs();
            args.filename = filename;
            args.ncopies = ncopies;
            args.printername = printername;
            args.duplex = duplex;
            args.tumble = tumble;
            Thread t = new Thread(doitArgs);
            t.IsBackground = false; // We want print job to finish even if main thread exits
            t.Start(args);
        }
        public static void doitArgs(object _args)
        {
            PrintPDFArgs args = (PrintPDFArgs)_args;
            doit(args.filename, args.ncopies, args.printername, args.duplex, args.tumble);
        }
        public static void doit(string filename, int ncopies, string printername, bool duplex, bool tumble)
        {
            //////Aspose.Pdf.License license = new Aspose.Pdf.License();
            //////license.SetLicense(@"C:\Program Files (x86)\Aspose\Aspose.Pdf for .NET\Bin\net4.0\Aspose.Pdf.lic");


            ////////create PdfViewer object
            //////PdfViewer viewer = new PdfViewer();

            ////////open input PDF file
            //////viewer.OpenPdfFile(filename);

            ////////set attributes for printing
            //////viewer.AutoResize = true;         //print the file with adjusted size
            //////viewer.AutoRotate = true;         //print the file with adjusted rotation
            //////viewer.PrintPageDialog = false;   //do not produce the page number dialog when printing

            ////////create objects for printer and page settings and PrintDocument
            //////System.Drawing.Printing.PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();
            //////System.Drawing.Printing.PageSettings pgs = new System.Drawing.Printing.PageSettings();
            //////System.Drawing.Printing.PrintDocument prtdoc = new System.Drawing.Printing.PrintDocument();

            ////////set printer name
            //////ps.PrinterName = printername;

            ////////set PageSize (if required)
            //////pgs.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);

            ////////set PageMargins (if required)
            //////pgs.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

            //////ps.Copies = (short)ncopies;

            ////////print document using printer and page settings
            //////viewer.PrintDocumentWithSettings(pgs, ps);

            ////////close the PDF file after priting
            //////viewer.ClosePdfFile();

            string gsbin = @"C:\Program Files (x86)\gs\gs9.06\bin\gswin32c.exe";

            string tmpfile = Path.GetTempFileName();

            Console.WriteLine("tmpfile " + tmpfile);

            string helperfile = 
@"mark
/Duplex {0}
/Tumble {1}
/NumCopies {2}
(pxlcolor) finddevice
putdeviceprops
";
            helperfile = string.Format(helperfile, duplex ? "true" : "false", tumble ? "true" : "false", ncopies);
            Console.WriteLine("helperfile = " + helperfile);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(tmpfile))
            {
                file.WriteLine(helperfile);
            }

            string args = @"-q -dBATCH -sDEVICE=pxlcolor -sPAPERSIZE=a4 -dPDFFitPage -dPrinted  -sOutputFile=""{0}"" -dBATCH -dNOPROMPT -f ""{1}"" ""{2}""";
            args = string.Format(args, printername, tmpfile, filename);

            Console.WriteLine("args = " + args);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = gsbin;
            psi.Arguments = args;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;

            psi.RedirectStandardInput = true;
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();
            using (StreamWriter sw = p.StandardInput)
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
            }
            p.WaitForExit();
            File.Delete(tmpfile);
        }
        public static void Main(string[] args)
        {
            string filename = @"P:\OurProperties\F5 110 Samuel St\tenancies\R2\SarahDarkwah\deposit_protection.pdf";
            int ncopies = 1;
            string printername = @"\\HPMAIN\LaserPrinter";
            bool duplex = true;
            bool tumble = false;
            doitThread(filename, ncopies, printername, duplex, tumble);
        }
    }
}
