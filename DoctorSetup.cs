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
using MaterialSkin;
using MaterialSkin.Controls;

namespace Rajni
{
    public partial class DoctorSetup : MaterialForm
    {
        
        public DoctorSetup()
        {
            InitializeComponent();          
            
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=I:\\Project\\Rajni\\HemaDB.mdf;Integrated Security=True");
        
        private void Form2_Load(object sender, EventArgs e)
        {
            refresh_DataGridView();
        }
        public void refresh_DataGridView()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM Doctor", con);
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);

                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid SQL Operation\n" + ex);
                }
                con.Close();

                dataGridView.DataSource = DS.Tables[0];
                for (int i = 0; i < 3; i++)
                {
                    this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                tboxDid.Text = "";
                tboxDname.Text = "";
                tboxDphone.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tboxDid.Text != "" && tboxDname.Text != "" && tboxDphone.Text != "" ) 
            {
                SqlCommand cmd = new SqlCommand("AddDoctor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Doc_Id", tboxDid.Text);
                cmd.Parameters.AddWithValue("@Doc_Name", tboxDname.Text);
                cmd.Parameters.AddWithValue("@Doc_mobile", tboxDphone.Text);
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid SQL Operation\n " + ex);

                }
                con.Close();
                refresh_DataGridView();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert Doctor ID, Name and Phone Number");
            }       
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(tboxDid.Text != "" && tboxDphone.Text != "") 
            {
                SqlCommand cmd = new SqlCommand("UpdateDoctor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Doc_Id", tboxDid.Text);
                cmd.Parameters.AddWithValue("@Doc_mobile", tboxDphone.Text);
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid SQL Operation\n " + ex);
                }
                con.Close();
                refresh_DataGridView();
                System.Windows.Forms.MessageBox.Show("Update Successful");
            }         
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tboxDid.Text != "") 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Doctor where Doc_Id = @Doc_Id", con);
                    cmd.Parameters.AddWithValue("@Doc_Id", tboxDid.Text);
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n " + ex);
                    }
                    con.Close();
                    refresh_DataGridView();
                    System.Windows.Forms.MessageBox.Show("Successfully Deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tboxDid.Text != "")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SearchDoctor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Doc_Id", tboxDid.Text);

                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet DS = new DataSet();
                    DA.Fill(DS);

                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n" + ex);
                    }
                    con.Close();

                    dataGridView.DataSource = DS.Tables[0];
                    for (int i = 0; i < 3; i++)
                    {
                        this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert Doctor id to search");
            }
        }
    }
}
