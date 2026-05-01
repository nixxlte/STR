using System;

namespace STR
{
    class RUN
    { //MOV EAX, 5
        public static int DelayMs = 50;

        public static void error(int code, bool fatal) {
            Console.WriteLine("ERROR! code: ", code.ToString());
            Console.Write("Error is: ");
            switch (fatal) {
                case true:
                    Console.Write("Fatal\n");
                    Thread.Sleep(5000);
                    Environment.Exit(0);
                    break;
                case false:
                    Console.Write("Not fatal\n");
                    break;
            }
        }

        public static void main() {
            while (CPU.running)
            {                
                // sets the instruction 
                CPU.currentInstruction = (Int16)CPU.mem[CPU.pc];
                switch (CPU.currentInstruction)
                {
                    case 1: // load (reg, val)
                    {
                        int reg = CPU.mem[CPU.pc + 1];
                        int val = CPU.mem[CPU.pc + 2];

                        CPU.reg[reg] = val;
                        CPU.pc += 3;
                        break;
                    }

                    case 2: // add (reg, reg)
                    {
                        int r1 = CPU.mem[CPU.pc + 1];
                        int r2 = CPU.mem[CPU.pc + 2];

                        CPU.reg[r1] += CPU.reg[r2];
                        CPU.pc += 3;
                        break;
                    }

                    case 99: // HALT ()
                    {
                        Console.WriteLine("HALT! stopping");
                        Console.WriteLine($"last instruction before halt: {CPU.mem[CPU.pc - 1]}");
                        Console.WriteLine($"r1: {CPU.reg[1]}");
                        Console.WriteLine($"r2: {CPU.reg[2]}");

                        CPU.running = false;
                        break;
                    }
                    default:
                        error(999, true);
                        break;
                }
                Thread.Sleep(RUN.DelayMs);
            }
        }

        // mov(EAX, 10)        
        public static void mov(int var, int val) {
            var = val;
        }

        public static void INT(float val) {
            if (val == 10f) {
                
            } 
            else {
                error(1, true);
            }
        }
    }
}