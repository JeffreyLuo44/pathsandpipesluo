using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    abstract class ComputerPlayer
    {
        //The move number in the computer's turn
        protected int _computerMovesInTurn;

        //The picturebox and graphics for the computer player
        protected PictureBox _computerDisplay;
        protected Graphics _computerInterface;
        protected Graphics _boardInterface;

        //Computer's deck object
        protected Deck _deckComputerPlayer;
        //Computer's deck card type
        protected Constants.CARD_TYPE _deckCardType;
        //Computer's hand object
        protected Hand _computerHand;

        //Computer's boulder card object
        protected BoulderCard _boulderCard;
        //Whether or not the boulder card has been used
        protected bool _boulderCardUsed = false;
        //Computer's list of obstacle card objects
        protected List<ObstacleCard> _obstacleCards;
        /// <summary>
        /// The computer player class for the computer player which holds the player's cardtype, obstacle cards, boulder card, cards played, 
        /// own deck of gamecards & own hand of gamecards & boulder card. Also, references the computer display and gameboard display.
        /// </summary>
        /// <param name="computerDisplay">The interface the computer is controlling</param>
        /// <param name="boardDisplay">The referenced gameboard display</param>
        /// <param name="cardType">Can be either pipes or paths</param>
        public ComputerPlayer(PictureBox computerDisplay, PictureBox boardDisplay, Constants.CARD_TYPE cardType)
        {
            //For refreshing computer interface
            _computerDisplay = computerDisplay;
            //Creates new graphics for computer interface and board interface
            _computerInterface = computerDisplay.CreateGraphics();
            _boardInterface = boardDisplay.CreateGraphics();

            _deckComputerPlayer = new Deck(cardType);
            _deckCardType = cardType;
            _computerHand = new Hand();

            _boulderCard = new BoulderCard(ComputerCardType, -1);
            _obstacleCards = new List<ObstacleCard>();
        }

        /// <summary>
        /// List of player's obstacle cards not used
        /// </summary>
        public Constants.CARD_TYPE ComputerCardType
        {
            get { return _deckCardType; }
        }

        /// <summary>
        /// Cards played in a turn
        /// </summary>
        public List<ObstacleCard> ObstacleCards
        {
            get { return _obstacleCards; }
        }

        /// <summary>
        /// Sets up the player's starting objects
        /// </summary>
        /// <param name="gameBoard">The gameboard display</param>
        public virtual void SetUpComputerPlayer(GameBoard gameBoard)
        {
            int initialRowPos = Constants.BLOCK_ROW_NUM / 2;
            int initialColPos = Constants.BLOCK_COLUMN_NUM / 2;

            _deckComputerPlayer.SetupDeck();
            _computerHand.UpdateHand(_deckComputerPlayer);

            //Creates the obstacle cards for the computer player
            for (int i = 0; i < Constants.OBSTACLE_NUM; i++)
            {
                ObstacleCard newObstacleCard = new ObstacleCard(ComputerCardType, -1);
                _obstacleCards.Add(newObstacleCard);
            }

            //Row based on whether computer cardtype is path or pipe
            if (ComputerCardType == Constants.CARD_TYPE.Path)
            {
                initialRowPos = Constants.BLOCK_ROW_NUM / 2 - 1;
            }
            else
            {
                initialRowPos = Constants.BLOCK_ROW_NUM / 2;
            }

            //Overrides the placeholder blocks to put initial starting blocks with starting cards referenced, and uses the last card in the computer player's hand
            gameBoard.blocks[initialRowPos, initialColPos]
                = new BlockOnBoard(initialRowPos, initialColPos, _computerHand.HandCards[_computerHand.HandCards.Count-1], false);
            gameBoard.blocks[initialRowPos, initialColPos].DrawBlockOnBoard(_boardInterface, gameBoard.BlockWidthSize, gameBoard.BlockHeightSize);
            _computerHand.HandCards.RemoveAt(_computerHand.HandCards.Count - 1);

            RefreshInterface();
        }

        /// <summary>
        /// Redraws everything on the computer player's interface
        /// </summary>
        public virtual void RefreshInterface()
        {
            _computerDisplay.Refresh();
            _computerHand.DrawCardsInHand(_computerInterface);

            //Draws a "free" de-select square
            Rectangle rect = new Rectangle(Constants.DESELECT_SQUARE_X_POSITION, Constants.CARD_GAP, Constants.CARD_SIZE, Constants.CARD_SIZE);
            _computerInterface.DrawRectangle(Constants.BORDER_PENS[2], rect);
            _computerInterface.FillRectangle(Constants.DESELECT_BRUSH, rect);
            _computerInterface.DrawString("De- select", Constants.TEXT_FONT, Constants.TEXT_BRUSH, rect);

            if (_boulderCardUsed == false)
            {
                _boulderCard.Draw(_computerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
            }

            //Draws the obstacle cards separated by a gap
            if (ObstacleCards.Count != 0)
            {
                int x = Constants.OBSTACLE_X_POSITION;
                foreach (ObstacleCard obstacleCard in ObstacleCards)
                {
                    obstacleCard.XPos = x;
                    obstacleCard.Draw(_computerInterface, Constants.CARD_SIZE, Constants.CARD_SIZE);
                    x += Constants.CARD_SIZE + Constants.CARD_GAP;
                }
            }
        }

        public abstract int PlayMove(List<BlockOnBoard> p1CardsPlayed, GameBoard gameBoard);
    }
}
