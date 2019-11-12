using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    mapButton[j, i].Size = new Size(100,100);
                    mapButton[j, i].Location = new Point(100 + j * 100,100 + i * 100);
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

            mapBoard[0, 8].CurrentPiece = new AnimalPiece(1,1);


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
                    }
                    else
                    {
                        mapButton[j, i].Text = "";
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
