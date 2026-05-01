using System;

namespace STR
{
    class RUN
    { //MOV EAX, 5
        public static int DelayMs = 50;

        public static void error(int code, bool fatal) {
            Console.Write("ERROR! ");
            switch (code)
            {
                case 1: { // getting invalid register
                    Console.WriteLine("Cannot get invalid register");
                    break;
                }
                case 2: { // setting invalid register
                    Console.WriteLine("Cannot set invalid register");
                    break;
                }
                case 998: {
                    Console.WriteLine("Invalid int");
                    break;
                }
            }

            if (fatal) {
                Console.Write("Fatal\n");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
        }

        public static void main() {
            
        }

        public static void HALT() {
            
        }

        // TODO: move this bullshit to a better place
        // we arent even using this functions
        public static void MOV(Int16 reg, Int16 val) {
            CPU.SetRegister(reg, val);
        }

        public static void INT(Int16 interrupt) {
            switch (interrupt) {
                case 0: // print reg
                    Console.WriteLine(CPU.GetRegister(0));
                    break;
                case 10:
                    Console.Clear();
                    break;
                default:
                    error(998, false);
                    break;
            }
        }
    }
}