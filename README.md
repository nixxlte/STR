# STR:  A Custom CPU in C#
A little CPU made in C# running ASM

### ISA (Instruction Set Architecture)
- `LOAD (reg, val)` = `1`
- `ADD (reg, imm)` = `2`
- `JMP (addr)` = `3`
- `JZ (addr)` = `4`
- `CMP (reg, reg)` = `5`
- `HALT ()` = `99`

#### Credits:
[raic.e](https://github.com/RiceTheDev/): CPU<br>
[nixxlte](https://github.com/nixxlte): Assembly interpreter