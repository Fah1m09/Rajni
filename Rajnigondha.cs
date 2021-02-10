using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rajni
{
    public partial class Rajnigondha : Form
    {
        public Rajnigondha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            part tpt = new part();
            tpt.Show();
        }

        private void btnhematology3_Click(object sender, EventArgs e)
        {
            openchildForm(new part());
        }

        private Form activeForm = null;

        private void openchildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelfill.Controls.Add(childForm);
            panelfill.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openchildForm(new DoctorSetup());
        }

        int key = 0;
        private void btnDataTransfer_Click_1(object sender, EventArgs e)
        {
            if(key == 0)
            {
                Form1 frm = new Form1();
                frm.Show();
                key = 1;
            }           
        }

        private void btnsinatory_Click(object sender, EventArgs e)
        {
            openchildForm(new Signatory());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openchildForm(new TestMaping());
        }

        private void boxbtnreport_Click_1(object sender, EventArgs e)
        {
            openchildForm(new part());
        }

        private void boxbtndoc_Click_1(object sender, EventArgs e)
        {
            openchildForm(new DoctorSetup());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openchildForm(new TestMaping());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openchildForm(new Signatory());
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            openchildForm(new Form1());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                Form1 frm = new Form1();
                frm.Show();
                key = 1;
            }
        }
    }
}
