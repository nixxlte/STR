mov bx, 27
loop:
int 0
int 16
cmp ax, bx
jz exit
jmp loop
exit:
HALT