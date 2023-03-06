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
    public partial class Types : Form
    {
        public Types()
        {
            InitializeComponent();
            populate();
            GetCategories();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\MyProjectsVS\HotelManagSystem\HotelManagSystem\HotelSystem.mdf;Integrated Security=True");

        private void populate()
        {
            con.Open();
            string Query = "select * from TypeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TypesDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void InsertCategories()
        {
            if (TypeNameTb.Text == "" || CostTb.Text == "")
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO TypeTbl(TypeName,TypeCost) values(@TName,@TCost)", con);
                    cmd.Parameters.AddWithValue("@TName", TypeNameTb.Text);
                    cmd.Parameters.AddWithValue("@TCost", CostTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        private void EditCategorie()
        {
            if (TypeNameTb.Text == "" || CostTb.Text == "")
            {
                MessageBox.Show("There smt is missing");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE TypeTbl SET TypeName = @TName,TypeCost = @TCost where TypeId = @TKey", con);
                    cmd.Parameters.AddWithValue("@TName", TypeNameTb.Text);
                    cmd.Parameters.AddWithValue("@TCost", CostTb.Text);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Updated");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void DeleteCategorie()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select type for deleting");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM TypeTbl WHERE TypeId = @TKey", con);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category deleted");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exist some reservation with this category");
                    MessageBox.Show(ex.Message);
                    
                }

            }

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertCategories();
            con.Close();
        }
        


        private void label3_Click(object sender, EventArgs e)
        {
            Rooms Obj = new Rooms();
            Obj.Show();
            this.Hide();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditCategorie();
            con.Close();

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteCategorie();
            con.Close();

        }
        int Key = 0;
        private void GetCategories()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from RoomTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomId", typeof(int));
            dt.Load(rdr);
            //TypeId.ValueMember = "RoomId";
            //RoomNameCb.DataSource = dt;
            con.Close();
        }
        private void TypesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TypeNameTb.Text = TypesDGV.SelectedRows[0].Cells[1].Value.ToString();
            CostTb.Text = TypesDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (TypeNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TypesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
