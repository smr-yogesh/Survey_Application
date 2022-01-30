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
    /// Interaction logic for CreateSurvey.xaml
    /// </summary>
    public partial class CreateSurvey : Window
    {
        
        string options;
        int x;
        int z = 1;
        MySqlConnection connection = new MySqlConnection("server=127.0.01;user=root;database=appproject;port=3306;password=");
        public CreateSurvey()
        {
            InitializeComponent();
            this.Title = "Create Survey";
            add_option_grid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Check_Box_Checked(object sender, RoutedEventArgs e)
        {
            Text_Answer.IsChecked = false;
            Multiple_Choice.IsChecked = false;
            x = 1;
            this.add_survey.Visibility = System.Windows.Visibility.Hidden;
            this.add_option_grid.Visibility = System.Windows.Visibility.Visible;
        }
        private void Multiple_Choice_Checked(object sender, RoutedEventArgs e)
        {
            Text_Answer.IsChecked = false;
            Check_Box.IsChecked = false;
            x = 2;
            this.add_survey.Visibility = System.Windows.Visibility.Hidden;
            this.add_option_grid.Visibility = System.Windows.Visibility.Visible;
        }

        private void Text_Answer_Checked(object sender, RoutedEventArgs e)
        {
            Check_Box.IsChecked = false;
            Multiple_Choice.IsChecked = false;
            x = 3;
            add_option_grid.Visibility = System.Windows.Visibility.Hidden;
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            MySqlCommand addquestion = new MySqlCommand("INSERT INTO questions(QuestionDescription,QuestionType,Compulsory,Options,Page_number,survey) VALUES (@Q_D,@Q_T,@com,@op,@p_n,@sName)", connection);
            //Add survey name

            MySqlCommand cmd = new MySqlCommand("Select title from survey where title=@survey", connection);
            cmd.Parameters.AddWithValue("@survey", this.Survey_Name.Text);
            var result = cmd.ExecuteScalar();
            if (z == 1 && result != null)
            {
                MessageBox.Show("Attention!!!\nSurvey " + Survey_Name.Text + " already exist\nQuestions will be added on previous survey.");
            }
            else if (z == 1)
            {
                MySqlCommand addsurvey = new MySqlCommand("INSERT INTO survey(title,StartDate) VALUES (@sTitle,@date)", connection);
                addsurvey.Parameters.AddWithValue("@sTitle", Survey_Name.Text);
                addsurvey.Parameters.AddWithValue("@date", DateTime.Today.ToString("dd/MM/yyyy"));
                addsurvey.ExecuteNonQuery();
            }
            z++;
            //Data for adding question. 
            addquestion.Parameters.AddWithValue("@Q_D", this.Question_box.Text);
            addquestion.Parameters.AddWithValue("@Q_T", x);
            addquestion.Parameters.AddWithValue("@com", 1);
            addquestion.Parameters.AddWithValue("@op",options);
            addquestion.Parameters.AddWithValue("@p_n", 1);
            addquestion.Parameters.AddWithValue("@sName", Survey_Name.Text);
            //Run command
            addquestion.ExecuteNonQuery();
            MessageBox.Show("Success");
            //Extra Config
            options = null;
            Check_Box.IsChecked = false;
            Multiple_Choice.IsChecked = false;
            Question_box.Clear();
            //Config done
            connection.Close();
        }

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            options = options + option.Text + "#";
            //optionlist.Add(options);
            MessageBox.Show("Added");
            option.Clear();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            
            this.add_option_grid.Visibility = System.Windows.Visibility.Hidden;
            this.add_survey.Visibility = System.Windows.Visibility.Visible;
        }

        private void option_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Done1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
