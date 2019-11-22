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
            refreshMap();

            //set the piece strength to 0 (zero) because it stepped on a trap
            if ((turn==1 && gBoard.mapBoard[xButton, yButton].IsTrapP2) || (turn == 2 && gBoard.mapBoard[xButton, yButton].IsTrapP1)) //check the player side and the trap side
            {
                //if player 1 step into player 2 trap its strength will be set to 0 (zero)
                gBoard.mapBoard[xButton, yButton].CurrentPiece.Strength = 0; //if player 1 step into its own trap, nothing happened
            }
            else if ((turn == 1 && gBoard.mapBoard[xButton, yButton].IsDen2) || (turn == 2 && gBoard.mapBoard[xButton, yButton].IsDen1)) //check the player side and the den side
            {
                // one of the players win
                if (turn == 1) endGame(1);
                else if (turn == 2) endGame(2);
            }
            else
            { 
                // returning animal's strength to its normal strength when the animal is outside the trap
                gBoard.mapBoard[xButton, yButton].CurrentPiece.Strength = gBoard.mapBoard[xButton, yButton].CurrentPiece.maxStrength; 
            }
            movePiece = false;
            currentAnimal.Text = $"Current Animal: None";
            changeTurns();
        }

        private void endGame(int player)
        {
            MessageBox.Show("GAME OVER!\nPlayer "+player+" is the winner!");
            this.Close();
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
                // ( Second Click Check )
                else
                {
                    if (xButton == xPieceChosen && yButton == yPieceChosen)
                    {
                        // cancel select
                        cancelSelect();
                    }
                    else
                    {
                        //move to empty slot without enemy
                        if (gBoard.mapBoard[xButton, yButton].CurrentPiece == null)
                        {
                            // If it's a second click :)
                            //check whether the dest node is null and is valid move (1 tile away)
                            if (Math.Abs(xButton - xPieceChosen) + Math.Abs(yButton - yPieceChosen) == 1)
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
                                    else
                                    {
                                        // if animal cannot move, then move is invalid
                                        MessageBox.Show("Invalid Move!\nAnimal Can't Swim!");
                                    }
                                }
                                else if (turn==1 && gBoard.mapBoard[xButton, yButton].IsDen1)
                                {
                                    // if player move to his own den
                                    MessageBox.Show("Invalid Move!\nCant't move into your own Den!");
                                }
                                else
                                {
                                    // if not a water then go...
                                    movePieceTo(xButton, yButton);
                                }
                            }

                            // for jump
                            else if (xButton == xPieceChosen || yButton == yPieceChosen)
                            {
                                // check wheter selected animal can jump
                                if (gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.CanJump)
                                {
                                    // jump vertical  (Y=2 to Y=6) / (Y=6 to Y=2)
                                    if ((xPieceChosen == 1 || xPieceChosen == 2 || xPieceChosen == 4 || xPieceChosen == 5) && ((yPieceChosen == 2 && yButton == 6) || (yPieceChosen == 6 && yButton == 2)))
                                    {
                                        bool foundMouse = false;
                                        //check if there is no mouse at the river ( VERTICALLY )
                                        for (int i = 3; i < 6; i++)
                                        {
                                            if (gBoard.mapBoard[xPieceChosen, i].CurrentPiece != null ) 
                                            {
                                                foundMouse = true; // mouse is found
                                            }
                                        }

                                        // check sebelah air //not use
                                        // if (gBoard.mapBoard[xPieceChosen + 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen + 1].IsWater || gBoard.mapBoard[xPieceChosen - 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen - 1].IsWater)

                                        if (!foundMouse)
                                        {
                                            movePieceTo(xButton, yButton); // animal jump
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid Move!\nThere is mouse in the river!");
                                        }
                                    }

                                    // jump horizontal
                                    else if ((yPieceChosen > 2 && yPieceChosen < 6))
                                    {
                                        bool foundMouse = false;

                                        // (X=0 to X=3) or (X=3 to X=0)
                                        if ((xPieceChosen == 0 && xButton == 3) || (xPieceChosen == 3 && xButton == 0)) // crossing the left river
                                        {
                                            // check if there is no mouse at the river ( HORIZONTALLY )
                                            for (int j = 1; j <=2; j++)
                                            {
                                                if (gBoard.mapBoard[j, yPieceChosen].CurrentPiece != null)
                                                {
                                                    foundMouse = true; // mouse is found
                                                }
                                            }
                                        }
                                        // (X=3 to X=6) or (X=6 to X=3) 
                                        else if ((xPieceChosen == 3 && xButton == 6) || (xPieceChosen == 6 && xButton == 3)) // crossing the right river
                                        {
                                            // check if there is no mouse at the river ( HORIZONTALLY )
                                            for (int j = 1; j <= 2; j++)
                                            {
                                                if (gBoard.mapBoard[j, yPieceChosen].CurrentPiece != null)
                                                {
                                                    foundMouse = true; // mouse is found
                                                }
                                            }
                                        }

                                        //check sebelah air //not use
                                        //if (gBoard.mapBoard[xPieceChosen + 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen + 1].IsWater || gBoard.mapBoard[xPieceChosen - 1, yPieceChosen].IsWater || gBoard.mapBoard[xPieceChosen, yPieceChosen - 1].IsWater)
                                        
                                        if (!foundMouse)
                                        {
                                            movePieceTo(xButton, yButton); // animal jump
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid Move!");
                                        }
                                    }
                                }
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        
                        //move to slot with enemy
                        else
                        {
                            // move 1 step
                            if (Math.Abs(xButton - xPieceChosen) + Math.Abs(yButton - yPieceChosen) == 1)
                            {
                                // declare the strength and name
                                int currPlayerStrenght = gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.Strength;
                                int targetStrenght = gBoard.mapBoard[xButton, yButton].CurrentPiece.Strength;
                                string currPlayerName = gBoard.mapBoard[xPieceChosen, yPieceChosen].CurrentPiece.Name;
                                string targetName = gBoard.mapBoard[xButton, yButton].CurrentPiece.Name;
                                currPlayerName = currPlayerName.Substring(0, currPlayerName.IndexOf("\n"));
                                targetName = targetName.Substring(0, targetName.IndexOf("\n"));
                                                                
                                // check wether the target location is enemy's animalpiece 
                                if (gBoard.mapBoard[xButton, yButton].CurrentPiece.Player != 1)
                                {
                                    //declare the strenght of current player piece and the targeted piece

                                    if (targetStrenght == 0)
                                    {
                                        //kill the enemy inside trap
                                        movePieceTo(xButton, yButton);
                                        MessageBox.Show(currPlayerName + " (player 1)\n CAPTURED \n" + targetName + " (player 2)");
                                    }
                                    else if ( (yPieceChosen>2 && yPieceChosen<6) && ( xPieceChosen==1 || xPieceChosen == 2 || xPieceChosen == 4 || xPieceChosen == 5 ) )
                                    {
                                        //check the mouse position inside the river
                                        MessageBox.Show("Mouse cannot attack from the river!");
                                    }
                                    else if (currPlayerStrenght == 1 && targetStrenght == 8)
                                    {
                                        //mouse kill elephant
                                        //animal move to the targeted location and kill enemy's animalpiece
                                        movePieceTo(xButton, yButton);
                                        MessageBox.Show(currPlayerName + " (player 1)\n CAPTURED \n" + targetName + " (player 2)");
                                    }
                                    else if (currPlayerStrenght == 8 && targetStrenght == 1)
                                    {
                                        //elephant cannot kill mouse
                                        MessageBox.Show("ELEPHANT CAN NOT CAPTURE MOUSE.\nRUN FOR YOUR LIFE !");
                                    }
                                    else if (currPlayerStrenght >= targetStrenght)
                                    {
                                        //animal move to the targeted location and kill enemy's animalpiece
                                        movePieceTo(xButton, yButton);
                                        MessageBox.Show(currPlayerName + " (player 1)\nCAPTURED \n" + targetName + " (player 2)");
                                    }
                                    else
                                    {
                                        //not a valid move because current player strenght is lower than enemy strenght
                                        MessageBox.Show("Invalid Move! Your strength is lower than enemy's!");
                                    }
                                }
                                else
                                {
                                    //not a valid move because targeted piece is another player's piece
                                    MessageBox.Show("Invalid Move. Targeted piece is yours!");
                                }
                                

                            }
                            //attack with jump 
                            else if (xButton == xPieceChosen || yButton == yPieceChosen)
                            {



                            }


                        }
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
