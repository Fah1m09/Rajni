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
                for (int i = 0; i < 21; i++)
                {
                    this.dataGridViewdt.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }           
        }
 
        private void btnGo_Click_1(object sender, EventArgs e)
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
        private void Search_Click(object sender, EventArgs e)
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
            SqlCommand cmd1 = new SqlCommand("Select wbc, lymp, grp, midp, rbc, hgb, hct, mcv, mch, mchc, rdw_cv, rdw_sd, plt, mpv, pct, pdw, p_lcc, p_lcr from PT where pid = @pid", con);
            cmd1.Parameters.AddWithValue("@pid", tboxPid.Text);
            con.Open();
            SqlDataReader da = cmd1.ExecuteReader();
            while (da.Read())
            {
                tboxWbc.Text = da.GetValue(0).ToString();
                tboxLyp.Text = da.GetValue(1).ToString();
                tboxGrp.Text = da.GetValue(2).ToString();
                tboxMop.Text = da.GetValue(3).ToString();
                tboxRbc.Text = da.GetValue(4).ToString();
                tboxHgb.Text = da.GetValue(5).ToString();
                tboxHct.Text = da.GetValue(6).ToString();
                tboxMcv.Text = da.GetValue(7).ToString();
                tboxMch.Text = da.GetValue(8).ToString();
                tboxMchc.Text = da.GetValue(9).ToString();
                tboxRdw_cv.Text = da.GetValue(10).ToString();
                tboxRdw_sd.Text = da.GetValue(11).ToString();
                tboxPlt.Text = da.GetValue(12).ToString();
                tboxMpv.Text = da.GetValue(13).ToString();
                tboxPct.Text = da.GetValue(14).ToString();
                tboxPdw.Text = da.GetValue(15).ToString();
                tboxP_lcc.Text = da.GetValue(16).ToString();
                tboxP_lcr.Text = da.GetValue(17).ToString();
            }
                con.Close();
                int flag = 1;
                if (flag == 1)
                {
                    SqlCommand cmd2 = new SqlCommand("Select esr, eso, baso, bt, ct from UserInput where pid = @pid", con);

                    cmd2.Parameters.AddWithValue("@pid", tboxPid.Text);
                    con.Open();
                    SqlDataReader da1 = cmd2.ExecuteReader();
                    while (da1.Read())
                    {
                        tboxEsr.Text = da1.GetValue(0).ToString();
                        tboxEso.Text = da1.GetValue(1).ToString();
                        tboxBaso.Text = da1.GetValue(2).ToString();
                        tboxBT.Text = da1.GetValue(3).ToString();
                        tboxCT.Text = da1.GetValue(4).ToString();
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

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (tboxPid.Text != "" && tboxPname.Text != "")
            {
                SqlCommand cmd = new SqlCommand("AddPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pid", tboxPid.Text);
                cmd.Parameters.AddWithValue("@p_name", tboxPname.Text);
                cmd.Parameters.AddWithValue("@age", tboxAge.Text);
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
                tboxPid.Text = "";
                tboxPname.Text = "";
                tboxAge.Text = "";
                cboxSex.Text = "";
                tboxPhone.Text = "";
                tboxBedno.Text = "";
                tboxAddress.Text = "";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert Patient Details");
            }
        }

        private void btnParamUpdate_Click(object sender, EventArgs e)
        {
            if (tboxEsr.Text != "" && tboxWbc.Text != "")
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
                cmd2.Parameters.AddWithValue("@rdw_sd", tboxRdw_sd.Text);
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
                refresh_DataGridView();
                tboxEsr.Text = "";
                tboxBT.Text = "";
                tboxCT.Text = "";
                tboxWbc.Text = "";
                tboxLyp.Text = "";
                tboxGrp.Text = "";
                tboxMop.Text = "";
                tboxEso.Text = "";
                tboxBaso.Text = "";
                tboxRbc.Text = "";
                tboxHgb.Text = "";
                tboxHct.Text = "";
                tboxMcv.Text = "";
                tboxMch.Text = "";
                tboxMchc.Text = "";
                tboxRdw_cv.Text = "";
                tboxRdw_sd.Text = "";
                tboxPlt.Text = "";
                tboxMpv.Text = "";
                tboxPct.Text = "";
                tboxPdw.Text = "";
                tboxP_lcc.Text = "";
                tboxP_lcr.Text = "";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert a Pid and press search");
            }
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            if (tboxPid.Text != "" && tboxSigid.Text != "" && tboxDocid.Text != "")
            {
                con.Open();

                SqlCommand cmd1 = new SqlCommand("SearchPatient", con);
                cmd1.CommandType = CommandType.StoredProcedure;
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

                SqlCommand cmd7 = new SqlCommand("SELECT * FROM WBCRange WHERE pcode = 1", con);
                SqlCommand cmd8 = new SqlCommand("SELECT * from LY where pcode = 2", con);
                SqlCommand cmd9 = new SqlCommand("SELECT * from GR where pcode = 3", con);
                SqlCommand cmd10 = new SqlCommand("SELECT * from ESO where pcode = 5", con);
                SqlCommand cmd11 = new SqlCommand("SELECT * from BASO where pcode = 6", con);
                SqlCommand cmd12 = new SqlCommand("SELECT * from MONO where pcode = 4", con);

                SqlCommand cmd13 = new SqlCommand("SELECT * FROM RBC WHERE pcode = 7", con);
                SqlCommand cmd14 = new SqlCommand("SELECT * from HCT where pcode = 8", con);
                SqlCommand cmd15 = new SqlCommand("SELECT * from MCV where pcode = 9", con);
                SqlCommand cmd16 = new SqlCommand("SELECT * from MCH where pcode = 10", con);
                SqlCommand cmd17 = new SqlCommand("SELECT * from MCHC where pcode = 11", con);
                SqlCommand cmd18 = new SqlCommand("SELECT * from RDW_CD where pcode = 12", con);
                SqlCommand cmd19 = new SqlCommand("SELECT * from RDW_SD where pcode = 13", con);

                SqlCommand cmd20 = new SqlCommand("SELECT * FROM PLT WHERE pcode = 14", con);
                SqlCommand cmd21 = new SqlCommand("SELECT * from MPV where pcode = 15", con);
                SqlCommand cmd22 = new SqlCommand("SELECT * from PCT where pcode = 16", con);
                SqlCommand cmd23 = new SqlCommand("SELECT * from PDW where pcode = 17", con);
                SqlCommand cmd24 = new SqlCommand("SELECT * from P_LCC where pcode = 18", con);
                SqlCommand cmd25 = new SqlCommand("SELECT * from P_LCR where pcode = 19", con);
                SqlCommand cmd26 = new SqlCommand("SELECT * from HGB where pcode = 20", con);
                SqlCommand cmd27 = new SqlCommand("SELECT * from ESR where pcode = 21", con);

                SqlCommand cmd28 = new SqlCommand("SearchWbcHist", con);
                SqlCommand cmd29 = new SqlCommand("SearchRbcHist", con);
                SqlCommand cmd30 = new SqlCommand("SearchPltHist", con);


                cmd28.CommandType = CommandType.StoredProcedure;
                cmd28.Parameters.AddWithValue("@pid", tboxPid.Text);

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

                SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
                DataTable dt7 = new DataTable();
                da7.Fill(dt7);

                SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                DataTable dt8 = new DataTable();
                da8.Fill(dt8);

                SqlDataAdapter da9 = new SqlDataAdapter(cmd9);
                DataTable dt9 = new DataTable();
                da9.Fill(dt9);

                SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                DataTable dt10 = new DataTable();
                da10.Fill(dt10);

                SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                DataTable dt11 = new DataTable();
                da11.Fill(dt11);

                SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
                DataTable dt12 = new DataTable();
                da12.Fill(dt12);

                SqlDataAdapter da13 = new SqlDataAdapter(cmd13);
                DataTable dt13 = new DataTable();
                da13.Fill(dt13);

                SqlDataAdapter da14 = new SqlDataAdapter(cmd14);
                DataTable dt14 = new DataTable();
                da14.Fill(dt14);

                SqlDataAdapter da15 = new SqlDataAdapter(cmd15);
                DataTable dt15 = new DataTable();
                da15.Fill(dt15);

                SqlDataAdapter da16 = new SqlDataAdapter(cmd16);
                DataTable dt16 = new DataTable();
                da16.Fill(dt16);

                SqlDataAdapter da17 = new SqlDataAdapter(cmd17);
                DataTable dt17 = new DataTable();
                da17.Fill(dt17);

                SqlDataAdapter da18 = new SqlDataAdapter(cmd18);
                DataTable dt18 = new DataTable();
                da18.Fill(dt18);

                SqlDataAdapter da19 = new SqlDataAdapter(cmd19);
                DataTable dt19 = new DataTable();
                da19.Fill(dt19);

                SqlDataAdapter da20 = new SqlDataAdapter(cmd20);
                DataTable dt20 = new DataTable();
                da20.Fill(dt20);

                SqlDataAdapter da21 = new SqlDataAdapter(cmd21);
                DataTable dt21 = new DataTable();
                da21.Fill(dt21);

                SqlDataAdapter da22 = new SqlDataAdapter(cmd22);
                DataTable dt22 = new DataTable();
                da22.Fill(dt22);

                SqlDataAdapter da23 = new SqlDataAdapter(cmd23);
                DataTable dt23 = new DataTable();
                da23.Fill(dt23);

                SqlDataAdapter da24 = new SqlDataAdapter(cmd24);
                DataTable dt24 = new DataTable();
                da24.Fill(dt24);

                SqlDataAdapter da25 = new SqlDataAdapter(cmd25);
                DataTable dt25 = new DataTable();
                da25.Fill(dt25);

                SqlDataAdapter da26 = new SqlDataAdapter(cmd26);
                DataTable dt26 = new DataTable();
                da26.Fill(dt26);

                SqlDataAdapter da27 = new SqlDataAdapter(cmd27);
                DataTable dt27 = new DataTable();
                da27.Fill(dt27);

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
                ReportDataSource rds7 = new ReportDataSource("wbc", dt7);
                ReportDataSource rds8 = new ReportDataSource("ly", dt8);
                ReportDataSource rds9 = new ReportDataSource("gr", dt9);
                ReportDataSource rds10 = new ReportDataSource("eso", dt10);
                ReportDataSource rds11 = new ReportDataSource("baso", dt11);
                ReportDataSource rds12 = new ReportDataSource("mono", dt12);
                ReportDataSource rds13 = new ReportDataSource("rbc", dt13);
                ReportDataSource rds14 = new ReportDataSource("hct", dt14);
                ReportDataSource rds15 = new ReportDataSource("mcv", dt15);
                ReportDataSource rds16 = new ReportDataSource("mch", dt16);
                ReportDataSource rds17 = new ReportDataSource("mchc", dt17);
                ReportDataSource rds18 = new ReportDataSource("rdw_cv", dt18);
                ReportDataSource rds19 = new ReportDataSource("rdw_sd", dt19);
                ReportDataSource rds20 = new ReportDataSource("plt", dt20);
                ReportDataSource rds21 = new ReportDataSource("mpv", dt21);
                ReportDataSource rds22 = new ReportDataSource("pct", dt22);
                ReportDataSource rds23 = new ReportDataSource("pdw", dt23);
                ReportDataSource rds24 = new ReportDataSource("p_lcc", dt24);
                ReportDataSource rds25 = new ReportDataSource("p_lcr", dt25);
                ReportDataSource rds26 = new ReportDataSource("hgb", dt26);
                ReportDataSource rds27 = new ReportDataSource("esr", dt27);
                ReportDataSource rds28 = new ReportDataSource("histwbc", dt28);
                ReportDataSource rds29 = new ReportDataSource("histwbc", dt29);
                ReportDataSource rds30 = new ReportDataSource("histwbc", dt30);

                reportViewer1.LocalReport.ReportPath = "I:\\Project\\Rajni\\Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds1);
                reportViewer1.LocalReport.DataSources.Add(rds2);
                reportViewer1.LocalReport.DataSources.Add(rds3);
                reportViewer1.LocalReport.DataSources.Add(rds4);
                reportViewer1.LocalReport.DataSources.Add(rds5);
                reportViewer1.LocalReport.DataSources.Add(rds6);
                reportViewer1.LocalReport.DataSources.Add(rds7);
                reportViewer1.LocalReport.DataSources.Add(rds8);
                reportViewer1.LocalReport.DataSources.Add(rds9);
                reportViewer1.LocalReport.DataSources.Add(rds10);
                reportViewer1.LocalReport.DataSources.Add(rds11);
                reportViewer1.LocalReport.DataSources.Add(rds12);
                reportViewer1.LocalReport.DataSources.Add(rds13);
                reportViewer1.LocalReport.DataSources.Add(rds14);
                reportViewer1.LocalReport.DataSources.Add(rds15);
                reportViewer1.LocalReport.DataSources.Add(rds16);
                reportViewer1.LocalReport.DataSources.Add(rds17);
                reportViewer1.LocalReport.DataSources.Add(rds18);
                reportViewer1.LocalReport.DataSources.Add(rds19);
                reportViewer1.LocalReport.DataSources.Add(rds20);
                reportViewer1.LocalReport.DataSources.Add(rds21);
                reportViewer1.LocalReport.DataSources.Add(rds22);
                reportViewer1.LocalReport.DataSources.Add(rds23);
                reportViewer1.LocalReport.DataSources.Add(rds24);
                reportViewer1.LocalReport.DataSources.Add(rds25);
                reportViewer1.LocalReport.DataSources.Add(rds26);
                reportViewer1.LocalReport.DataSources.Add(rds27);
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

        private void btnDelete_Click_1(object sender, EventArgs e)
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
    }
}
