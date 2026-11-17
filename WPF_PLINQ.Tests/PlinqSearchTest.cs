using WPF_PLINQ.Classes;

namespace WPF_PLINQ.Tests
{
    public class PlinqSearchTest
    {
        // Тест 5. PLINQ на том же маленьком массиве, что в тестах 1-4
        [Fact]
        public void Plinq_First_Last_Sum6_Return_Correct_Numbers()
        {
            //Arrange (Подготовка)
            int[] numbers = { 24, -3, 0, 15, 3, -7, 1666, 6666, 51, -1666, 606, 42, 9, 6066, 1005, -24, 660, 33, 7, 666 };
            int[] expectedResult = { -24, -3, 3, 15, 24, 33, 42, 51, 660, 1005 };

            //Act (Действия)
            PlinqSearch search = new PlinqSearch();
            int[] actualResult = search.FindFirstLastSum6(numbers);

            //Assert (Сравнение)
            Assert.Equal(expectedResult, actualResult);
        }

        // Тест 6. Три шестерки подряд в разных местах числа, 6606 и 60606 не подходят
        [Fact]
        public void Plinq_666_Inside_Number()
        {
            //Arrange (Подготовка)
            int[] numbers = { 36660, 966, -6661, 6606, 60606, 1666000, 6, -96669, 6660006, 1234 };
            int[] expectedResult = { -96669, -6661, 36660, 1666000, 6660006 };

            //Act (Действия)
            PlinqSearch search = new PlinqSearch();
            int[] actualResult = search.Find666(numbers);

            //Assert (Сравнение)
            Assert.Equal(expectedResult, actualResult);
        }

        // Тест 7. На случайном массиве все 4 запроса LINQ и PLINQ отвечают одинаково (seed 913 дает каждый раз тот же массив)
        [Fact]
        public void Linq_Plinq_Same_On_Random()
        {
            //Arrange (Подготовка)
            int[] numbers = ArrayGenerator.Generate(500000, -500000, 500000, new Random(913));
            INumberSearch linq = new LinqSearch();
            INumberSearch plinq = new PlinqSearch();

            for (int q = 0; q < 4; q++)
            {
                //Act (Действия)
                int[] linqResult = Run(linq, q, numbers);
                int[] plinqResult = Run(plinq, q, numbers);

                //Assert (Сравнение)
                Assert.Equal(linqResult, plinqResult);
            }
        }

        // Тест 8. Потоков несколько, а нечетные все равно идут по возрастанию
        [Fact]
        public void Plinq_Odd_Goes_Up()
        {
            //Arrange (Подготовка)
            int[] numbers = ArrayGenerator.Generate(80000, -3000, 3000, new Random(5));

            //Act (Действия)
            PlinqSearch search = new PlinqSearch();
            int[] actualResult = search.FindOdd(numbers);
            int[] expectedResult = actualResult.OrderBy(x => x).ToArray(); //тот же массив, отсортированный заново

            //Assert (Сравнение)
            Assert.NotEmpty(actualResult);
            Assert.Equal(expectedResult, actualResult);
        }

        //номер запроса как в окне: 0 четные, 1 нечетные, 2 сумма цифр 6, 3 есть 666
        private static int[] Run(INumberSearch search, int q, int[] arr)
        {
            if (q == 0)
                return search.FindEven(arr);
            else if (q == 1)
                return search.FindOdd(arr);
            else if (q == 2)
                return search.FindFirstLastSum6(arr);
            else
                return search.Find666(arr);
        }
    }
}
