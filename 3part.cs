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
    public partial class part : MaterialForm
    {
        public part()
        {
            InitializeComponent();
            refresh_DataGridView();       
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=I:\\Project\\Rajni\\HemaDB.mdf;Integrated Security=True");
        private void _3part_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }         
        public void refresh_DataGridView()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ShowPatient", con);
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
                for (int i = 0; i < 9; i++)
                {
                    this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            try
            {
                SqlCommand cmd1 = new SqlCommand("ShowP3Data", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
                DataSet DS1 = new DataSet();
                DA1.Fill(DS1);
                con.Open();
                try
                {
                    cmd1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid SQL Operation\n" + ex);
                }
                con.Close();

                dataGridViewdt.DataSource = DS1.Tables[0];
                for (int i = 0; i < 20; i++)
                {
                    this.dataGridViewdt.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }           
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from PT where tdate between @datefrom and @dateto", con);

                cmd.Parameters.AddWithValue("@datefrom", dateFrom.Text);
                cmd.Parameters.AddWithValue("@dateto", dateTo.Text);

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

                dataGridViewdt.DataSource = DS.Tables[0];
                for (int i = 0; i < 9; i++)
                {
                    this.dataGridViewdt.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tboxPid.Text != "")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SearchPatient", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", tboxPid.Text);
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
                    for (int i = 0; i < 9; i++)
                    {
                        this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
                if (tboxPid.Text != "")
                {
                    SqlCommand cmd1 = new SqlCommand("Select wbc, lymp, grp, rbc, hgb, hct, mcv, mch, mchc, rdw_cv, rdw_sd, plt, mpv, pct, pdw, p_lcc, p_lcr from PT where pid = @pid", con);
                    cmd1.Parameters.AddWithValue("@pid", tboxPid.Text);
                    con.Open();
                    SqlDataReader da = cmd1.ExecuteReader();
                    while (da.Read())
                    {
                        tboxWbc.Text = da.GetValue(0).ToString();
                        tboxLyp.Text = da.GetValue(1).ToString();
                        tboxGrp.Text = da.GetValue(2).ToString();
                        tboxRbc.Text = da.GetValue(3).ToString();
                        tboxHgb.Text = da.GetValue(4).ToString();
                        tboxHct.Text = da.GetValue(5).ToString();
                        tboxMcv.Text = da.GetValue(6).ToString();
                        tboxMch.Text = da.GetValue(7).ToString();
                        tboxMchc.Text = da.GetValue(8).ToString();
                        tboxRdw_cv.Text = da.GetValue(9).ToString();
                        tboxRdw_sd.Text = da.GetValue(10).ToString();
                        tboxPlt.Text = da.GetValue(11).ToString();
                        tboxMpv.Text = da.GetValue(12).ToString();
                        tboxPct.Text = da.GetValue(13).ToString();
                        tboxPdw.Text = da.GetValue(14).ToString();
                        tboxP_lcc.Text = da.GetValue(15).ToString();
                        tboxP_lcr.Text = da.GetValue(16).ToString();
                        
                    }
                    con.Close();
                    int flag = 1;
                    if (flag == 1)
                    {
                        SqlCommand cmd2 = new SqlCommand("Select esr, mono, eso, baso, bt, ct from UserInput where pid = @pid", con);

                        cmd2.Parameters.AddWithValue("@pid", tboxPid.Text);
                        con.Open();
                        SqlDataReader da1 = cmd2.ExecuteReader();
                        while (da1.Read())
                        {
                            tboxEsr.Text = da1.GetValue(0).ToString();
                            tboxMop.Text = da1.GetValue(1).ToString();
                            tboxEso.Text = da1.GetValue(2).ToString();
                            tboxBaso.Text = da1.GetValue(3).ToString();
                            tboxBT.Text = da1.GetValue(4).ToString();
                            tboxCT.Text = da1.GetValue(5).ToString();
                        }
                        con.Close();
                        flag = 0;
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert valid Pid");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tboxPid.Text != "" && tboxPname.Text != "")
            {
                SqlCommand cmd = new SqlCommand("AddPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pid", tboxPid.Text);
                cmd.Parameters.AddWithValue("@p_name", tboxPname.Text);
                cmd.Parameters.AddWithValue("@age_date", tboxDate.Text);
                cmd.Parameters.AddWithValue("@age_month", tboxMonth.Text);
                cmd.Parameters.AddWithValue("@age_year", tboxYear.Text);
                cmd.Parameters.AddWithValue("@sex", cboxSex.Text);
                cmd.Parameters.AddWithValue("@phone", tboxPhone.Text);
                cmd.Parameters.AddWithValue("@p_address", tboxAddress.Text);
                cmd.Parameters.AddWithValue("@bed_no", tboxBedno.Text);

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
                tboxPid.Text = ""; tboxPname.Text = ""; tboxDate.Text = ""; tboxMonth.Text = ""; tboxYear.Text = "";
                cboxSex.Text = ""; tboxPhone.Text = ""; tboxBedno.Text = ""; tboxAddress.Text = "";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert Patient Details");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tboxPid.Text != "")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DeletePatient", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pid", tboxPid.Text);

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
                    tboxPid.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert valid Pid to delete Patient Record");
            }
        }

        private void btnParamUpdate_Click(object sender, EventArgs e)
        {
            if (tboxEsr.Text != "" && tboxWbc.Text != "" && tboxTotal.Text == "100")
            {
                SqlCommand cmd1 = new SqlCommand("UpdateUserinput", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                int key = 0;
                cmd1.Parameters.AddWithValue("@pid", tboxPid.Text);
                cmd1.Parameters.AddWithValue("@esr", tboxEsr.Text);
                cmd1.Parameters.AddWithValue("@eso", tboxEso.Text);
                cmd1.Parameters.AddWithValue("@baso", tboxBaso.Text);
                cmd1.Parameters.AddWithValue("@mono", tboxMop.Text);
                cmd1.Parameters.AddWithValue("@bt", tboxBT.Text);
                cmd1.Parameters.AddWithValue("@ct", tboxCT.Text);
                float tce, t;
                t = float.Parse(tboxEso.Text);
                tce = float.Parse(tboxWbc.Text) * t / 100;
                cmd1.Parameters.AddWithValue("@tce", tce);
                if (key == 0)
                {
                    con.Open();
                    try
                    {
                        cmd1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n " + ex);
                    }
                    con.Close();
                    key = 1;
                }
                SqlCommand cmd2 = new SqlCommand("UpdatePT", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@pid", tboxPid.Text);
                cmd2.Parameters.AddWithValue("@lyp", tboxLyp.Text);
                cmd2.Parameters.AddWithValue("@wbc", tboxWbc.Text);
                cmd2.Parameters.AddWithValue("@grp", tboxGrp.Text);
                cmd2.Parameters.AddWithValue("@rbc", tboxRbc.Text);
                cmd2.Parameters.AddWithValue("@hct", tboxHct.Text);
                cmd2.Parameters.AddWithValue("@mcv", tboxMcv.Text);
                cmd2.Parameters.AddWithValue("@mch", tboxMch.Text);
                cmd2.Parameters.AddWithValue("@mchc", tboxMchc.Text);
                cmd2.Parameters.AddWithValue("@rdw_cv", tboxRdw_cv.Text);
                cmd2.Parameters.AddWithValue("@rdw_sd", rdw_sd.Text);
                cmd2.Parameters.AddWithValue("@hgb", tboxHgb.Text);
                cmd2.Parameters.AddWithValue("@plt", tboxPlt.Text);
                cmd2.Parameters.AddWithValue("@mpv", tboxMpv.Text);
                cmd2.Parameters.AddWithValue("@pct", tboxPct.Text);
                cmd2.Parameters.AddWithValue("@pdw", tboxPdw.Text);
                cmd2.Parameters.AddWithValue("@p_lcc", tboxP_lcc.Text);
                cmd2.Parameters.AddWithValue("@p_lcr", tboxP_lcr.Text);
                if (key == 1)
                {
                    con.Open();
                    try
                    {
                        cmd2.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n " + ex);
                    }
                    con.Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Total must be 100");
                }
                refresh_DataGridView();
               // tboxEsr.Text = ""; tboxBT.Text = ""; tboxCT.Text = ""; tboxWbc.Text = ""; tboxLyp.Text = ""; tboxGrp.Text = ""; tboxMop.Text = ""; tboxEso.Text = "";
              //  tboxBaso.Text = ""; tboxRbc.Text = ""; tboxHgb.Text = ""; tboxHct.Text = ""; tboxMcv.Text = ""; tboxMch.Text = ""; tboxMchc.Text = ""; tboxRdw_cv.Text = ""; rdw_sd.Text = ""; tboxPlt.Text = "";
               // tboxMpv.Text = ""; tboxPct.Text = ""; tboxPdw.Text = ""; tboxP_lcc.Text = ""; tboxP_lcr.Text = ""; tboxMp.Text = ""; tboxBlast.Text = ""; tboxTotal.Text = "";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert a Pid and press search");
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (tboxPid.Text != "" && tboxSigid.Text != "" && tboxDocid.Text != "")
            {
                con.Open();

                SqlCommand cmd1 = new SqlCommand("SearchPatient", con);                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@pid", tboxPid.Text);

                SqlCommand cmd2 = new SqlCommand("SearchSig", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@sigid", tboxSigid.Text);

                SqlCommand cmd3 = new SqlCommand("SearchPT3", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@pid", tboxPid.Text);

                SqlCommand cmd4 = new SqlCommand("SearchDoctor", con);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@Doc_Id", tboxDocid.Text);

                SqlCommand cmd5 = new SqlCommand("SELECT * from TestMaping", con);

                SqlCommand cmd6 = new SqlCommand("SearchUserInput", con);
                cmd6.CommandType = CommandType.StoredProcedure;
                cmd6.Parameters.AddWithValue("@pid", tboxPid.Text);

                SqlCommand cmd28 = new SqlCommand("SearchWbcHist", con);
                cmd28.CommandType = CommandType.StoredProcedure;
                cmd28.Parameters.AddWithValue("@pid", tboxPid.Text);
                SqlCommand cmd29 = new SqlCommand("SearchRbcHist", con);
                cmd29.CommandType = CommandType.StoredProcedure;
                cmd29.Parameters.AddWithValue("@pid", tboxPid.Text);
                SqlCommand cmd30 = new SqlCommand("SearchPltHist", con);
                cmd30.CommandType = CommandType.StoredProcedure;
                cmd30.Parameters.AddWithValue("@pid", tboxPid.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);

                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);

                SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
                DataTable dt6 = new DataTable();
                da6.Fill(dt6);

                SqlDataAdapter da28 = new SqlDataAdapter(cmd28);
                DataTable dt28 = new DataTable();
                da28.Fill(dt28);

                SqlDataAdapter da29 = new SqlDataAdapter(cmd29);
                DataTable dt29 = new DataTable();
                da29.Fill(dt29);

                SqlDataAdapter da30 = new SqlDataAdapter(cmd30);
                DataTable dt30 = new DataTable();
                da30.Fill(dt30);

                ReportDataSource rds1 = new ReportDataSource("DataSet1", dt1);
                ReportDataSource rds2 = new ReportDataSource("SigData", dt2);
                ReportDataSource rds3 = new ReportDataSource("TestData", dt3);
                ReportDataSource rds4 = new ReportDataSource("DocData", dt4);
                ReportDataSource rds5 = new ReportDataSource("ParameterData", dt5);
                ReportDataSource rds6 = new ReportDataSource("UserInputData", dt6);                
                ReportDataSource rds28 = new ReportDataSource("histwbc", dt28);
                ReportDataSource rds29 = new ReportDataSource("histrbc", dt29);
                ReportDataSource rds30 = new ReportDataSource("histplt", dt30);

                reportViewer1.LocalReport.ReportPath = "I:\\Project\\Rajni\\Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds1);
                reportViewer1.LocalReport.DataSources.Add(rds2);
                reportViewer1.LocalReport.DataSources.Add(rds3);
                reportViewer1.LocalReport.DataSources.Add(rds4);
                reportViewer1.LocalReport.DataSources.Add(rds5);
                reportViewer1.LocalReport.DataSources.Add(rds6);          
                reportViewer1.LocalReport.DataSources.Add(rds28);
                reportViewer1.LocalReport.DataSources.Add(rds29);
                reportViewer1.LocalReport.DataSources.Add(rds30);
                reportViewer1.RefreshReport();
                System.Windows.Forms.MessageBox.Show("Report is Ready");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert Valid Pid, Sig id and Doc id to Print Report");
            }
        }

        private void tboxMop_TextChanged(object sender, EventArgs e)
        {
            try { tboxTotal.Text = (float.Parse(tboxLyp.Text) + float.Parse(tboxGrp.Text) + float.Parse(tboxMop.Text) + float.Parse(tboxBaso.Text) + float.Parse(tboxEso.Text)).ToString(); }
            catch { }

        }

        private void tboxLyp_TextChanged(object sender, EventArgs e)
        {

            try { tboxTotal.Text = (float.Parse(tboxLyp.Text) + float.Parse(tboxGrp.Text) + float.Parse(tboxMop.Text) + float.Parse(tboxBaso.Text) + float.Parse(tboxEso.Text)).ToString(); }
            catch { }
        }

        private void tboxGrp_TextChanged(object sender, EventArgs e)
        {

            try { tboxTotal.Text = (float.Parse(tboxLyp.Text) + float.Parse(tboxGrp.Text) + float.Parse(tboxMop.Text) + float.Parse(tboxBaso.Text) + float.Parse(tboxEso.Text)).ToString(); }
            catch { }
        }

        private void tboxEso_TextChanged(object sender, EventArgs e)
        {

            try { tboxTotal.Text = (float.Parse(tboxLyp.Text) + float.Parse(tboxGrp.Text) + float.Parse(tboxMop.Text) + float.Parse(tboxBaso.Text) + float.Parse(tboxEso.Text)).ToString(); }
            catch { }
        }

        private void tboxBaso_TextChanged(object sender, EventArgs e)
        {

            try { tboxTotal.Text = (float.Parse(tboxLyp.Text) + float.Parse(tboxGrp.Text) + float.Parse(tboxMop.Text) + float.Parse(tboxBaso.Text) + float.Parse(tboxEso.Text)).ToString(); }
            catch { }
        }

        private void tboxDocid_TextChanged(object sender, EventArgs e)
        {
            try {

                SqlCommand cmd1 = new SqlCommand("SELECT Doc_Name FROM Doctor WHERE Doc_Id = @did ", con);
                cmd1.Parameters.AddWithValue("@did", tboxDocid.Text);            
                    con.Open();
                    try
                    {
                        cmd1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid SQL Operation\n " + ex);
                    }
                    con.Close();                 
                }                     
            catch
            {

            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
