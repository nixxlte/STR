using System;
using System.IO;

namespace STR {
    class Program {

        public static bool IsCommand(string input, string full) {
            return input.Equals(full, StringComparison.OrdinalIgnoreCase);
        }

        static void Main(string[] args) {
            var cmd = args.FirstOrDefault() ?? "";
            if (args[0] != string.Empty) {
                CPU.mem[0] = 1; // load
                CPU.mem[1] = 1; // reg 1
                CPU.mem[2] = 50; // value reg 1

                CPU.mem[3] = 1; // load
                CPU.mem[4] = 2; // reg 2
                CPU.mem[5] = 404; // value reg 2

                CPU.mem[6] = 2; // add
                CPU.mem[7] = 1; // reg 1
                CPU.mem[8] = 2; // reg 2

                CPU.mem[9] = 99; // halt
                CPU.pc = 0;
                CPU.running = true;
                RUN.main();
                
                // reg 1 = 50
                // reg 2 = 404
            } else {
                Console.WriteLine("Please enter a file to run in STR cpu");
                Thread.Sleep(5000);
            }
        }
    }
}