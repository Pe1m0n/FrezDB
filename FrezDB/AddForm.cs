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
using System.Configuration;
using System.Xml;

namespace FrezDB
{
    public partial class AddForm : Form
    {
        private SqlConnection sqlConnection;

        public AddForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FrezDB"].ConnectionString);
            sqlConnection.Open();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBox4.Text = string.Empty;
            this.textBox5.Text = string.Empty;
            this.textBox6.Text = string.Empty;
            this.textBox7.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.textBox2.Text == string.Empty ||
               this.textBox3.Text == string.Empty ||
               this.textBox4.Text == string.Empty ||
               this.textBox5.Text == string.Empty ||
               this.textBox6.Text == string.Empty)

            {

                MessageBox.Show("Не все обязательные поля были заполненны!");

            }

            else
            {
                //clearing from backspase before and after text
                this.textBox1.Text = this.textBox1.Text.TrimEnd();
                this.textBox1.Text = this.textBox1.Text.TrimStart();
                this.textBox2.Text = this.textBox2.Text.TrimEnd();
                this.textBox2.Text = this.textBox2.Text.TrimStart();
                this.textBox3.Text = this.textBox3.Text.TrimEnd();
                this.textBox3.Text = this.textBox3.Text.TrimStart();
                this.textBox4.Text = this.textBox4.Text.TrimEnd();
                this.textBox4.Text = this.textBox4.Text.TrimStart();
                this.textBox5.Text = this.textBox5.Text.TrimEnd();
                this.textBox5.Text = this.textBox5.Text.TrimStart();
                this.textBox6.Text = this.textBox6.Text.TrimEnd();
                this.textBox6.Text = this.textBox6.Text.TrimStart();
                this.textBox7.Text = this.textBox7.Text.TrimEnd();
                this.textBox7.Text = this.textBox7.Text.TrimStart();
                //end clearing

                try
                {

                    var command1 = "INSERT INTO [NameTable] (fname,description) VALUES (" +
                    "\'" + textBox3.Text + "\'" + "," +
                    "\'" + textBox1.Text + "\'" + ")";


                    SqlCommand sqlCommand = new SqlCommand(command1, sqlConnection);

                    sqlCommand.ExecuteNonQuery();

                    var command2 = "Select @@Identity";

                    int ID;

                    sqlCommand.CommandText = command2;

                    ID = Convert.ToInt32(sqlCommand.ExecuteScalar());



                    var command3 = "INSERT INTO [Table] (id,ftype,D,N,LipN,coating) VALUES (" +
                        ID.ToString() + "," +
                        "\'" + textBox4.Text + "\'" + "," +
                        textBox2.Text + "," +
                        textBox5.Text + "," +
                        textBox6.Text + "," +
                        "\'" + textBox7.Text + "\'" + ")";

                    sqlCommand.CommandText = command3;

                    sqlCommand.ExecuteNonQuery();

                    MessageBox.Show("Добавленно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    
                    sqlConnection.Close();
                    this.Dispose();
                }




            }


        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check for psessing only numbers or backspace/delete
            char pressedkey = e.KeyChar;

            if (!(pressedkey == 127) && !(pressedkey == 8) && !Char.IsDigit(pressedkey))
            {
                e.Handled = true;
            }

        }


    }
}
