using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Time_tracker_log
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int noofentries;
        string filelocation;
        int count = 60;

        
        

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            PerformInvoke(groupBox1, () => { groupBox1.Visible = false; });
            PerformInvoke(label2, () => { label2.Visible = true; });

            try
            {
                noofentries = Convert.ToInt32(numericUpDown1.Value);
                PerformInvoke(listView1, () => { listView1.Items.Clear(); });
                List<string> text = File.ReadLines(filelocation.ToString()).Reverse().Take(noofentries).ToList();

                for (int i = 0; i < text.Count; i++)
                {
                    string entry = text[i].ToString();
                    string[] singleentry = entry.Split(',', ',', ',', ',', ',', ',', ',');

                    string[] row = { singleentry[0], singleentry[1], singleentry[2], singleentry[3], singleentry[4], singleentry[5], singleentry[6], singleentry[7], };
                    var listViewItem = new ListViewItem(row);

                    PerformInvoke(listView1, () => { listView1.Items.Add(listViewItem); });
                   
                }
                PerformInvoke(groupBox1, () => { groupBox1.Visible = true; });
                PerformInvoke(label2, () => { label2.Visible = false; });
            }

            catch (Exception ex)
            {
                PerformInvoke(label2, () => { label2.Visible = true; });
                PerformInvoke(label2, () => { label2.Text = ex.Message.ToString(); });
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.AppendText("" + dateTimePicker1.Value.Date.Day.ToString() + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Year.ToString());
            filelocation = textBox2.Text;
            //textBox2.Text = "Logbook";
            backgroundWorker1.RunWorkerAsync();
            timer1.Enabled = true;
            timer1.Start();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReFresh();
            
            
        }

        private void ReFresh()
        {
            count = 60;
            backgroundWorker1.RunWorkerAsync();
        }

        public static void PerformInvoke(Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke(action);
            else
                action();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            DialogResult dr = op.ShowDialog();
            if (dr==DialogResult.OK)
            {
                textBox2.Text = op.SafeFileName.ToString();
                filelocation = op.FileName.ToString();
                ReFresh();
            }
        }

       

       

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                backgroundWorker1.RunWorkerAsync();
                noofentries = Convert.ToInt32(numericUpDown1.Value);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

            if (count > 00)
            {
                count--;
                label4.Text = count.ToString();
            }
            else if (count==00)
            {
                
                backgroundWorker1.RunWorkerAsync();
                count = 60;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //DateTime res = 
            string fn = dateTimePicker1.Value.Date.Day.ToString() + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Year.ToString();
            this.Text = dateTimePicker1.Value.Date.Day.ToString() + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Year.ToString();
            textBox2.Text = ("" + dateTimePicker1.Value.Date.Day.ToString() + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Year.ToString());
            ReFresh();

        }

       

        
    }
}
