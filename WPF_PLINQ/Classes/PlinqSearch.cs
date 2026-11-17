using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_PLINQ.Classes
{
    //те же запросы, после AsParallel() массив проверяют сразу несколько потоков
    internal class PlinqSearch : INumberSearch
    {
        public int[] FindEven(int[] numbers)
        {
            return numbers.AsParallel().Where(x => x % 2 == 0).OrderBy(x => x).ToArray();
        }

        public int[] FindOdd(int[] numbers)
        {
            return numbers.AsParallel().Where(x => x % 2 != 0).OrderBy(x => x).ToArray();
        }

        public int[] FindFirstLastSum6(int[] numbers)
        {
            return numbers.AsParallel().Where(x => Digits.First(x) + Digits.Last(x) == 6).OrderBy(x => x).ToArray();
        }

        public int[] Find666(int[] numbers)
        {
            return numbers.AsParallel().Where(x => Digits.Has666(x)).OrderBy(x => x).ToArray(); //OrderBy тоже парал., ToArray собирает по порядку
        }
    }
}
