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

        private void button8_Click(object sender, EventArgs e)
        {            
                Form1 frm = new Form1();
                frm.Show();          
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            openchildForm(new part());
        }

        private void sidebtnDoctor_Click(object sender, EventArgs e)
        {
            openchildForm(new DoctorSetup());
        }

        private void sidebtnData_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void sidebtnSignature_Click(object sender, EventArgs e)
        {
            openchildForm(new Signatory());
        }

        private void sidebtnMaping_Click(object sender, EventArgs e)
        {
            openchildForm(new TestMaping());
        }

        private void sidebtnAbout_Click(object sender, EventArgs e)
        {
            openchildForm(new About());
        }
    }
}
