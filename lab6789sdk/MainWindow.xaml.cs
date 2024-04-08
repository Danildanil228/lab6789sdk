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
using System.Collections.Generic;

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Student
    {
        public string LastName { get; set; }
        public double AverageScore { get; set; }
    }
    public partial class MainWindow : Window
    {
        private List<double> listLab1;
        private Queue<Student> studentQueue = new Queue<Student>();
        private System.Collections.Generic.LinkedList<double> list;
        private Dictionary<char, int> charCounts;

        public MainWindow()
        {
            InitializeComponent();
            listLab1 = new List<double>();
            list = new System.Collections.Generic.LinkedList<double>();
            charCounts = new Dictionary<char, int>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listLab1.Add(double.Parse(tbElement.Text));
            lbList.ItemsSource = null;
            lbList.ItemsSource = listLab1;
            tbElement.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int index = lbList.SelectedIndex;
            listLab1.RemoveAt(index);
            lbList.ItemsSource = null;
            lbList.ItemsSource = listLab1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (double l in listLab1)
            {
                if (l >= 3 && l <= 12) count++;
            }
            Result.Text = "Количество чисел от 3 до 12: " + count.ToString();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = LastNameTextBox.Text;
            double averageScore;
            if (string.IsNullOrWhiteSpace(lastName) || !double.TryParse(AverageScoreTextBox.Text, out averageScore))
            {
                MessageBox.Show("Введите корректные данные");
                return;
            }
            studentQueue.Enqueue(new Student { LastName = lastName, AverageScore = averageScore });
            UpdateQueueDisplay();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (QueueListBox.SelectedItem != null)
            {
                string selectedItem = QueueListBox.SelectedItem.ToString();
                string lastName = selectedItem.Split(',')[0].Split(':')[1].Trim();
                var studentToRemove = studentQueue.FirstOrDefault(student => student.LastName == lastName);
                if (studentToRemove != null)
                {
                    studentQueue = new Queue<Student>(studentQueue.Where(student => student != studentToRemove));
                    UpdateQueueDisplay();
                }
            }
        }
        private void UpdateQueueDisplay()
        {
            QueueListBox.Items.Clear();
            foreach (var student in studentQueue)
            {
                QueueListBox.Items.Add($"Фамилия: {student.LastName}, Средний балл: {student.AverageScore}");
            }
        }
        private void dellist()
        {
            list1.Items.Clear();
            foreach (var item in list)
            {
                list1.Items.Add(item);
            }
        }
        private void RemoveNegativeTwo()
        {
            var node = list.First;
            while (node != null && node.Next != null)
            {
                if (node.Next.Value == -2)
                {
                    list.Remove(node.Next);
                }
                else
                {
                    node = node.Next;
                }
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtElement.Text))
            {
                if (double.TryParse(txtElement.Text, out double element))
                {
                    list.AddLast(element);
                    txtElement.Clear();
                    dellist();
                }
                else
                {
                    MessageBox.Show("Введите действительное число.");
                }
            }
            else
            {
                MessageBox.Show("Введите число для добавления в список.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            RemoveNegativeTwo();
            list.AddLast(33);
            dellist();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (list1.SelectedIndex != -1)
            {
                var selectedNode = list1.SelectedItem;
                list.Remove((double)selectedNode);
                dellist();
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления.");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string input = inputTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                foreach (char c in input)
                {
                    if (charCounts.ContainsKey(c))
                    {
                        charCounts[c]++;
                    }
                    else
                    {
                        charCounts[c] = 1;
                    }
                }
                UpdateListBox();
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string input = inputTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                foreach (char c in input)
                {
                    if (charCounts.ContainsKey(c))
                    {
                        charCounts[c]--;
                        if (charCounts[c] == 0)
                        {
                            charCounts.Remove(c);
                        }
                    }
                }
                UpdateListBox();
            }
        }
        private void UpdateListBox()
        {
            list2.Items.Clear();
            foreach (var pair in charCounts)
            {
                list2.Items.Add($"Буква: {pair.Key}, Количество: {pair.Value}");
            }
        }
    }
}