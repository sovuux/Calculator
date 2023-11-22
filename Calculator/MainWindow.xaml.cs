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
using System.Globalization;

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
            textBox.Text = "0";
        }

        public static double result;
        public static double number1;
        public static double number2;
        public static string textString;

        private double memoryValue = 0;

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Contains(","))
            {
                textBox.Text = textBox.Text.Replace(",", ".");
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
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
                        textBox.Text = "∞";
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
                            textBox.Text = "∞";
                            return;
                        }

                        textBox.Text = resultString.ToString();
                    }
                    catch (OverflowException)
                    {
                        textBox.Text = "∞";
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
                textBox.Text = "0";
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
                if (!textBox.Text.Contains("+") && !textBox.Text.Contains("-") && !textBox.Text.Contains("/") && !textBox.Text.Contains("*") && !textBox.Text.Contains("^"))
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
                        else
                        {
                            buttonMR.IsEnabled = false;
                            buttonMC.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    memoryValue = 0;
                    buttonMR.IsEnabled = true;
                    buttonMC.IsEnabled = true;
                }

            }

            else if (number == "M-")
            {
                if (!textBox.Text.Contains("+") && !textBox.Text.Contains("-") && !textBox.Text.Contains("/") && !textBox.Text.Contains("*") && !textBox.Text.Contains("^"))
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
                        else
                        {
                            buttonMR.IsEnabled = false;
                            buttonMC.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    memoryValue = 0;
                    buttonMR.IsEnabled = true;
                    buttonMC.IsEnabled = true;
                }
            }

            else if (number == "CE")
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    int lastNumberIndex = textBox.Text.LastIndexOfAny(new char[] { '+', '-', '*', '/', '^' });
                    if (lastNumberIndex != -1)
                    {
                        textBox.Text = textBox.Text.Remove(lastNumberIndex);
                    }
                    else
                    {
                        textBox.Clear();
                        textBox.Text = "0";
                        result = 0;
                    }
                }
            }

            else if (number == "=")  // вывод базовых математических решений
            {
                if (textBox.Text != "")
                {
                    try
                    {
                        CalculateResult();
                    }
                    catch 
                    {
                    }
                }
            }

            else if (number == "1")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "1";
                }
                else
                {
                    textBox.Text += "1";
                }
            }

            else if (number == "2")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "2";
                }
                else
                {
                    textBox.Text += "2";
                }
            }

            else if (number == "3")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "3";
                }
                else
                {
                    textBox.Text += "3";
                }
            }

            else if (number == "4")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "4";
                }
                else
                {
                    textBox.Text += "4";
                }
            }

            else if (number == "5")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "5";
                }
                else
                {
                    textBox.Text += "5";
                }
            }

            else if (number == "6")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "6";
                }
                else
                {
                    textBox.Text += "6";
                }
            }

            else if (number == "7")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "7";
                }
                else
                {
                    textBox.Text += "7";
                }
            }

            else if (number == "8")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "8";
                }
                else
                {
                    textBox.Text += "8";
                }
            }

            else if (number == "9")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "9";
                }
                else
                {
                    textBox.Text += "9";
                }
            }

            else if (number == "0")
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "0";
                }
                else
                {
                    textBox.Text += "0";
                }
            }

            else if (number == ",")
            {
                if (!textBox.Text.Contains("."))
                {
                    textBox.Text += ".";
                }
            }

            else if (number == "+")
            {
                if (textBox.Text != "")
                {
                    textBox.Text += " + ";
                }
            }

            else if (number == "-")
            {
                if (textBox.Text != "")
                {
                    textBox.Text += " - ";
                }
            }

            else if (number == "×") // замена знака умножения
            {
                if (textBox.Text != "")
                {
                    textBox.Text += " * ";
                }

            }

            else if (number == "÷") // замена знака деление
            {
                if (textBox.Text != "")
                {
                    textBox.Text += " / ";
                }
            }

            else if (number == "%")
            {
                if (textBox.Text != "")
                {
                    result = Convert.ToDouble(textBox.Text);
                    result /= 100;
                    textBox.Text = result.ToString();
                }
            }

            else if (clickedButton == clearSymbol) // удаление последнего символа
            {
                if (textBox.Text != "")
                {
                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        if (textBox.Text.Contains(" "))
                        {
                            textBox.Text = textBox.Text.Trim();
                            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 2);
                            if (textBox.Text.Length < 1)
                            {
                                textBox.Text += "0";
                            }
                        }
                        else
                        {
                            textBox.Text = textBox.Text.Trim();
                            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                            if (textBox.Text.Length < 1)
                            {
                                textBox.Text += "0";
                            }
                        }
                    }
                }
                else
                {
                    textBox.Text += "0";
                }
            }

            else if (number == "eˣ") // вывод экспоненты
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Exp(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "x²") // вывод числа в квадрате
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Pow(textBoxValue, 2);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "²√x") // вывод квадратного корня
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Sqrt(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "¹/x") // вывод числа 1/x
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = 1 / textBoxValue;
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "sin") // вывод синуса
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Sin(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "cos") // вывод косинуса
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Cos(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "tg") //  вывод тангеса
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Tan(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "arctg") // вывод арктангенса
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Atan(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "log") // вывод логарифма 
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Log(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "Ln") // вывод логарифма по основанию 10
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textBoxValue = double.Parse(textBox.Text, CultureInfo.InvariantCulture);
                    double result = Math.Log10(textBoxValue);
                    textBox.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }

            else if (number == "!") // вывод факториала числа
            {
                if (textBox.Text != "")
                {
                    if (int.TryParse(textBox.Text, out int value))
                    {
                        if (value < 0)
                        {

                        }
                        else
                        {
                            int result = CalculateFactorial(value);
                            textBox.Text = result.ToString();
                        }
                    }
                }
            }

            else if (number == "xʸ") // вывод числа x в степени y
            {

                if (textBox.Text != "")
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
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "1";
                }
                else
                {
                    textBox.Text += "1";
                }
            }


            else if (e.Key == Key.D2) //2
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "2";
                }
                else
                {
                    textBox.Text += "2";
                }
            }

            else if (e.Key == Key.D3) //3
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "3";
                }
                else
                {
                    textBox.Text += "3";
                }
            }

            else if (e.Key == Key.D4) //4
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "4";
                }
                else
                {
                    textBox.Text += "4";
                }
            }

            else if (e.Key == Key.D5) //5
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "5";
                }
                else
                {
                    textBox.Text += "5";
                }
            }

            else if (e.Key == Key.D6) //6
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "6";
                }
                else
                {
                    textBox.Text += "6";
                }
            }

            else if (e.Key == Key.D7) //7
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "7";
                }
                else
                {
                    textBox.Text += "7";
                }
            }

            else if (e.Key == Key.D8) //8
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "8";
                }
                else
                {
                    textBox.Text += "8";
                }
            }

            else if (e.Key == Key.D9) //9
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "9";
                }
                else
                {
                    textBox.Text += "9";
                }
            }

            else if (e.Key == Key.D0) //0
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "0";
                }
                else
                {
                    textBox.Text += "0";
                }
            }

            else if (e.Key == Key.NumPad0) //0
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "0";
                }
                else
                {
                    textBox.Text += "0";
                }
            }

            else if (e.Key == Key.NumPad1) //1
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "1";
                }
                else
                {
                    textBox.Text += "1";
                }
            }

            else if (e.Key == Key.NumPad2) //2
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "2";
                }
                else
                {
                    textBox.Text += "2";
                }
            }

            else if (e.Key == Key.NumPad3) //3
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "3";
                }
                else
                {
                    textBox.Text += "3";
                }
            }

            else if (e.Key == Key.NumPad4) //4
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "4";
                }
                else
                {
                    textBox.Text += "4";
                }
            }

            else if (e.Key == Key.NumPad5) //5
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "5";
                }
                else
                {
                    textBox.Text += "5";
                }
            }

            else if (e.Key == Key.NumPad6) //6
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "6";
                }
                else
                {
                    textBox.Text += "6";
                }
            }

            else if (e.Key == Key.NumPad7) //7
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "7";
                }
                else
                {
                    textBox.Text += "7";
                }
            }

            else if (e.Key == Key.NumPad8) //8
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "8";
                }
                else
                {
                    textBox.Text += "8";
                }
            }

            else if (e.Key == Key.NumPad9) //9
            {
                textString = textBox.Text;
                char firstSymbol = textString[0];
                if (firstSymbol == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                    textBox.Text += "9";
                }
                else
                {
                    textBox.Text += "9";
                }
            }
            
            else if (e.Key == Key.Back) //стирает последний символ
            {
                if (textBox.Text != "")
                {
                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        if (textBox.Text.Contains(" "))
                        {
                            textBox.Text = textBox.Text.Trim();
                            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 2);
                            if (textBox.Text.Length < 1)
                            {
                                textBox.Text += "0";
                            }
                        }
                        else
                        {
                            textBox.Text = textBox.Text.Trim();
                            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                            if (textBox.Text.Length < 1)
                            {
                                textBox.Text += "0";
                            }
                        }
                    }
                }
                else
                {
                    textBox.Text += "0";
                }
            }

            else if (e.Key == Key.Space) //пробел
            {
                textBox.Text += " ";
            }

            else if (e.Key == Key.Multiply) //умножение
            {
                textBox.Text += " * ";
            }

            else if (e.Key == Key.Divide) //деление
            {
                textBox.Text += " /" ;
            }

            else if (e.Key == Key.Add) // плюс
            {
                textBox.Text += " + ";
            }

            else if (e.Key == Key.Subtract) //минус
            {
                textBox.Text += " - ";
            }

            else if (e.Key == Key.Enter) // вычеслить результат
            {
                if (textBox.Text != "")
                {
                    try
                    {
                        CalculateResult();
                    }
                    catch (Exception ex) 
                    { 
                    }
                }
            }
            else if  (e.Key == Key.OemPeriod) //клавиша точки
            {
                if (!textBox.Text.Contains("."))
                {
                    textBox.Text += ".";
                }
            }
            else if (e.Key == Key.NumLock) //numlock точка
            {
                if (!textBox.Text.Contains('.'))
                {
                    textBox.Text += ".";
                }
            }
        }
    }
}