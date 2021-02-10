﻿using System;                                 
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
        int temp = 0;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=I:\\Project\\Rajni\\HemaDB.mdf;Integrated Security=True");
        string dtwbchist, dtrbchist, dtplthist;
        double wbc, plt, mono, eso;
        string[] hist = { "WBCHistogram", "RBCHistogram", "PLTHistogram" };
        string[] str = { "WBC", "LY%", "MI%", "GR%", "RBC", "HGB", "HCT", "MCV", "MCH", "MCHC", "RDW_CV", "RDW_SD", "PLT", "MPV", "PDW", "PCT", "P_LCR", "P_LCC" };
        float[] vari = new float[18]; float[] id = new float[2];
        int[] histdata = new int[3];

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
            tBoxDataIN.Text+= dataIN;
            string dataparse = dataIN;
         
            string[] words = dataparse.Split('\n');
            foreach (var word in words)
            {
                if (word.Contains("PID"))
                {
                    string[] tup = word.Split('|');
                    id[0] = float.Parse(tup[2]);
                }
            }
            foreach (var word in words)
            {
                if (word.Contains("OBR"))
                {
                    string[] tup = word.Split('|');                   
                    id[1] = float.Parse(tup[3]);                                   
                }
            }           
            foreach (var word in words)
            {
               
                    if (word.Contains(str[i]))
                    {
                        string[] tup = word.Split('|');
                        if (tup[1] == "1")
                        {
                            vari[i] = float.Parse(tup[5].ToString());
                        }
                    }
                    if (word.Contains(hist[0]) && word.Contains("ED"))
                    {
                        string[] tup = word.Split('|');
                        
                            if (tup[5] != "")
                            {
                                string[] ht = tup[5].Split('^');
                                //histdata[0] = ht[4].ToString();
                            }
                        
            }       }        
                int flag = 0;
                if (flag == 0)
                {
                    SqlCommand cmd = new SqlCommand("AddPpT3", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    cmd.Parameters.AddWithValue("@pid", id[0]);
                    cmd.Parameters.AddWithValue("@iid", id[1]);
                    cmd.Parameters.AddWithValue("@tdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@wbc", vari[0]);
                    cmd.Parameters.AddWithValue("@lymp", vari[1]);
                    cmd.Parameters.AddWithValue("@midp", vari[2]);
                    cmd.Parameters.AddWithValue("@grp", vari[3]);
                    cmd.Parameters.AddWithValue("@rbc", vari[4]);
                    cmd.Parameters.AddWithValue("@hgb", vari[5]);
                    cmd.Parameters.AddWithValue("@hct", vari[6]);
                    cmd.Parameters.AddWithValue("@mcv", vari[7]);
                    cmd.Parameters.AddWithValue("@mch", vari[8]);
                    cmd.Parameters.AddWithValue("@mchc", vari[9]);
                    cmd.Parameters.AddWithValue("@rdw_cv", vari[10]);
                    cmd.Parameters.AddWithValue("@rdw_sd", vari[11]);
                    cmd.Parameters.AddWithValue("@plt", vari[12]);
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
                    flag = 1;
                }

                if (flag == 1)
                {
                    SqlCommand cmd2 = new SqlCommand("AddUserInput", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd2.Parameters.AddWithValue("@pid", id[0]);
                    cmd2.Parameters.AddWithValue("@eso", eso);
                    try
                    {
                        cmd2.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n" + ex);
                    }
                    con.Close();
                    flag = 2;
                }
                if (flag == 2)
                {
                    string cd1 = "wbc", cd2 = "rbc", cd3 = "plt";
                    SqlCommand cmd3 = new SqlCommand("Addhistogram", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd3.Parameters.AddWithValue("@pid", id[0]);
                    cmd3.Parameters.AddWithValue("@code", cd1);
                    cmd3.Parameters.AddWithValue("@ycord", histdata[0]);                   
                    try
                    {
                        cmd3.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n" + ex);
                    }
                    con.Close();
                    temp = temp + 1; 
                }          
            refresh_DataGridView();
        }
        public void refresh_DataGridView()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ShowP3Data", con);
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
                for (int i=0; i<21; i++)
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





