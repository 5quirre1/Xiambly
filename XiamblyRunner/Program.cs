using System;
using XiamblyVM;

namespace XiamblyRunner
{
    class Program
    {
        static void Main()
        {
            var cpu = new XiamblyCPU();
            var instructions = XiamblyAssembler.Assemble("program.xib");
            cpu.LoadProgram(instructions);
            cpu.Run();
        }
    }
}
