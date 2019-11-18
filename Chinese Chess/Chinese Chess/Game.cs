using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Chinese_Chess
{
    public partial class Game : Form
    {
        //Data Saved Variables
        Button[,] mapButton = new Button[7, 9]; // <--- Map nya gan tapi buat tampilan saja alias button saja
        SquareNode[,] mapBoard = new SquareNode[7, 9]; // <--- Data yang disimpan ke dalam Map

        //Game Mechanic Variables
        int turn;
        bool movePiece = false;
        int xPieceChosen;
        int yPieceChosen;

        public Game()
        {
            InitializeComponent();
        }


        private void Game_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //Start of Map Creation
            for (int i=0;i<9;i++)
            {
                for(int j=0;j<7;j++)
                {
                    //Membuat tampilan Peta
                    mapButton[j, i] = new Button();
                    mapButton[j, i].Size = new Size(75,75);
                    mapButton[j, i].Location = new Point(25 + j * 75,25 + i * 75);
                    mapButton[j, i].Click += new EventHandler(mapClick);
                    this.Controls.Add(mapButton[j, i]);

                    //Menyiapkan variabel mapBoard
                    mapBoard[j, i] = new SquareNode(false);
                }
            }

            //Menyiapkan variabel mapBoard
            mapBoard[1, 3].IsWater = true; mapButton[1, 3].BackColor = Color.Blue;
            mapBoard[2, 3].IsWater = true; mapButton[2, 3].BackColor = Color.Blue;
            mapBoard[1, 4].IsWater = true; mapButton[1, 4].BackColor = Color.Blue;
            mapBoard[2, 4].IsWater = true; mapButton[2, 4].BackColor = Color.Blue;
            mapBoard[1, 5].IsWater = true; mapButton[1, 5].BackColor = Color.Blue;
            mapBoard[2, 5].IsWater = true; mapButton[2, 5].BackColor = Color.Blue;

            mapBoard[4, 3].IsWater = true; mapButton[4, 3].BackColor = Color.Blue;
            mapBoard[5, 3].IsWater = true; mapButton[5, 3].BackColor = Color.Blue;
            mapBoard[4, 4].IsWater = true; mapButton[4, 4].BackColor = Color.Blue;
            mapBoard[5, 4].IsWater = true; mapButton[5, 4].BackColor = Color.Blue;
            mapBoard[4, 5].IsWater = true; mapButton[4, 5].BackColor = Color.Blue;
            mapBoard[5, 5].IsWater = true; mapButton[5, 5].BackColor = Color.Blue;

            //PLAYER 1 (atas)
            mapBoard[6, 2].CurrentPiece = new AnimalPiece(8, 1); //elephant
            mapBoard[4, 2].CurrentPiece = new AnimalPiece(3, 1); //wolf
            mapBoard[2, 2].CurrentPiece = new AnimalPiece(5, 1); //leopard 
            mapBoard[0, 2].CurrentPiece = new AnimalPiece(1, 1); //rat
                                                             
            mapBoard[5, 1].CurrentPiece = new AnimalPiece(2, 1); //cat
            mapBoard[1, 1].CurrentPiece = new AnimalPiece(4, 1); //dog
                                                             
            mapBoard[6, 0].CurrentPiece = new AnimalPiece(6, 1); //tiger
            mapBoard[0, 0].CurrentPiece = new AnimalPiece(7, 1); //lion

            //PLAYER 2 (bawah)
            mapBoard[0, 6].CurrentPiece = new AnimalPiece(8,2); //elephant
            mapBoard[2, 6].CurrentPiece = new AnimalPiece(3,2); //wolf
            mapBoard[4, 6].CurrentPiece = new AnimalPiece(5,2); //leopard 
            mapBoard[6, 6].CurrentPiece = new AnimalPiece(1,2); //rat

            mapBoard[1, 7].CurrentPiece = new AnimalPiece(2,2); //cat
            mapBoard[5, 7].CurrentPiece = new AnimalPiece(4,2); //dog

            mapBoard[0, 8].CurrentPiece = new AnimalPiece(6,2); //tiger
            mapBoard[6, 8].CurrentPiece = new AnimalPiece(7,2); //lion


            //Turn starts from player 1
            turn = 1;

            refreshMap();
        }

        //Function untuk semua button yang ada di peta
        private void mapClick(object sender, EventArgs e)
        {
            Button tempButton = (Button)sender;
            int xButton = -1;
            int yButton = -1;

            //Untuk mendapatkan Index dari Button yang akan di click
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (mapButton[j, i] == tempButton)
                    {
                        yButton = i;
                        xButton = j;
                        MessageBox.Show("X : "+j +" - Y : " + i); // Harap comment line ini, hanya untuk debug
                    }
                }
            }

            if (!movePiece)
            {
                //First Click
                if(mapBoard[xButton,yButton].CurrentPiece != null)
                {
                    //Boleh Gerak karena di kotak itu ada hewan kayak kamu
                    movePiece = true;
                    xPieceChosen = xButton;
                    yPieceChosen = yButton;
                }
                else
                {
                    MessageBox.Show("Gak ada hewannya lur");
                }
            }
            else
            {
                //Second Click
                if(mapBoard[xButton,yButton].CurrentPiece == null && (Math.Abs(xButton - xPieceChosen) + Math.Abs(yButton - yPieceChosen) == 1))
                {
                    //BOLEH GERAK
                    mapBoard[xButton, yButton].CurrentPiece = mapBoard[xPieceChosen, yPieceChosen].CurrentPiece;
                    mapBoard[xPieceChosen, yPieceChosen].CurrentPiece = null;
                    movePiece = false;
                }
                else
                {
                    MessageBox.Show("Invalid Move");
                }
            }
            refreshMap();
        }

        //Hanya merefresh button yang ada dengan data yang ada
        private void refreshMap()
        {
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<7;j++)
                {
                    if(mapBoard[j,i].CurrentPiece != null)
                    {
                        mapButton[j, i].Text = mapBoard[j, i].CurrentPiece.Name;
                        //mapButton[j, i].Image = mapBoard[j, i].CurrentPiece.Icon;
                        mapButton[j, i].BackgroundImage = mapBoard[j, i].CurrentPiece.Icon;
                        mapButton[j, i].BackgroundImageLayout = ImageLayout.Center;



                    }
                    else
                    {
                        mapButton[j, i].Text = "";
                        mapButton[j, i].BackgroundImage = null;
                    }
                }
            }
        }

        //Hanya toggle turn player dari 1 ke 2
        private void changeTurns()
        {
            if(turn == 1)
            {
                turn = 2;
            }
            else
            {
                turn = 1;
            }
        }
    }
}
