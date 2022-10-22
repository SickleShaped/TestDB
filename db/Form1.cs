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
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace db
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);

            sqlConnection.Open();

            if(sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Parse(textBox3.Text);

            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Students] (Name, Subname, Birthday) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', '{date.Month}/{date.Day}/{date.Year}')",
                sqlConnection );

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }
    }
}
