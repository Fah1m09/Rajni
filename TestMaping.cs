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
using Microsoft.Reporting.WinForms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Rajni
{
    public partial class TestMaping : MaterialForm
    {
        public TestMaping()
        {
            InitializeComponent();
            refresh_DataGridView();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=I:\\Project\\Rajni\\HemaDB.mdf;Integrated Security=True");

        public void refresh_DataGridView()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM TestMaping", con);
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
                for (int i = 0; i < 5; i++)
                {
                    this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                tboxPcode.Text = "";
                tboxUnit.Text = "";
                tboxParameter.Text = "";
                tboxGroup.Text = "";
                tboxRange.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (tboxPcode.Text != "")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SearchTest", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pcode", tboxPcode.Text);

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
                    for (int i = 0; i > 7; i++)
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
                System.Windows.Forms.MessageBox.Show("Insert Pcode to search");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tboxPcode.Text != "" && (tboxUnit.Text != "" || tboxRange.Text != "") ) 
            {
                SqlCommand cmd = new SqlCommand("AddTest", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pcode", tboxPcode.Text);
                cmd.Parameters.AddWithValue("@unit", tboxUnit.Text);
                cmd.Parameters.AddWithValue("@parameter", tboxParameter.Text);
                cmd.Parameters.AddWithValue("@g_group", tboxGroup.Text);
                cmd.Parameters.AddWithValue("@r_range", tboxRange.Text);

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
                System.Windows.Forms.MessageBox.Show("Insert Pcode, Unit or Range");
            }
        }
    }    
}
