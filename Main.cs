using System;

namespace WDT_Ass_1
{
    class Driver
    {
        static void Main(string[] args)
        {
            Menu m = new Menu();
            m.MainMenu();
            ASREngine eng = new ASREngine();
            //eng.RoomsAvailable("2019-12-01");
        }
    }
}
