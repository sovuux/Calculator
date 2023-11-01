using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            textBox.IsReadOnly = true;
            foreach (UIElement element in mainGrid.Children) 
            {
                if (element is Button)
                {
                    ((Button) element).Click += Button_Click;
                }    
            }
            
        }
        public static double result;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)e.OriginalSource;
            string number = clickedButton.Content.ToString();
            if (number == "C") //очистка textbox
            {
                textBox.Clear();
            }

            else if (number == "=")  // вывод базовых математических решений
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Введите число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    double resultString = Convert.ToDouble(new DataTable().Compute(textBox.Text, null));
                    textBox.Text = resultString.ToString();
                }
            }
            
            else if (number == ",") // замена знака запятой
            {
                textBox.Text += ".";
            }

            else if (number == "×") // замена знака умножения
            {
                textBox.Text += "*";
            }

            else if (number == "÷") // замена знака деление
            {
                textBox.Text += "/";
            }

            else if (clickedButton == clearSymbol) // удаление последнего символа
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = textBox.Text.Trim();
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                }
            }

            else if (number == "eˣ") // вывод экспоненты
            {
                result = Math.Exp(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "x²") // вывод числа в квадрате
            {
                result = Math.Pow(Convert.ToDouble(textBox.Text), 2); 
                textBox.Text = result.ToString();
            }

            else if (number == "²√x") // вывод квадратного корня
            {
                result = Math.Sqrt(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "¹/x") // вывод числа 1/x
            {
                result = 1 / Convert.ToDouble(textBox.Text);
                textBox.Text = result.ToString();
            }

            else if (number == "sin") // вывод синуса
            {
                result = Math.Sin(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "cos") // вывод косинуса
            {
                result = Math.Cos(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "tg") //  вывод тангеса
            {
                result = Math.Tan(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "arctg") // вывод арктангенса
            {
                result = Math.Atan(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "log") // вывод логарифма 
            {
                result = Math.Log(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "Ln") // вывод логарифма по основанию 10
            {
                result = Math.Log10(Convert.ToDouble(textBox.Text));
                textBox.Text = result.ToString();
            }

            else if (number == "!") // вывод факториала числа
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Введите число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (int.TryParse(textBox.Text, out int value))
                    {
                        if (value < 0)
                        {
                            MessageBox.Show("Факториал определяется только для неотрицательных чисел!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            int result = CalculateFactorial(value);
                            textBox.Text = result.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Некорректный формат числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }

            else if (textBox.Text == "xʸ") // вывод числа x в степени y
            {
               
            }

            else
            {
                textBox.Text += number;
            }
        }

        private int CalculateFactorial(int number) // расчет факториала
        {
            if (number == 0)
            {
                return 1;
            }
            else  
            { 
                return number * CalculateFactorial(number - 1);
            }
        }
    }
}
