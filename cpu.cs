using System;

namespace STR
{
    class CPU
    {
        public static Int16 pc = 1; // program counter
        public static int[] mem = new int[256]; // 256 bytes
        public static int[] reg = new int[16]; // 16 registers
        public static bool running = true;
        public static Int16 cycles = 0;
        public static Int16 currentInstruction = 0;

        public static void SetMem(Int16 reg, Int16 value)
        {
            mem[reg] = value;
        }
    }
}