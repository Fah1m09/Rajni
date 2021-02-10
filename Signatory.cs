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
    public partial class Signatory : MaterialForm
    {
        public Signatory()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=I:\\Project\\Rajni\\HemaDB.mdf;Integrated Security=True");

        private void Signatory_Load(object sender, EventArgs e)
        {
            refresh_DataGridView();
        }
        public void refresh_DataGridView()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("ShowSig", con);
                cmd.CommandType = CommandType.StoredProcedure;

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
                for (int i = 0; i < 4; i++)
                {
                    this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                tboxSigid.Text = "";
                tboxSig1.Text = "";
                tboxSig2.Text = "";
                tboxSig3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tboxSigid.Text != "") 
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("SearchSig", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sigid", tboxSigid.Text);

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
                    for (int i=0; i<4; i++)
                    {
                        this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }  
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }             
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tboxSigid.Text != "" && (tboxSig1.Text != "" || tboxSig2.Text != "" || tboxSig3.Text != "")) 
            {
                SqlCommand cmd = new SqlCommand("AddSig", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sigid", tboxSigid.Text);
                cmd.Parameters.AddWithValue("@sig1", tboxSig1.Text);
                cmd.Parameters.AddWithValue("@sig2", tboxSig2.Text);
                cmd.Parameters.AddWithValue("@sig3", tboxSig3.Text);

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
                System.Windows.Forms.MessageBox.Show("Insert Signature ID and Signature");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(tboxSigid.Text != "") 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DeleteSig", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@sigid", tboxSigid.Text);

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
    }
}
