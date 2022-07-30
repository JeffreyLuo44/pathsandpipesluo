using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    //Name: Jeffrey Luo
    //ID: 1535901

    /// <summary>
    /// The main GUI for the Pipes and Paths game which includes smart computer and vsHuman checkboxes,
    /// with start and reset buttons. Pictureboxes are interactive when the game starts
    /// </summary>
    public partial class Form : System.Windows.Forms.Form
    {
        //Creates a new gameboard object
        GameBoard gameBoard = new GameBoard();
        //Initialises the player 1 object which is used all the time in a game by the user
        Player player1;
        //Initialises the player 2 object which is optional
        Player player2;
        //Initialises the computer player object which is optional
        ComputerPlayer computerPlayer;
        //Selected cards by each player
        Card currentP1 = null;
        Card currentP2 = null;
        //Whether or not a smart computer has been selected
        bool smartComputer = false;
        //Whether or not two players has been selected 
        bool vsHuman = false;
        //Whether or not player 1 is using paths cards and region
        bool player1IsPaths = false;
        //Whether or not it is player 1's turn
        bool player1Turn = false;
        //Number of moves a player has done in turn
        int player1MoveInTurn = 0;
        int player2MoveInTurn = 0;
        int computerPlayerMoveInTurn = 0;
        //Initial block width and height size
        int blockWidthSize = Constants.CARD_SIZE;
        int blockHeightSize = Constants.CARD_SIZE;
        public Form()
        {
            InitializeComponent();
            InitialiseGame();
        }

        /// <summary>
        /// Sets every variable back to its default value which is before starting a game
        /// </summary>
        private void InitialiseGame()
        {
            player1 = null;
            player2 = null;
            computerPlayer = null;
            gameBoard = new GameBoard();
            player1MoveInTurn = 0;
            player2MoveInTurn = 0;
            pictureBoxTopInterface.Refresh();
            pictureBoxGameBoard.Refresh();
            pictureBoxBottomInterface.Refresh();
            pictureBoxTopInterface.BackColor = DefaultBackColor;
            pictureBoxBottomInterface.BackColor = DefaultBackColor;
            //So the user cannot interact with the pictureboxes before a game has started
            pictureBoxTopInterface.Enabled = false;
            pictureBoxGameBoard.Enabled = false;
            pictureBoxBottomInterface.Enabled = false;
            checkBoxSmartComputerPlayer.Enabled = true;
            checkBoxVsHuman.Enabled = true;
            buttonStart.Enabled = true;
            buttonReset.Enabled = false;
        }

        /// <summary>
        /// Change the back colour of the picturebox interfaces depending on whether it is player 1 or player 2's turn.
        /// If only player 1 is playing with the computer, then the back colour will remain on the picturebox interface of player 1.
        /// </summary>
        private void ChangeBackColour()
        {
            if (player1Turn == true)
            {
                //Depends on whether player 1 is paths or not
                if (player1IsPaths == true)
                {
                    pictureBoxTopInterface.BackColor = Color.CadetBlue;
                    pictureBoxBottomInterface.BackColor = DefaultBackColor;
                }
                else
                {
                    pictureBoxBottomInterface.BackColor = Color.CadetBlue;
                    pictureBoxTopInterface.BackColor = DefaultBackColor;
                }
            }
            //Player 2 turn is true
            else
            {
                //Depends on whether player 1 is paths or not
                if (player1IsPaths == true)
                {
                    pictureBoxBottomInterface.BackColor = Color.CadetBlue;
                    pictureBoxTopInterface.BackColor = DefaultBackColor;
                }
                else
                {
                    pictureBoxTopInterface.BackColor = Color.CadetBlue;
                    pictureBoxBottomInterface.BackColor = DefaultBackColor;
                }
            }

            player1.RefreshInterface();
            //To avoid player 2 being null if player 1 is playing with the computer
            if (vsHuman == true)
            {
                player2.RefreshInterface();
            }
        }

        /// <summary>
        /// Clicking the gameboard determines whether moves have played by human players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxGameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            //Player 2's turn
            if (player1MoveInTurn == 2 && vsHuman == true)
            {
                player2MoveInTurn = player2.PlayMove(gameBoard, currentP2, e.X, e.Y);
                currentP2 = null;
                //Resets game automatically because of win/loss
                if (player2MoveInTurn == -1)
                {
                    InitialiseGame();
                    MessageBox.Show("Select playing options and press 'start' for a new game.");
                }
                //Change so it is player 1's turn
                if (player2MoveInTurn == 2)
                {
                    player1MoveInTurn = 0;
                    player2MoveInTurn = 0;
                    player1Turn = true;
                    ChangeBackColour();
                }
                
            }
            else
            {
                //Player 1's turn
                player1MoveInTurn = player1.PlayMove(gameBoard, currentP1, e.X, e.Y);
                currentP1 = null;
                //Resets game automatically because of win/loss
                if (player1MoveInTurn == -1)
                {
                    InitialiseGame();
                    MessageBox.Show("Select playing options and press 'start' for a new game.");
                }
                //Computer player's turn
                if (player1MoveInTurn == 2 && vsHuman == false)
                {
                    //Getting the moves in turn is only used to detect whether the method has returned -1 which means to reset the game
                    computerPlayerMoveInTurn = computerPlayer.PlayMove(player1.CardsPlayed, gameBoard);
                    player1.CardsPlayed.Clear();
                    //Resets game automatically because of win/loss
                    if (computerPlayerMoveInTurn == -1)
                    {
                        InitialiseGame();
                        MessageBox.Show("Select playing options and press 'start' for a new game.");
                    }
                    //Change so it is player 1's turn
                    player1MoveInTurn = 0;
                }
                //Change so it is player 2's turn
                if (player1MoveInTurn == 2 && vsHuman == true)
                {
                    player1Turn = false;
                    ChangeBackColour();
                }
            }
        }

        /// <summary>
        /// When game starts, appropriate controls are disabled and reset is enabled
        /// </summary>
        private void DisableOptionControls()
        {
            checkBoxSmartComputerPlayer.Enabled = false;
            checkBoxVsHuman.Enabled = false;
            buttonStart.Enabled = false;
            buttonReset.Enabled = true;
        }

        /// <summary>
        /// Gets the game setting values from the controls and starts a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            pictureBoxTopInterface.Enabled = true;
            pictureBoxGameBoard.Enabled = true;
            pictureBoxBottomInterface.Enabled = true;

            //Sets up gameboard display
            Graphics boardDisplay = pictureBoxGameBoard.CreateGraphics();
            gameBoard.BlockWidthSize = blockWidthSize;
            gameBoard.BlockHeightSize = blockHeightSize;
            gameBoard.DrawBoard(boardDisplay);

            smartComputer = checkBoxSmartComputerPlayer.Checked;
            vsHuman = checkBoxVsHuman.Checked;

            //If player 1 is paths
            if (comboBoxSide.Text == "Paths")
            {
                player1IsPaths = true;
                player1Turn = true;
                player1 = new Player(pictureBoxTopInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Path);
                player1.SetUpPlayer(gameBoard);
                //Versing computer (not player 2)
                if (vsHuman == false)
                {
                    if (smartComputer == true)
                    {
                        computerPlayer = new SmartComputerPlayer(pictureBoxBottomInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Pipe);
                    }
                    else
                    {
                        computerPlayer = new EasyComputerPlayer(pictureBoxBottomInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Pipe);
                    }
                    computerPlayer.SetUpComputerPlayer(gameBoard);
                }
                //Versing player 2
                else
                {
                    player2 = new Player(pictureBoxBottomInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Pipe);
                    player2.SetUpPlayer(gameBoard);
                }
                DisableOptionControls();
                ChangeBackColour();
            }

            //If player 2 is pipes
            else if (comboBoxSide.Text == "Pipes")
            {
                player1IsPaths = false;
                player1Turn = true;
                player1 = new Player(pictureBoxBottomInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Pipe);
                player1.SetUpPlayer(gameBoard);
                //Versing computer (not player 2)
                if (vsHuman == false)
                {
                    if (smartComputer == true)
                    {
                        computerPlayer = new SmartComputerPlayer(pictureBoxTopInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Path);
                    }
                    else
                    {
                        computerPlayer = new EasyComputerPlayer(pictureBoxTopInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Path);
                    }
                    computerPlayer.SetUpComputerPlayer(gameBoard);
                }
                //Versing player 2
                else
                {
                    player2 = new Player(pictureBoxTopInterface, pictureBoxGameBoard, Constants.CARD_TYPE.Path);
                    player2.SetUpPlayer(gameBoard);
                }
                DisableOptionControls();
                ChangeBackColour();
            }
            //Not valid
            else
            {
                InitialiseGame();
                MessageBox.Show("Player 1, please choose a valid side!");
            }

            //Chosen to play with smart computer and human
            if (smartComputer == true && vsHuman == true)
            {
                InitialiseGame();
                MessageBox.Show("Please choose either one but not both");
            }
        }

        /// <summary>
        /// For the human player selecting their cards drawn on the top interface (paths)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxTopInterface_MouseClick(object sender, MouseEventArgs e)
        {
            //Player 1 is paths and can use this interface
            if (player1IsPaths == true)
            {
                if (player1MoveInTurn != 2)
                {
                    //Deselects the selected card (free space in deselect square)
                    if (currentP1 != null)
                    {
                        currentP1.Selected = false;
                        currentP1 = null;
                    }
                    //Selects a card that to potentially place on gameboard
                    currentP1 = player1.ChangeCurrentSelected(currentP1, e.X, e.Y);
                }
                else
                {
                    MessageBox.Show("It is not your turn!");
                }
            }
            //Player 2 is paths and can use this interface
            else
            {
                if (vsHuman == true && player1MoveInTurn == 2)
                {
                    //Deselects the selected card (free space in deselect square)
                    if (currentP2 != null)
                    {
                        currentP2.Selected = false;
                        currentP2 = null;
                    }
                    //Selects a card that to potentially place on gameboard
                    currentP2 = player2.ChangeCurrentSelected(currentP2, e.X, e.Y);
                }
                else
                {
                    MessageBox.Show("It is not this player's turn yet!");
                }
            }
        }

        /// <summary>
        /// For the human player selecting their cards drawn on the bottom interface (pipes)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxBottomInterface_MouseClick(object sender, MouseEventArgs e)
        {
            //Player 1 is pipes and can use this interface
            if (player1IsPaths == false)
            {
                if (player1MoveInTurn != 2)
                {
                    //Deselects the selected card (free space in deselect square)
                    if (currentP1 != null)
                    {
                        currentP1.Selected = false;
                        currentP1 = null;
                    }
                    //Selects a card that to potentially place on gameboard
                    currentP1 = player1.ChangeCurrentSelected(currentP1, e.X, e.Y);
                }
                else
                {
                    MessageBox.Show("It is not your turn!");
                }
            }
            //Player 2 is pipes and can use this interface
            else
            {
                if (vsHuman == true && player1MoveInTurn == 2)
                {
                    //Deselects the selected card (free space in deselect square)
                    if (currentP2 != null)
                    {
                        currentP2.Selected = false;
                        currentP2 = null;
                    }
                    //Selects a card that to potentially place on gameboard
                    currentP2 = player2.ChangeCurrentSelected(currentP2, e.X, e.Y);
                }
                else
                {
                    MessageBox.Show("It is not this player's turn yet!");
                }
            }
        }

        /// <summary>
        /// To restart the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            InitialiseGame();
            MessageBox.Show("Select playing options and press 'start' for a new game.");
        }

        /// <summary>
        /// To change the gameboard display size when the form size has been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxGameBoard_SizeChanged(object sender, EventArgs e)
        {
            //Clears the gameboard and draws the board again
            pictureBoxGameBoard.Refresh();
            Graphics boardDisplay = pictureBoxGameBoard.CreateGraphics();
            gameBoard.DrawBoard(boardDisplay);
            //Calculates the new block width and height size based on a proportion of the new gameboard picturebox dimensions
            blockWidthSize = Constants.CARD_SIZE + (pictureBoxGameBoard.Width - pictureBoxGameBoard.MinimumSize.Width) / Constants.BLOCK_COLUMN_NUM;
            blockHeightSize = Constants.CARD_SIZE + (pictureBoxGameBoard.Height - pictureBoxGameBoard.MinimumSize.Height) / Constants.BLOCK_ROW_NUM;
            gameBoard.BlockWidthSize = blockWidthSize;
            gameBoard.BlockHeightSize = blockHeightSize;
            //Draws all blocks on gameboard which are placed cards
            gameBoard.RedrawBlocksOnGameBoard(boardDisplay);
        }
    }
}
