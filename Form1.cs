using System;
using System.IO;
using System.Windows.Forms;

// ▄▀▀█▄▄   ▄▀▀▄ ▀▀▄      ▄▀▀█▄▄   ▄▀▀▄ ▄▀▄  ▄▀▀▀▀▄          ▄█  ▄▀▀█▄▄▄▄  ▄▀▀▀█▀▀▄ 
//▐ ▄▀   █ █   ▀▄ ▄▀     ▐ ▄▀   █ █  █ ▀  █ █          ▄▀▀▀█▀ ▐ ▐  ▄▀   ▐ █    █  ▐ 
//  █▄▄▄▀  ▐     █         █▄▄▄▀  ▐  █    █ █    ▀▄▄  █    █      █▄▄▄▄▄  ▐   █     
//  █   █        █         █   █    █    █  █     █ █ ▐    █      █    ▌     █      
// ▄▀▄▄▄▀      ▄▀         ▄▀▄▄▄▀  ▄▀   ▄▀   ▐▀▄▄▄▄▀ ▐   ▄   ▀▄   ▄▀▄▄▄▄    ▄▀       
//█    ▐       █         █    ▐   █    █    ▐            ▀▀▀▀    █    ▐   █         
//▐            ▐         ▐        ▐    ▐                         ▐        ▐    
//                                                                              2021
namespace CL7_9_DashTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public byte[] dashbin; //Holds contents of bin
        byte selectedgood = 0; //selected valid reading

        //total = 179856
        //Trip A = 458.8
        //Trip B = 458.9
        public byte[] Manual = { 0x03, 0x36, 0x38, 0x00, 0xF2, 0x02, 0x95, 0x2D, 0xE0, 0x00, 0xB7, 0xCA, 0xA1, 0xA9, 0xB6, 0xA3, 0x0A, 0x00, 0x2C, 0x00, 0x20, 0x12, 0x7C, 0x67, 0x16, 0x51, 0xF1, 0x03, 0x2E, 0x03, 0x7C, 0xF4, 0x9D, 0x4B, 0x0C, 0x20, 0x05, 0x0C, 0xBB, 0xCC, 0x0E, 0x11, 0x03, 0x00, 0x18, 0x0C, 0x80, 0x10, 0x10, 0x23, 0x41, 0x55, 0x8D, 0x40, 0x13, 0x6F, 0x69, 0xC9, 0x00, 0x00, 0xFF, 0xFF, 0x10, 0x42, 0x10, 0x42, 0x02, 0x42, 0x10, 0x42, 0x10, 0x42, 0x00, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0xFF, 0x7D, 0xED, 0xBD, 0x73, 0x4B, 0x67, 0xA2, 0x00, 0x00, 0xFF, 0x00, 0x08, 0x10, 0x60, 0x00, 0x80, 0x82, 0x85, 0x62, 0xB6, 0xA6, 0x90, 0x00, 0x86, 0x88, 0x71, 0x22, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x03, 0x01, 0xB1, 0x46, 0x01, 0x54, 0x2A, 0x05, 0x44, 0x55, 0x85, 0x10, 0x64, 0xE2, 0x02, 0x1F, 0x04, 0x00, 0x00, 0x08, 0x0A, 0xFD, 0x02, 0x8A, 0x40, 0x9D, 0xAF, 0xAD, 0x35, 0xAD, 0x41, 0x10, 0x42, 0x10, 0x92, 0x5B, 0x57, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x2D, 0x54, 0x0F, 0xA0, 0x29, 0x08, 0x2D, 0x54, 0x17, 0xA0, 0x31, 0x08, 0x37, 0xE0, 0x28, 0xE3, 0x0D, 0xE0, 0x12, 0xE3, 0x0B, 0xE0, 0x14, 0xE3, 0x36, 0x00, 0xC9, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x45, 0x00, 0xBA, 0x01, 0x5A, 0x00, 0xA5, 0x00, 0x0D, 0x00, 0xF2, 0x01, 0x50, 0x00, 0xAF, 0x00, 0x56, 0x00, 0xA9, 0x00, 0xE4, 0x2B, 0x1C, 0xD4, 0xE3, 0x2B, 0x1C, 0xD4, 0xE3, 0x2B, 0x1C, 0xD4, 0xE3, 0x2B, 0x1C, 0xD4, 0xE3, 0x2B, 0x1C, 0xD4, 0xE3, 0x2B, 0x1C, 0xD4, 0xE3, 0x2B, 0x1C, 0xD4, 0xE3, 0x2B, 0x1C, 0xD4 };
        //total = 151996
        //Trip A = 0.4
        //Trip B = 0.1
        public byte[] Auto = { 0x3A, 0x3A, 0x00, 0x06, 0xF9, 0x00, 0x95, 0x2D, 0xE0, 0x00, 0xB7, 0xCA, 0xA1, 0xA9, 0xB6, 0xA3, 0x0A, 0x00, 0x2C, 0x00, 0x20, 0x12, 0x7C, 0x67, 0x16, 0x51, 0xF1, 0x03, 0x2E, 0x03, 0x7C, 0xF4, 0xF2, 0x40, 0x0B, 0x20, 0x66, 0x1E, 0x0E, 0xD0, 0x05, 0x11, 0x03, 0x00, 0x18, 0x0C, 0x22, 0xC0, 0x10, 0x23, 0x41, 0x55, 0x00, 0x40, 0x13, 0x6F, 0xE4, 0xC9, 0x00, 0x00, 0xFF, 0xFF, 0x10, 0x42, 0x10, 0x42, 0x02, 0x42, 0x10, 0x42, 0x10, 0x42, 0x00, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0x10, 0x42, 0xFF, 0x7D, 0xED, 0xBD, 0xA2, 0x5B, 0xBE, 0xBE, 0x00, 0x00, 0xFF, 0x00, 0xDF, 0xED, 0x2D, 0x00, 0xFE, 0xFE, 0xE0, 0x26, 0xBC, 0xBA, 0xB7, 0x4D, 0x89, 0x8B, 0x01, 0x62, 0x00, 0x00, 0x0E, 0x00, 0x00, 0x00, 0x05, 0xB1, 0x45, 0x10, 0x54, 0x2A, 0x05, 0x44, 0x55, 0x85, 0x10, 0x64, 0xE2, 0x02, 0x1F, 0x04, 0x00, 0x00, 0x89, 0x0A, 0x7C, 0x02, 0x8A, 0x40, 0x9D, 0xAF, 0xAD, 0x35, 0xAD, 0x41, 0x10, 0x42, 0x10, 0x92, 0x5B, 0x57, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x37, 0x4A, 0x3A, 0x80, 0x38, 0x0E, 0x37, 0x4A, 0x3A, 0x80, 0x38, 0x0E, 0x55, 0x00, 0xAA, 0x00, 0x56, 0x00, 0xA9, 0x00, 0x58, 0x00, 0xA7, 0x01, 0x39, 0x00, 0xC6, 0x00, 0x3A, 0x00, 0xC5, 0x00, 0x03, 0xE0, 0x1C, 0xE2, 0x53, 0xE0, 0x4C, 0xE2, 0x30, 0xE0, 0x2F, 0xE2, 0x52, 0xE0, 0x4D, 0xE3, 0x53, 0xE0, 0x4C, 0xE2, 0x18, 0x25, 0xE7, 0xDA, 0x18, 0x25, 0xE7, 0xDA, 0x18, 0x25, 0xE7, 0xDA, 0x18, 0x25, 0xE7, 0xDA, 0x18, 0x25, 0xE7, 0xDA, 0x18, 0x25, 0xE7, 0xDA, 0x18, 0x25, 0xE0, 0xDA, 0x1F, 0x25, 0xFF, 0xFF };
       
        private void button1_Click(object sender, EventArgs e)
        {
            //Open file button
            selectedgood = 0;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Dash Bin";
            theDialog.Filter = "bin files|*.bin";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(theDialog.FileName))
                    {
                        dashbin = File.ReadAllBytes(theDialog.FileName);

                        //Check its expected length
                        if (dashbin.Length == 256)
                        {
                            checksanaty();
                            Processbin();
                            button3.Enabled = true;
                            button2.Enabled = false;
                            maskedTextBox1.Enabled = true;
                            button4.Enabled = true;
                            button5.Enabled = true;
                            button6.Enabled = true;
                            button7.Enabled = true;
                        }
                        else
                        {
                            failedOpen("Bin wrong file size");
                        }
                    }
                    else
                    {
                        failedOpen("Nothing Opened");
                    }
                }
                catch(Exception emsg)
                {
                    failedOpen(emsg.Message.ToString());
                }
            }
        }

        private void failedOpen(string errormsg)
        {
            maskedTextBox1.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            maskedTextBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            MessageBox.Show(errormsg);
        }

        private void Processbin()
        {
            //Gets current values stored
            //trip B 0xAC
            textBox3.Text = ((dashbin[0xAC+5] | dashbin[0xAC+4] << 8 | dashbin[0xAC+3] << 16 |dashbin[0xAC+2] << 24 | dashbin[0xAC + 1] << 32 | dashbin[0xAC] << 40)).ToString();
            //trip A 0xB2
            textBox2.Text = ((dashbin[0xB2+5] | dashbin[0xB2 + 4] << 8 | dashbin[0xB2 + 3] << 16 | dashbin[0xB2 + 2] << 24 | dashbin[0xB2 + 1] << 32 | dashbin[0xB2] << 40)).ToString();
            //Low QWord 0xCC
            byte offset = dashbin[0xCC];
            int rev = 0;
            if (offset != 0xFF)
            {
                int minor = offset;
                while (Convert.ToBoolean(minor))
                {
                    int r = minor % 10;
                    rev = rev * 10 + r;
                    minor = minor / 10;
                }
            }
            //High Qword 
            //0 E0
            //1 E4
            //2 E8
            //3 EC
            //4 F0
            //5 F4
            //6 F8
            //7 FC
            maskedTextBox1.Text = (((dashbin[selectedgood] | dashbin[selectedgood + 1] << 8) * 16) + rev).ToString();
        }


        private byte checksum(byte inbyte)
        {
            //CheckSum function.
            return (byte)(inbyte ^ byte.MaxValue);
        }

        private bool checksanaty()
        {
            //Checks each km reading to see what ones have valid checksums.
            int OdomAddress = 0xE0;
            for (int i = 0; i < 8; i++)
            {
                if (dashbin[OdomAddress] != checksum(dashbin[OdomAddress+2]))
                {
                    //MessageBox.Show("CS mismatch @ " + OdomAddress);
                }
                else
                {
                    if (selectedgood == 0)
                    selectedgood = (byte)OdomAddress;
                }
                OdomAddress += 0x04;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Save modded bin.
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save Bin File";
                saveFileDialog1.DefaultExt = "bin";
                saveFileDialog1.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog1.FileName, dashbin);
                    MessageBox.Show(saveFileDialog1.FileName + " has been saved");
                    Processbin();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //validates KM to nearest major step.
            int NewKEstimate = (int)Math.Ceiling(double.Parse(maskedTextBox1.Text) / 16);
            int NewKs = (int)Math.Floor(double.Parse(maskedTextBox1.Text) / 16);
            maskedTextBox1.Text = (NewKEstimate * 16).ToString();

            byte NKbyte1 = (byte)NewKs,
            NKbyte2 = (byte)(NewKs >> 8),
            NKCS1 = checksum(NKbyte1),
            NKCS2 = checksum(NKbyte2);

            //Zeros out the minor steps
            byte minoroffset = 0xCC;
            dashbin[minoroffset] = 0x00;
            dashbin[minoroffset + 1] = 0x00;
            dashbin[minoroffset + 2] = checksum(dashbin[minoroffset]);
            dashbin[minoroffset + 3] = checksum(dashbin[minoroffset + 1]);

            //sets major steps
            int OdomAddress = 0xE0;
            for (int i = 0; i < 8; i++)
            {
                dashbin[OdomAddress] = NKbyte1;
                dashbin[OdomAddress + 1] = NKbyte2;
                dashbin[OdomAddress + 2] = NKCS1;
                dashbin[OdomAddress + 3] = NKCS2;
                OdomAddress += 0x04;
            }
            button2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Clear Trip A B2
            dashbin[0xB2 + 5] = 0xFF;
            dashbin[0xB2 + 4] = 0xFF;
            dashbin[0xB2 + 3] = 0xFF;
            dashbin[0xB2 + 2] = 0xFF;
            dashbin[0xB2 + 1] = 0xFF;
            dashbin[0xB2] = 0xFF;
            textBox2.Text = "000000";
            button2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Clear Trip B AC
            dashbin[0xAC + 5] = 0xFF;
            dashbin[0xAC + 4] = 0xFF;
            dashbin[0xAC + 3] = 0xFF;
            dashbin[0xAC + 2] = 0xFF;
            dashbin[0xAC + 1] = 0xFF;
            dashbin[0xAC] = 0xFF;
            textBox3.Text = "000000";
            button2.Enabled = true;
        }

        private byte[] MergeData(byte[] NewRom, byte[] OldRom)
        {
            byte[] WorkingData = OldRom;
            int position = 0;
            foreach (byte newdata in NewRom)
            {
                switch(position)
                {
                    case 0xAC:
                        break;
                    case 0xAD:
                        break;
                    case 0xAE:
                        break;
                    case 0xAF:
                        break;
                    case 0xB0:
                        break;
                    case 0xB1:
                        break;
                    case 0xB2:
                        break;
                    case 0xB3:
                        break;
                    case 0xB4:
                        break;
                    case 0xB5:
                        break;
                    case 0xB6:
                        break;
                    case 0xB7:
                        break;
                    case 0xCC:
                        break;
                    case 0xCD:
                        break;
                    case 0xCE:
                        break;
                    case 0xCF:
                        break;
                    default:
                        if (position < 224)
                        WorkingData[position] = newdata;
                        break;
                }
                position++;
            }
            return WorkingData;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Convert To Auto Code
            dashbin = MergeData(Auto, dashbin);
            button2.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Convert To Manual Code
            dashbin = MergeData(Manual, dashbin);
            button2.Enabled = true;
        }
    }
}
