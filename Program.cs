using System;
using System.IO;

namespace STR {
    class Program {

        public static bool IsCommand(string input, string full) {
            return input.Equals(full, StringComparison.OrdinalIgnoreCase);
        }

        static void Main(string[] args) {
            var cmd = args.FirstOrDefault() ?? "";
            if (args[0] != string.Empty && args[0].ToLower() != "--compile") {
                //Compiler.Compile();

                var bytes = File.ReadAllBytes(args[0]);
                for (int i = 0; i < bytes.Length; i++) { // load the code on the memory
                    CPU.mem[i] = bytes[i];
                    Console.WriteLine(bytes[i]);
                }

                // little program to:
                // ax = 0, bx = 5
                // while ax != 5, ax++

                // AX = 0
                //CPU.mem[0] = 1;
                //CPU.mem[1] = CPU.AX; 8
                //CPU.mem[2] = 7;

                // BX = 5
                //CPU.mem[3] = 1;
                //CPU.mem[4] = CPU.BX; 9
                //CPU.mem[5] = 5;

                // CMP AX, BX
                //CPU.mem[6] = 5;
                //CPU.mem[7] = CPU.AX; 8 
                //CPU.mem[8] = CPU.BX; 9

                // JZ -> HALT
                //CPU.mem[9] = 4;
                //CPU.mem[10] = 16;

                // AX = AX + 1
                //CPU.mem[11] = 2;
                //CPU.mem[12] = CPU.AX;
                //CPU.mem[13] = 1;

                // JMP loop
                //CPU.mem[14] = 3;
                //CPU.mem[15] = 6;

                // HALT
                //CPU.mem[16] = 99;

                //RUN.INT(10);

                CPU.Update();
            } else if (IsCommand(cmd, "--compile")) {
                Compiler.Compile(args[1]);
            } else {
                Console.WriteLine("Please enter a file to run in STR cpu");
                Thread.Sleep(5000);
            }
        }
    }
}