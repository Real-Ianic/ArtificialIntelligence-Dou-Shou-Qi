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
        GameBoard gBoard;

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

            gBoard = new GameBoard();

            //Turn starts from player 1
            turn = 1;

            for(int i=0;i<9;i++)
            {
                for(int j=0;j<7;j++)
                {
                    gBoard.mapBoard[j, i].TheButton.Click += new EventHandler(mapClick);
                    this.Controls.Add(gBoard.mapBoard[j, i].TheButton);
                }
            }

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
                    if (gBoard.mapBoard[j, i].TheButton == tempButton)
                    {
                        yButton = i;
                        xButton = j;
                        //MessageBox.Show("X : "+j +" - Y : " + i); // Harap comment line ini, hanya untuk debug
                    }
                }
            }
            
            /*
             * NOTE CURRENT PIECE ERROR KETIKA DI CLICK UNTUK CLICK KE 2, HARAP DIGANTI -ian
             */ 

            if(gBoard.mapBoard[xButton,yButton].CurrentPiece.Player == turn )
            {

                if (!movePiece)
                {
                    //First Click
                    if (gBoard.mapBoard[xButton, yButton].CurrentPiece != null)
                    {
                        //Boleh Gerak karena di kotak itu ada hewan kayak kamu
                        movePiece = true;
                        xPieceChosen = xButton;
                        yPieceChosen = yButton;
                        currentAnimal.Text = $"Current Animal : {gBoard.mapBoard[xButton, yButton].CurrentPiece.Name}";
                    }
                    else
                    {
                        MessageBox.Show("Gak ada hewannya lur");
                    }
                }
                else
                {
                    //Second Click
                    if (gBoard.mapBoard[xButton, yButton].CurrentPiece == null && (Math.Abs(xButton - xPieceChosen) + Math.Abs(yButton - yPieceChosen) == 1))
                    {
                        //BOLEH GERAK
                        gBoard.mapBoard[xButton, yButton].CurrentPiece = gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece;
                        gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece = null;
                        movePiece = false;
                        currentAnimal.Text = "Current Animal : None";
                        changeTurns();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Move");
                    }
                }
                refreshMap();
            }
            else MessageBox.Show("Gilirannya siapa lur");
        }

        //Hanya merefresh button yang ada dengan data yang ada
        private void refreshMap()
        {
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<7;j++)
                {
                    if(gBoard.mapBoard[j,i].CurrentPiece != null)
                    {
                        gBoard.mapBoard[j, i].TheButton.Text = gBoard.mapBoard[j, i].CurrentPiece.Name;
                        //mapButton[j, i].Image = mapBoard[j, i].CurrentPiece.Icon;
                        gBoard.mapBoard[j, i].TheButton.BackgroundImage = gBoard.mapBoard[j, i].CurrentPiece.Icon;
                        gBoard.mapBoard[j, i].TheButton.BackgroundImageLayout = ImageLayout.Center;



                    }
                    else
                    {
                        gBoard.mapBoard[j, i].TheButton.Text = "";
                        gBoard.mapBoard[j, i].TheButton.BackgroundImage = null;
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
