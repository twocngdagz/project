using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using aejw.Network;

namespace Utilities
{
    public static class SharedDrives
    {
        public static void connectShares()
        {
            NetworkDrive oNetDrive = new NetworkDrive();
            try
            {
                //set propertys
                oNetDrive.Force = false;
                oNetDrive.Persistent = true;
                oNetDrive.LocalDrive = "P:";
                oNetDrive.PromptForCredentials = false;
                oNetDrive.ShareName = @"\\erwin\property"; ;
                oNetDrive.SaveCredentials = false;

                oNetDrive.MapDrive("<user>", "<password>");
            }
            catch (Exception)
            {
                // Console.WriteLine("e {0}", e.Message);
                // Probably already mapped
            }
            oNetDrive = null;
        }
    }
}
