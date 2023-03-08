using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagSystem
{
    public partial class Booking : Form
    {
        public Booking()
        {
            InitializeComponent();
            populate();
            GetRoom();
            GetCustomer();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\MyProjectsVS\HotelManagSystem\HotelManagSystem\HotelSystem.mdf;Integrated Security=True");
        private void populate()
        {
            con.Open();
            string Query = "select * from RoomTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void GetRoom()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from RoomTbl where RoomStatus = 'Available'", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomId", typeof(int));
            dt.Load(rdr);
            RoomCb.ValueMember = "RoomId";
            RoomCb.DataSource = dt;
            con.Close();
        }
        private void FetchCost()
        {
            con.Open();
            //string query =
            con.Close();
        }
        private void GetCustomer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CustomerTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerId", typeof(int));
            dt.Load(rdr);
            CustomerCb.ValueMember = "CustomerId";
            CustomerCb.DataSource = dt;
            con.Close();
        }
        private void BookBtn_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Rooms Obj = new Rooms();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Types Obj = new Types();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }
    }
}
