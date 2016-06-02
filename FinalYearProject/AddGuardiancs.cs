using CosmosApplication.DAC;
using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalYearProject
{
    public partial class AddGuardiancs : Form
    {
        public string type = "";
        public Guardian guardian = new Guardian();
        public AddStudent addStudentForm;
        public AddGuardiancs(AddStudent addStudentForm)
        {
            InitializeComponent();

            if (addStudentForm.type == "Add")
            {
                searchBox.Visible = false;
                newStdGuardUpdateBtn.Visible = false;
            }
            else if (addStudentForm.type == "Existing")
            {
                newStdGuardSaveBtn.Visible = false;
                newStdGuarCnic.Enabled = false;
                newStdGuarName.Enabled = false;
                newStdGuarRel.Enabled = false;
                newStdGurNo.Enabled = false;
                newStdGuardEmailTxt.Enabled = false;
                newStdGuardAddressTxt.Enabled = false;
            }

            this.addStudentForm = addStudentForm;
        }

        private void modStdPhnNo2_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel27_Click(object sender, EventArgs e)
        {

        }

        private void modStdEmailTxt_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel23_Click(object sender, EventArgs e)
        {

        }

        private void modStdAddressTxt_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel22_Click(object sender, EventArgs e)
        {

        }

        private void modStdPhnNo3_Click(object sender, EventArgs e)
        {

        }

        private void modStdPhnNo1_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel21_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel20_Click(object sender, EventArgs e)
        {

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void modStdSaveBtn_Click(object sender, EventArgs e)
        {
            //Guardian gur = new Guardian();
            guardian.Name = newStdGuarName.Text;
            guardian.CNIC = newStdGuarCnic.Text;
            guardian.Relation = newStdGuarRel.Text;
            guardian.Gender = newStdGuardianGen.SelectedText;
            guardian.GuardianNo = newStdGurNo.Text;
            guardian.Email = newStdGuardEmailTxt.Text;
            guardian.Address = newStdGuardAddressTxt.Text;

            this.type = "Guardian";

            EnrollmentForm enrollForm = new EnrollmentForm(this);
            enrollForm.ShowDialog();

            Guardian addedGuardian = new GuardianDAC().AddGuardian(guardian);
            if (addedGuardian.Id != 0)
            {
                StudentGardians studentGuardian = new StudentGardians() { GuardianId = addedGuardian.Id, StudentId = addStudentForm.student.Id };
                new StudentGuardianDAC().AddStudentGuardian(studentGuardian);
                addedGuardian.FingerCode = new byte[0];
                addStudentForm.guardians.Add(addedGuardian);
                MessageBox.Show("Guardian is added");
                
            }
            else
            {
                MessageBox.Show("Please Enter Correct Information");
            }

            this.Close();

        }
        public void newGuardianReset()
        {
            newStdGuarName.Text = "";
            newStdGuarCnic.Text = "";
            newStdGuarRel.Text = "";
            newStdGurNo.Text = "";
            newStdGuardEmailTxt.Text = "";
            newStdGuardAddressTxt.Text = "";
            //newstdGuardPicBox.pi
        }
        private void newStdResetBtn_Click(object sender, EventArgs e)
        {
            newGuardianReset();
        }

        private void newStdGuardPicBrowseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Title = "Select any Image";
            openFileDialog2.Filter = "Select Image|*.Jpg;*.png";
            openFileDialog2.FileName = "";
            openFileDialog2.Multiselect = false;
            openFileDialog2.ShowDialog();

            if (openFileDialog2.FileName != "")
            {
                newstdGuardPicBox.Load(openFileDialog2.FileName);
                newstdGuardPicBox.ImageLocation.LastIndexOf('.');
                MessageBox.Show("PictureBox is Added");

            }
            else
            {
                MessageBox.Show("Select a Related Image");
            }
        }

        private void newStdGuardSearchBtn_Click(object sender, EventArgs e)
        {
            Guardian guardian = new GuardianDAC().SelectGuardianByCNIC(newStdGuarSeachCNIC.Text);

            if (guardian != null)
            {
                newStdGuarName.Text = guardian.Name;
                newStdGuarCnic.Text = guardian.CNIC;
                newStdGuarRel.Text = guardian.Relation;
                newStdGurNo.Text = guardian.GuardianNo;
                newStdGuardEmailTxt.Text = guardian.Email;
                newStdGuardAddressTxt.Text = guardian.Address;
            }
        }

        private void newStdGuardUpdateBtn_Click(object sender, EventArgs e)
        {
            Guardian guardian = new GuardianDAC().SelectGuardianByCNIC(newStdGuarSeachCNIC.Text);

            //guardian.Name = newStdGuarName.Text;
            //guardian.Relation = newStdGuarRel.Text;
            //guardian.Gender = newStdGuardianGen.SelectedText;
            //guardian.GuardianNo = newStdGurNo.Text;
            //guardian.Email = newStdGuardEmailTxt.Text;
            //guardian.Address = newStdGuardAddressTxt.Text;

            StudentGardians studentGuardian = new StudentGardians() { GuardianId = guardian.Id, StudentId = addStudentForm.student.Id };
            new StudentGuardianDAC().AddStudentGuardian(studentGuardian);
            addStudentForm.guardians.Add(guardian);
            MessageBox.Show("Guardian is added");
            this.Close();
        }
    }
}
