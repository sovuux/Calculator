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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                string number = (string)((Button)e.OriginalSource).Content;

            if (number == "C")
            {
                textBox.Clear();
            }
            else if (number == "=")
            {
                 string res = new DataTable().Compute(textBox.Text, null).ToString();
                 textBox.Text = res;
            }
            else
            {
                textBox.Text += number;
            }
                
        }
    }
}
