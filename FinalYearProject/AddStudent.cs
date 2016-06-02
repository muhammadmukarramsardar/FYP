using CosmosApplication.DAC;
using CosmosApplication.Entities;
using MetroFramework.Controls;
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
    public partial class AddStudent : Form
    {
        private int studentId = 0;
        public Student student;
        public string type = "Student";
        public List<Guardian> guardians;
        public AddStudent()
        {
            InitializeComponent();
            student = new Student();
            guardians = new List<Guardian>();
            studentId = 0;
            List<Class> dbClasses = new ClassDAC().GetAllClasses();

            dbClasses.Insert(0, new Class() { Id = 0, Name = "Select" });

            classComboBox.DataSource = dbClasses;
            classComboBox.DisplayMember = "Name";
            classComboBox.ValueMember = "Id";

            List<ClassSection> sectionsByClass = new List<ClassSection>();

            sectionsByClass.Insert(0, new ClassSection() { Id = 0, Name = "Select" });
            sectionsComboBox.DataSource = sectionsByClass;
            sectionsComboBox.DisplayMember = "Name";
            sectionsComboBox.ValueMember = "Id";

            sectionsByClass.Insert(1, new ClassSection() { Id = 1, Name = "Male" });
            sectionsByClass.Insert(2, new ClassSection() { Id = 2, Name = "Female" });
            genderComboBox.DataSource = sectionsByClass;
            genderComboBox.DisplayMember = "Name";
            genderComboBox.ValueMember = "Id";


            List<ClassSection> religions = new List<ClassSection>();

            religions.Insert(0, new ClassSection() { Id = 0, Name = "Select" });
            religions.Insert(1, new ClassSection() { Id = 1, Name = "Islam" });
            religions.Insert(2, new ClassSection() { Id = 2, Name = "Christian" });
            religions.Insert(3, new ClassSection() { Id = 3, Name = "Hindus" });
            religions.Insert(4, new ClassSection() { Id = 4, Name = "Sikh" });
            religionComboBox.DataSource = religions;
            religionComboBox.DisplayMember = "Name";
            religionComboBox.ValueMember = "Id";

            addNewGuardianBtn.Visible = false;
            addExistingGuardianBtn.Visible = false;

        }

        private void AddStudent_Load(object sender, EventArgs e)
        {

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newstdPicBox_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel12_Click(object sender, EventArgs e)
        {

        }

        private void newStdSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Student std = new Student();

                student.Gender = genderComboBox.Text;
                student.RegNo = newStdRegNo.Text;
                student.Name = newStdNameTxt.Text;
                student.FatherName = newStdFatherNameTxt.Text;
                student.FatherCNIC = newStdFatherNicTxt.Text;
                student.SectionId = Convert.ToInt32(sectionsComboBox.SelectedValue);
                student.RollNo = newStdRollNo.Text;

                student.Religion = religionComboBox.Text;
                student.Dob = Convert.ToDateTime(newStdDob.Text);
                student.HomeNumber = newStdHomeNo.Text;
                student.FatherNo = newStdFatherNo.Text;
                student.Email = newStdEmailTxt.Text;
                student.Address = newStdAddressTxt.Text;

                //int newStdId = new StudentDAC().AddStudent(std);

                this.type = "Student";
                EnrollmentForm enrollForm = new EnrollmentForm(this);
                enrollForm.ShowDialog();

                ////Student newlyAddedStudent = new StudentDAC().GetStudentById(newStdId);

                int newStdId = new StudentDAC().AddStudent(student);
                student.Id = newStdId;
                if (newStdId != 0)
                {

                    MessageBox.Show("Student is Added");
                    addNewGuardianBtn.Visible = true;
                    addExistingGuardianBtn.Visible = true;
                    BindingSource bs = new BindingSource();
                    guardiansGridView.DataSource = bs;
                    guardians = new List<Guardian>();
                }
                else
                {
                    new StudentDAC().DeleteStudentById(newStdId);
                }




            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Valid Information");
            }

        }

        public void NewStudentReset()
        {
            newStdRegNo.Text = "";
            newStdNameTxt.Text = "";
            newStdFatherNameTxt.Text = "";
            newStdFatherNicTxt.Text = "";

            newStdRollNo.Text = "";
            genderComboBox.SelectedText = "";
            religionComboBox.SelectedText = "";
            newStdDob.Text = "";
            newStdHomeNo.Text = "";
            newStdFatherNo.Text = "";
            newStdEmailTxt.Text = "";
            newStdAddressTxt.Text = "";
        }

        private void newStdResetBtn_Click(object sender, EventArgs e)
        {
            NewStudentReset();
        }

        private void newStdPicBrowseBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select any Image";
            openFileDialog1.Filter = "Select Image|*.Jpg;*.png";
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                newstdPicBox.Load(openFileDialog1.FileName);
                newstdPicBox.ImageLocation.LastIndexOf('.');
                MessageBox.Show("PictureBox is Added");

            }
            else
            {
                MessageBox.Show("Select a Related Image");
            }
        }

        private void newStdGenderTxt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void classComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MetroComboBox classCombo = (MetroComboBox)sender;

                if (classCombo != null && Convert.ToInt32(classCombo.SelectedValue) != 0)
                {
                    List<ClassSection> sectionsByClass = new ClassSectionDAC().GetAllSectionsByClassId(Convert.ToInt32(classCombo.SelectedValue));

                    sectionsByClass.Insert(0, new ClassSection() { Id = 0, Name = "Select" });

                    sectionsComboBox.DataSource = sectionsByClass;
                    sectionsComboBox.DisplayMember = "Name";
                    sectionsComboBox.ValueMember = "Id";
                }
                else if (Convert.ToInt32(classCombo.SelectedValue) == 0)
                {
                    List<ClassSection> sectionsByClass = new List<ClassSection>();

                    sectionsByClass.Insert(0, new ClassSection() { Id = 0, Name = "Select" });
                    sectionsComboBox.DataSource = sectionsByClass;
                    sectionsComboBox.DisplayMember = "Name";
                    sectionsComboBox.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void addNewGuardianBtn_Click(object sender, EventArgs e)
        {
            this.type = "Add";
            AddGuardiancs guardianForm = new AddGuardiancs(this);
            guardianForm.ShowDialog();

            BindingSource bs = new BindingSource();
            bs.DataSource = guardians;
            guardiansGridView.DataSource = bs;
        }

        private void addExistingGuardianBtn_Click(object sender, EventArgs e)
        {
            this.type = "Existing";
            AddGuardiancs guardianForm = new AddGuardiancs(this);
            guardianForm.ShowDialog();

            BindingSource bs = new BindingSource();
            bs.DataSource = guardians;
            guardiansGridView.DataSource = bs;
        }
    }
}
