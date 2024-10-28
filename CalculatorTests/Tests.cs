using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class Tests
    {
        private MainWindow mainWindow;
        private Button equalsButton;

        [SetUp]
        public void Setup()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            equalsButton = mainWindow.FindName("buttonEquals") as Button;
            Assert.That(equalsButton, Is.Not.Null, "Кнопка '=' не найдена");
        }

        [TearDown]
        public void TearDown()
        {
            if (mainWindow != null)
            {
                mainWindow.Close();
            }
        }

        //Имитация нажатия кнопки "С"
        [Test]
        public void Test_Calc_Clear()
        {
            mainWindow.TextBoxControl.Text = "123";
            mainWindow.ClearTextBox();
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("0"));
        }

        //Сложение
        [Test]
        public void Test_Calc_Addition()
        {
            // 2 + 2 = 4
            mainWindow.TextBoxControl.Text = "2 + 2";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("4"));

            // -1 + 1 = 0
            mainWindow.TextBoxControl.Text = "-1 + 1";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("0"));

            // 0 + 0 = 0
            mainWindow.TextBoxControl.Text = "0 + 0";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("0"));
        }

        //Вычитание
        [Test]
        public void Test_Calc_Subtraction()
        {
            //5-3
            mainWindow.TextBoxControl.Text = "5 - 3";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("2"));

            //0-4
            mainWindow.TextBoxControl.Text = "0 - 4";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("-4"));

            //-2-(-2)
            mainWindow.TextBoxControl.Text = "-2 - (-2)";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("0"));
        }

        //Умножение
        [Test]
        public void Test_Calc_Multiplication()
        {
            //3*3
            mainWindow.TextBoxControl.Text = "3 * 3";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("9"));

            //-2*2
            mainWindow.TextBoxControl.Text = "-2 * 2";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("-4"));

            //0*5
            mainWindow.TextBoxControl.Text = "0 * 5";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("0"));
        }

        //Деление
        [Test]
        public void Test_Calc_Division()
        {
            //10/2
            mainWindow.TextBoxControl.Text = "10 / 2";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("5"));

            //5/-1
            mainWindow.TextBoxControl.Text = "5 / -1";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("-5"));

            //0/3
            mainWindow.TextBoxControl.Text = "0 / 3";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("0"));

            //10/0
            mainWindow.TextBoxControl.Text = "10 / 0";
            equalsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            Assert.That(mainWindow.TextBoxControl.Text, Is.EqualTo("∞"));
        }
    }
}