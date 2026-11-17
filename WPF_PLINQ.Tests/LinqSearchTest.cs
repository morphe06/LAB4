using WPF_PLINQ.Classes;

namespace WPF_PLINQ.Tests
{
    public class LinqSearchTest
    {
        //один и тот же маленький массив для тестов 1-4, ответы посчитаны вручную
        private readonly int[] numbers = { 24, -3, 0, 15, 3, -7, 1666, 6666, 51, -1666, 606, 42, 9, 6066, 1005, -24, 660, 33, 7, 666 };

        // Тест 1. Четные числа, 0 и отрицательные тоже четные
        [Fact]
        public void Linq_Even_Return_Correct_Numbers()
        {
            //Arrange (Подготовка)
            int[] expectedResult = { -1666, -24, 0, 24, 42, 606, 660, 666, 1666, 6066, 6666 };

            //Act (Действия)
            LinqSearch search = new LinqSearch();
            int[] actualResult = search.FindEven(numbers);

            //Assert (Сравнение)
            Assert.Equal(expectedResult, actualResult);
        }

        // Тест 2. Нечетные числа, -3 и -7 тоже должны найтись
        [Fact]
        public void Linq_Odd_Return_Correct_Numbers()
        {
            //Arrange (Подготовка)
            int[] expectedResult = { -7, -3, 3, 7, 9, 15, 33, 51, 1005 };

            //Act (Действия)
            LinqSearch search = new LinqSearch();
            int[] actualResult = search.FindOdd(numbers);

            //Assert (Сравнение)
            Assert.Equal(expectedResult, actualResult);
        }

        // Тест 3. Сумма первой и последней цифры 6, у 3 и -3 первая цифра равна последней
        [Fact]
        public void Linq_First_Last_Sum6_Return_Correct_Numbers()
        {
            //Arrange (Подготовка)
            int[] expectedResult = { -24, -3, 3, 15, 24, 33, 42, 51, 660, 1005 };

            //Act (Действия)
            LinqSearch search = new LinqSearch();
            int[] actualResult = search.FindFirstLastSum6(numbers);

            //Assert (Сравнение)
            Assert.Equal(expectedResult, actualResult);
        }

        // Тест 4. Есть 666 подряд: 6666 и -1666 подходят, 606 и 6066 нет
        [Fact]
        public void Linq_666_Return_Correct_Numbers()
        {
            //Arrange (Подготовка)
            int[] expectedResult = { -1666, 666, 1666, 6666 };

            //Act (Действия)
            LinqSearch search = new LinqSearch();
            int[] actualResult = search.Find666(numbers);

            //Assert (Сравнение)
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
