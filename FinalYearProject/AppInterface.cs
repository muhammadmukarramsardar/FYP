using CosmosApplication.DAC;
using CosmosApplication.Entities;
using CosmosApplication.SmsSendHelper;
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
    public partial class AppInterface : Form
    {
        public AppInterface()
        {
            InitializeComponent();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            AddStudent ads = new AddStudent();
            ads.Show();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            GuardiansTakeAway gTA = new GuardiansTakeAway();
            gTA.Show();
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            List<Guardian> guardians = new GuardianTimesDAC().FindLateGuardianTime();

            if (guardians != null && guardians.Count > 0)
            {
                foreach (var item in guardians)
                {
                    new SmsUtility().SendSms(item.GuardianNo, "Your childern is/are waiting for you at school. Reach ASAP\n Cosmos School");
                }
            }

            MessageBox.Show("Sms have been sent to late parents.");
        }
    }
}
