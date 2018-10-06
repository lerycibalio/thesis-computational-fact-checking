﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Fact_Checker
{
    public partial class Form1 : Form
    {
        Process process;
        StreamWriter writer;
        StreamReader reader;

        public Form1()
        {
            InitializeComponent();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = txtInterpreter.Text;
            start.Arguments = string.Format("{0}", script_loc);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardInput = true;
            start.CreateNoWindow = false;
            process = Process.Start(start);
            writer = process.StandardInput;
            reader = process.StandardOutput;
        }

        private string stream_read_write(string input) {
                writer.WriteLine(input);
                return reader.ReadLine();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            
            string subject1 = sub1.Text;
            string subject2 = sub2.Text;
            string subject3 = sub3.Text;
         

            string objectText = obj.Text;


            string result = stream_read_write(subject1);
            txtoutput.Text += $"\n{result}";
                        //s answerLabel.Text = "PROCESSING";
                        //
                        /*
                                    bool verdict = getVerdict(subjectText, objectText);
                                    if (verdict)
                                    {
                                        answerLabel.Text = "True";
                                    } else
                                    {
                                        answerLabel.Text = "False";
                                    }
                                    */
        }

        public bool getVerdict()
        {



// MessageBox.Show(run_cmd());

            return true;
            
        }
        

        

        string script_loc = $@"""D:\Files\Desktop\thesis-computational-fact-checking\cfc_backend.py""";
        public string run_cmd() {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = txtInterpreter.Text;
            start.Arguments = string.Format($"\"{script_loc}\" \"{sub1.Text}\" \"{sub2.Text}\" \"{sub3.Text}\" \"{obj.Text}\" ");
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = false; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(start)) {
                using (StreamReader reader = process.StandardOutput) {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    if (stderr != "") {
                        MessageBox.Show(stderr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                   return result;
                }
            }
        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInterpreter_Click(object sender, EventArgs e) {

            DialogResult x = ofd.ShowDialog();
            if (x == DialogResult.OK) {
                txtInterpreter.Text = ofd.FileName;
            }
        }
    }
}
