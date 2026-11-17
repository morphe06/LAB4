using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_PLINQ.Classes
{
    //обычный LINQ, все в одном потоке
    internal class LinqSearch : INumberSearch
    {
        public int[] FindEven(int[] numbers)
        {
            return numbers.Where(x => x % 2 == 0).OrderBy(x => x).ToArray(); //ToArray сразу выполняет запрос
        }

        public int[] FindOdd(int[] numbers)
        {
            return numbers.Where(x => x % 2 != 0).OrderBy(x => x).ToArray(); //!= 0: у -7 остаток -1, а не 1
        }

        public int[] FindFirstLastSum6(int[] numbers)
        {
            return numbers.Where(x => Digits.First(x) + Digits.Last(x) == 6).OrderBy(x => x).ToArray();
        }

        public int[] Find666(int[] numbers)
        {
            return numbers.Where(x => Digits.Has666(x)).OrderBy(x => x).ToArray();
        }
    }
}
