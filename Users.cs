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
using System.Windows.Input;

namespace HotelManagSystem
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\MyProjectsVS\HotelManagSystem\HotelManagSystem\HotelSystem.mdf;Integrated Security=True");
        private void populate()
        {
            con.Open();
            string Query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void InsertUser()
        {
            if (UnameTb.Text == "" || UphoneTb.Text == "" || PasswordTb.Text == "" || GenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserTbl(UserName, UserPhone, UserPassword, UserGender) values(@UName,@UPhone, @UPassword, @UGender)", con);
                    cmd.Parameters.AddWithValue("@UName", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UPhone", UphoneTb.Text);
                    cmd.Parameters.AddWithValue("@UGender", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UPassword", PasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        private void EditUser()
        {
            if (UnameTb.Text == "" || UphoneTb.Text == "" || PasswordTb.Text == "" || GenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE UserTbl SET UserName = @UName, UserPhone = @UPhone, UserPassword = @UPassword, UserGender = @UGender where UserId = @UKey", con);
                    cmd.Parameters.AddWithValue("@UName", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UPhone", UphoneTb.Text);
                    cmd.Parameters.AddWithValue("@UGender", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UPassword", PasswordTb.Text);
                    cmd.Parameters.AddWithValue("@UKey", Key);
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
        private void DeleteUser()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select user for deleting");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM UserTbl WHERE UserId = @UKey", con);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        int Key = 0;        

        private void UsersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UsersDGV.SelectedRows[0].Cells[1].Value.ToString();
            UphoneTb.Text = UsersDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenderCb.Text = UsersDGV.SelectedRows[0].Cells[3].Value.ToString();
            PasswordTb.Text = UsersDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (UnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(UsersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertUser();
            con.Close();

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditUser();
            con.Close();

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteUser();
            con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Types Obj = new Types();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Rooms Obj = new Rooms();
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
