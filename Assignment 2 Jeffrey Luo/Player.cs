using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    class Player
    {
        //The move number in the player's turn
        protected int _playerMovesInTurn;

        //The picturebox and graphics for the player
        protected PictureBox _playerDisplay;
        protected Graphics _playerInterface;
        protected Graphics _boardInterface;

        //Player's deck object
        protected Deck _deckPlayer;
        //Player's deck card type
        protected Constants.CARD_TYPE _deckCardType;
        //Player's hand object
        protected Hand _playerHand;

        //Player's boulder card object
        protected BoulderCard _boulderCard;
        //Whether or not the boulder card has been used
        bool _boulderCardUsed = false;
        //Player's list of obstacle card objects
        protected List<ObstacleCard> _obstacleCards;

        //Player's list of cards played in turn
        protected List<BlockOnBoard> _cardsPlayed;
        /// <summary>
        /// The player class for player 1 or 2 which holds the player's cardtype, obstacle cards, boulder card, cards played, 
        /// own deck of gamecards & own hand of gamecards & boulder card. Also, references the player display and gameboard display
        /// </summary>
        /// <param name="playerDisplay">The interface the player is controlling</param>
        /// <param name="boardDisplay">The referenced gameboard display</param>
        /// <param name="deckCardType">Can be either pipes or paths</param>
        public Player(PictureBox playerDisplay, PictureBox boardDisplay, Constants.CARD_TYPE deckCardType)
        {
            //For refreshing player interface
            _playerDisplay = playerDisplay;
            //Creates new graphics for player interface and board interface
            _playerInterface = playerDisplay.CreateGraphics();
            _boardInterface = boardDisplay.CreateGraphics();

            _deckPlayer = new Deck(deckCardType);
            _deckCardType = deckCardType;
            _playerHand = new Hand();

            _boulderCard = new BoulderCard(PlayerCardType, -1);
            _obstacleCards = new List<ObstacleCard>();

            _cardsPlayed = new List<BlockOnBoard>();
        }

        /// <summary>
        /// Can be either pipes or paths
        /// </summary>
        public Constants.CARD_TYPE PlayerCardType
        {
            get { return _deckCardType; }
        }

        /// <summary>
        /// List of player's obstacle cards not used
        /// </summary>
        public List<ObstacleCard> ObstacleCards
        {
            get { return _obstacleCards; }
        }

        /// <summary>
        /// Cards played in a turn
        /// </summary>
        public List<BlockOnBoard> CardsPlayed
        {
            get { return _cardsPlayed; }
            set { _cardsPlayed = value; }
        }

        /// <summary>
        /// Sets up the player's starting objects
        /// </summary>
        /// <param name="gameBoard">The gameboard display</param>
        public void SetUpPlayer(GameBoard gameBoard)
        {
            int initialRowPos = 0;
            int initialColPos = Constants.BLOCK_COLUMN_NUM / 2;

            _deckPlayer.SetupDeck();
            _playerHand.UpdateHand(_deckPlayer);

            //Creates the obstacle cards for player
            for (int i = 0; i < Constants.OBSTACLE_NUM; i++)
            {
                ObstacleCard newObstacleCard = new ObstacleCard(PlayerCardType, -1);
                _obstacleCards.Add(newObstacleCard);
            }
 
            //Row based on whether player cardtype is path or pipe
            if (PlayerCardType == Constants.CARD_TYPE.Path)
            {
                initialRowPos = Constants.BLOCK_ROW_NUM / 2 - 1;
            }
            else
            {
                initialRowPos = Constants.BLOCK_ROW_NUM / 2;
            }

            //Overrides the placeholder blocks to put initial starting blocks with starting cards referenced, and uses the last card in the player's hand
            gameBoard.blocks[initialRowPos, initialColPos]
                = new BlockOnBoard(initialRowPos, initialColPos, _playerHand.HandCards[_playerHand.HandCards.Count - 1], false);
            gameBoard.blocks[initialRowPos, initialColPos].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);
            _playerHand.HandCards.RemoveAt(_playerHand.HandCards.Count - 1);
            _cardsPlayed.Add(gameBoard.blocks[initialRowPos, initialColPos]);

            RefreshInterface();
        }

        /// <summary>
        /// Checks if the player's move is valid where it is either removing one countdown token or placing a card to be a new block on board
        /// </summary>
        /// <param name="gameBoard">The referenced gameboard interface</param>
        /// <param name="current">The current card selected</param>
        /// <param name="x">x position of mouse click</param>
        /// <param name="y">y position of mouse click</param>
        /// <returns>The player's moves in turn</returns>
        public int PlayMove(GameBoard gameBoard, Card current, int x, int y)
        {
            int finishRow = 0;

            //If player has moved twice from last turn, reset moves in turn to 0.
            if (_playerMovesInTurn == 2)
            {
                _cardsPlayed.Clear();
                _playerMovesInTurn = 0;
            }

            if (current != null)
            {
                current.Selected = false;
            }

            //Get blocks array position of clicked square
            (int rowPos, int colPos) = gameBoard.GetArrayPositionOfClickedSquare(x, y);
            if (rowPos == -1 && colPos == -1)
            {
                MessageBox.Show("You clicked in an invalid position. Please try again.");
                return _playerMovesInTurn;
            }

            //If no card is selected and a block with an obstacle card referenced which is not the player's type is clicked on,
            //remove a countdown token from the obstacle card referenced
            if (current == null)
            {
                if (gameBoard.blocks[rowPos, colPos].Card is ObstacleCard && gameBoard.blocks[rowPos, colPos].Type != PlayerCardType)
                {
                    int tokenCount = 0;
                    tokenCount = (int)gameBoard.blocks[rowPos, colPos].Card.GetType().GetMethod("CountDownAToken").Invoke(gameBoard.blocks[rowPos, colPos].Card, null);
                    //Block's referenced card becomes 0 if there are no tokens left
                    if (tokenCount == 0)
                    {
                        gameBoard.blocks[rowPos, colPos].Obstacle = false;
                        gameBoard.blocks[rowPos, colPos].Card.Number = 0;
                    }
                    gameBoard.blocks[rowPos, colPos].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);
                    _playerMovesInTurn += 1;
                }
                else
                {
                    MessageBox.Show(PlayerCardType + "'s player, please select a card you would like to place or obstacle token you would like to remove.");
                    return _playerMovesInTurn;
                }
            }

            //If there is a card selected and the block position is valid, create a new block on board object at that position according to the card referenced
            if (current != null && gameBoard.ValidateClickedSquare(current, rowPos, colPos, true))
            {
                current.Selected = false;
                if (current == _boulderCard)
                {
                    gameBoard.blocks[rowPos, colPos] = new BlockOnBoard(rowPos, colPos, current, true);
                    _boulderCardUsed = true;
                }
                else if (current is ObstacleCard)
                {
                    gameBoard.blocks[rowPos, colPos] = new BlockOnBoard(rowPos, colPos, current, true);
                    _obstacleCards.Remove((ObstacleCard)current);
                }
                else
                {
                    gameBoard.blocks[rowPos, colPos] = new BlockOnBoard(rowPos, colPos, current, false);
                    _playerHand.HandCards.Remove((GameCard)current);
                    _cardsPlayed.Add(gameBoard.blocks[rowPos, colPos]);
                }
                gameBoard.blocks[rowPos, colPos].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);

                _playerMovesInTurn += 1;

                //Checks if placing this new block on board creates a winning path which is a 
                //finishing row back to the middle edge of the player's region
                if (PlayerCardType == Constants.CARD_TYPE.Path)
                {
                    finishRow = Constants.BLOCK_ROW_NUM - 1;
                }

                //Checks all the obstacle and placeholder cards
                gameBoard.DefaultBlocksToCheck();

                //Checks each column for a block of the player's type to check a valid path for
                for (int colNum = 0; colNum < Constants.BLOCK_COLUMN_NUM; colNum++)
                {
                    if (gameBoard.blocks[finishRow, colNum].Type == PlayerCardType && gameBoard.blocks[finishRow, colNum].Obstacle == false)
                    { 
                        //Player wins if the valid path is true and returns back -1 to restart game
                        if (gameBoard.CheckValidPath(PlayerCardType, finishRow, colNum))
                        {
                            //Refresh so the last played card is seen to be gone from hand
                            RefreshInterface();
                            MessageBox.Show("You win, " + PlayerCardType.ToString() + " Player!");
                            _playerMovesInTurn = -1;
                            return _playerMovesInTurn;
                        }
                    }
                }
            }

            //Update hand after the player's turn ends 
            if (_playerMovesInTurn == 2)
            {
                _playerHand.UpdateHand(_deckPlayer);
            }
           
            RefreshInterface();

            //If the player runs out of cards, the game ends
            if (_playerHand.HandCards.Count == 0)
            {
                MessageBox.Show(PlayerCardType + "'s player, you have run out of cards to play. You lose!");
                _playerMovesInTurn = -1;
            }

            return _playerMovesInTurn;
        }

        /// <summary>
        /// Redraws everything on the player's interface
        /// </summary>
        public void RefreshInterface()
        {
            _playerDisplay.Refresh();
            _playerHand.DrawCardsInHand(_playerInterface);

            //Draws a "free" de-select square
            Rectangle rect = new Rectangle(Constants.DESELECT_SQUARE_X_POSITION, Constants.CARD_GAP, Constants.CARD_SIZE, Constants.CARD_SIZE);
            _playerInterface.DrawRectangle(Constants.BORDER_PENS[2], rect);
            _playerInterface.FillRectangle(Constants.DESELECT_BRUSH, rect);
            _playerInterface.DrawString("De- select", Constants.TEXT_FONT, Constants.TEXT_BRUSH, rect);

            if (_boulderCardUsed == false)
            {
                _boulderCard.Draw(_playerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
            }

            //Draws the obstacle cards separated by a gap
            if (ObstacleCards.Count!=0)
            {
                int x = Constants.OBSTACLE_X_POSITION;
                foreach (ObstacleCard obstacleCard in ObstacleCards)
                {
                    obstacleCard.XPos = x;
                    obstacleCard.Draw(_playerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
                    x += Constants.CARD_SIZE + Constants.CARD_GAP;
                }
            }
        }

        /// <summary>
        /// Checking all cards if a card has been clicked on, redrawing the border where necessary
        /// </summary>
        /// <param name="current">The current card selected</param>
        /// <param name="x">x position of mouse click</param>
        /// <param name="y">y position of mouse click</param>
        /// <returns>The new current selected card</returns>
        public Card ChangeCurrentSelected(Card current, int x, int y)
        {
            //If the mouse click position is on a card, the card is selected and its border is redrawn

            foreach (Card c in _playerHand.HandCards)
            {
                if (c.IsMouseOn(x, y))
                {
                    current = c;
                    current.Selected = true;
                    c.Draw(_playerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
                    break;
                }
            }

            if (_boulderCardUsed == false)
            {
                if (_boulderCard.IsMouseOn(x, y))
                {
                    current = _boulderCard;
                    current.Selected = true;
                    _boulderCard.Draw(_playerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
                }
            }

            foreach (Card c in ObstacleCards)
            {
                if (c.IsMouseOn(x, y))
                {
                    current = c;
                    current.Selected = true;
                    c.Draw(_playerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
                    break;
                }
            }

            //Non-selected cards' border to be redrawn
            _playerHand.DrawCardsInHand(_playerInterface);
            if (_boulderCardUsed == false)
            {
                _boulderCard.Draw(_playerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
            }
            foreach (Card c in ObstacleCards)
            {
                c.Draw(_playerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
            }

            //Draws a "free" de-select square
            Rectangle rect = new Rectangle(Constants.DESELECT_SQUARE_X_POSITION, Constants.CARD_GAP, Constants.CARD_SIZE, Constants.CARD_SIZE);
            _playerInterface.DrawRectangle(Constants.BORDER_PENS[2], rect);
            _playerInterface.FillRectangle(Constants.DESELECT_BRUSH, rect);
            _playerInterface.DrawString("De- select", Constants.TEXT_FONT, Constants.TEXT_BRUSH, rect);

            return current;
        }
    }
}
