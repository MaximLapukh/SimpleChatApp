using client.Models;
using client.ServiceChat;
using System.Linq;
using System.ServiceModel;
using System.Windows;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        private ServiceChatClient client;
        private int id;

        private bool connection = false;
        public MainWindow()
        {
            InitializeComponent();
            BSend.IsEnabled = false;
        }

        private void Button_Connect(object sender, RoutedEventArgs e)
        {
            if (!connection)
            {
                var signInWinodw = new SignInWindow();
                if (signInWinodw.ShowDialog() == false)
                {
                    if (signInWinodw.operation != Operation.None)
                    {
                        if (string.IsNullOrWhiteSpace(signInWinodw.name.Text) ||
                            string.IsNullOrWhiteSpace(signInWinodw.password.Text))
                        {
                            ShowErrorMessage("uncorrect name or password");
                            return;
                        }

                        var username = signInWinodw.name.Text;
                        var userpassword = signInWinodw.password.Text;

                        if (signInWinodw.operation == Operation.SignIn) SignIn(username, userpassword);
                        else if (signInWinodw.operation == Operation.Register) Register(username, userpassword);
                    }
                }
            }
            else
            {
                Disconnect();
            }
        }
        private void Button_Send(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(BoxMessage.Text))
            {
                client.SendMsg(id, BoxMessage.Text);
                BoxMessage.Text = string.Empty;
            }          
        }

        private void Disconnect()
        {
            client.Disconnection(id);
            client = null;
            connection = false;
            BConnect.Content = "Connect";
            Messages.Items.Clear();
            BSend.IsEnabled = false;
            Username.Text = "";
        }

        private void Register(string username, string userpassword)
        {
            using (var db = new ChatdbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.name == username);
                if(user != null)
                {
                    ShowErrorMessage("this name is occupied"); 
                    return;
                }
                var newuser = new User() { name = username, password = userpassword };
                db.Users.Add(newuser);

                db.SaveChanges();
            }
            SignIn(username, userpassword);
        }

        private void SignIn(string username,string userpassword)
        {
            using (var db = new ChatdbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.name == username);
                if (user != null)
                {
                    if (user.password == userpassword)
                    {
                        client = new ServiceChatClient(new InstanceContext(this));
                        var data = client.Connection(username);
                        id = data.Item1;
                        if (data.Item2.Length > 0)
                        {
                            foreach (var msg in data.Item2)
                            {
                                Messages.Items.Add(msg);
                            }
                        }
                        Username.Text = username;
                        BConnect.Content = "Disconnect";
                        connection = true;
                        BSend.IsEnabled = true;
                    }
                    else
                    {
                        ShowErrorMessage("uncorrect name or password");
                        return;
                    }
                }
                else
                {
                    ShowErrorMessage("uncorrect name or password");
                    return;
                }
            }
        }
 
        private void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void CallbackMsg(string msg)
        {
            Messages.Items.Add(msg);
        }
    }
}
