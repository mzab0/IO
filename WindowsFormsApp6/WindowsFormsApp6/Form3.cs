using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp6
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=BazaTest;Integrated Security=True";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.MaxLength = 10;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlCommand cmd1;
            SqlConnection con = new SqlConnection(conString);
            int godzinaDo;
            int godzinaOd;
            bool test=true;

            int numerSali = 0;
            string queryString = "select ID FROM ZASOB2 WHERE NUMER_SALI= '" + this.textBox4.Text + "'";
            
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        numerSali = reader.GetInt32(0);
                        reader.Close();
                    }
                }
            }
            int.TryParse(textBox2.Text, out godzinaOd);
            int.TryParse(textBox3.Text, out godzinaDo);
            if(textBox4.Text!="")
            { 
            do
            {
                    if (godzinaOd >= 0 && godzinaOd <= 24 && godzinaDo >= 0 && godzinaDo <= 24 && textBox1.Text!=""&&(textBox4.Text!=""||textBox5.Text!=""))
                    {
                        con.Open();
                        cmd1 = new SqlCommand("DELETE FROM REZERWACJE WHERE OD_DATA = '" + this.textBox1.Text + "' AND OD_GODZINA = @hourStart AND ZASÓB_ID = @numerSali", con);
                        cmd1.Parameters.AddWithValue("@numerSali", numerSali);
                        cmd1.Parameters.AddWithValue("@hourStart", godzinaOd);
                        cmd1.ExecuteNonQuery();
                        godzinaOd++;
                        //MessageBox.Show(Convert.ToString(godzinaOd));
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wprowadź odpowiednie godziny lub uzupełnij dane");
                        test = false;
                    }
            }
            while (godzinaOd < godzinaDo&&test);
            }
            else
            {
                do
                {
                    if (godzinaOd >= 0 && godzinaOd <= 24 && godzinaDo >= 0 && godzinaDo <= 24 && textBox1.Text != "" && (textBox4.Text != "" || textBox5.Text != ""))
                    {
                        con.Open();
                        cmd1 = new SqlCommand("DELETE FROM REZERWACJE WHERE OD_DATA = '" + this.textBox1.Text + "' AND OD_GODZINA = @hourStart AND ZASÓB_ID = '" + this.textBox5.Text + "'", con);
                        cmd1.Parameters.AddWithValue("@hourStart", godzinaOd);
                        cmd1.ExecuteNonQuery();
                        godzinaOd++;
                        con.Close();
                    }
                    else
                    { 
                        MessageBox.Show("Uzupełnij lub wprowadź poprawne dane");
                        test = false;
                    }
                }
                while (godzinaOd < godzinaDo&&test);
            }
            this.Close();
        }
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.MaxLength = 2;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.MaxLength = 2;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
