/**
* (C) Copyright 2000-2005 SAP AG Walldorf
*
* Author: SAP AG, Security Development
*
* SAP AG DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE,
* INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS, IN NO
* EVENT SHALL SAP AG BE LIABLE FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL
* DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR
Java and .NET Code Samples for SAP
Logon Ticket Verification
© 2005 SAP AG 6
* PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS
* ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE
* OF THIS SOFTWARE.
*
*/
using System;
using System.IO;
using System.Reflection;

namespace sapssoext
{
    /// <summary>
    /// Example class for SAPSSOEXT library implemented in CSharp (C#).
    ///
    /// Compile this class with .Net compiler:
    /// csc ssosample.cs
    ///
    /// This class performs the calls via Reflection because no further
    /// references have to be set, but the COM component in SAPSSOEXT must
    /// be registered to the Windows registry:
    /// 1) ensure that "regsvr32 sapssoext.dll" component registration was done
    /// 2) if a null library property (Cryptlib) is passed, the environment
    /// variable SSF_LIB is taken.
    /// </summary>
    public class SSO2Ticket
    {
        // Constant definitions for
        // ParseCertificate function
        const long ISSUER_CERT_SUBJECT = 0;
        const long ISSUER_CERT_ISSUER = 1;
        const long ISSUER_CERT_SERIALNO = 2;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // plausi check
            if (getCommandParam(args, "-i") == null)
            {
                PrintHelp();
                return;
            }
            Object[] RetArray;
            Type MyType;
            Object MyObj;
            string subject;
            string issuer;
            string ticket;
            try
            {
                // get type from registry and initiate an instance of it

                MyType = Type.GetTypeFromProgID("SAPSSOEXT.SSO2Ticket", true);
                MyObj = Activator.CreateInstance(MyType);
                // read the ticket from a File
                ticket = getTicket(getCommandParam(args, "-i"));
                // build parameters for fuction calls
                Object[] seclib = { getCommandParam(args, "-L") };
                Object[] parms = { ticket, getFullFilePath(getCommandParam(args, "-p")), null };
                // invoke the first call: set the property CryptLib
                MyType.InvokeMember("CryptLib",
                System.Reflection.BindingFlags.SetProperty, null, MyObj, seclib);
                // invoke the main method to check the ticket
                RetArray = (Object[])MyType.InvokeMember("EvalLogonTicket",
                System.Reflection.BindingFlags.InvokeMethod, null, MyObj, parms);
                // build parameters for cert parser
                Object[] parms1 = { RetArray[3], ISSUER_CERT_SUBJECT };
                // invoke ParseCertificat
                subject = (string)MyType.InvokeMember("ParseCertificate",
                System.Reflection.BindingFlags.InvokeMethod, null, MyObj, parms1);
                // build parameters for cert parser
                Object[] parms2 = { RetArray[3], ISSUER_CERT_ISSUER };
                // invoke ParseCertificat
                issuer = (string)MyType.InvokeMember("ParseCertificate",
                System.Reflection.BindingFlags.InvokeMethod, null, MyObj, parms2);
                // Finally print out all parameter from the ticket:
                // RetArray(0) is the user name
                // RetArray(1) is the client of the issuing system
                // RetArray(2) is the id of the issuing system
                // RetArray(3) is the X.509 certificate of the issuing system
                // The "certificate" object is a Base64 (PEM) encoded X.509 certificate.
                //
                PrintResults((string)RetArray[0],
                (string)RetArray[1],
                (string)RetArray[2],
                subject,
                issuer,
                ticket);
            } // error related to COM
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (ex.ErrorCode == -2147221005)
                {
                    Console.WriteLine(
                    "Register object SAPSSOEXT.SSO2Ticket:\nregsvr32 sapssoext.dll");
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            } // the inner exception means the error within SAPSSOEXT
            catch (System.Reflection.TargetInvocationException fex)
            {
                Console.WriteLine(fex.InnerException.Message);
            } // catch the rest of possible exceptions
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        // print the parameters from ticket
        static void PrintResults(string user, string sysid, string client,
        string subject, string issuer, string ticket)
        {
            Console.WriteLine("***********************************************");
            Console.WriteLine(" Output of program:");
            Console.WriteLine("***********************************************");
            Console.Write("\n");
            Console.Write("The ticket\n\n" + ticket + "\n");
            Console.WriteLine("was successfully validated.");
            Console.WriteLine("User : " + user);
            Console.WriteLine("Ident of ticket issuing system:");
            Console.WriteLine("Sysid : " + sysid);
            Console.WriteLine("Client : " + client);
            Console.WriteLine("Certificate data of issuing system:");
            Console.WriteLine("Subject : " + subject);
            Console.WriteLine("Issuer : " + issuer);
            Console.Write("\n");
        }
        // read the ticket string from a File
        public static String getTicket(string filename)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(filename))
                {
                    String line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                throw new FieldAccessException(
                "File " + filename + " could not be read", e);
            }

        }
        // parse the arguments for an option
        public static String getCommandParam(string[] args, string option)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals(option) && args.Length > i + 1)
                {
                    return args[i + 1];
                }
            }
            return null;
        }
        // print help to console
        public static void PrintHelp()
        {
            Console.WriteLine(" ssosample -i <ticket_file> [-L <SSF_LIB>]");
            Console.WriteLine(" [-p <file containing public key>]");
        }
        // get the full path to a file
        public static String getFullFilePath(string filename)
        {
            String path;
            if (Path.HasExtension(filename))
            {
                path = Path.GetFullPath(filename);
            }
            else
            {
                path = Path.GetFullPath(filename + ".pse");
            }
            if (!File.Exists(path))
                throw new FileNotFoundException(
                "File " + filename + " does not exists", filename);
            return path;
        }
    }
}