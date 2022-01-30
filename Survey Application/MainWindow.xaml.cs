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
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Survey_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        //DATABASE CONNECTION
        MySqlConnection connection = new MySqlConnection("server=127.0.01;user=root;database=appproject;port=3306;password=");

        
        private Manager manager;
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Survey";
            this.Manager_grid.Visibility = System.Windows.Visibility.Hidden;
            this.respondent_grid.Visibility = System.Windows.Visibility.Hidden;

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (manager == null)
                manager = new Manager();

            if (manager.CheckManagerKey(key_box.Password) == true)
            {
                MySqlCommand cmd = new MySqlCommand("Select username from manager_account where Username=@username", connection);
                cmd.Parameters.AddWithValue("@username", this.ID_box.Text);
                connection.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Username " + ID_box.Text + " already exist\nPlease use unique username.");
                    ID_box.Clear();
                    connection.Close();
                }

                else if(ID_box.Text == "" && passwordBox.Password == "")
                {
                    MessageBox.Show("You can't keep username and password empty");
                    connection.Close();
                }

                else
                {
                    MySqlCommand addusernameAndPassword = new MySqlCommand("INSERT INTO manager_account(username,Password) VALUES (@id,@password)", connection);
                    addusernameAndPassword.Parameters.AddWithValue("@password", this.passwordBox.Password.Trim());
                    addusernameAndPassword.Parameters.AddWithValue("@id", this.ID_box.Text.Trim());
                    addusernameAndPassword.ExecuteNonQuery();
                    key_box.Clear();
                    ID_box.Clear();
                    passwordBox.Clear();
                    MessageBox.Show("Account Created");
                    connection.Close();
                }

            }
            else
                MessageBox.Show("Wrong key");
        }
        
        private void Idbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Login_button_Click(object sender, RoutedEventArgs e)
        {

            MySqlCommand cmd = new MySqlCommand("Select username from manager_account where Username=@username", connection);
            cmd.Parameters.AddWithValue("@username", this.Login_id_box.Text.Trim());
            connection.Open();
            var result = cmd.ExecuteScalar();
            if (result == null)
            {
                Login_id_box.Clear();
                login_password_box.Clear();
                MessageBox.Show("Username " + ID_box.Text + " does not exist!");
                connection.Close();
            }
            else
            {
                MySqlCommand login = new MySqlCommand("select Password from manager_account WHERE username=@id ", connection);
                login.Parameters.AddWithValue("@id", this.Login_id_box.Text.Trim());
                string x = login.ExecuteScalar().ToString();
                if (login_password_box.Password == x)
                {
                    Login_id_box.Clear();
                    login_password_box.Clear();
                    ManagerAccount Dashboard = new ManagerAccount();
                    Dashboard.ShowDialog();
                    connection.Close();
                }
                else

                {
                    MessageBox.Show("Wrong Password");
                    login_password_box.Clear();
                    connection.Close();
                }
            }
            connection.Close();
        }

        private void Responder_login_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("Select Respondent_ID from key_a where Respondent_ID=@key", connection);
            cmd.Parameters.AddWithValue("@key", this.responder_key_box.Password.Trim());
            connection.Open();
            var result = cmd.ExecuteScalar();
            if (result == null)
            {
                Login_id_box.Clear();
                login_password_box.Clear();
                MessageBox.Show("Username " + responder_key_box.Password + " does not exist!");
                connection.Close();
            }
            else
            {
                    responder_key_box.Clear();
                    Responder Dashboard = new Responder();
                    Dashboard.ShowDialog();
                    connection.Close();
            }
            connection.Close();
        }

        private void manager_button_Click(object sender, RoutedEventArgs e)
        {
            this.Into_application.Visibility = System.Windows.Visibility.Hidden;
            this.Manager_grid.Visibility = System.Windows.Visibility.Visible; 
        }

        private void responder_button_Click(object sender, RoutedEventArgs e)
        {
            this.Into_application.Visibility = System.Windows.Visibility.Hidden;
            this.respondent_grid.Visibility = System.Windows.Visibility.Visible;
        }

        private void back_button_from_mananger_Click(object sender, RoutedEventArgs e)
        {
            this.Into_application.Visibility = System.Windows.Visibility.Visible;
            this.Manager_grid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Back_button_from_responder_Click(object sender, RoutedEventArgs e)
        {
            this.Into_application.Visibility = System.Windows.Visibility.Visible;
            this.respondent_grid.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
