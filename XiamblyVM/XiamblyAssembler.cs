using System;
using System.Collections.Generic;
using System.IO;

namespace XiamblyVM
{
    public class XiamblyAssembler
    {
        static Dictionary<string, Opcode> opMap = new() {
            { "load", Opcode.LOAD },
            { "stor", Opcode.STOR },
            { "pull", Opcode.PULL },
            { "drop", Opcode.DROP },
            { "bump", Opcode.BUMP },
            { "clip", Opcode.CLIP },
            { "zap", Opcode.ZAP },
            { "fwd", Opcode.FWD },
            { "hopz", Opcode.HOPZ },
            { "hopnz", Opcode.HOPNZ },
            { "equate", Opcode.EQUATE },
            { "say", Opcode.SAY },
            { "halt", Opcode.HALT },
        };

        public static List<Instruction> Assemble(string path)
        {
            var lines = File.ReadAllLines(path);
            return Assemble_Code(lines);
        }

        public static List<Instruction> Assemble_Code(string[] code)
        {
            var program = new List<Instruction>();
            foreach (var line in code)
            {
                var clean = line.Split(';')[0].Trim();
                if (string.IsNullOrWhiteSpace(clean)) continue;
                var parts = clean.Split(' ', 2);
                var opcode = parts[0].ToLower();
                var args = parts.Length > 1 ? parts[1].Split(',', StringSplitOptions.TrimEntries) : new string[0];
                if (!opMap.ContainsKey(opcode)) throw new Exception($"Unknown instruction: {opcode}");
                program.Add(new Instruction(opMap[opcode], args));
            }
            return program;
        }
    }
}
