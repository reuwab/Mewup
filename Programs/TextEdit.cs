using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using OperatingSystem;

namespace OperatingSystem.Programs
{
    public static class globalvars
    {
        public static ArrayList filecontents;
        public static string fp;
    }
    class TextEdit : OperatingSystem.Core.Types.Program
    {
        public TextEdit()
        {
            this.Name = "TextEdit";
            this.Version = "0.1";
            this.Creator = "Reuben Butt";
            this.Category = "Text Editors";
        }
        public static void Run(string filepath)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine(filepath);
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ESC] [F1] New [F2] Save [F3] Open");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            var file = Sys.FileSystem.VFS.VFSManager.GetFile(filepath);
            var filestream = file.GetFileStream();
            if (filestream.CanRead)
            {
                byte[] text = new byte[filestream.Length];
                filestream.Read(text,0,(int)filestream.Length);
                Console.WriteLine(Encoding.Default.GetString(text));
                globalvars.fp = filepath;
            }
            Listen();
        }
        public static void Run()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ESC] [F1] New [F2] Save [F3] Open [F4] Edit Line");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Listen();
        }
        public static void Save()
        {
            if (File.Exists(globalvars.fp))
            {
                var file = Sys.FileSystem.VFS.VFSManager.GetFile(globalvars.fp);
                var filestream = file.GetFileStream();
                string full = string.Join(Environment.NewLine, globalvars.filecontents);
                byte[] text = Encoding.Default.GetBytes(full);
                filestream.Write(text,0,text.Length);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Enter filepath: ");
                Console.ResetColor();
                globalvars.fp = Console.ReadLine();
                Save();
            }
        }
        public static void Exit()
        {
            OperatingSystem.Core.TextMode.Start();
        }
        public static void Listen()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape){Exit();}
            else if (key.Key == ConsoleKey.F3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter Filepath:");
                Console.ResetColor();
                var filepath = Console.ReadLine();
                Run(filepath);
            }
            else if (key.Key == ConsoleKey.F1){Run();}
            else if (key.Key == ConsoleKey.F2){Save();}
            else if (key.Key == ConsoleKey.Enter){globalvars.filecontents.Add(Console.ReadLine());}
            else if (key.Key == ConsoleKey.F4)
            {
                Console.WriteLine("Enter line number: ");
                var ln = Console.ReadLine();
                Console.WriteLine(ln);
                Listen();
            }
        }
    }
}
