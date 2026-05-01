# STR:  A Custom CPU in C#
A little CPU made in C# running ASM

### ISA (Instruction Set Architecture)
- `LOAD (reg, val)` = `1` - Loads `val` in to `reg`
- `ADD (reg, imm)` = `2` - Adds `imm` to `reg` (addition)
- `JMP (addr)` = `3` - Sets the pc(program counter) to `addr`
- `JZ (addr)` = `4` - IF ZF equals zero, jump to addr
- `CMP (reg, reg)` = `5` - Compares `reg` with `reg` (then sets ZF)
- `CLR (val)` = `6` - Sets registers and `reg + 1` to `val`
- `HALT ()` = `99` - Halts the CPU

#### Credits:
[raic.e](https://github.com/RiceTheDev/): CPU and Raylib functions (framebuffer)<br>
[nixxlte](https://github.com/nixxlte): Assembly 8086 interpreter and Raylib functions (keyboard)