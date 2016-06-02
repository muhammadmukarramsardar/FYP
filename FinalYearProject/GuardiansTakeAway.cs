using CosmosApplication.DAC;
using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinalYearProject
{
    public partial class GuardiansTakeAway : Form
    {
        public Guardian guardian = new Guardian();
        private SpeechSynthesizer reader { get; set; }
        public GuardiansTakeAway()
        {
            InitializeComponent();
            reader = new SpeechSynthesizer();
            CallStudents();
        }

        public void CallStudents()
        {
            VerificationForm verfyForm = new VerificationForm(this);
            verfyForm.ShowDialog();

            if (guardian.Id != 0)
            {
                CosmosApplication.Entities.GuardianTime timeObj = new CosmosApplication.Entities.GuardianTime() { DateTime = DateTime.Today, GuardianId = guardian.Id };
                new GuardianTimesDAC().AddGuardianTime(timeObj);

                List<Student> students = new StudentGuardianDAC().SelectAllStudentsByParentId(guardian.Id);

                if (students != null && students.Count > 0)
                {
                    string studentNames = "";
                    foreach (var std in students)
                    {
                        studentNames = studentNames + " " + std.Name;
                    }

                    string textToSpeech = "Your parent is here to receive you. Student names are" + studentNames + " Please reach at school gate as soon as possible";

                    reader.Rate = -3;
                    reader.SelectVoice("Microsoft Zira Desktop");
                    reader.SpeakAsync(textToSpeech);
                }
            }

            CallStudents();
            //string textToSpeech = "Your parent is here to receive you. Student names are" + " Ahmer " + " Please reach at school gate as soon as possible";
            
        }
    }
}
