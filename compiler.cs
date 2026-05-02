using System;
using System.IO;

namespace STR {
    class Compiler {
        public static void Compile() {
            byte[] program = new byte[] {
                1, 8, 7,
                1, 9, 5,
                5, 8, 9,
                4, 16,
                2, 8, 1,
                3, 6,
                99
            };

            File.WriteAllBytes("program.str", program);
        }
    }
}

