using System;
using System.Collections.Generic;
using System.IO;

// Code by NixxLTE -w-

class Compiler {
    static Dictionary<string, byte> opcodes = new(StringComparer.OrdinalIgnoreCase) {
        { "MOV", 1 },
        { "JMP", 3 },
        { "JZ", 4 },
        { "CMP", 5 },
        { "CLR", 6 },
        { "INT", 7 },
        { "ADD", 8 },
        { "PUSH", 9 },
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
        var binary = new List<byte>();
        int address = 0;
        foreach (var line in lines) {
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
            switch (instr) {
                case "MOV": address += 3;  break;
                case "CMP": address += 3;  break;
                case "JZ": address += 2;   break;
                case "JMP": address += 2;  break;
                case "INT": address += 2;  break;
                case "CLR": address += 2;  break;
                case "ADD": address += 3;  break;
                case "PUSH": address += 2; break;
                case "HALT": address += 1; break;
            }
        }

        address = 0;
        foreach (var line in lines) {
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
                    binary.Add(opcodes["MOV"]);
                    binary.Add(registers[parts[1]]);
                    binary.Add(byte.Parse(parts[2]));
                    break;

                case "CMP":
                    binary.Add(opcodes["CMP"]);
                    binary.Add(registers[parts[1]]);
                    binary.Add(registers[parts[2]]);
                    break;

                case "JZ":
                    binary.Add(opcodes["JZ"]);
                    binary.Add((byte)labels[parts[1]]);
                    break;

                case "HALT":
                    binary.Add(opcodes["HALT"]);
                    break;

                case "JMP":
                    binary.Add(opcodes["JMP"]);
                    binary.Add((byte)labels[parts[1]]);
                    break;

                case "CLR":
                    binary.Add(opcodes["CLR"]);
                    binary.Add(byte.Parse(parts[1]));
                    break;

                case "INT":
                    binary.Add(opcodes["INT"]);
                    binary.Add(byte.Parse(parts[1]));
                    break;

                case "ADD":
                    binary.Add(opcodes["ADD"]);
                    binary.Add(byte.Parse(parts[1]));
                    binary.Add(byte.Parse(parts[2]));
                    break;

                case "PUSH":
                    binary.Add(opcodes["PUSH"]);
                    binary.Add(byte.Parse(parts[1]));
                    break;
            }
        }

        File.WriteAllBytes("output.str", binary.ToArray());
        Console.WriteLine("COMPILED!");
    }
}