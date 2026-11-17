using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_PLINQ.Classes
{
    //цифры берем у модуля числа, минус не цифра
    internal static class Digits
    {
        public static int Last(int x)
        {
            return Math.Abs(x % 10); //у -24 остаток -4, поэтому Abs
        }

        public static int First(int x)
        {
            int v = Math.Abs(x);
            while (v >= 10)
                v /= 10; //убираем посл. цифру, пока не останется одна
            return v;
        }

        public static bool Has666(int x)
        {
            int v = Math.Abs(x);
            while (v >= 666) //число меньше 666 на 666 уже не кончится
            {
                if (v % 1000 == 666) //посл. три цифры 666
                    return true;
                v /= 10; //убираем посл. цифру, проверяем след. три
            }
            return false;
        }
    }
}
