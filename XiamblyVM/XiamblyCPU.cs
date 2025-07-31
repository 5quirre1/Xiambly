using System;
using System.Collections.Generic;

namespace XiamblyVM
{
    public enum Opcode
    {
        LOAD, STOR, PULL, DROP,
        BUMP, CLIP, ZAP,
        FWD, HOPZ, HOPNZ,
        EQUATE, SAY, HALT
    }

    public class Instruction
    {
        public Opcode Op;
        public string[] Args;

        public Instruction(Opcode op, string[] args)
        {
            Op = op;
            Args = args;
        }
    }

    public class XiamblyCPU
    {
        public int[] Registers = new int[8];
        public int PC = 0;
        public int SP = 0xFFFC;
        public int Flags = 0;
        public byte[] Memory = new byte[65536];
        Stack<int> Stack = new Stack<int>();

        public List<Instruction> Program = new();

        public void LoadProgram(List<Instruction> instructions)
        {
            Program = instructions;
        }

        public void Run()
        {
            while (PC < Program.Count)
            {
                var inst = Program[PC];
                switch (inst.Op)
                {
                    case Opcode.LOAD:
                        Registers[Reg(inst.Args[0])] = ParseVal(inst.Args[1]);
                        break;
                    case Opcode.STOR:
                        int addr = ParseVal(inst.Args[1]);
                        BitConverter.GetBytes(Registers[Reg(inst.Args[0])]).CopyTo(Memory, addr);
                        break;
                    case Opcode.PULL:
                        Stack.Push(Registers[Reg(inst.Args[0])]);
                        break;
                    case Opcode.DROP:
                        Registers[Reg(inst.Args[0])] = Stack.Pop();
                        break;
                    case Opcode.BUMP:
                        Registers[Reg(inst.Args[0])] += Registers[Reg(inst.Args[1])];
                        break;
                    case Opcode.CLIP:
                        Registers[Reg(inst.Args[0])] -= Registers[Reg(inst.Args[1])];
                        break;
                    case Opcode.ZAP:
                        Registers[Reg(inst.Args[0])] = 0;
                        break;
                    case Opcode.FWD:
                        PC = ParseVal(inst.Args[0]) - 1;
                        break;
                    case Opcode.HOPZ:
                        if ((Flags & 1) != 0) PC = ParseVal(inst.Args[0]) - 1;
                        break;
                    case Opcode.HOPNZ:
                        if ((Flags & 1) == 0) PC = ParseVal(inst.Args[0]) - 1;
                        break;
                    case Opcode.EQUATE:
                        Flags = Registers[Reg(inst.Args[0])] == Registers[Reg(inst.Args[1])] ? 1 : 0;
                        break;
                    case Opcode.SAY:
                        Console.WriteLine(Registers[Reg(inst.Args[0])]);
                        break;
                    case Opcode.HALT:
                        return;
                }
                PC++;
            }
        }

        int Reg(string r) => int.Parse(r[1].ToString());
        int ParseVal(string v)
        {
            if (v.StartsWith("r"))
                return Registers[Reg(v)];
            if (v.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                return Convert.ToInt32(v, 16);
            return int.Parse(v);
        }
    }
}
