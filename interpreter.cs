using System;

namespace STR
{
    class RUN
    { //MOV EAX, 5
        public static int DelayMs = 50;

        public static void error(int code, bool fatal) {
            Console.Write("ERROR! ");
            switch (code)
            {
                case 1: { // getting invalid register
                    Console.WriteLine("Cannot get invalid register");
                    break;
                }

                case 2: { // setting invalid register
                    Console.WriteLine("Cannot set invalid register");
                    break;
                }
            }

            if (fatal)
            {
                Console.Write("Fatal\n");
                Thread.Sleep(5000);
                Environment.Exit(0);
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
                        Int16 val = CPU.mem[CPU.pc + 2];

                        CPU.SetRegister(reg, val);
                        CPU.pc += 3;
                        break;
                    }

                    case 2: // add (reg, imm)
                    {
                        int r = CPU.mem[CPU.pc + 1];
                        int val = CPU.mem[CPU.pc + 2];

                        int result = CPU.GetRegister(r) + val;

                        CPU.SetRegister(r, result);
                        CPU.ZF = (result == 0);

                        CPU.pc += 3;
                        break;
                    }

                    case 3: // JMP (addr)
                    {
                        int addr = CPU.mem[CPU.pc + 1];
                        CPU.pc = (Int16)addr;
                        break;
                    }

                    case 4: // JZ (addr)
                    {
                        int addr = CPU.mem[CPU.pc + 1];

                        if (CPU.ZF) CPU.pc = (Int16)addr;
                        else CPU.pc += 2;

                        break;
                    }

                    case 5: // CMP (reg, reg)
                    {
                        int r1 = CPU.mem[CPU.pc + 1];
                        int r2 = CPU.mem[CPU.pc + 2];

                        int val1 = CPU.GetRegister(r1);
                        int val2 = CPU.GetRegister(r2);

                        CPU.ZF = (val1 == val2);

                        CPU.pc += 3;
                        break;
                    }

                    case 99: // HALT ()
                    {
                        Console.WriteLine("HALT! stopping");
                        Console.WriteLine($"last instruction before halt: {CPU.mem[CPU.pc - 1]}");
                        Console.WriteLine($"AX: 0x{CPU.reg[0]:X4}");
                        Console.WriteLine($"BX: 0x{CPU.reg[1]:X4}");
                        Console.WriteLine($"CX: 0x{CPU.reg[2]:X4}");
                        Console.WriteLine($"DX: 0x{CPU.reg[3]:X4}");

                        CPU.running = false;
                        break;
                    }
                    default:
                        Console.WriteLine($"Unknown opcode: {CPU.currentInstruction}");
                        break;
                }
                Thread.Sleep(RUN.DelayMs);
            }
        }

        // mov(EAX, 10)        
        public static void mov(Int16 reg, Int16 val) {
            CPU.reg[reg] = val;
        }

        public static void INT(Int16 interrupt) {
            switch (interrupt) {
                case 0: // print reg
                    Console.WriteLine(CPU.GetRegister(0));
                    break;
                default:
                    error(998, false);
                    break;
            }
        }
    }
}