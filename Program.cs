using System;
using System.IO;

namespace STR {
    class Program {
        public static int versionMajor = 0;
        public static int versionMinor = 1;

        public static bool IsCommand(string input, string full) {
            return input.Equals(full, StringComparison.OrdinalIgnoreCase);
        }

        static void ParseArgs(string[] args) {
            foreach (string arg in args)
            {
                switch (arg.ToLower())
                {
                    case "--compile":
                    {
                        if (args.Length != 2) {
                            Console.WriteLine("No .asm file supplied for compiling,\nUse: --compile <file.str>");
                            break;
                        }

                        var file = args[1];

                        if (!File.Exists(file)) {
                            Console.WriteLine("The file does not exist!");
                            break;
                        }

                        // just my opinion, but better not check for .S files,
                        // not common with 8086 assembly.
                        if (!file.ToLower().EndsWith(".asm")) {
                            Console.WriteLine("This isn\'t a .asm file! Rename it.");
                            break;
                        }
                        
                        Compiler.Compile(file);
                        break;
                    }
                    case "--help":
                    {
                        Console.WriteLine("str - stupidly simple cpu emulator");
                        Console.WriteLine();

                        Console.WriteLine("Usage:");
                        Console.WriteLine("  str --compile <file.asm>    Compile an ASM file into a STR binary");
                        Console.WriteLine("  str <file.str>              Run a STR program");
                        Console.WriteLine();

                        Console.WriteLine("Options:");
                        Console.WriteLine("  --help                      Show this help message");
                        Console.WriteLine("  --version                   Show version information");

                        break;
                    }

                    case "--version":
                    {
                        Console.WriteLine($"str version {versionMajor}.{versionMinor}");
                        Console.WriteLine("a simple cpu emulator written in c#");
                        Console.WriteLine();
                        Console.WriteLine("made by nixxlte and raice");
                        break;
                    }

                    default:
                    {
                        if (arg.ToLower().EndsWith(".str")) {
                            if (File.Exists(arg)) {
                                var bytes = File.ReadAllBytes(arg);
                                for (int i = 0; i < bytes.Length; i++) { // load the code on the memory
                                    CPU.mem[i] = bytes[i];
                                    Console.WriteLine(bytes[i]);
                                }

                                CPU.Initialize();
                            }
                        } else {
                            Console.WriteLine($"Unknown argument: {arg}");
                        }
                        break;
                    }
                }
            }
        }

        static void Main(string[] args) {
            ParseArgs(args);

            // HACK: quick hack for checking if user opened via consolew
            // im NOT proud of this :cry:
            var (left, top) = Console.GetCursorPosition();
            if (left==0 && top==0)
            {
                Thread.Sleep(3000); // waiting 3 seconds for user to read in case of errors
            }
        }
    }
}