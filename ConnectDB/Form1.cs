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

namespace ConnectDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Show();
        }

        public void Show()
        {
            string db = @"Data Source=MANHDUCPC;Initial Catalog=QLSV;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(db);
            string qr = "select * from SV where MSSV = @M";
            SqlCommand cmd = new SqlCommand(qr, cnn);
            cmd.Parameters.Add("@M",SqlDbType.NVarChar);
            cmd.Parameters["@M"].Value = textBox1.Text;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV",typeof(string)),
                new DataColumn("NameSV",typeof(string)),
                new DataColumn("Gender",typeof(bool)),
                new DataColumn("NS",typeof(DateTime)),
                new DataColumn("ID_Lop",typeof(int))
            });
            cnn.Open();
            //int n = (int)cmd.ExecuteScalar();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                DataRow dr = dt.NewRow();
                dr["MSSV"] = r["MSSV"];
                dr["NameSV"] = r["NameSV"];
                dr["Gender"] = r["Gender"];
                dr["NS"] = Convert.ToDateTime(r["NS"]).Date;
                dr["ID_Lop"] = Convert.ToInt32(r["ID_Lop"]).ToString();
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            //cnn.Close();
            //textBox1.Text = n.ToString();


        }
        private void ShowDS()
        {
            string cmnstr = @"Data Source=MANHDUCPC;Initial Catalog=QLSV;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(cmnstr);
            string qr = "select * from SV";
          //  SqlCommand cmd = new SqlCommand(qr, cnn);
            SqlDataAdapter da = new SqlDataAdapter(qr,cnn);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            cnn.Open();
            //da.Fill(ds,"SV");
            da.Fill(dt);
            cnn.Close();
            // dataGridView1.DataSource = ds.Tables["SV"];
            dataGridView1.DataSource = dt;

        }
    }
}
