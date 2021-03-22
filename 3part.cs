using MaterialSkin.Controls;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                SqlCommand cmd = new SqlCommand("SELECT pid as pid, p_name as name, p_address as address, sex, age_year as year, age_month as month, age_date as date, phone FROM Patient", con);
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
                for (int i = 0; i < 8; i++)
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
                SqlCommand cmd1 = new SqlCommand("SELECT pid, i_id as iid, tdate, wbc, rbc, hgb, plt  FROM PT", con);
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

                dateFrom.DataSource = DS1.Tables[0];
                for (int i = 0; i < 7; i++)
                {
                    this.dateFrom.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void tboxDocid_TextChanged(object sender, EventArgs e)
        {
            try
            {
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
            { }
        }

        private void Print_Click(object sender, EventArgs e)
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

                SqlCommand cmd5 = new SqlCommand("SearchUserInput", con);
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.Parameters.AddWithValue("@pid", tboxPid.Text);

                SqlCommand cmd6 = new SqlCommand("Select * From Tblcumm", con);
                SqlCommand cmd7 = new SqlCommand("Select * From fl", con);
                SqlCommand cmd8 = new SqlCommand("Select * From P_Param", con);
                SqlCommand cmd9 = new SqlCommand("Select * From P_Percent", con);
                SqlCommand cmd10 = new SqlCommand("SearchWbcHist", con);
                cmd10.CommandType = CommandType.StoredProcedure;
                cmd10.Parameters.AddWithValue("@pid", tboxPid.Text);
                SqlCommand cmd11 = new SqlCommand("SearchRbcHist", con);
                cmd11.CommandType = CommandType.StoredProcedure;
                cmd11.Parameters.AddWithValue("@pid", tboxPid.Text);
                SqlCommand cmd12 = new SqlCommand("SearchPltHist", con);
                cmd12.CommandType = CommandType.StoredProcedure;
                cmd12.Parameters.AddWithValue("@pid", tboxPid.Text);

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

                ReportDataSource rds1 = new ReportDataSource("DataSet1", dt1);
                ReportDataSource rds2 = new ReportDataSource("SigData", dt2);
                ReportDataSource rds3 = new ReportDataSource("TestData", dt3);
                ReportDataSource rds4 = new ReportDataSource("DocData", dt4);
                ReportDataSource rds5 = new ReportDataSource("UserInputData", dt5);
                ReportDataSource rds6 = new ReportDataSource("Tblcumm", dt6);
                ReportDataSource rds7 = new ReportDataSource("fl", dt7);
                ReportDataSource rds8 = new ReportDataSource("P_Param", dt8);
                ReportDataSource rds9 = new ReportDataSource("P_Percent", dt9);
                ReportDataSource rds10 = new ReportDataSource("histwbc", dt10);
                ReportDataSource rds11 = new ReportDataSource("histrbc", dt11);
                ReportDataSource rds12 = new ReportDataSource("histplt", dt12);

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
                reportViewer1.RefreshReport();
                System.Windows.Forms.MessageBox.Show("Report is Ready");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Insert Valid Pid, Sig id and Doc id to Print Report");
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

                dateFrom.DataSource = DS.Tables[0];
                for (int i = 0; i < 9; i++)
                {
                    this.dateFrom.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                        tboxLyp.Text = da.GetValue(1).ToString(); tboxGrp.Text = da.GetValue(2).ToString();
                        tboxRbc.Text = da.GetValue(3).ToString(); tboxHgb.Text = da.GetValue(4).ToString();
                        tboxHct.Text = da.GetValue(5).ToString(); tboxMcv.Text = da.GetValue(6).ToString();
                        tboxMch.Text = da.GetValue(7).ToString(); tboxMchc.Text = da.GetValue(8).ToString();
                        tboxRdw_cv.Text = da.GetValue(9).ToString(); rdw_sd.Text = da.GetValue(10).ToString();
                        tboxPlt.Text = da.GetValue(11).ToString(); tboxMpv.Text = da.GetValue(12).ToString();
                        tboxPct.Text = da.GetValue(13).ToString(); tboxPdw.Text = da.GetValue(14).ToString();
                        tboxP_lcc.Text = da.GetValue(15).ToString(); tboxP_lcr.Text = da.GetValue(16).ToString();
                    }
                    con.Close();
                    int flag = 1;
                    if (flag == 1)
                    {
                        SqlCommand cmd2 = new SqlCommand("Select esr, mono, eso, baso, bt, ct, mp, blast from UserInput where pid = @pid", con);

                        cmd2.Parameters.AddWithValue("@pid", tboxPid.Text);
                        con.Open();
                        SqlDataReader da1 = cmd2.ExecuteReader();
                        while (da1.Read())
                        {
                            tboxEsr.Text = da1.GetValue(0).ToString(); tboxMop.Text = da1.GetValue(1).ToString();
                            tboxEso.Text = da1.GetValue(2).ToString(); tboxBaso.Text = da1.GetValue(3).ToString();
                            tboxBT.Text = da1.GetValue(4).ToString(); tboxCT.Text = da1.GetValue(5).ToString();
                            tboxMp.Text = da1.GetValue(6).ToString(); tboxBlast.Text = da1.GetValue(7).ToString();
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
                System.Windows.Forms.MessageBox.Show("Patient Data Saved");

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
                    SqlCommand cmd = new SqlCommand("Delete from Patient where pid = @pid", con);
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
                    System.Windows.Forms.MessageBox.Show("Patient Data Deleted Successfully");

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
            if (tboxEsr.Text != "" && tboxWbc.Text != "")
            {
                if (tboxTotal.Text == "100")
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
                    cmd1.Parameters.AddWithValue("@mp", tboxMp.Text);
                    cmd1.Parameters.AddWithValue("@blast", tboxBlast.Text);
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
                            System.Windows.Forms.MessageBox.Show("Test Results are updated");
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
                }
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Insert a Pid and press search");
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

        private void tboxGrp_TextChanged(object sender, EventArgs e)
        {
            try { tboxTotal.Text = (float.Parse(tboxLyp.Text) + float.Parse(tboxGrp.Text) + float.Parse(tboxMop.Text) + float.Parse(tboxBaso.Text) + float.Parse(tboxEso.Text)).ToString(); }
            catch { }
        }
    }
}
