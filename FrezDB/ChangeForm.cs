using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace FrezDB
{
    public partial class ChangeForm : Form
    {
        private SqlConnection sqlConnection;
        private ListViewItem changeitem;
        public ChangeForm(ListViewItem item)
        {
            InitializeComponent();

            changeitem = item;
        }

        private void ChangeForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FrezDB"].ConnectionString);
            sqlConnection.Open();

            this.textBox1.Text = changeitem.SubItems[6].Text;
            this.textBox2.Text = changeitem.SubItems[3].Text;
            this.textBox3.Text = changeitem.SubItems[0].Text;
            this.textBox4.Text = changeitem.SubItems[2].Text;
            this.textBox5.Text = changeitem.SubItems[3].Text;
            this.textBox6.Text = changeitem.SubItems[4].Text;
            this.textBox7.Text = changeitem.SubItems[5].Text;
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

                    var command1 = "UPDATE [NameTable] SET " +
                    "fname = " + "\'" + textBox3.Text + "\'" + "," +
                    "description =" + "\'" + textBox1.Text + "\'" +
                    "WHERE [ID] = " + changeitem.Tag;


                    SqlCommand sqlCommand = new SqlCommand(command1, sqlConnection);

                    sqlCommand.ExecuteNonQuery();

                    var command2 = "UPDATE [Table] SET " +
                        "ftype = " + "\'" + textBox4.Text + "\'" + "," +
                        "D = " + textBox2.Text + "," +
                        "N = " + textBox5.Text + "," +
                        "LipN = " + textBox6.Text + "," +
                        "coating = " + "\'" + textBox7.Text + "\'" +
                        "WHERE [ID] = " + changeitem.Tag;

                    sqlCommand.CommandText = command2;

                    sqlCommand.ExecuteNonQuery();

                    MessageBox.Show("Изменено");
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
