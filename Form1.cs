using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace Generador_Carnet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string anio = txtAnio.Text;
            int anioNumero = int.Parse(anio);
       
            if (anioNumero < 2020)
            {
                MessageBox.Show(
                    "No se permiten carnés menores al año 2020");

                return;
            }
            string carrera = txtCarrera.Text;
            string orden = txtOrden.Text;

            // Unir primeros 9 dígitos
            string baseCarnet = anio + carrera + orden;

            // Vector de control
            int[] control = { 3, 5, 7, 2, 1, 4, 6, 8, 9 };

            int suma = 0;

            for (int i = 0; i < 9; i++)
            {
                suma += int.Parse(baseCarnet[i].ToString()) * control[i];
            }

            int digito = suma % 10;

            string nuevoCarnet = baseCarnet + digito;

            txtCarnet.Text = nuevoCarnet;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                txtCarnet.Text,
                QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrImage = qrCode.GetGraphic(20);

            pictureBox1.Image = qrImage;
        }
    }
}
