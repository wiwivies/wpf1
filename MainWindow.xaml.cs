using System;
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


namespace wpf1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isAnsw = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonNumberClick(object sender, RoutedEventArgs e)
        {
            char buttonContent = (sender as Button).Content.ToString()[0];

            if (char.IsDigit(buttonContent) || buttonContent == ',')
            {
                if (isAnsw)
                {
                    TBBottom.Text = "";
                    TBTop.Text = "";
                }
            }

            if (buttonContent == ',')
            {
                if (TBBottom.Text.Length == 0 || !char.IsDigit(TBBottom.Text[TBBottom.Text.Length - 1]))
                {
                    TBBottom.Text += '0';
                }
                else
                {
                    return;
                }
            }

            TBBottom.Text += buttonContent;
            isAnsw = false;

            if (TBBottom.Text.Length > 1 && TBBottom.Text[TBBottom.Text.Length - 2] == '0' && TBBottom.Text[TBBottom.Text.Length - 1] != ',')
            { 
            
            }
            else if (TBBottom.Text.Length > 2 && TBBottom.Text[TBBottom.Text.Length - 3] == '0' && (TBBottom.Text[TBBottom.Text.Length - 1] < '0' || TBBottom.Text[TBBottom.Text.Length - 1] > '9'))
            { 
            
            }
            else
            {
                return;
            }

            MessageBox.Show("Incorrect input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            TBBottom.Text = "";
        }

        private void ButtonEqualClick(object sender, RoutedEventArgs e)
        {
            if (TBBottom.Text.Length == 0)
            {
                MessageBox.Show("Expression is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TBTop.Text = TBBottom.Text;

            List<double> numbers = new List<double>();
            string txt = "";
            char prev = '0';

            foreach (var item in TBBottom.Text + "/")
            {
                if (char.IsDigit(item) || item == ',')
                {
                    txt += item;
                    continue;
                }
                else if (prev == '*')
                {
                    numbers[numbers.Count - 1] *= Convert.ToDouble(txt);
                }
                else if (prev == '/')
                {
                    numbers[numbers.Count - 1] /= Convert.ToDouble(txt);
                }
                else if (prev == '-')
                {
                    numbers.Add(-Convert.ToDouble(txt));
                }
                else if (prev == '+')
                {
                    numbers.Add(Convert.ToDouble(txt));
                }
                else
                {
                    try
                    {
                        numbers.Add(Convert.ToDouble(txt));
                    }
                    catch
                    { }
                }

                if (item == '+' || item == '-' || item == '*' || item == '/')
                {
                    prev = item;
                }
                txt = "";
            }

            TBBottom.Text = Math.Round(numbers.Sum(x => x), 8).ToString();
            isAnsw = true;
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            if (TBBottom.Text != "")
                TBBottom.Text = TBBottom.Text.Remove(TBBottom.Text.Length - 1);
        }
        private void ButtonCEClick(object sender, RoutedEventArgs e)
        {
            TBBottom.Text = "";
        }

        private void ButtonCClick(object sender, RoutedEventArgs e)
        {
            TBBottom.Text = "";
            TBTop.Text = "";
        }

    }
}