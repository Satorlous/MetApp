using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetCostV2
{
    public delegate void AddRecordEventHandler(object sender, RecordAddedEventArgs e);    
    
    public partial class Form2 : MaterialSkin.Controls.MaterialForm
    {
        public event AddRecordEventHandler RecordAdded;
        public Form2()
        {
            InitializeComponent();
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            var indication = new Indication
                (
                    new Water(int.Parse(kitch_Cold_TB.Text), int.Parse(kitch_Hot_TB.Text)),
                    new Water(int.Parse(bath_Cold_TB.Text), int.Parse(bath_Hot_TB.Text)),
                    new Electricity(int.Parse(dayTB.Text), int.Parse(nightTB.Text)),
                    DateTime.Now
                );
            RecordAddedEventArgs eventArgs = new RecordAddedEventArgs() { Indication = indication };
            RecordAdded(this, eventArgs);
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            bath_Cold_TB.Select();
        }
    }

    public class RecordAddedEventArgs : EventArgs
    {
        public Indication Indication { get; set; }
    }
}
