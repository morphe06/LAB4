using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_PLINQ.Classes
{
    internal interface INumberSearch
    {
        //все методы возвращают найденные числа по возрастанию
        public int[] FindEven(int[] numbers); //четные

        public int[] FindOdd(int[] numbers); //нечетные

        public int[] FindFirstLastSum6(int[] numbers); //1-я цифра + посл. = 6

        public int[] Find666(int[] numbers); //есть 666 подряд
    }
}
