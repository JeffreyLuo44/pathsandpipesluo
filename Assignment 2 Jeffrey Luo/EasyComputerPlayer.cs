using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    class EasyComputerPlayer : ComputerPlayer
    {
        //Random number generator object
        Random _random;
        //List of all blocks on board
        List<BlockOnBoard> _allBlocksOnBoard;
        /// <summary>
        /// For the human player to play against the easy computer player's cards and moves
        /// </summary>
        /// <param name="computerDisplay">The interface the easy computer is controlling</param>
        /// <param name="boardDisplay">The referenced gameboard display</param>
        /// <param name="cardType">Can be either pipes or path</param>
        public EasyComputerPlayer(PictureBox computerDisplay, PictureBox boardDisplay, Constants.CARD_TYPE cardType):base(computerDisplay, boardDisplay, cardType)
        {
            _random = new Random();
            _allBlocksOnBoard = new List<BlockOnBoard>();
        }
        
        /// <summary>
        /// Essentially loops until there is a random validly placed card next to a random block on board (card subclass based on set probability)
        /// Does both moves in turn during one call of this method
        /// </summary>
        /// <param name="p1CardsPlayed">Gets the last played p1 cards from p1's last turn. Note: the easy computer class does NOT use them</param>
        /// <param name="gameBoard">>The referenced gameboard interface</param>
        /// <returns>The computer's moves in turn which would be either 2 or -1. -1 would reset the game</returns>
        public override int PlayMove(List<BlockOnBoard> p1CardsPlayed, GameBoard gameBoard)
        {
            int finishRow = 0;

            for (int i = 0; i < Constants.BLOCK_ROW_NUM; i++)
            {
                for (int j = 0; j < Constants.BLOCK_COLUMN_NUM; j++)
                {
                    if (gameBoard.blocks[i, j].Placeholder == false && gameBoard.blocks[i, j].Obstacle == false)
                    {
                        _allBlocksOnBoard.Add(gameBoard.blocks[i, j]);
                    }
                }
            }

            //Repeats this move twice for a turn
            for (int move = 0; move < 2; move++)
            {
                bool countDownTokenRemoved = false;
                bool cardIsSelected = false;
                int randomCardSelectedIndicator = 0;
                Card cardSelected = null;
                bool cardPlaced = false;
                int randomBlockOnBoardIndex = 0;
                int adjacentRowShift = 0;
                int adjacentColShift = 0;
                int rowSelected = 0;
                int colSelected = 0;

                //If computer player has moved twice (from last turn), reset moves in turn to 0
                if (_computerMovesInTurn == 2)
                {
                    _computerMovesInTurn = 0;
                }

                //Loop until a card is placed validly
                while (cardPlaced == false)
                {
                    randomCardSelectedIndicator = _random.Next(0, 10);
                    //Chooses random gamecard from computer hand to select
                    if (randomCardSelectedIndicator >= 0 && randomCardSelectedIndicator <= 5)
                    {
                        if (_computerHand.HandCards.Count != 0)
                        {
                            cardSelected = _computerHand.HandCards[_random.Next(0, _computerHand.HandCards.Count)];
                            cardIsSelected = true;
                        }
                        else
                        {
                            MessageBox.Show("I have ran out of cards. You win!");
                            _computerMovesInTurn = -1;
                            return _computerMovesInTurn;
                        }
                    }
                    //Selects the boulder card
                    else if (randomCardSelectedIndicator == 6)
                    {
                        if (_boulderCardUsed == false)
                        {
                            cardSelected = _boulderCard;
                            cardIsSelected = true;
                        }
                    }
                    //Selects an obstacle card
                    else if (randomCardSelectedIndicator == 7)
                    {
                        if (ObstacleCards.Count != 0)
                        {
                            cardSelected = ObstacleCards[0];
                            cardIsSelected = true;
                        }
                    }
                    //Remove a human's countdown token if present on board
                    else
                    {
                        for (int i = 0; i < Constants.BLOCK_ROW_NUM; i++)
                        {
                            for (int j = 0; j < Constants.BLOCK_COLUMN_NUM; j++)
                            {
                                if (gameBoard.blocks[i, j].Card is ObstacleCard && gameBoard.blocks[i, j].Type != ComputerCardType && gameBoard.blocks[i, j].Obstacle == true)
                                {
                                    int tokenCount = 0;
                                    tokenCount = (int)gameBoard.blocks[i, j].Card.GetType().GetMethod("CountDownAToken").Invoke(gameBoard.blocks[i, j].Card, null);
                                    //Block's referenced card becomes 0 if there are no tokens left
                                    if (tokenCount == 0)
                                    {
                                        gameBoard.blocks[i, j].Obstacle = false;
                                        gameBoard.blocks[i, j].Card.Number = 0;
                                    }
                                    gameBoard.blocks[i, j].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);
                                    _computerMovesInTurn += 1;
                                    countDownTokenRemoved = true;
                                    break;
                                }
                            }
                            if (countDownTokenRemoved == true)
                            {
                                break;
                            }
                        }
                    }

                    //Gets out of the card placed loop
                    if (countDownTokenRemoved == true)
                    {
                        break;
                    }

                    //If the random number was supposed to make a countdown token be removed but 
                    //there was no obstacle block found, then choose a random gamecard from its hand
                    if (cardIsSelected == false)
                    {
                        if (_computerHand.HandCards.Count != 0)
                        {
                            cardSelected = _computerHand.HandCards[_random.Next(0, _computerHand.HandCards.Count)];
                            //No need for "card is selected" bool value assigning here
                        }
                        else
                        {
                            MessageBox.Show("I have ran out of cards. You win!");
                            _computerMovesInTurn = -1;
                            return _computerMovesInTurn;
                        }
                    }

                    //Chooses random block on board
                    randomBlockOnBoardIndex = _random.Next(0, _allBlocksOnBoard.Count);
                    adjacentRowShift = _random.Next(-1, 2);
                    adjacentColShift = _random.Next(-1, 2);
                    //The candidate row and column for a new block on board
                    rowSelected = _allBlocksOnBoard[randomBlockOnBoardIndex].RowPos + adjacentRowShift;
                    colSelected = _allBlocksOnBoard[randomBlockOnBoardIndex].ColPos + adjacentColShift;

                    //Adjusts the rows and/or columns if out of array boundaries
                    if (rowSelected > Constants.BLOCK_ROW_NUM - 1)
                    {
                        rowSelected = Constants.BLOCK_ROW_NUM - 1;
                    }

                    if (rowSelected < 0)
                    {
                        rowSelected = 0;
                    }

                    if (colSelected > Constants.BLOCK_COLUMN_NUM - 1)
                    {
                        colSelected = Constants.BLOCK_COLUMN_NUM - 1;
                    }

                    if (colSelected < 0)
                    {
                        colSelected = 0;
                    }

                    //If the block position is valid, create a new block on board object at that position according to the card referenced
                    if (gameBoard.ValidateClickedSquare(cardSelected, rowSelected, colSelected, false))
                    {
                        if (cardSelected == _boulderCard)
                        {
                            gameBoard.blocks[rowSelected, colSelected] = new BlockOnBoard(rowSelected, colSelected, cardSelected, true);
                            _boulderCardUsed = true;
                        }
                        else if (cardSelected is ObstacleCard)
                        {
                            gameBoard.blocks[rowSelected, colSelected] = new BlockOnBoard(rowSelected, colSelected, cardSelected, true);
                            ObstacleCards.Remove((ObstacleCard)cardSelected);
                        }
                        else
                        {
                            gameBoard.blocks[rowSelected, colSelected] = new BlockOnBoard(rowSelected, colSelected, cardSelected, false);
                            _computerHand.HandCards.Remove((GameCard)cardSelected);
                        }
                        _allBlocksOnBoard.Add(gameBoard.blocks[rowSelected, colSelected]);
                        gameBoard.blocks[rowSelected, colSelected].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);
                        cardPlaced = true;

                        _computerMovesInTurn += 1;

                        //Checks if placing this new block on board creates a winning path which is a 
                        //finishing row back to the middle edge of the player's region
                        if (ComputerCardType == Constants.CARD_TYPE.Path)
                        {
                            finishRow = Constants.BLOCK_ROW_NUM - 1;
                        }

                        //Checks all the obstacle and placeholder cards
                        gameBoard.DefaultBlocksToCheck();

                        //Checks each column for a block of the player's type to check a valid path for
                        for (int colNum = 0; colNum < Constants.BLOCK_COLUMN_NUM; colNum++)
                        {
                            if (gameBoard.blocks[finishRow, colNum].Type == ComputerCardType && gameBoard.blocks[finishRow, colNum].Obstacle == false)
                            {
                                if (gameBoard.CheckValidPath(ComputerCardType, finishRow, colNum))
                                {
                                    //Refresh so the last played card is seen to be gone from hand
                                    RefreshInterface();
                                    MessageBox.Show("You win, " + ComputerCardType.ToString() + " Computer Player!");
                                    _computerMovesInTurn = -1;
                                    return _computerMovesInTurn;
                                }
                            }
                        }
                    }
                }

                //If the computer player has moved twice (finished turn), then update hand
                if (_computerMovesInTurn == 2)
                {
                    _computerHand.UpdateHand(_deckComputerPlayer);
                }

                RefreshInterface();
            }
            return _computerMovesInTurn;
        }
    }
}
