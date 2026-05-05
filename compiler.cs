using System;
using System.Collections.Generic;
using System.IO;

class Compiler {
    static Dictionary<string, byte> opcodes = new(StringComparer.OrdinalIgnoreCase) {
        { "MOV", 1 },
        { "JMP", 3 },
        { "JZ", 4 },
        { "CMP", 5 },
        { "HALT", 99 }
    };

    static Dictionary<string, byte> registers = new(StringComparer.OrdinalIgnoreCase) {
        { "AX", 8 },
        { "BX", 9 },
        { "CX", 10 },
        { "DX", 11 }
    };

    static Dictionary<string, int> labels = new(StringComparer.OrdinalIgnoreCase) {};

    public static void Compile(string input) {
        var lines = File.ReadAllLines(input);
        var program = new List<byte>();
        int address = 0;
        foreach (var line in lines)
        {
            var clean = line.Trim();
            if (string.IsNullOrEmpty(clean)) continue;

            if (clean.EndsWith(":")) {
                var label = clean.Replace(":", "");
                labels[label] = address;
                continue;
            }

            var parts = clean.Replace(",", "").Split(' ');
            var instr = parts[0].ToUpper();

            // Calculate instruction size
            switch (instr)
            {
                case "MOV": address += 3; break;
                case "CMP": address += 3; break;
                case "JZ": address += 2; break;
                case "JMP": address += 2; break;
                case "INT": address += 2; break;
                case "CLR": address += 2; break;
                case "HALT": address += 1; break;
            }
        }

        address = 0;
        foreach (var line in lines)
        {
            var clean = line.Trim();
            if (string.IsNullOrEmpty(clean)) continue;

            if (clean.EndsWith(":")) {
                continue;
            }

            var parts = clean.Replace(",", "").Split(' ');
            var instr = parts[0].ToUpper();

            switch (instr)
            {
                case "MOV":
                    program.Add(opcodes["MOV"]);
                    program.Add(registers[parts[1]]);
                    program.Add(byte.Parse(parts[2]));
                    break;

                case "CMP":
                    program.Add(opcodes["CMP"]);
                    program.Add(registers[parts[1]]);
                    program.Add(registers[parts[2]]);
                    break;

                case "JZ":
                    program.Add(opcodes["JZ"]);
                    program.Add((byte)labels[parts[1]]);
                    break;

                case "HALT":
                    program.Add(opcodes["HALT"]);
                    break;

                case "JMP":
                    program.Add(opcodes["JMP"]);
                    program.Add((byte)labels[parts[1]]);
                break;
            }
        }

        File.WriteAllBytes("output.str", program.ToArray());
        Console.WriteLine("COMPILED!");
    }
}