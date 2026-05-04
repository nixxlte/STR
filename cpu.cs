using System;
using Raylib_cs;

namespace STR
{
    class CPU
    {
        // registers definition
        public static Int16 AL = 0;
        public static Int16 AH = 1;
        public static Int16 BL = 2;
        public static Int16 BH = 3;
        public static Int16 CL = 4;
        public static Int16 CH = 5;
        public static Int16 DL = 6;
        public static Int16 DH = 7;
        public static Int16 AX = 8;
        public static Int16 BX = 9;
        public static Int16 CX = 10;
        public static Int16 DX = 11;

        public static Int16 pc = 0; // program counter
        public static Int16[] mem = new Int16[512]; // 512 bytes
        public static UInt16[] reg = new UInt16[4]; // general purpose registers
        public static bool ZF = false; // zero flag
        public static bool running = true;
        public static Int16 currentInstruction = 0;

        public static Int16 currentKey;

        private static void SetMem(Int16 reg, Int16 value)
        {
            mem[reg] = value;
        }

        private static Int16 GetMem(Int16 reg)
        {
            return mem[reg];
        }

        private static byte GetLow(int r)
        {
            return (Byte)(reg[r] & 0x00FF);
        }

        private static byte GetHigh(int r)
        {
            return (Byte)((reg[r] & 0xFF00) >> 8);
        }

        private static void SetLow(int r, byte value)
        {
            reg[r] = (UInt16)((reg[r] & 0xFF00) | value);
        }

        private static void SetHigh(int r, byte value)
        {
            reg[r] = (UInt16)((reg[r] & 0x00FF) | value << 8);
        }

        public static int GetRegister(int code)
        {
            switch (code)
            {
                case 0: return GetLow(0);  // AL
                case 1: return GetHigh(0); // AH
                case 2: return GetLow(1);  // BL
                case 3: return GetHigh(1); // BH
                case 4: return GetLow(2);  // CL
                case 5: return GetHigh(2); // CH
                case 6: return GetLow(3);  // DL
                case 7: return GetHigh(3); // DH

                case 8: return reg[0];     // AX
                case 9: return reg[1];     // BX
                case 10: return reg[2];    // CX
                case 11: return reg[3];    // DX

                default: {
                    RUN.error(1, true);
                    return 0;
                }
            }
        }

        public static void SetRegister(int code, int value)
        {
            switch (code)
            {
                case 0: SetLow(0, (byte)value); break;  // AL
                case 1: SetHigh(0, (byte)value); break; // AH
                case 2: SetLow(1, (byte)value); break;  // BL
                case 3: SetHigh(1, (byte)value); break; // BH
                case 4: SetLow(2, (byte)value); break;  // CL
                case 5: SetHigh(2, (byte)value); break; // CH
                case 6: SetLow(3, (byte)value); break;  // DL
                case 7: SetHigh(3, (byte)value); break; // DH

                case 8:  reg[0] = (UInt16)value; break;  // AX
                case 9:  reg[1] = (UInt16)value; break;  // BX
                case 10: reg[2] = (UInt16)value; break;  // CX
                case 11: reg[3] = (UInt16)value; break;  // DX

                default: {
                    RUN.error(2, true);
                    break;
                }
            }
        }

        public static void Update() {
            Raylib.InitWindow(800, 600, "STR Framebuffer");

            while (!Raylib.WindowShouldClose()) {

                Raylib.BeginDrawing();
                if (CPU.running) {
                    CPU.currentInstruction = (Int16)CPU.mem[CPU.pc];
                    
                    currentKey = (Int16)Raylib.GetKeyPressed();
                    Console.WriteLine(currentKey);

                    switch (CPU.currentInstruction) {
                        case 0: {break;}

                        case 1: { // load (reg, val)
                            int reg = CPU.mem[CPU.pc + 1];
                            Int16 val = CPU.mem[CPU.pc + 2];

                            CPU.SetRegister(reg, val);
                            CPU.pc += 3;
                            break;
                        }

                        case 2: { // add (reg, imm)
                            int r = CPU.mem[CPU.pc + 1];
                            int val = CPU.mem[CPU.pc + 2];

                            int result = CPU.GetRegister(r) + val;

                            CPU.SetRegister(r, result);
                            CPU.ZF = (result == 0);

                            CPU.pc += 3;
                            break;
                        }

                        case 3: { // JMP (addr)
                            int addr = CPU.mem[CPU.pc + 1];
                            CPU.pc = (Int16)addr;
                            break;
                        }

                        case 4: { // JZ (addr)
                            int addr = CPU.mem[CPU.pc + 1];

                            if (CPU.ZF) CPU.pc = (Int16)addr;
                            else CPU.pc += 2;

                            break;
                        }

                        case 5: { // CMP (reg, reg)
                            int r1 = CPU.mem[CPU.pc + 1];
                            int r2 = CPU.mem[CPU.pc + 2];

                            int val1 = CPU.GetRegister(r1);
                            int val2 = CPU.GetRegister(r2);

                            CPU.ZF = (val1 == val2);

                            CPU.pc += 3;
                            break;
                        }

                        case 99: { // HALT ()
                            Console.WriteLine("HALT! stopping");
                            Console.WriteLine($"last instruction before halt: {CPU.mem[CPU.pc - 1]}");
                            Console.WriteLine($"AX: 0x{CPU.reg[0]:X4}");
                            Console.WriteLine($"BX: 0x{CPU.reg[1]:X4}");
                            Console.WriteLine($"CX: 0x{CPU.reg[2]:X4}");
                            Console.WriteLine($"DX: 0x{CPU.reg[3]:X4}");

                            RUN.Exit();
                            CPU.running = false;
                            break;
                        }
                        default:
                            Console.WriteLine($"Unknown opcode: {CPU.currentInstruction}");
                            break;
                    }
                    Raylib.EndDrawing();

                    Thread.Sleep(RUN.DelayMs);
                }
            }
            if (Raylib.WindowShouldClose()) {
                if (CPU.running)
                {
                    CPU.running = false;
                    RUN.Exit();
                    Console.WriteLine("HALT! stopping");
                    Console.WriteLine($"last instruction before halt: {CPU.mem[CPU.pc - 1]}");
                    Console.WriteLine($"AX: 0x{CPU.reg[0]:X4}");
                    Console.WriteLine($"BX: 0x{CPU.reg[1]:X4}");
                    Console.WriteLine($"CX: 0x{CPU.reg[2]:X4}");
                    Console.WriteLine($"DX: 0x{CPU.reg[3]:X4}");

                }
            }
        }

    }
}