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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            populate();
            
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\MyProjectsVS\HotelManagSystem\HotelManagSystem\HotelSystem.mdf;Integrated Security=True");
        private void populate()
        {
            con.Open();
            string Query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomersDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void InsertCustomer()
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustPassTb.Text == "" || CustInfoTb.Text == "" || GenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTbl(CustomerName, CustomerPassportRC, CustomerPhone, CustomerGender, CustomerInfo) values(@CustName,@CustPass, @CustPhone, @CustGender, @CustInfo)", con);
                    cmd.Parameters.AddWithValue("@CustName", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CustPass", CustPassTb.Text);
                    cmd.Parameters.AddWithValue("@CustPhone", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CustInfo", CustInfoTb.Text);
                    cmd.Parameters.AddWithValue("@CustGender", GenderCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        private void EditCustomer()
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustPassTb.Text == "" || CustInfoTb.Text == "" || GenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE CustomerTbl SET CustomerName = @CustName, CustPass = @CustPass, CustPhone = @CustPhone, CustomerGender = @CustGender where CustomerId = @CustKey", con);
                    cmd.Parameters.AddWithValue("@CustName", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CustPass", CustPassTb.Text);
                    cmd.Parameters.AddWithValue("@CustPhone", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CustInfo", CustInfoTb.Text);
                    cmd.Parameters.AddWithValue("@CustGender", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CustKey", Key);
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
        private void DeleteCustomer()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select customer for deleting");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM CustomerTbl WHERE CustomerId = @CustKey", con);
                    cmd.Parameters.AddWithValue("@CustKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer deleted");
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
        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustPassTb.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustPhoneTb.Text = CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustInfoTb.Text = CustomersDGV.SelectedRows[0].Cells[4].Value.ToString();
            GenderCb.Text = CustomersDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertCustomer();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditCustomer();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
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

        private void label7_Click(object sender, EventArgs e)
        {
            Booking Obj = new Booking();
            Obj.Show();
            this.Hide();
        }
    }
}
