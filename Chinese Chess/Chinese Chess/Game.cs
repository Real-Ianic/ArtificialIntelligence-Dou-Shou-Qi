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
        //Players
        Player p1;
        Player p2;

        //Data Saved Variables
        GameBoard gBoard;

        //Game Mechanic Variables
        int turn;
        bool movePiece = false;
        int xPieceChosen;
        int yPieceChosen;

        public Game(String gameType)
        {
            InitializeComponent();

            if(gameType == "pvp")
            {
                //Harap nanti menggunakan Message Box untuk input nama;
                p1 = new Human("Adrian");
                p2 = new Human("Jose");
            }
            else if(gameType == "pve")
            {

            }
            else if(gameType == "eve")
            {

            }
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

        // Void to Move a Piece
        private void movePieceTo(int xButton, int yButton)
        {
            gBoard.mapBoard[xButton, yButton].CurrentPiece = gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece;
            gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece = null;
            movePiece = false;
            changeTurns();
            currentAnimal.Text = $"Current Animal: None";
        }

        private void selectPiece(int xButton, int yButton)
        {
            movePiece = true;
            xPieceChosen = xButton;
            yPieceChosen = yButton;
            currentAnimal.Text = $"Current Animal : {gBoard.mapBoard[xButton, yButton].CurrentPiece.Name}";
        }

        // Void to Cancel Selection / Deselect Animal
        private void cancelSelect()
        {
            xPieceChosen = -1;
            yPieceChosen = -1;
            movePiece = false;
            currentAnimal.Text = $"Current Animal : None";
        }

        //Function untuk semua button yang ada di peta
        private void mapClick(object sender, EventArgs e)
        {
            if(p1 is Human && turn == 1)
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

                // Check Wheter is Any Animal Selected.. ( First Click Check )
                if(!movePiece)
                {
                    // check cliked is null ?
                    if (gBoard.mapBoard[xButton, yButton].CurrentPiece != null)
                    {
                        //check wheter selected animal belongs to this player
                        if (gBoard.mapBoard[xButton, yButton].CurrentPiece.Player == turn)
                        {
                            //this is first click
                            selectPiece(xButton, yButton);
                        }
                    }
                }
                else
                {
                    // If it's a second click :)
                    //check whether the dest node is null and is valid move (1 tile away)
                    if(gBoard.mapBoard[xButton, yButton].CurrentPiece == null && (Math.Abs(xButton - xPieceChosen) + Math.Abs(yButton - yPieceChosen) == 1))
                    {
                        // if this a valid move then
                        //check wheter dest node is water or not
                        if (gBoard.mapBoard[xButton, yButton].IsWater)
                        {
                            // if dest is water then
                            // check wheter current animal piece can swim, jump, or cannot both
                            if(gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.CanSwim)
                            {
                                // if current animal can swim
                                // move current animal to dest
                                movePieceTo(xButton, yButton);
                            }
                            else if(gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.CanJump)
                            {
                                // if current animal can jump
                                // move current animal to edge of water (Currently not working / not yet done)
                                movePieceTo(xButton, yButton);
                            }
                            else
                            {
                                // if animal cannot move, then move is invalid
                                MessageBox.Show("Invalid Move!");
                            }
                        }
                        else
                        {
                            // if not a water then go...
                            movePieceTo(xButton, yButton);
                        }
                    }
                    else if(xButton == xPieceChosen && yButton == yPieceChosen)
                    {
                        // cancel select
                        cancelSelect();
                    }
                    else if (xButton == xPieceChosen || yButton == yPieceChosen)
                    {
                        // check wheter selected animal can jump
                        if(gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.CanJump)
                        {
                            //check sebelah air
                            if(gBoard.mapBoard[xPieceChosen + 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen + 1].IsWater || gBoard.mapBoard[xPieceChosen - 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen - 1].IsWater)
                            {
                                // animal jump
                                movePieceTo(xButton,yButton);
                            }
                            else
                            {
                                MessageBox.Show("Invalid Move!");
                            }
                        }
                    }
                    else
                    {
                        // not a valid move
                        MessageBox.Show("Invalid Move!");
                    }
                }
            }
            else
            {
                //AI MOVE HERE
            }

            if (p2 is Human && turn == 2)
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

                // Check Wheter is Any Animal Selected.. ( First Click Check )
                if (!movePiece)
                {
                    // check cliked is null ?
                    if (gBoard.mapBoard[xButton, yButton].CurrentPiece != null)
                    {
                        //check wheter selected animal belongs to this player
                        if (gBoard.mapBoard[xButton, yButton].CurrentPiece.Player == turn)
                        {
                            //this is first click
                            selectPiece(xButton, yButton);
                        }
                    }
                }
                else
                {
                    // If it's a second click :)
                    //check whether the dest node is null and is valid move (1 tile away)
                    if (gBoard.mapBoard[xButton, yButton].CurrentPiece == null && (Math.Abs(xButton - xPieceChosen) + Math.Abs(yButton - yPieceChosen) == 1))
                    {
                        // if this a valid move then
                        //check wheter dest node is water or not
                        if (gBoard.mapBoard[xButton, yButton].IsWater)
                        {
                            // if dest is water then
                            // check wheter current animal piece can swim, jump, or cannot both
                            if (gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.CanSwim)
                            {
                                // if current animal can swim
                                // move current animal to dest
                                movePieceTo(xButton, yButton);

                            }
                            else if (gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.CanJump)
                            {
                                // if current animal can jump
                                // move current animal to edge of water (Currently not working / not yet done)
                                movePieceTo(xButton, yButton);
                            }
                            else
                            {
                                // if animal cannot move, then move is invalid
                                MessageBox.Show("Invalid Move!");
                            }
                        }
                        else
                        {
                            // if not a water then go...
                            movePieceTo(xButton, yButton);
                        }
                    }
                    else if (xButton == xPieceChosen && yButton == yPieceChosen)
                    {
                        // cancel select
                        cancelSelect();
                    }
                    else if (xButton == xPieceChosen || yButton == yPieceChosen)
                    {
                        // check wheter selected animal can jump
                        if (gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.CanJump)
                        {
                            //check sebelah air
                            if (gBoard.mapBoard[xPieceChosen + 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen + 1].IsWater || gBoard.mapBoard[xPieceChosen - 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen - 1].IsWater)
                            {
                                // animal jump
                                movePieceTo(xButton, yButton);
                            }
                            else
                            {
                                MessageBox.Show("Invalid Move!");
                            }
                        }
                    }
                    else
                    {
                        // not a valid move
                        MessageBox.Show("Invalid Move!");
                    }
                }
            }
            else
            {
                //AI MOVE HERE
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
            turn = turn==1?2:1;
            labelTurn.Text = $"Current Turn: {turn}";
        }
    }
}
