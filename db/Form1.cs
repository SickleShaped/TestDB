using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace db
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlConnection NorthConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);

            sqlConnection.Open();

            NorthConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthWindDB"].ConnectionString);
            NorthConnection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Products", NorthConnection);
            DataSet db = new DataSet();
            dataAdapter.Fill(db);
            dataGridView1.DataSource = db.Tables[0];

            /*if(NorthConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Parse(textBox3.Text);

            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Students] (Name, Subname, Birthday) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', '{date.Month}/{date.Day}/{date.Year}')",
                sqlConnection );

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                textBox4.Text,
                NorthConnection
                );

            DataSet dataSet= new DataSet();
            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            SqlDataReader dataReader = null;

            try
            {
                SqlCommand sqlComand = new SqlCommand("SELECT ProductName, QuantityPerUnit, UnitPrice FROM Products", NorthConnection);

                dataReader = sqlComand.ExecuteReader();
                ListViewItem item = null;

                while(dataReader.Read())
                {
                    item = new ListViewItem(new string[] { Convert.ToString(dataReader["ProductName"]), Convert.ToString(dataReader["QuantityPerUnit"]) , Convert.ToString(dataReader["UnitPrice"]) });
                    listView1.Items.Add(item);
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"ProductName LIKE '%{textBox4.Text}%'";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"UnitsInStock<=10";
                    break;

                case 1:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"UnitsInStock>=10 AND UnitsInStock<=50";
                    break;

                case 2:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"UnitsInStock>=50"; //чтобы сделать пустой фильтр задай его просто "", и тогда весь фильр сбросится

                    break;
            }
        }
    }
}
