namespace STR {
    class Code {
        public static void assembly() {
            RUN.MOV(CPU.AL, 2);
            RUN.INT(10);
            RUN.HALT();
            // main.str
        }
    }
}