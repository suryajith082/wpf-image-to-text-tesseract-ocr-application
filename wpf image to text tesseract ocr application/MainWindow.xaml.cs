using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace wpf_image_to_text_tesseract_ocr_application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //Creating a Rectangle object which will  
            //capture our Current Screen
            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            //Creating a new Bitmap object
            Bitmap captureBitmap = new Bitmap(captureRectangle.Right - captureRectangle.Left, captureRectangle.Bottom - captureRectangle.Top, PixelFormat.Format32bppArgb);
            //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
            //Creating a New Graphics Object
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            //Copying Image from The Screen
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            //Saving the Image File

            //captureBitmap.Save(@"Capture.jpg", ImageFormat.Jpeg);
            using (var image = new Image<Bgr, byte>(captureBitmap))
            {
                using (var tess = new Tesseract("C:\\Users\\Acer PC\\source\\repos\\test and try project\\test and try project\\obj\\Debug\\tessdata", "eng", OcrEngineMode.TesseractCubeCombined))
                {
                    tess.Recognize(image);
                    string text = tess.GetText().TrimEnd();
                    //Console.WriteLine(text);
                    textBox.Text = text;
                }
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
        }
    }
}
