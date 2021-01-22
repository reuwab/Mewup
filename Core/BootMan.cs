using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using OperatingSystem.Core;

namespace OperatingSystem.Core
{
    class BootMan
    {
        public static void Boot()
        {
            // Try and see if this file exists. If it does, it's not the first boot
            try
            {
                var fbswitch = Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\fb.swi");

                if (Sys.FileSystem.VFS.VFSManager.FileExists(fbswitch)==true)
                {
                    return;
                }
            }
            catch (Exception e)
            {
                // Start the First Boot process
                Sys.FileSystem.VFS.VFSManager.CreateFile(@"0:\fb.swi");
                return;
            }
        }
    }
}
