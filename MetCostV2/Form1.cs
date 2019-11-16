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
        private const string path = "indications.txt";
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
            var list = new List<Indication>();
            list.Add(new Indication(new Water(13, 34), new Water(107, 72), new Electricity(23629, 9211), DateTime.Parse("2018-08-13")));
            list.Add(new Indication(new Water(13, 34), new Water(109, 73), new Electricity(23724, 9289), DateTime.Parse("2018-09-14")));
            list.Add(new Indication(new Water(13, 35), new Water(110, 74), new Electricity(23816, 9383), DateTime.Parse("2018-10-13")));
            list.Add(new Indication(new Water(13, 35), new Water(112, 75), new Electricity(23916, 9478), DateTime.Parse("2018-11-15")));
            list.Add(new Indication(new Water(14, 36), new Water(114, 76), new Electricity(24008, 9570), DateTime.Parse("2018-12-14")));
            list.Add(new Indication(new Water(14, 36), new Water(114, 76), new Electricity(24123, 9662), DateTime.Parse("2019-01-16")));
            list.Add(new Indication(new Water(14, 37), new Water(115, 77), new Electricity(24325, 9746), DateTime.Parse("2019-02-14")));
            list.Add(new Indication(new Water(14, 37), new Water(116, 77), new Electricity(24352, 9823), DateTime.Parse("2019-03-15")));
            list.Add(new Indication(new Water(14, 38), new Water(118, 78), new Electricity(24477, 9927), DateTime.Parse("2019-04-15")));
            list.Add(new Indication(new Water(14, 38), new Water(119, 79), new Electricity(24559, 9992), DateTime.Parse("2019-05-10")));
            list.Add(new Indication(new Water(15, 39), new Water(122, 80), new Electricity(24755, 10155), DateTime.Parse("2019-06-12")));
            list.Add(new Indication(new Water(15, 40), new Water(124, 81), new Electricity(24966, 10381), DateTime.Parse("2019-09-13")));
            list.Add(new Indication(new Water(15, 40), new Water(126, 83), new Electricity(25079, 10423), DateTime.Parse("2019-10-10")));
            list.Add(new Indication(new Water(16, 41), new Water(128, 84), new Electricity(25201, 10528), DateTime.Parse("2019-11-16")));


            string input = JsonConvert.SerializeObject(list);
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.Write(input);
            }
            FillGrid();
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
