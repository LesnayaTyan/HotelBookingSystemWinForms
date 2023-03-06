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

namespace HotelManagSystem
{
    public partial class Rooms : Form
    {
        public Rooms()
        {
            InitializeComponent();
            populate();
            GetCategories();
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
            RoomsDGV.DataSource = ds.Tables[0];
            con.Close();
        }


        private void InsertRooms()
        {
            if(RoomNameTbl.Text == "" || RoomTypeCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
            try
            {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO RoomTbl(RoomName, RoomType, RoomStatus) values(@RName,@RType, @RStatus)", con);
            cmd.Parameters.AddWithValue("@RName", RoomNameTbl.Text);
            cmd.Parameters.AddWithValue("@RType", RoomTypeCb.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@RStatus", "Available");
            cmd.ExecuteNonQuery();
            MessageBox.Show("Room Added");
            con.Close();    
            populate();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            }

        }
        private void EditRooms()
        {
            if (RoomNameTbl.Text == "" || RoomTypeCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE RoomTbl SET RoomName = @RName, RoomType = @RType, RoomStatus = @RStatus where RoomId = @RKey", con);
                    cmd.Parameters.AddWithValue("@RName", RoomNameTbl.Text);
                    cmd.Parameters.AddWithValue("@RType", RoomTypeCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@RStatus", StatusCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        private void DeleteRooms()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select room for deleting");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM RoomTbl WHERE RoomId = @RKey", con);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room deleted");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        private void GetCategories()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from TypeTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeId", typeof(int));
            dt.Load(rdr);
            RoomTypeCb.ValueMember= "TypeId";
            RoomTypeCb.DataSource= dt;
            con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertRooms();
            
        }

        int Key = 0;
        private void RoomsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RoomNameTbl.Text= RoomsDGV.SelectedRows[0].Cells[1].Value.ToString();
            RoomTypeCb.Text = RoomsDGV.SelectedRows[0].Cells[2].Value.ToString();
            StatusCb.Text = RoomsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (RoomNameTbl.Text == "")
            {
                Key= 0;
            }
            else
            {
                Key = Convert.ToInt32(RoomsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditRooms();
           // con.Close();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteRooms();
            //con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Types Obj = new Types();
            Obj.Show();
            this.Hide();
        }
    }

}
