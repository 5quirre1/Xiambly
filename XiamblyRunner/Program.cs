using System;
using System.IO;
using XiamblyVM;

namespace XiamblyRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            try
            {
                var command = args[0].ToLower();

                switch (command)
                {
                    case "run":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("error: missing filename");
                            ShowHelp();
                            return;
                        }
                        RunProgram(args[1]);
                        break;

                    case "help":
                        ShowHelp();
                        break;

                    case "version":
                        ShowVersion();
                        break;

                    default:
                        Console.WriteLine($"unknown command: {command}");
                        ShowHelp();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }

        static void RunProgram(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"error: file not found - {filename}");
                return;
            }

            try
            {
                var cpu = new XiamblyCPU();
                var instructions = XiamblyAssembler.Assemble(filename);
                cpu.LoadProgram(instructions);

                Console.WriteLine($"running {filename}...");
                cpu.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"runtime error: {ex.Message}");
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("xiambly - cool language made by steve");
            Console.WriteLine("usage: xiambly [command] [options]");
            Console.WriteLine();
            Console.WriteLine("commands:");
            Console.WriteLine("  run <filename>    run a program");
            Console.WriteLine("  help              show ts message");
            Console.WriteLine("  version           display version");
        }

        static void ShowVersion()
        {
            Console.WriteLine("xiambly v1.1 (prob)");
        }
    }
}
