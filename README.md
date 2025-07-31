```markdown
# Xiambly

A cool little asm-style programming language made in C# for fun.

## Overview

Xiambly is a simple assembly-like virtual machine and assembler implemented in C#. It allows you to write programs in a custom assembly language, assemble them, and run them on a simulated CPU.

- **XiamblyAssembler.cs**: Parses and assembles `.xib` source files into executable instructions.
- **XiamblyCPU.cs**: Simulates a CPU with registers, memory, stack, and an instruction set.
- **Program.cs**: Entry point for running assembled Xiambly programs.

## Features

- 8 general-purpose registers
- 64KB memory
- Stack operations
- Branching and arithmetic instructions
- Console output

### Supported Instructions

| Mnemonic | Description                       |
|----------|-----------------------------------|
| load     | Load value into register          |
| stor     | Store register value to memory    |
| pull     | Push register value to stack      |
| drop     | Pop value from stack to register  |
| bump     | Add register values               |
| clip     | Subtract register values          |
| zap      | Zero a register                   |
| fwd      | Jump to instruction               |
| hopz     | Conditional jump if flag is set   |
| hopnz    | Conditional jump if flag is clear |
| equate   | Set flag if registers are equal   |
| say      | Print register value              |
| halt     | Stop execution                    |

## Usage

1. **Write a program** in Xiambly assembly and save as `program.xib`.  
   Example:
   
```
load r1, 10
load r2, 32
bump r1, r2
say r1
equate r1, r2
hopz 6
load r1, 99
say r1
halt
```