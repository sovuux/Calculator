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
            buttonMC.IsEnabled = false;
            buttonMR.IsEnabled = false;
        }

        public static double result;
        public static double number1;
        public static double number2;

        private double memoryValue = 0;
        public void CalculateResult()
        {
            char powSymbol = '^';

            if (textBox.Text.Contains(powSymbol))
            {
                string[] parts = textBox.Text.Split(powSymbol);

                if (parts.Length == 2)
                {
                    number1 = Convert.ToDouble(parts[0]);
                    number2 = Convert.ToDouble(parts[1]);

                    result = Math.Pow(number1, number2);

                    if (double.IsInfinity(result))
                    {
                        MessageBox.Show("Неверное значение! Результат вычисления равен бесконечности.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    textBox.Text = result.ToString();
                }
            }
            else
            {
                {
                    DataTable dataTable = new DataTable();
                    double resultString;

                    try
                    {
                        resultString = Convert.ToDouble(dataTable.Compute(textBox.Text, null));

                        if (double.IsInfinity(resultString))
                        {
                            MessageBox.Show("Неверное значение! Результат вычисления равен бесконечности.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }

                        textBox.Text = resultString.ToString();
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Неверное значение! Проверьте правильность ввода числа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Неверное значение! Результат вычисления выходит за пределы допустимого диапазона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)e.OriginalSource;
            string number = clickedButton.Content.ToString();
            if (number == "C") //очистка textbox
            {
                textBox.Clear();
                result = 0;
            }

            else if (number == "MC")
            {
                memoryValue = 0;
                buttonMR.IsEnabled = false;
                buttonMC.IsEnabled = false;
            }

            else if (number == "MR")
            {
                if (!textBox.Text.Contains("+") && !textBox.Text.Contains("-") && !textBox.Text.Contains("/") && !textBox.Text.Contains("*") && !textBox.Text.Contains("^"))
                {
                    textBox.Text = memoryValue.ToString();
                }
                else
                {
                    textBox.Text += memoryValue.ToString();
                }
            }

            else if (number == "M+")
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = Convert.ToDouble(textBox.Text);
                    memoryValue += textBoxValue;
                    if (memoryValue != 0)
                    {
                        buttonMR.IsEnabled = true;
                        buttonMC.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Не выполнена операция для записи в память калькулятора!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            else if (number == "M-")
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = Convert.ToDouble(textBox.Text);
                    memoryValue -= textBoxValue;
                    if (memoryValue != 0)
                    {
                        buttonMR.IsEnabled = true;
                        buttonMC.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Память была пуста!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            else if (number =="CE")
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    int lastNumberIndex = textBox.Text.LastIndexOfAny(new char[] { '+', '-', '*', '/', '^'});
                    if (lastNumberIndex != -1)
                    {
                        textBox.Text = textBox.Text.Remove(lastNumberIndex);
                    }
                    else
                    {
                        textBox.Clear();
                        result = 0;
                    }
                }
            }

            else if (number == "=")  // вывод базовых математических решений
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    CalculateResult();
                }
            }
            

            else if (number == ",") // замена знака запятой
            {
                if (textBox.Text == "")
                {
                    textBox.Text += "0.";
                }
            }
            else if (number == "+")
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    textBox.Text += " + ";
                }
            }

            else if (number == "-")
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    textBox.Text += " - ";
                }
            }

            else if (number == "×") // замена знака умножения
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    textBox.Text += " * ";
                }
               
            }

            else if (number == "÷") // замена знака деление
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    textBox.Text += " / ";
                }

            }

            else if (number == "%")
            {
               if (textBox.Text == "")
               {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
               }
               else
               {
                    result = Convert.ToDouble(textBox.Text);
                    result /= 100;
                    textBox.Text = result.ToString();
               }
            }

            else if (clickedButton == clearSymbol) // удаление последнего символа
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        textBox.Text = textBox.Text.Trim();
                        textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                    }
                }
                
            }

            else if (number == "eˣ") // вывод экспоненты
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Exp(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "x²") // вывод числа в квадрате
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Pow(Convert.ToDouble(textBox.Text), 2); 
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "²√x") // вывод квадратного корня
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Sqrt(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "¹/x") // вывод числа 1/x
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = 1 / Convert.ToDouble(textBox.Text);
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "sin") // вывод синуса
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Sin(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "cos") // вывод косинуса
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Cos(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "tg") //  вывод тангеса
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Tan(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "arctg") // вывод арктангенса
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Atan(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "log") // вывод логарифма 
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Log(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "Ln") // вывод логарифма по основанию 10
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    result = Math.Log10(Convert.ToDouble(textBox.Text));
                    textBox.Text = result.ToString();
                }
                
            }

            else if (number == "!") // вывод факториала числа
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели  число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

            else if (number == "xʸ") // вывод числа x в степени y
            {
                
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    textBox.Text += " ^ ";
                }
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

        private void Buttons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D1) //1
            {
                textBox.Text += "1";
            }


            else if (e.Key == Key.D2) //2
            {
                textBox.Text += "2";
            }

            else if (e.Key == Key.D3) //3
            {
                textBox.Text += "3";
            }

            else if (e.Key == Key.D4) //4
            {
                textBox.Text += "4";
            }

            else if (e.Key == Key.D5) //5
            {
                textBox.Text += "5";
            }

            else if (e.Key == Key.D6) //6
            {
                textBox.Text += "6";
            }

            else if (e.Key == Key.D7) //7
            {
                textBox.Text += "7";
            }

            else if (e.Key == Key.D8) //8
            {
                textBox.Text += "8";
            }

            else if (e.Key == Key.D9) //9
            {
                textBox.Text += "9";
            }

            else if (e.Key == Key.D0) //0
            {
                textBox.Text += "0";
            }

            else if (e.Key == Key.NumPad0) //0
            {
                textBox.Text += "0";
            }

            else if (e.Key == Key.NumPad1) //1
            {
                textBox.Text += "1";
            }

            else if (e.Key == Key.NumPad2) //2
            {
                textBox.Text += "2";
            }

            else if (e.Key == Key.NumPad3) //3
            {
                textBox.Text += "3";
            }

            else if (e.Key == Key.NumPad4) //4
            {
                textBox.Text += "4";
            }

            else if (e.Key == Key.NumPad5) //5
            {
                textBox.Text += "5";
            }

            else if (e.Key == Key.NumPad6) //6
            {
                textBox.Text += "6";
            }

            else if (e.Key == Key.NumPad7) //7
            {
                textBox.Text += "7";
            }

            else if (e.Key == Key.NumPad8) //8
            {
                textBox.Text += "8";
            }

            else if (e.Key == Key.NumPad9) //9
            {
                textBox.Text += "9";
            }
            
            else if (e.Key == Key.Back) //стирает последний символ
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = textBox.Text.Trim();
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                }
            }

            else if (e.Key == Key.Space) //пробел
            {
                textBox.Text += " ";
            }

            else if (e.Key == Key.Multiply) 
            {
                textBox.Text += "*";
            }

            else if (e.Key == Key.Divide)
            {
                textBox.Text += "/";
            }

            else if (e.Key == Key.Add)
            {
                textBox.Text += "+";
            }

            else if (e.Key == Key.Subtract) 
            {
                textBox.Text += "-";
            }

            else if (e.Key == Key.Enter)
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Вы не ввели число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    CalculateResult();
                }
                
            }
        }
    }
}
