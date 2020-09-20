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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=BazaTest;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView3.Visible = true;
            button5.Visible = false;
            button4.Visible = false;
            button7.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT id, numer_sali FROM ZASOB2 WHERE RODZAJ_SALI='LABORATOR'", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView3.DataSource = dtbl;

            }

            textBox1.Visible = true;
            textBox2.Visible = true;
            
            label1.Visible = true;
           
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button3.Visible = false;
            dataGridView1.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT id, rodzaj FROM ZASOB2 WHERE RODZAJ!='SALA'", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }

            textBox1.Visible = true;
            textBox2.Visible = true;            
            label1.Visible = true;            
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button7.Visible = true;

            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            
            label1.Visible = true;
            
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;

            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT id, numer_sali FROM ZASOB2 WHERE RODZAJ_SALI='WYKLADOWA'", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView2.DataSource = dtbl;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button1.Visible) { }
            else button1.Visible = true;
            if (button2.Visible) { }
            else button2.Visible = true;
            if (button3.Visible) { }
            else button3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            label1.Visible = false;            
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button7.Visible = false;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.MaxLength = 10;
        }

        

        private void button7_Click(object sender, EventArgs e)
        {
            int hourResult=-1;
            int hourStart=-1;
            int hourEnd;
            int maxId=0;

           
                int.TryParse(textBox4.Text, out hourResult);
                int.TryParse(textBox3.Text, out hourStart);
           

            hourEnd = hourStart + 1;
            maxId=maxId + 1;
            SqlCommand cmd1;
            for (int i = 0; i < hourResult; i++)
            {
                string queryString = "select max(NUMER_REZERWACJI) FROM REZERWACJE";
                using (SqlConnection connection = new SqlConnection(conString))
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            try
                            {
                                maxId = reader.GetInt32(0);
                            }
                            catch (FormatException)
                            {
                                maxId = 0;
                            }

                            reader.Close();

                            maxId++;

                        }
                    }

                }

                if (hourStart >= 0 && hourStart <= 24)
                {                    
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    cmd1 = new SqlCommand("INSERT INTO Rezerwacje (NUMER_REZERWACJI, OD_data, Do_data, ZASÓB_ID, od_godzina, do_godzina) values( @maxId ,'" + this.textBox2.Text + "','" + this.textBox2.Text + "','" + this.textBox1.Text + "', @hourStart , @hourEnd)", con);
                    cmd1.Parameters.AddWithValue("@maxId", maxId);
                    cmd1.Parameters.AddWithValue("@hourStart", hourStart);
                    cmd1.Parameters.AddWithValue("@hourEnd", hourEnd);
                    cmd1.ExecuteNonQuery();


                    if (hourStart == 24)
                    {
                        hourStart = 1;
                    }
                    else
                    {
                        hourStart++;
                    }

                    if (hourEnd == 24)
                    {
                        hourEnd = 1;
                    }
                    else
                    {
                        hourEnd++;
                    }
                }
                else
                {
                    MessageBox.Show("Wprowadź poprawną godzinę");
                }
            }

            //MessageBox.Show("Rezerwacja złożona");

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.MaxLength = 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.MaxLength = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }
    }
}
