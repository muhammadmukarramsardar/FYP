using CosmosApplication.DAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FinalYearProject
{
    public partial class EnrollmentForm : Form
    {
        private AddStudent addStudentForm;
        private AddGuardiancs addGuardianForm;
        private string type = "";
        // Constructor
        public EnrollmentForm(AddStudent addStudentForm)
        {
            this.addStudentForm = addStudentForm;
            type = this.addStudentForm.type;
            InitializeComponent();
            Data = new AppData();
            ExchangeData(true);                                 // Init data with default control values;
            Data.OnChange += delegate { ExchangeData(false); }; // Track data changes to keep the form synchronized
        }

        public EnrollmentForm(AddGuardiancs addGuardianForm)
        {
            this.addGuardianForm = addGuardianForm;
            type = this.addGuardianForm.type;
            InitializeComponent();
            Data = new AppData();
            ExchangeData(true);                                 // Init data with default control values;
            Data.OnChange += delegate { ExchangeData(false); }; // Track data changes to keep the form synchronized
        }

        // Simple dialog data exchange (DDX) implementation.
        public void ExchangeData(bool read)
        {
            if (read)
            {   // read values from the form's controls to the data object
                Data.EnrolledFingersMask = EnrollmentControl.EnrolledFingerMask;
                Data.MaxEnrollFingerCount = EnrollmentControl.MaxEnrollFingerCount;
            }
            else {  // read values from the data object to the form's controls
                EnrollmentControl.EnrolledFingerMask = Data.EnrolledFingersMask;
                EnrollmentControl.MaxEnrollFingerCount = Data.MaxEnrollFingerCount;
            }
        }

        // event handling
        public void EnrollmentControl_OnEnroll(Object Control, int Finger, DPFP.Template Template, ref DPFP.Gui.EventHandlerStatus Status)
        {
            if (Data.IsEventHandlerSucceeds)
            {
                Data.Templates[Finger - 1] = Template;          // store a finger template
                ExchangeData(true);								// update other data

                byte[] FingerPrintData = new byte[0];

                Template.Serialize(ref FingerPrintData);

                //string conStr = "Data Source=.; Initial Catalog=Registration; Integrated Security=true";

                //SqlConnection con = new SqlConnection(conStr);
                //SqlCommand cmd = new SqlCommand("Insert into FingerPrints([FingerPrintData]) values(@fingerData)", con);
                //cmd.Parameters.Add("@fingerData", SqlDbType.VarBinary);
                //cmd.Parameters["@fingerData"].Value = FingerPrintData;

                //con.Open();

                //using (con)
                //{
                //    cmd.ExecuteNonQuery();
                //}

                //bool updateStudent = new StudentDAC().UpdateFingerCode(studentId, FingerPrintData);

                if (type == "Student")
                {
                    addStudentForm.student.FingerCode = FingerPrintData;
                }
                else if (type == "Guardian")
                {
                    addGuardianForm.guardian.FingerCode = FingerPrintData;

                }

                
                this.Close();

                ListEvents.Items.Insert(0, String.Format("OnEnroll: finger {0}", Finger));
            }
            else
                Status = DPFP.Gui.EventHandlerStatus.Failure;   // force a "failure" status
        }

        public void EnrollmentControl_OnDelete(Object Control, int Finger, ref DPFP.Gui.EventHandlerStatus Status)
        {
            if (Data.IsEventHandlerSucceeds)
            {
                Data.Templates[Finger - 1] = null;              // clear the finger template
                ExchangeData(true);                             // update other data

                ListEvents.Items.Insert(0, String.Format("OnDelete: finger {0}", Finger));
            }
            else
                Status = DPFP.Gui.EventHandlerStatus.Failure;   // force a "failure" status
        }

        private void EnrollmentControl_OnCancelEnroll(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnCancelEnroll: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnReaderConnect(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnReaderConnect: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnReaderDisconnect(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnReaderDisconnect: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnStartEnroll(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnStartEnroll: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnFingerRemove(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnFingerRemove: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnFingerTouch(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnFingerTouch: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnSampleQuality(object Control, string ReaderSerialNumber, int Finger, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            ListEvents.Items.Insert(0, String.Format("OnSampleQuality: {0}, finger {1}, {2}", ReaderSerialNumber, Finger, CaptureFeedback));
        }

        private void EnrollmentControl_OnComplete(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnComplete: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private AppData Data;

        private void EnrollmentForm_Load(object sender, EventArgs e)
        {
            this.ListEvents.Items.Clear();
        }

        private void EnrollmentControl_Load(object sender, EventArgs e)
        {

        }
    }
}