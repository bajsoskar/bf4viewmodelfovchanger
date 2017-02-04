using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;



namespace Battlefield_4_Viewmodel_Fov_Changer
{
    public partial class Form1 : Form
    {
        #region Global variables
        Memory myMemory = new Memory();
        Process[] myProcess;
        bool isGameAvailable = false;

        bool fovChange = false;
        string FovPointer = "1424AD330";
        int[] FovOffset = { 0x0250 };
        int FovToChange = 120;

        #endregion





        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fovChangeButton_Click(object sender, EventArgs e)
        {
            if (isGameAvailable)
            {
                if (fovChange)
                {
                    fovChange = false;
                    fovChangeButton.Text = "DEACTIVATED";
                }
                else
                {
                    fovChange = true;
                    fovChangeButton.Text = "ACTIVATED";
                }


            }
        }

        private void GameAvailabilityTimer_Tick(object sender, EventArgs e)
        {
            myProcess = Process.GetProcessesByName("bf4");
            if (myProcess.Length != 0)
            {
                isGameAvailable = true;
                StatusLabel.Text = "STATUS: bf4.exe WAS FOUND";
            }

            else
            {
                isGameAvailable = false;
                StatusLabel.Text = "STATUS: bf4.exe WAS NOT FOUND";
            }




        }

        private void UpdateFovTimer_Tick(object sender, EventArgs e)
        {
            if (isGameAvailable)
            {
                #region Fov Change
                if (fovChange)
                {
                    myMemory.ReadProcess = myProcess[0];
                    myMemory.Open();
                    int pointerAddress = HexToDec(FovPointer);
                    int[] pointerOffset = FovOffset;
                    int bytesWritten;
                    byte[] valueToWrite = BitConverter.GetBytes(FovToChange);
                    string writtenAddress = myMemory.PointerWrite((IntPtr)pointerAddress, valueToWrite, pointerOffset, out bytesWritten);
                    myMemory.CloseHandle();
                }
                #endregion

            }


        }

        public static string HexToDec(int DEC)
        {
            return DEC.ToString("X");
        }
        public static int HexToDec(string Hex)
       {
           return int.Parse(Hex, NumberStyles.HexNumber);
            
        }

        private void fovAmount_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void fovAmount_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void fovLabel_Click(object sender, EventArgs e)
        {
           
        }
    }
}
