using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace cc21728rafaelcguerinojogodememoria
{
    public partial class Form1 : Form
    {

        int mov, click, cartasEncontradas, tag;

        Image[] img = new Image[6];

        List<string> lista = new List<string>();

        int[] guardarTag = new int[2];



        public Form1()
        {
            InitializeComponent();
            Começar();
        }


        void Começar()
        {
            foreach (PictureBox imagem in Controls.OfType<PictureBox>()) // percorre as pictureBox
            {
                tag = int.Parse(String.Format("{0}", imagem.Tag)); // converte a tag da imagem para um inteiro
                img[tag] = imagem.Image;
                imagem.Image = Properties.Resources.narutoLogo; // deixa todas as imagem com o verso para cima
                imagem.Enabled = true;
            }
            Embaralhar();
        }


        void Embaralhar()
        {
            foreach (PictureBox imagem in Controls.OfType<PictureBox>())
            {
                Random aleatorio = new Random();

                int[] x = { 79, 216, 359, 472 }; // posições na horizontal
                int[] y = { 80, 175, 277 }; // posições na vertical

            Repetir:
                var xPosicao = x[aleatorio.Next(0, x.Length)]; // pega uma posição aleatória na horizonta
                var yPosicao = y[aleatorio.Next(0, y.Length)]; // pega uma posição aleatória na vertical

                imagem.Location = new Point(xPosicao, yPosicao); // coloca a imagem nas posições aleatórias

                string verificar = xPosicao.ToString() + yPosicao.ToString();

                if (lista.Contains(verificar)) // verifica se já há uma imagem na posição sorteada
                {
                    goto Repetir;
                }
                else
                {
                    imagem.Location = new Point(xPosicao, yPosicao); // coloca a imagem nas posições aleatórias
                    lista.Add(verificar); // adiciona as posições a lista   
                }
            }
        }
        private void pbNaruto_Click(object sender, EventArgs e)
        {
            bool acerto = false;

            PictureBox pb = (PictureBox)sender;
            click++;
            tag = int.Parse(String.Format("{0}", pb.Tag));
            pb.Image = img[tag];
            pb.Refresh();

            if (click == 1)
            {
                guardarTag[0] = int.Parse(String.Format("{0}", pb.Tag));
            }
            else if (click == 2)
            {
                mov++;
                lbMov.Text = "Movimentos :" + mov.ToString();
                guardarTag[1] = int.Parse(String.Format("{0}", pb.Tag));
                acerto = AcertouPares();
                RevelarCarta(acerto);
            }

        }

        bool AcertouPares() // este metdo verifica se o jogador acertou os pares das cartas 
        {
            click = 0;

            if (guardarTag[0] == guardarTag[1])
                return true;
            else
                return false;
        }

        void RevelarCarta(bool check)
        {
            Thread.Sleep(500);

            foreach (PictureBox imagem in Controls.OfType<PictureBox>())
            {
                if (int.Parse(String.Format("{0}", imagem.Tag)) == guardarTag[0] ||
                    int.Parse(String.Format("{0}", imagem.Tag)) == guardarTag[1])
                {

                    if (check == true)
                    {
                        imagem.Enabled = false;
                        cartasEncontradas++;
                    }
                    else
                    {
                        imagem.Image = Properties.Resources.narutoLogo;
                        imagem.Refresh();
                    }
                }
            }

            Acabo();
        }
    
   

        void Acabo()
        {
            if (cartasEncontradas == img.Length * 2)
            {
                MessageBox.Show("Parabéns você venceu o jogo com " + mov.ToString(), " movimentos");
                DialogResult msg = MessageBox.Show("Quer continuar jogando?", "Caixa de pergunta", MessageBoxButtons.YesNo);

                if(msg == DialogResult.Yes)
                {
                    click = 0; mov = 0; cartasEncontradas = 0;
                    lista.Clear();
                    Começar();
                }
                else if(msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigado pro jogar :)");
                    Application.Exit();
                }
            }
        }
    }

}
