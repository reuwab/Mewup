using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using OperatingSystem.Core;

namespace OperatingSystem
{
    public class Kernel : Sys.Kernel
    {
        public static string file;
        public static CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        protected override void BeforeRun()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[MSG] Attempting to Register Filesystem");
            Console.ResetColor();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[MSG] Filesystem Registered");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[MSG] Attempting to Start BootMan");
            Console.ResetColor();
            BootMan.Boot();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[MSG] BootMan Started");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[BootMan] Entering Text Mode");
            Console.ResetColor();
            Console.WriteLine("Mewup v0.1");
            Console.WriteLine("Copyright 2021 Reuben Butt");
            Core.CurrentDir.CurrentDirectory = @"0:\";
        }

        protected override void Run()
        {
            TextMode.Start();
        }
    }
}
