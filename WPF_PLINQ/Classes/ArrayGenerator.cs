using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_PLINQ.Classes
{
    internal static class ArrayGenerator
    {
        //count случайных чисел из [from; to]
        //Range дает номера 0..count-1, номер не нужен (поэтому _), на каждый берем случайное число
        public static int[] Generate(int count, int from, int to, Random rnd)
        {
            return Enumerable.Range(0, count).Select(_ => rnd.Next(from, to + 1)).ToArray(); //+1, иначе to не выпадет
        }
    }
}
