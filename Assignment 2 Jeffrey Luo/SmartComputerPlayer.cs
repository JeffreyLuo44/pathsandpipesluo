using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    class SmartComputerPlayer : ComputerPlayer
    {
        //Random number generator object
        Random _random;
        //Whether or not the human player's immediately played cards (in their previous turn) can be overriden
        bool _cannotOverrideAnyImmediateCards;
        //List of all computer blocks on board
        List<BlockOnBoard> _computerBlocksOnBoard;
        //List of all blocks on board
        List<BlockOnBoard> _allBlocksOnBoard;
        /// <summary>
        /// For the human player to play against the smart computer player's cards and moves
        /// </summary>
        /// <param name="computerDisplay">The interface the smart computer is controlling</param>
        /// <param name="boardDisplay">The referenced gameboard display</param>
        /// <param name="cardType">Can be either pipes or path</param>
        public SmartComputerPlayer(PictureBox computerDisplay, PictureBox boardDisplay, Constants.CARD_TYPE cardType) : base(computerDisplay, boardDisplay, cardType)
        {
            _random = new Random();
            _cannotOverrideAnyImmediateCards = false;
            _computerBlocksOnBoard = new List<BlockOnBoard>();
            _allBlocksOnBoard = new List<BlockOnBoard>();
        }

        /// <summary>
        /// Essentially works on a few main priorities in the following order. In a move, 
        /// 0. 25% chance it will try remove one countdown token from a human player's obstacle block if present on gameboard as a move
        /// 1. The computer will try override the human player's immediately last played cards. Will not try again for the TURN if no card in hand is high enough
        /// 2. The computer will try find its block closest to its finish, and try play one random card closer. Will not try again for the MOVE if not valid to place
        /// 3. The computer will try place a random blocking card instead of a gamecard next to a random block on board
        /// 4. The computer will try place a random gamecard next to a random block on board
        /// Does both moves in turn during one call of this method and remembers the first card placed (new block on board) in a turn (one call) by adding it to the computer blocks list
        /// </summary>
        /// <param name="p1CardsPlayed">Gets the last played p1 cards from p1's last turn. Note: the easy computer class does NOT use them</param>
        /// <param name="gameBoard">>The referenced gameboard interface</param>
        /// <returns>The computer's moves in turn which would be either 2 or -1. -1 would reset the game</returns>
        public override int PlayMove(List<BlockOnBoard> p1CardsPlayed, GameBoard gameBoard)
        {
            int finishRow = 0;

            //Gets all the computer blocks on board and all blocks on board
            for (int i = 0; i < Constants.BLOCK_ROW_NUM; i++)
            {
                for (int j = 0; j < Constants.BLOCK_COLUMN_NUM; j++)
                {
                    if (gameBoard.blocks[i, j].Placeholder == false && gameBoard.blocks[i, j].Type == ComputerCardType && gameBoard.blocks[i, j].Obstacle == false)
                    {
                        _computerBlocksOnBoard.Add(gameBoard.blocks[i, j]);
                    }
                    if (gameBoard.blocks[i, j].Placeholder == false && gameBoard.blocks[i, j].Obstacle == false)
                    {
                        _allBlocksOnBoard.Add(gameBoard.blocks[i, j]);
                    }
                }
            }

            //Repeats this move twice for a turn
            for (int move = 0; move < 2; move++)
            {
                bool notValidToPlaceForDirectOffense = false;
                bool _cardPlaced = false;
                bool countDownTokenRemoved = false;
                bool cardIsSelected = false;
                int cardOrTokenRemovalSelectedIndicator = 0;
                Card cardSelected = null;
                int closestBlockRowToEnd = 0;
                int cardNumberDifference = 0;
                int selectedBlockOnBoardIndex = 0;
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
                while (_cardPlaced == false)
                {
                    //Chooses random gamecard from computer hand to select
                    cardOrTokenRemovalSelectedIndicator = _random.Next(0, 4);
                    if (cardOrTokenRemovalSelectedIndicator >= 0 && cardOrTokenRemovalSelectedIndicator <= 2)
                    {
                        if (_computerHand.HandCards.Count != 0)
                        {
                            cardSelected = _computerHand.HandCards[_random.Next(0,_computerHand.HandCards.Count)];
                            cardIsSelected = true;
                        }
                        else
                        {
                            MessageBox.Show("I have ran out of cards. You win!");
                            _computerMovesInTurn = -1;
                            return _computerMovesInTurn;
                        }
                    }
                    //25% chance to remove a human's countdown token if present on board
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

                    //Checks whether it is possible to override any of the human player's immediate played cards
                    if (_cannotOverrideAnyImmediateCards == false)
                    {
                        for (int j = 0; j < _computerHand.HandCards.Count; j++)
                        {
                            for (int i = 0; i < p1CardsPlayed.Count; i++)
                            {
                                if (_cardPlaced == true)
                                {
                                    break;
                                }
                                cardNumberDifference = 1;
                                //Limits the hand card to be only a maximum of 3 higher
                                while (cardNumberDifference < 4)
                                {
                                    if (_computerHand.HandCards[j].Number - p1CardsPlayed[i].Card.Number == cardNumberDifference)
                                    {
                                        if (gameBoard.ValidateClickedSquare(_computerHand.HandCards[j], p1CardsPlayed[i].RowPos, p1CardsPlayed[i].ColPos, false))
                                        {
                                            _cardPlaced = true;
                                            gameBoard.blocks[p1CardsPlayed[i].RowPos, p1CardsPlayed[i].ColPos] 
                                                = new BlockOnBoard(p1CardsPlayed[i].RowPos, p1CardsPlayed[i].ColPos, _computerHand.HandCards[j], false);
                                            _computerHand.HandCards.Remove(_computerHand.HandCards[j]);
                                            gameBoard.blocks[p1CardsPlayed[i].RowPos, p1CardsPlayed[i].ColPos].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);
                                            if (_computerMovesInTurn == 0)
                                            {
                                                _computerBlocksOnBoard.Add(gameBoard.blocks[p1CardsPlayed[i].RowPos, p1CardsPlayed[i].ColPos]);
                                            }
                                            p1CardsPlayed.Remove(p1CardsPlayed[i]);
                                            _computerMovesInTurn += 1;
                                            break;
                                        }
                                    }
                                    cardNumberDifference += 1;
                                }
                            }
                        }
                        if (_cardPlaced == true)
                        {
                            break;
                        }
                        //After checking if overriding any cards is possible (in first move), if did not override any, 
                        //then set bool value to true to skip checking in next move and save data processing
                        if (_cardPlaced == false)
                        {
                            _cannotOverrideAnyImmediateCards = true;
                        }
                    }

                    //Find its block closest to its finish, and try play a random card closer. Will not try again for the MOVE if not valid to place
                    if (notValidToPlaceForDirectOffense == false && _computerBlocksOnBoard.Count != 0 && cardSelected is GameCard)
                    {
                        closestBlockRowToEnd = _computerBlocksOnBoard[0].RowPos;
                        for (int i = 0; i < _computerBlocksOnBoard.Count; i++)
                        {
                            //Where to place gamecard depends on what card type the computer is and so, this determines the end the computer is trying to reach
                            if (ComputerCardType == Constants.CARD_TYPE.Path)
                            {
                                if (_computerBlocksOnBoard[i].RowPos > closestBlockRowToEnd)
                                {
                                    closestBlockRowToEnd = _computerBlocksOnBoard[i].RowPos;
                                    selectedBlockOnBoardIndex = _computerBlocksOnBoard.IndexOf(_computerBlocksOnBoard[i]);
                                    adjacentRowShift = 1;
                                }
                            }
                            else
                            {
                                if (_computerBlocksOnBoard[i].RowPos < closestBlockRowToEnd)
                                {
                                    closestBlockRowToEnd = _computerBlocksOnBoard[i].RowPos;
                                    selectedBlockOnBoardIndex = _computerBlocksOnBoard.IndexOf(_computerBlocksOnBoard[i]);
                                    adjacentRowShift = -1;
                                }
                            }
                        }
                        //The candidate row and column for a new block on board
                        rowSelected = _computerBlocksOnBoard[selectedBlockOnBoardIndex].RowPos + adjacentRowShift;
                        colSelected = _computerBlocksOnBoard[selectedBlockOnBoardIndex].ColPos + adjacentColShift;
                    }
                    else
                    {
                        //The computer will try place a random blocking card instead of a gamecard next to a random block on board
                        if (_boulderCardUsed == false)
                        {
                            cardSelected = _boulderCard;
                            cardIsSelected = true;
                        }
                        else if (ObstacleCards.Count != 0)
                        {
                            cardSelected = ObstacleCards[0];
                            cardIsSelected = true;
                        }

                        //Chooses random block on board
                        selectedBlockOnBoardIndex = _random.Next(0, _allBlocksOnBoard.Count);
                        adjacentRowShift = _random.Next(-1, 2);
                        adjacentColShift = _random.Next(-1, 2);
                        //The candidate row and column for a new block on board
                        rowSelected = _allBlocksOnBoard[selectedBlockOnBoardIndex].RowPos + adjacentRowShift;
                        colSelected = _allBlocksOnBoard[selectedBlockOnBoardIndex].ColPos + adjacentColShift;
                    }

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
                        gameBoard.blocks[rowSelected, colSelected].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);
                        _cardPlaced = true;
                        if (_computerMovesInTurn == 0 && gameBoard.blocks[rowSelected, colSelected].Obstacle == false)
                        {
                            _computerBlocksOnBoard.Add(gameBoard.blocks[rowSelected, colSelected]);
                        }

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
                    //Since the card is not valid to place, for its closest block to its finish, the computer will NOT try placing a random card closer for the MOVE
                    else
                    {
                         notValidToPlaceForDirectOffense = true;
                    }
                }

                //If the computer player has moved twice (finished turn), then update hand
                if (_computerMovesInTurn == 2)
                {
                    _computerHand.UpdateHand(_deckComputerPlayer);
                    _cannotOverrideAnyImmediateCards = false;
                }

                RefreshInterface();
            }
            return _computerMovesInTurn;
        }
    }
}
