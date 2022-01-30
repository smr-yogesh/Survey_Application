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
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Survey_Application
{
    /// <summary>
    /// Interaction logic for ManagerAccount.xaml
    /// </summary>
    public partial class ManagerAccount : Window
    {
        MySqlConnection connection = new MySqlConnection("server=127.0.01;user=root;database=appproject;port=3306;password=");
        string selected_survey;
        string selected_question,a_type;
        int q_id;
        public ManagerAccount()
        {
            InitializeComponent();
            this.Title = "Manager Account";
            this.loggedInManager_grid.Visibility = System.Windows.Visibility.Visible;
            this.information_grid.Visibility = System.Windows.Visibility.Hidden;
            this.responder_grid.Visibility = System.Windows.Visibility.Hidden;
            this.survey_grid.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Create_Respondent_button_Click(object sender, RoutedEventArgs e)
        {
            Add_respondent Dashboard = new Add_respondent();
            Dashboard.ShowDialog();
        }

        private void Responder_s_list_Click(object sender, RoutedEventArgs e)
        {
            this.responder_grid.Visibility = System.Windows.Visibility.Visible;
            this.survey_grid.Visibility = System.Windows.Visibility.Hidden;
            Responder_list.Items.Clear();
            try
            {
                MySqlDataAdapter cmd = new MySqlDataAdapter("SELECT Respondent_ID FROM key_a", connection);
                DataTable dt = new DataTable();
                cmd.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Responder_list.Items.Add(row["Respondent_ID"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Didn't found any responder keys");
            }

        }
        private void Create_Survey_Click(object sender, RoutedEventArgs e)
        {
            CreateSurvey Dashboard = new CreateSurvey();
            Dashboard.ShowDialog();
        }
        private void Check_survey_Click(object sender, RoutedEventArgs e)
        {
            this.responder_grid.Visibility = System.Windows.Visibility.Hidden;
            this.survey_grid.Visibility = System.Windows.Visibility.Visible;
            Survey_list.Items.Clear();
            try
            {
                MySqlDataAdapter cmd = new MySqlDataAdapter("SELECT title FROM survey ", connection);
                DataTable dt = new DataTable();
                cmd.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Survey_list.Items.Add(row["title"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Didn't found any surveys");
            }
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show("You have been logged out");
        }
        private void previous_Click(object sender, RoutedEventArgs e)
        {
            if (Questions_list.SelectedIndex > 0)
            {
                Questions_list.SelectedIndex = Questions_list.SelectedIndex - 1;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (Questions_list.SelectedIndex < Questions_list.Items.Count - 1)
            {
                Questions_list.SelectedIndex = Questions_list.SelectedIndex + 1;
            }
        }

        private void Survey_select_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (object data in Survey_list.SelectedItems)
            {
                selected_survey = data.ToString();
                if (selected_survey == "")
                {
                    MessageBox.Show("Survey not selected");
                }
                this.loggedInManager_grid.Visibility = System.Windows.Visibility.Hidden;
                this.responder_grid.Visibility = System.Windows.Visibility.Hidden;
                this.information_grid.Visibility = System.Windows.Visibility.Visible;
                try
                {
                    MySqlDataAdapter getquestion = new MySqlDataAdapter("SELECT QuestionDescription FROM questions WHERE survey=\"" + selected_survey + "\"", connection);
                    DataTable dt = new DataTable();
                    getquestion.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        Questions_list.Items.Add(row["QuestionDescription"].ToString());
                    }
                }
                catch
                {
                    MessageBox.Show("No questions available");
                }
            }
        }

        
        private void Done_button_Click(object sender, RoutedEventArgs e)
        {
            Questions_list.Items.Clear();
            this.information_grid.Visibility = System.Windows.Visibility.Hidden;
            this.loggedInManager_grid.Visibility = System.Windows.Visibility.Visible;

        }

        private void Show_answers_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            foreach (object data in Questions_list.SelectedItems)
            {
                selected_question = data.ToString();
            }
            //question.Content = selected_question;
            MySqlCommand getquestionid = new MySqlCommand("SELECT Question_ID FROM questions WHERE QuestionDescription=\"" + selected_question + "\"", connection);
            q_id = Convert.ToInt32(getquestionid.ExecuteScalar());
            MySqlCommand getanswertype = new MySqlCommand("SELECT answer_type FROM answers WHERE Question_ID=" + q_id , connection);
            a_type = getanswertype.ExecuteScalar().ToString();
            /* if (a_type == "3")
            {
                answerbox.Items.Clear();
                MySqlCommand getanswer = new MySqlCommand("SELECT Answer_Description FROM answers WHERE Question_ID=" + q_id, connection);
                string answer = getanswer.ExecuteScalar().ToString();
                answerbox.Items.Add(answer); 
                

                MySqlDataAdapter getquestion = new MySqlDataAdapter("SELECT Answer_Description FROM answers WHERE Question_ID=" + q_id, connection);
                DataTable dt = new DataTable();
                getquestion.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    answerbox.Items.Add(row["Answer_Description"].ToString());
                }
                connection.Close();
            } */
            //else
            if (a_type == "2")
            {
                answerbox.Items.Clear();
                MySqlCommand getanswer = new MySqlCommand("SELECT Answer_Description FROM answers WHERE Question_ID=" + q_id, connection);
                string answer = getanswer.ExecuteScalar().ToString();
                string[] answerlist = answer.Split('#');
                Array.Resize(ref answerlist, answerlist.Length - 1);
                answerbox.ItemsSource = answerlist;
                connection.Close();
            }
            else
            {
                answerbox.ItemsSource = null;
                answerbox.Items.Clear();
                MySqlDataAdapter getanswer = new MySqlDataAdapter("SELECT Answer_Description FROM answers WHERE Question_ID=" + q_id, connection);
                DataTable dt = new DataTable();
                getanswer.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    answerbox.Items.Add(row["Answer_Description"].ToString());
                }
                connection.Close();
            }
        }

        

        private void Questions_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void resp_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

        
    
