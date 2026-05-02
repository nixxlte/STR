namespace STR {
    class Code {
        public static void assembly() {
            RUN.MOV(CPU.AX, 7);
            RUN.MOV(CPU.BX, 5);
            // RUN.JZ(16);
            
            RUN.HALT();
            // main.str
        }
    }
}