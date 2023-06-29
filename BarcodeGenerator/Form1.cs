using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Barcode;


namespace BarcodeGenerator
{
    public partial class Form1 : Form
    {
        BarcodeSettings bs = new BarcodeSettings();

        public Form1()
        {
            InitializeComponent();
            Array bTypes=Enum.GetValues(typeof(BarCodeType));
            comboBox1.DataSource = bTypes;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //BarcodeSettings bs = new BarcodeSettings();
            bs.Type = BarCodeType.Code25;
            bs.Data = "ABC 12330";
            
            BarCodeGenerator bg = new BarCodeGenerator(bs);
            bg.GenerateImage().Save("Code25bAR.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Diagnostics.Process.Start("Code25bAR.jpeg");
            textBox1.Text = bs.Data;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string barS = comboBox1.SelectedItem.ToString();           
           
            bs.Type= (BarCodeType)Enum.Parse(typeof(BarCodeType), barS);
            
            if (textBox1.Text.ToString() == "")
            {
                bs.Data = "ABC 123456789";
                textBox1.Text = bs.Data;
            }
            else
            {
                bs.Data = textBox1.Text.ToString();
            }
            BarCodeGenerator bg = new BarCodeGenerator(bs);
            pictureBox1.Image = bg.GenerateImage();

            
        }

        private void button3_Click(object sender, EventArgs e)//Barcode Image kaydet
        {
            BarCodeGenerator bg = new BarCodeGenerator(bs);
            string barS = comboBox1.SelectedItem.ToString();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap Image(*.bmp)|*.bmp|JPEG Image(*.jpg)|*.jpg|PNG Image(*.png)|*.png";
            saveFileDialog.Title = "Barkod nereye kaydedilsin ?";
            string filename = textBox1.Text + "-" + barS;
            saveFileDialog.FileName = filename;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bg.GenerateImage().Save(saveFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)//Dosyadan barkod oku
        {
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.Filter = "Bitmap Image(*.bmp)|*.bmp|JPEG Image(*.jpg)|*.jpg|PNG Image(*.png)|*.png";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image=Image.FromFile(ofd.FileName);
                string readBarcode = BarcodeScanner.ScanOne(pictureBox2.Image as Bitmap);
                
                textBox2.Text = readBarcode;
                // bs.Type = (BarCodeType)Enum.Parse(typeof(BarCodeType),readBarcode);
               // textBox2.Text = bs.Type.ToString();
            }
        }
    }
}
