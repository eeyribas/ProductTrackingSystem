using ProductTrackingSystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductTrackingSystem
{
    public partial class Form2 : Form
    {
        private PrintDocument printDocument = new PrintDocument();
        private Ean13 barcode1 = new Ean13();
        private Ean13 barcode2 = new Ean13();
        private Ean13 barcode3 = new Ean13();
        private Ean13 barcode4 = new Ean13();
        private Ean13 barcode5 = new Ean13();

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            barcode1.CountryCode = "1";
            barcode1.ManufacturerCode = textBox1.Text;
            barcode1.ProductCode = textBox2.Text + textBox3.Text + textBox4.Text;
            pictureBox1.Image = barcode1.CreateBitmap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            barcode2.CountryCode = "1";
            barcode2.ManufacturerCode = textBox7.Text;
            barcode2.ProductCode = textBox8.Text + textBox9.Text + textBox10.Text;
            pictureBox2.Image = barcode2.CreateBitmap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            barcode3.CountryCode = "1";
            barcode3.ManufacturerCode = textBox13.Text;
            barcode3.ProductCode = textBox14.Text + textBox15.Text + textBox16.Text;
            pictureBox3.Image = barcode3.CreateBitmap();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            barcode4.CountryCode = "1";
            barcode4.ManufacturerCode = textBox19.Text;
            barcode4.ProductCode = textBox20.Text + textBox21.Text + textBox22.Text;
            pictureBox4.Image = barcode4.CreateBitmap();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            barcode5.CountryCode = "1";
            barcode5.ManufacturerCode = textBox25.Text;
            barcode5.ProductCode = textBox26.Text + textBox27.Text + textBox28.Text;
            pictureBox5.Image = barcode5.CreateBitmap();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            barcode1.ProductCode = textBox2.Text + textBox3.Text + textBox4.Text;
            barcode2.ProductCode = textBox8.Text + textBox9.Text + textBox10.Text;
            barcode3.ProductCode = textBox14.Text + textBox15.Text + textBox16.Text;
            barcode4.ProductCode = textBox20.Text + textBox21.Text + textBox22.Text;
            barcode5.ProductCode = textBox26.Text + textBox27.Text + textBox28.Text;

            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.Print();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            barcode1.ManufacturerCode = textBox2.Text;
            pictureBox1.Image = barcode1.CreateBitmap();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            barcode1.ManufacturerCode = textBox3.Text;
            pictureBox1.Image = barcode1.CreateBitmap();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            barcode1.ProductCode = textBox4.Text;
            pictureBox1.Image = barcode1.CreateBitmap();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            barcode2.ManufacturerCode = textBox8.Text;
            pictureBox2.Image = barcode2.CreateBitmap();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            barcode2.ManufacturerCode = textBox9.Text;
            pictureBox2.Image = barcode2.CreateBitmap();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            barcode2.ProductCode = textBox10.Text;
            pictureBox2.Image = barcode2.CreateBitmap();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            barcode3.ManufacturerCode = textBox14.Text;
            pictureBox3.Image = barcode3.CreateBitmap();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            barcode3.ManufacturerCode = textBox15.Text;
            pictureBox3.Image = barcode3.CreateBitmap();
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            barcode3.ProductCode = textBox16.Text;
            pictureBox3.Image = barcode3.CreateBitmap();
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            barcode4.ManufacturerCode = textBox20.Text;
            pictureBox4.Image = barcode4.CreateBitmap();
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            barcode4.ManufacturerCode = textBox21.Text;
            pictureBox4.Image = barcode4.CreateBitmap();
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            barcode4.ProductCode = textBox22.Text;
            pictureBox4.Image = barcode4.CreateBitmap();
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            barcode5.ManufacturerCode = textBox26.Text;
            pictureBox5.Image = barcode5.CreateBitmap();
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            barcode5.ManufacturerCode = textBox27.Text;
            pictureBox5.Image = barcode5.CreateBitmap();
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            barcode5.ProductCode = textBox28.Text;
            pictureBox5.Image = barcode5.CreateBitmap();
        }

        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            barcode1.DrawEan13Barcode(e.Graphics, (new PointF(Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text))));
            barcode2.DrawEan13Barcode(e.Graphics, (new PointF(Convert.ToInt32(textBox11.Text), Convert.ToInt32(textBox12.Text))));
            barcode3.DrawEan13Barcode(e.Graphics, (new PointF(Convert.ToInt32(textBox17.Text), Convert.ToInt32(textBox18.Text))));
            barcode4.DrawEan13Barcode(e.Graphics, (new PointF(Convert.ToInt32(textBox23.Text), Convert.ToInt32(textBox24.Text))));
            barcode5.DrawEan13Barcode(e.Graphics, (new PointF(Convert.ToInt32(textBox29.Text), Convert.ToInt32(textBox30.Text))));
        }
    }
}
