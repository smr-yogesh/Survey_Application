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
    /// Interaction logic for Responder.xaml
    /// </summary>
    public partial class Responder : Window
    {
        MySqlConnection connection = new MySqlConnection("server=127.0.01;user=root;database=appproject;port=3306;password=");
        string selected_survey;
        string selected_question;
        string q_type,q_id;
        string answer;

        
        public Responder()
        {
            InitializeComponent();
            this.Survey_grid.Visibility = System.Windows.Visibility.Hidden;
            this.Answer_grid.Visibility = System.Windows.Visibility.Hidden;
            this.textbox_grid.Visibility = System.Windows.Visibility.Hidden;
            this.listbox_grid.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void Show_survey_Click(object sender, RoutedEventArgs e)
        {
            this.Survey_grid.Visibility = System.Windows.Visibility.Visible;
            survey_list.Items.Clear();
            try
            {
                MySqlDataAdapter cmd = new MySqlDataAdapter("SELECT title FROM survey ", connection);
                DataTable dt = new DataTable();
                cmd.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    survey_list.Items.Add(row["title"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Didn't found any surveys");
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (object data in survey_list.SelectedItems)
            {
                selected_survey = data.ToString();
                if(selected_survey == "")
                {
                    MessageBox.Show("Survey not selected");
                }
                this.responder_grid.Visibility = System.Windows.Visibility.Hidden;
                this.Answer_grid.Visibility = System.Windows.Visibility.Visible;
                try
                {
                    MySqlDataAdapter getquestion = new MySqlDataAdapter("SELECT QuestionDescription FROM questions WHERE survey=\"" + selected_survey +"\"", connection);
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

        private void Questions_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }
        private void answer_button_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            foreach (object data in Questions_list.SelectedItems)
            {
                selected_question = data.ToString();
            }
            question.Content = selected_question;
            MySqlCommand getquestionid = new MySqlCommand("SELECT Question_ID FROM questions WHERE QuestionDescription=\"" + selected_question + "\"", connection);
            q_id = getquestionid.ExecuteScalar().ToString();
            MySqlCommand getoptiontype = new MySqlCommand("SELECT QuestionType FROM questions WHERE QuestionDescription=\"" + selected_question + "\"", connection);
            q_type = getoptiontype.ExecuteScalar().ToString();
            if (q_type == "3")
            {
                textanswer_box.Clear();
                this.listbox_grid.Visibility = System.Windows.Visibility.Hidden;
                this.textbox_grid.Visibility = System.Windows.Visibility.Visible;
                connection.Close();
            }
            else if(q_type == "2")
            {
                this.textbox_grid.Visibility = System.Windows.Visibility.Hidden;
                this.listbox_grid.Visibility = System.Windows.Visibility.Visible;
                answer_listbox.SelectionMode = SelectionMode.Multiple;
                MySqlCommand getoption = new MySqlCommand("SELECT Options FROM questions WHERE QuestionDescription=\"" + selected_question + "\"", connection);
                string options = getoption.ExecuteScalar().ToString();
                string[] optionlist = options.Split('#');
                Array.Resize(ref optionlist, optionlist.Length - 1);
                answer_listbox.ItemsSource = optionlist;
                connection.Close();
            }
            else
            {
                this.textbox_grid.Visibility = System.Windows.Visibility.Hidden;
                this.listbox_grid.Visibility = System.Windows.Visibility.Visible;
                answer_listbox.SelectionMode = SelectionMode.Single;
                MySqlCommand getoption = new MySqlCommand("SELECT Options FROM questions WHERE QuestionDescription=\"" + selected_question + "\"", connection);
                string options = getoption.ExecuteScalar().ToString();
                string[] optionlist = options.Split('#');
                Array.Resize(ref optionlist, optionlist.Length - 1);
                answer_listbox.ItemsSource = optionlist;
                connection.Close();
            }



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
        private void Done_button_Click(object sender, RoutedEventArgs e)
        {
            question.Content = "";
            Questions_list.Items.Clear();
            this.Answer_grid.Visibility = System.Windows.Visibility.Hidden;
            this.responder_grid.Visibility = System.Windows.Visibility.Visible;
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show("You have been logged out");
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            if (q_type == "3")
            {
                answer = textanswer_box.Text;
            }
            else if (q_type == "2")
            {

                foreach (var item in answer_listbox.SelectedItems)
                {
                    answer = answer + item.ToString() + '#';
                }
            }
            else
            {
                foreach (object item in answer_listbox.SelectedItems)
                {
                    answer = item.ToString();
                }
                
            }
            connection.Open();
            MySqlCommand addanswer = new MySqlCommand("INSERT INTO answers(Question_ID,Answer_Description,answer_type) VALUES (@q_id,@answer,@a_type)", connection);
            addanswer.Parameters.AddWithValue("@q_id", int.Parse(q_id));
            addanswer.Parameters.AddWithValue("@answer", answer);
            addanswer.Parameters.AddWithValue("@a_type", int.Parse(q_type));
            addanswer.ExecuteNonQuery();
            MessageBox.Show("Saved");
            connection.Close();
            answer = null;
        }

        private void textanswer_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
    
}
