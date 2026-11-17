using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_PLINQ.Classes;

namespace WPF_PLINQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random rnd = new Random(); //один генератор на все окно
        private int[]? numbers; //null, пока массив не создан
        private readonly string[] titles = { "Четные", "Нечетные", "1-я + посл. = 6", "Есть 666" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btGenerate_Click(object sender, RoutedEventArgs e)
        {
            Generate();
        }
        private void Generate()
        {
            if (!GetInput(out int count, out int from, out int to))
                return;

            numbers = ArrayGenerator.Generate(count, from, to, rnd);
            tbArray.Text = $"{count} шт., диапазон [{from}; {to}]";
            lbNumbers.ItemsSource = null; //чистим то, что осталось от прошлого массива
            tbFound.Text = "";
            tbTime.Text = "";
            tbCompare.Text = "";
        }

        //как в исправленной лаб. 3, TryParse вместо Convert, на каждую ошибку ввода свое сообщение
        private bool GetInput(out int count, out int from, out int to)
        {
            from = 0; //иначе не компилируется
            to = 0;
            if (!int.TryParse(tbCount.Text, out count) || count <= 0 || count > 20000000) //больше 20 млн массив и сортировка займут много памяти
            {
                MessageBox.Show("Размер массива: целое число от 1 до 20000000", "Неверный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!int.TryParse(tbMin.Text, out from) || !int.TryParse(tbMax.Text, out to))
            {
                MessageBox.Show("Диапазон чисел вводится целыми числами", "Неверный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (from < -100000000 || to > 100000000) //с запасом, чтобы to + 1 не вышло за int
            {
                MessageBox.Show("Диапазон можно брать в пределах плюс-минус 100000000", "Неверный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (from > to)
            {
                MessageBox.Show("Левая граница диапазона больше правой", "Неверный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void btRun_Click(object sender, RoutedEventArgs e)
        {
            if (numbers == null)
                Generate(); //массив еще не создавали
            if (numbers == null)
                return; //ввод неверный

            INumberSearch search = GetSearch(); //linq/plinq
            int query = cbQuery.SelectedIndex;

            Warmup(search, numbers); //пробный запуск

            Stopwatch sw = Stopwatch.StartNew(); //сортировка и ToArray тоже внутри замера
            int[] result = RunQuery(search, query, numbers);
            sw.Stop();

            lbNumbers.ItemsSource = result;
            tbFound.Text = result.Length.ToString();
            tbTime.Text = $"{sw.Elapsed.TotalMilliseconds:F2} мс";
        }

        private void btCompare_Click(object sender, RoutedEventArgs e)
        {
            if (numbers == null)
                Generate();
            if (numbers == null)
                return;

            INumberSearch linq = new LinqSearch();
            INumberSearch plinq = new PlinqSearch();

            Warmup(linq, numbers);
            Warmup(plinq, numbers);

            StringBuilder sb = new StringBuilder();
            Stopwatch sw = new Stopwatch();
            for (int q = 0; q < 4; q++) //все 4 запроса на одном массиве
            {
                sw.Restart();
                int[] linqResult = RunQuery(linq, q, numbers);
                sw.Stop();
                double tLinq = sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                int[] plinqResult = RunQuery(plinq, q, numbers);
                sw.Stop();
                double tPlinq = sw.Elapsed.TotalMilliseconds;

                sb.AppendLine($"{titles[q]}, найдено {linqResult.Length}");
                sb.AppendLine($"    LINQ {tLinq:F2} мс, PLINQ {tPlinq:F2} мс, {Verdict(tLinq, tPlinq)}");
                if (!linqResult.SequenceEqual(plinqResult))
                    sb.AppendLine("    внимание: ответы LINQ и PLINQ разные"); //на всякий случай, в норме не бывает
            }

            tbCompare.Text = sb.ToString();
        }

        //итог сравнения, tLinq / tPlinq больше 1 значит PLINQ быстрее
        private string Verdict(double tLinq, double tPlinq)
        {
            if (tLinq == 0 || tPlinq == 0)
                return "одно из времен 0 мс, отношение не считается"; //на ноль не делим
            double ratio = tLinq / tPlinq;
            if (Math.Round(ratio, 1) == 1.0) //на экране все равно 1,0
                return $"LINQ/PLINQ = {ratio:F1}, разницы нет";
            if (ratio > 1)
                return $"LINQ/PLINQ = {ratio:F1}, PLINQ выиграл";
            return $"LINQ/PLINQ = {ratio:F2}, PLINQ дольше";
        }

        //при первом AsParallel создаются потоки, поэтому сначала короткий прогон
        private void Warmup(INumberSearch search, int[] arr)
        {
            int[] part = arr.Take(50000).ToArray(); //первые 50000 чисел
            for (int q = 0; q < 4; q++)
                RunQuery(search, q, part);
        }

        private int[] RunQuery(INumberSearch search, int query, int[] arr)
        {
            if (query == 0)
                return search.FindEven(arr);
            else if (query == 1)
                return search.FindOdd(arr);
            else if (query == 2)
                return search.FindFirstLastSum6(arr);
            else
                return search.Find666(arr);
        }

        private INumberSearch GetSearch()
        {
            if (rbParallel.IsChecked == true)
                return new PlinqSearch();
            return new LinqSearch();
        }
    }
}
