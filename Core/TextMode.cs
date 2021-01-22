using System;
using System.Collections.Generic;
using System.Text;
using Cosmos;
using Sys = Cosmos.System;
using OperatingSystem.Programs;
using System.IO;
using System.Linq;
using OperatingSystem.Core;

namespace OperatingSystem.Core
{
    public static class CurrentDir
    {
        public static string CurrentDirectory;
        public static string[] Dirl;
        public static bool hasf;
    }
    public class TextMode
    {
        public static void Start()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("this.com");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" " + CurrentDir.CurrentDirectory + "#>");
                Console.ResetColor();
                var cmd = Console.ReadLine();
                CurrentDir.Dirl = cmd.Split(' ');
                try
                {
                    var directory_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(CurrentDir.CurrentDirectory + CurrentDir.Dirl[1]);
                    foreach (var directoryEntry in directory_list)
                    {
                        CurrentDir.hasf = true;
                    }
                }
                catch
                {
                    CurrentDir.hasf = false;
                }
                if (cmd.StartsWith("cd"))
                {
                    if (CurrentDir.Dirl[1] == "[special]")
                    {
                        CurrentDir.CurrentDirectory = @"0:\";
                    }
                    else
                    {
                        CurrentDir.CurrentDirectory = CurrentDir.CurrentDirectory + CurrentDir.Dirl[1] + "/";
                    }
                    Start();
                }
                else if (cmd == "clr" || cmd == "clear")
                {
                    Console.Clear();
                    Start();
                }
                else if (cmd == "dir")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Contents of: " + CurrentDir.CurrentDirectory);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine();
                    Console.WriteLine("File Name    Size");
                    Console.ResetColor();
                    var dirl = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(CurrentDir.CurrentDirectory);
                    foreach (var directoryEntry in dirl)
                    {
                        var file_stream = directoryEntry.GetFileStream();
                        var entry_type = directoryEntry.mEntryType;
                        if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                        {
                            byte[] content = new byte[file_stream.Length];
                            file_stream.Read(content, 0, (int)file_stream.Length);
                            Console.WriteLine(directoryEntry.mName + " " + directoryEntry.mSize);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine(directoryEntry.mName);
                        }
                    }
                    Start();
                }
                else if (cmd.StartsWith("mkdir"))
                {
                    Sys.FileSystem.VFS.VFSManager.CreateDirectory(CurrentDir.CurrentDirectory + CurrentDir.Dirl[1]);
                    Start();
                }
                else if (cmd == "shutdown" || cmd == "poweroff")
                {
                    Cosmos.System.Power.Shutdown();
                }
                else if (cmd == "reboot" || cmd == "rb")
                {
                    Cosmos.System.Power.Reboot();
                }
                else if (cmd.StartsWith("del"))
                {
                    try { Sys.FileSystem.VFS.VFSManager.DeleteDirectory(CurrentDir.Dirl[1], CurrentDir.hasf); }
                    catch { Sys.FileSystem.VFS.VFSManager.DeleteFile(CurrentDir.Dirl[1]); }
                    Start();
                }
                else if (cmd == "MIV")
                {
                    Programs.MIV.StartMIV();
                }
                else if (cmd.StartsWith("edit"))
                {
                    if (File.Exists(CurrentDir.CurrentDirectory + CurrentDir.Dirl[1]))
                    {
                        Programs.TextEdit.Run(CurrentDir.CurrentDirectory + CurrentDir.Dirl[1]);
                    }
                    else
                    {
                        Sys.FileSystem.VFS.VFSManager.CreateFile(CurrentDir.CurrentDirectory + CurrentDir.Dirl[1]);
                        Programs.TextEdit.Run(CurrentDir.CurrentDirectory + CurrentDir.Dirl[1]);
                    }
                }
                else if (cmd.StartsWith("pkgman"))
                {
                    if (CurrentDir.Dirl[1] == "list")
                    {
                        foreach (Core.Types.Program program in Core.PkgMan.globalvars.Pkgs)
                        {
                            Console.WriteLine(program.Name + " by " + program.Creator + ", " + program.Version + "; " + program.Category);
                        }
                    }
                    else
                    {
                        // Do Nothing
                    }
                    Start();
                }
                else
                {
                    Console.WriteLine("Command Not Found!");
                    Start();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] " + e);
                Console.ResetColor();
                Start();
            }
        }
    }
}