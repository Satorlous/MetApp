using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace MetCostV2
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public bool isSaved = true;
        private string path = @"..\..\indications.txt";
        public List<Indication> indications;
        public Form1()
        {
            InitializeComponent();            
        }

        public List<Indication> SetIndicationsList()
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                return JsonConvert.DeserializeObject<List<Indication>>(sr.ReadToEnd());
            }
        }

        public void WriteFile()
        {
            string input = JsonConvert.SerializeObject(indications);
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.Write(input);
            }
        }

        public void FillGrid()
        {            
            grid.Rows.Clear();
            int index = 1;
            for (int i = indications.Count - 1; i > 1 ; i--)
            {
                
                grid.Rows.Add(index,                    
                    indications[i].Water_Bathroom.Cold.ToString(), indications[i].Water_Bathroom.Hot.ToString(),
                    indications[i].Water_Kitchen.Cold.ToString(), indications[i].Water_Kitchen.Hot.ToString(),
                    indications[i].Electricity.Day.ToString(), indications[i].Electricity.Night.ToString(),
                    Indication.GetDifference(indications[i-1], indications[i]),
                    indications[i].Date.ToLongDateString()
                );
                index++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            indications = SetIndicationsList();
            FillGrid();            
        }

        private void AddNewRecordButton_Click(object sender, EventArgs e)
        {
            Form2 addRecordForm = new Form2();
            addRecordForm.RecordAdded += new AddRecordEventHandler(AddedHandler);
            addRecordForm.ShowDialog();
        }

        private void AddedHandler(object sender, RecordAddedEventArgs eventArgs)
        {
            var indication = eventArgs.Indication;
            isSaved = false;
            indications.Add(indication);
            FillGrid();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            indications.RemoveAt(indications.Count - 1);
            FillGrid();
            isSaved = false;
        }

        private void MaterialRaisedButton3_Click(object sender, EventArgs e)
        {
            //test button
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            WriteFile();
            isSaved = true;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            indications = SetIndicationsList();
            FillGrid();
            isSaved = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult dialogResult = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    WriteFile();
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
