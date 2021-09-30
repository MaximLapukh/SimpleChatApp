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
using System.Windows.Shapes;

namespace client
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public Operation operation { get; private set; }
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            operation = Operation.None;
            Close();
        }

        private void Button_SignIn(object sender, RoutedEventArgs e)
        {
            operation = Operation.SignIn;
            Close();
        }

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            operation = Operation.Register;
            Close();
        }
    }
}
