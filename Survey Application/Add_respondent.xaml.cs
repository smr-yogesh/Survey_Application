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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Survey_Application
{
    /// <summary>
    /// Interaction logic for Add_respondent.xaml
    /// </summary>
    public partial class Add_respondent : Window
    {
        MySqlConnection connection = new MySqlConnection("server=127.0.01;user=root;database=appproject;port=3306;password=");
        public Add_respondent()
        {
            InitializeComponent();
            this.Title = "Add Respondent";
        }

        private void Create_respondent_button_Click(object sender, RoutedEventArgs e)
        {
                long id;

                try
                {
                    id = Convert.ToInt64(create_repondent_box.Text);
                    MySqlCommand cmd = new MySqlCommand("Select Respondent_ID from key_a where Respondent_ID=@ID", connection);
                    cmd.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Key " + id + " already exist\nPlease use unique key.");
                        create_repondent_box.Clear();
                        connection.Close();
                    }
                    else if (create_repondent_box.Text == "")
                    {
                        MessageBox.Show("You can't keep Respondent key empty");
                        connection.Close();
                    }
                    else
                    {
                        MySqlCommand addrespondentKey = new MySqlCommand("INSERT INTO key_a(Respondent_ID) VALUES (@ID)", connection);
                        addrespondentKey.Parameters.AddWithValue("@ID", id);
                        //addusernameAndPassword.Parameters.AddWithValue("@id", this.ID_box.Text.Trim());
                        addrespondentKey.ExecuteNonQuery();
                        create_repondent_box.Clear();
                        MessageBox.Show("Key " + create_repondent_box.Text + "created");
                        this.Close();
                        connection.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Respondent ID can be numbers only.");
                    connection.Close();
                }
        }
        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
