using CosmosApplication.DAC;
using CosmosApplication.Entities;
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
    public partial class VerificationForm : Form
    {
        GuardiansTakeAway guardiansTakeAwayForm;
        public VerificationForm()
        {
            InitializeComponent();
            Data = new AppData();
        }

        public VerificationForm(GuardiansTakeAway gTA)
        {

            InitializeComponent();
            Data = new AppData();
            this.guardiansTakeAwayForm = gTA;
        }

        public void OnComplete(object Control, DPFP.FeatureSet FeatureSet, ref DPFP.Gui.EventHandlerStatus Status)
        {
            DPFP.Verification.Verification ver = new DPFP.Verification.Verification();
            DPFP.Verification.Verification.Result res = new DPFP.Verification.Verification.Result();

            //string conStr = "Data Source=.; Initial Catalog=Registration; Integrated Security=true";

            //SqlConnection con = new SqlConnection(conStr);
            //SqlCommand cmd = new SqlCommand("Select FingerPrintData from FingerPrints where Id = 11", con);

            //con.Open();

            //byte[] userFingerPrintData = new byte[0];

            //using (con)
            //{
            //    userFingerPrintData = cmd.ExecuteScalar() as byte[];
            //}

            //byte[] fingerPrintCode = new StudentDAC().GetFingerCodeById(2);

            List<Guardian> guardians = new GuardianDAC().SelectAllGuardians();
           
            if (guardians != null && guardians.Count > 0)
            {
                foreach (var item in guardians)
                {
                    ver = new DPFP.Verification.Verification();
                    res = new DPFP.Verification.Verification.Result();
                    DPFP.Template temp = new DPFP.Template();

                    if (item.FingerCode != null)
                    {
                        temp.DeSerialize(item.FingerCode);

                        if (temp != null)
                        {
                            // Compare feature set with particular template.

                            ver.Verify(FeatureSet, temp, ref res);

                            if (res.Verified)
                            {
                                guardiansTakeAwayForm.guardian = item;

                                this.Close();
                                break;
                            }
                        }
                    }
                    
                }
            }

            
            
            if (!res.Verified)
                Status = DPFP.Gui.EventHandlerStatus.Failure;

        }

        private AppData Data;
    }
}