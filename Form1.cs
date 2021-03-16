using System;                                 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Rajni
{
    public partial class Form1 : MaterialForm
    {
        string dataIN;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=I:\\Project\\Rajni\\HemaDB.mdf;Integrated Security=True");
        string[] n = new string[300]; string[] q = new string[150]; int[] knwbc = new int[300]; int[] knrbc = new int[300]; int[] knplt = new int[150];
        string[] str = { "WBC", "LY%", "MI%", "GR%", "RBC", "HGB", "HCT", "MCV", "MCHC", "MCH", "RDW_CV", "RDW_SD", "PLT", "MPV", "PDW", "PCT", "P_LCR", "P_LCC" };
        string[] vari = new string[18]; string[] id = new string[2]; int count = 0;
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
            }
        }
        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = 115200;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = (StopBits)1;
                serialPort1.Parity = (Parity)0;
                serialPort1.Open();
                progressBar1.Value = 100;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                lblStatusCom.Text = "ON";
                serialPort1.Handshake = Handshake.None;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblStatusCom.Text = "OFF";
            }
        }       
        public Form1()
        {
            InitializeComponent();
            refresh_DataGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            dataIN = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }
        public void ShowData(object sender, EventArgs e)
        {
            tBoxDataIN.Text += dataIN;
            string dataparse = dataIN;
            string[] words = dataparse.Split('\n');
          
            foreach (var word in words)
            {
                if (word.Contains("PID"))
                {
                    string[] tup = word.Split('|');
                    id[0] = tup[2];                                   
                    count = 1;
                }
                
                if (word.Contains("OBR"))
                {
                    string[] tup = word.Split('|');
                    id[1] = tup[3];
                    count = 2; 
                }

                for (int i = 0; i < str.Length; i++)
                {
                    if (word.Contains(str[i]))
                    {
                        string[] tup = word.Split('|');
                        if (tup[1] == "1")
                        {
                            vari[i] = tup[5].ToString();
                        }
                        
                        count = 3;
                    }
                   // if (word.Contains("MCH") && word.Contains("MCHC") == "False" ) { }
                    /*   if (word.Contains("WBCHistogram") && word.Contains("ED"))
                      {
                          string[] tup = word.Split('|');

                          if (tup[5] != "")
                          {
                              string[] ht = tup[5].Split('^');

                              for (int k = 10; k < 280; k += 2)
                              {
                                  n[k] = ht[4][k] + "" + ht[4][k + 1];
                                  knwbc[k] = Convert.ToInt32(n[k], 16);
                              }
                          }
                          count = 5;
                      }
                      if (word.Contains("RBCHistogram") && word.Contains("ED"))
                      {
                          string[] tup = word.Split('|');

                          if (tup[5] != "")
                          {
                              string[] ht = tup[5].Split('^');
                              for (int s = 10; s < 270; s += 2)
                              {
                                  n[s] = ht[4][s] + "" + ht[4][s + 1];
                                  knrbc[s] = (Convert.ToInt32(n[s], 16));
                              }
                          }
                          count = 6;
                      }
                     if (word.Contains("PLTHistogram") && word.Contains("ED"))
                      {
                          string[] tup = word.Split('|');

                          if (tup[5] != "")
                          {
                              string[] ht = tup[5].Split('^');
                              System.Console.WriteLine(ht[4]);
                              for (int y = 0; y < 100; y += 2)
                              {
                                  q[y] = ht[4][y] + "" + ht[4][y + 1];
                                  knplt[y] = Convert.ToInt32(q[y], 16);
                              }
                          }
                          count = 7;
                      }*/
                }
            }
            int flag = 0;
            if (flag == 0 && count ==4)
            {
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT (pid) FROM PT WHERE pid ='" + id[0] + "' ", con);
                con.Open();
                int i = int.Parse(cmd1.ExecuteScalar().ToString());
                con.Close();
                if (i > 0)
                { }
                else
                {
                    SqlCommand cmd = new SqlCommand("AddPpT3", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@pid", id[0]);
                    cmd.Parameters.AddWithValue("@iid", id[1]);
                    cmd.Parameters.AddWithValue("@tdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@wbc", float.Parse(vari[0]) * 1000);
                    cmd.Parameters.AddWithValue("@lymp", vari[1]);
                    cmd.Parameters.AddWithValue("@grp", vari[3]);
                    cmd.Parameters.AddWithValue("@rbc", vari[4]);
                    cmd.Parameters.AddWithValue("@hgb", vari[5]);
                    cmd.Parameters.AddWithValue("@hct", vari[6]);
                    cmd.Parameters.AddWithValue("@mcv", vari[7]);
                    cmd.Parameters.AddWithValue("@mch", vari[9]);
                    cmd.Parameters.AddWithValue("@mchc", vari[8]);
                    cmd.Parameters.AddWithValue("@rdw_cv", vari[10]);
                    cmd.Parameters.AddWithValue("@rdw_sd", vari[11]);
                    cmd.Parameters.AddWithValue("@plt", float.Parse(vari[12]) * 1000);
                    cmd.Parameters.AddWithValue("@mpv", vari[13]);
                    cmd.Parameters.AddWithValue("@pdw", vari[14]);
                    cmd.Parameters.AddWithValue("@pct", vari[15]);
                    cmd.Parameters.AddWithValue("@p_lcr", vari[16]);
                    cmd.Parameters.AddWithValue("@p_lcc", vari[17]);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n" + ex);
                    }
                    con.Close();
                }
                flag = 1;
            }

             if (flag == 1)
            {
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT (pid) FROM UserInput WHERE pid ='" + id[0] + "' ", con);
                con.Open();
                int i = int.Parse(cmd1.ExecuteScalar().ToString());
                con.Close();
                if (i > 0)
                {
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("AddUserInput", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd2.Parameters.AddWithValue("@pid", id[0]);
                    cmd2.Parameters.AddWithValue("@eso", float.Parse(vari[2]) * 0.3);
                    cmd2.Parameters.AddWithValue("@baso", 0);
                    cmd2.Parameters.AddWithValue("@mono", float.Parse(vari[2]) * 0.7);
                    try
                    {
                        cmd2.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n" + ex);
                    }
                    con.Close();
                }                              
              //  flag = 2;
            }
             if (flag == 2)
            {
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT (pid) FROM HistWbc WHERE pid ='" + id[0] + "' ", con);
                con.Open();
                int i = int.Parse(cmd1.ExecuteScalar().ToString());
                con.Close();
                if (i > 0)
                {
                }
                else
                {
                    for (int k = 14; k < 300; k = k + 2)
                    {

                        SqlCommand cmd3 = new SqlCommand("AddhistogramWBC", con);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd3.Parameters.AddWithValue("@pid", id[0]);
                        cmd3.Parameters.AddWithValue("@ycord", knwbc[k]);
                        cmd3.Parameters.AddWithValue("@hr_range", k);
                        try
                        {
                            cmd3.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid SQL Operation\n" + ex);
                        }
                        con.Close();
                    }
                    flag = 3;
                }
          
            }
            if (flag == 3)
            {
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT (pid) FROM HistTable WHERE pid ='" + id[0] + "' ", con);
                con.Open();
                int i = int.Parse(cmd1.ExecuteScalar().ToString());
                con.Close();
                if (i > 0)
                {
                }
                else
                {
                    for (int l = 30; l < 300; l = l + 2)
                    {
                        SqlCommand cmd4 = new SqlCommand("AddhistogramRBC", con);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd4.Parameters.AddWithValue("@pid", id[0]);
                        cmd4.Parameters.AddWithValue("@ycord", knrbc[l]);
                        cmd4.Parameters.AddWithValue("@hr_range", l);
                        try
                        {
                            cmd4.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid SQL Operation\n" + ex);
                        }
                        con.Close();

                    }
                }
                
                flag = 4;
            }
            if (flag == 4)
            {
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT (pid) FROM HistPlt WHERE pid ='" + id[0] + "' ", con);
                con.Open();
                int i = int.Parse(cmd1.ExecuteScalar().ToString());
                con.Close();
                if (i > 0)
                {
                }
                else
                {
                    for (int m = 0; m < 150; m = m + 2)
                    {
                        SqlCommand cmd5 = new SqlCommand("AddhistogramPLT", con);
                        cmd5.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd5.Parameters.AddWithValue("@pid", id[0]);
                        cmd5.Parameters.AddWithValue("@ycord", knplt[m]);
                        cmd5.Parameters.AddWithValue("@hr_range", m);
                        try
                        {
                            cmd5.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid SQL Operation\n" + ex);
                        }
                        con.Close();
                    }
                }            
            }
            refresh_DataGridView();
        }

        public void refresh_DataGridView()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM PT", con);
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
                for (int i = 0; i < 20; i++)
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
}
