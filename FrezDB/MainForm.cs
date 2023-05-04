


using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FrezDB
{

    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection;

        public MainForm()
        {
            InitializeComponent();

            // Create an instance of a ListView column sorter and assign it
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;

            form2 = new AddForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FrezDB"].ConnectionString);
            sqlConnection.Open();
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //listView1.Columns[0].
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();

            var command = "";

            if (textBox1.Text != "")
            {
                command = "SELECT NameTable.[Id] as [ID], NameTable.[fname] as 'Firma', [Table].[ftype] as 'Type', [Table].[D] as 'Diam', [Table].[N] as 'Number', [Table].[lipN] as 'LipN', [Table].[coating] as 'Coating', NameTable.[description] as 'Description' FROM [Table] INNER JOIN [NameTable] ON [Table].[id] = [NameTable].[id]" +
                        "WHERE NameTable.[fname] LIKE '%" + textBox1.Text + "%'" +
                        "OR [Table].[coating] LIKE '%" + textBox1.Text + "%'" +
                        "OR NameTable.[description] LIKE '%" + textBox1.Text + "%'" +
                        "OR [Table].[ftype] LIKE '%" + textBox1.Text + "%'";
            }
            else
            {
                command = "SELECT NameTable.[Id] as [ID], NameTable.[fname] as 'Firma', [Table].[ftype] as 'Type', [Table].[D] as 'Diam', [Table].[N] as 'Number', [Table].[lipN] as 'LipN', [Table].[coating] as 'Coating', NameTable.[description] as 'Description' FROM [Table] INNER JOIN [NameTable] ON [Table].[id] = [NameTable].[id] ";
            }

            listView1.Items.Clear();


            SqlDataReader sqlreader = null;



            try
            {
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                sqlreader = sqlCommand.ExecuteReader();



                ListViewItem item = null;


                while (sqlreader.Read())
                {
                    item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlreader["Firma"]),
                        Convert.ToString(sqlreader["Type"]),
                        Convert.ToString(sqlreader["Diam"]),
                        Convert.ToString(sqlreader["Number"]),
                        Convert.ToString(sqlreader["LipN"]),
                        Convert.ToString(sqlreader["Coating"]),
                        Convert.ToString(sqlreader["Description"]),
                    });
                    item.Tag = sqlreader["ID"];
                    listView1.Items.Add(item);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlreader != null && !sqlreader.IsClosed)
                {
                    sqlreader.Close();
                }

            }

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();

        }

        private void главнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                searchbtn.Text = "Поиск";
            }
            else
            {
                searchbtn.Text = "Обновить";
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.Show();

            form2.BringToFront();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            form2.Dispose();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выделенные строки?", "Подтверждение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand sqlCommand = new SqlCommand("", sqlConnection);
                foreach (ListViewItem item in this.listView1.SelectedItems)
                {
                    sqlCommand.CommandText = "DELETE FROM [Table] WHERE [ID] = " + item.Tag;

                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = "DELETE FROM [NameTable] WHERE [ID] = " + item.Tag;

                    sqlCommand.ExecuteNonQuery();

                }

                searchbtn_Click(sender, e);
            }


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
            {

                MessageBox.Show("Выберите одну строку для изменения");

            }
            else
            {
                var ID = this.listView1.SelectedItems[0];

                ChangeForm form3 = new ChangeForm(ID);
                form3.Show();


            }
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите закрыть программу", "Подтверждение выхода", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                sqlConnection.Close();
                this.Close();

            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}