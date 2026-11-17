using WPF_PLINQ.Classes;

namespace WPF_PLINQ.Tests
{
    public class DigitsTest
    {
        // Тест 9. У отрицательного числа цифры берутся по модулю, минус не цифра
        [Fact]
        public void Digits_Negative_Number_Use_Modulus()
        {
            //Arrange (Подготовка)
            int number = -1234;
            int expectedFirst = 1;
            int expectedLast = 4;

            //Act (Действия)
            int actualFirst = Digits.First(number);
            int actualLast = Digits.Last(number);

            //Assert (Сравнение)
            Assert.Equal(expectedFirst, actualFirst);
            Assert.Equal(expectedLast, actualLast);
        }
    }
}
