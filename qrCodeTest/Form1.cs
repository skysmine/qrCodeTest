using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QRCoder;
//using static QRCoder.PayloadGenerator;

namespace qrCodeTest
{
    public partial class Form1 : Form
    {
        //int preWidth;
        //int preHeight;
        //int preImageWidth;
        //int preImageHeight;

        Bitmap qrCodeImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //preWidth = this.Width;
            //preHeight = this.Height;

            //preImageHeight = pictureBox1.Height;
            //preImageWidth = pictureBox1.Width;
            if (this.Width > this.Height)
            {
                pictureBox1.Height = (int)(this.Height * 0.8);
                pictureBox1.Width = (int)(this.Height * 0.8);
                //pictureBox1.Location = new Point((int)(this.Height * 0.1), (int)(this.Height * 0.1));
            }
            else
            {
                pictureBox1.Height = (int)(this.Width * 0.8);
                pictureBox1.Width = (int)(this.Width * 0.8);
                //pictureBox1.Location = new Point((int)(this.Width * 0.1), (int)(this.Width * 0.1));
            }
            int locationX = (this.Width - pictureBox1.Width) / 2;
            int locationY = (this.Height - pictureBox1.Height) / 2;
            pictureBox1.Location = new Point(locationX, locationY);



            String str;
            StreamReader sr = new StreamReader(Application.StartupPath + "\\test.txt", false);
            str = sr.ReadToEnd().ToString();
            sr.Close();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            qrCodeImage = qrCode.GetGraphic(20);

            //WiFi generator = new WiFi("TP-LINK_E802", "18005647653", WiFi.Authentication.nopass);
            //string payload = generator.ToString();

            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            //QRCode qrCode = new QRCode(qrCodeData);
            //var qrCodeAsBitmap = qrCode.GetGraphic(20);

            this.pictureBox1.Image = PictureChangeSize(qrCodeImage);
        }


        //改变图片尺寸，使其可以在picturebox中显示
        private Bitmap PictureChangeSize(Bitmap qrCodeImage)
        {
            int sourceWidth = qrCodeImage.Width;
            int sourceHeight = qrCodeImage.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)pictureBox1.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)pictureBox1.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(qrCodeImage, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //float nPercent = 0;
            //float nPercentW = 0;
            //float nPercentH = 0;

            //nPercentW = (float)this.Width / (float)preWidth;
            //nPercentH = (float)this.Height / (float)preHeight;

            //if (nPercentH < nPercentW)
            //    nPercent = nPercentH;
            //else
            //    nPercent = nPercentW;

            //pictureBox1.Height = (int)(preHeight * nPercent);
            //pictureBox1.Width = (int)(preWidth * nPercent);

            if (this.Width > this.Height)
            {
                pictureBox1.Height = (int)(this.Height * 0.8);
                pictureBox1.Width = (int)(this.Height * 0.8);
                //pictureBox1.Location = new Point((int)(this.Height * 0.1), (int)(this.Height * 0.1));
            }
            else
            {
                pictureBox1.Height = (int)(this.Width * 0.8);
                pictureBox1.Width = (int)(this.Width * 0.8);
                //pictureBox1.Location = new Point((int)(this.Width * 0.1), (int)(this.Width * 0.1));
            }


            int locationX = (this.Width - pictureBox1.Width) / 2;
            int locationY = (this.Height - pictureBox1.Height) / 2;
            pictureBox1.Location = new Point(locationX, locationY);

            this.pictureBox1.Image = PictureChangeSize(qrCodeImage);
        }
    }
}
