using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment_2_Jeffrey_Luo
{
    class BlockOnBoard
    {
        //Whether or not the block has been checked
        protected bool _blockBeenChecked;
        //Whether or not the block is a placeholder
        protected bool _placeholder;
        //The block's card type 
        protected Constants.CARD_TYPE _cardType;
        //The row position of the 2D blocks array
        protected int _rowPos;
        //The column position of the 2D blocks array
        protected int _colPos;
        //The referenced card
        protected Card _card;
        //Whether or not the block is an obstacle
        protected bool _obstacle;
        /// <summary>
        /// To identify pipe and path side of the board. There are blocks set for each side to placeholder
        /// These are placeholder blocks
        /// </summary>
        /// <param name="cardType">Can be either paths or pipes</param>
        /// <param name="rowPos">Row array position of the 2D array</param>
        /// <param name="colPos">Column array position of the 2D array</param>
        public BlockOnBoard(Constants.CARD_TYPE cardType, int rowPos, int colPos)
        {
            _placeholder = true;
            _cardType = cardType;
            _rowPos = rowPos;
            _colPos = colPos;
            _obstacle = false;
        }

        /// <summary>
        /// Used when placing a new card to be a new block on board that is not a placeholder
        /// </summary>
        /// <param name="rowPos">Row array position of the 2D array</param>
        /// <param name="colPos">Column array position of the 2D array</param>
        /// <param name="card">The card referenced for the block on board</param>
        /// <param name="obstacle">Whether or not this block on board is an obstacle</param>
        public BlockOnBoard(int rowPos, int colPos, Card card, bool obstacle)
        {
            _placeholder = false;
            _cardType = card.CardType;
            _rowPos = rowPos;
            _colPos = colPos;
            _card = card;
            _obstacle = obstacle;
        }

        /// <summary>
        /// When finding a valid path, this property is set to true when the block has been accounted for
        /// </summary>
        public bool BlockBeenChecked
        {
            get { return _blockBeenChecked; }
            set { _blockBeenChecked = value; }
        }

        /// <summary>
        /// Before a block on board has a card reference, this property is set to true or else, if it has, it is set to false
        /// </summary>
        public bool Placeholder
        {
            get { return _placeholder; }
        }

        /// <summary>
        /// Can be either pipes or paths
        /// </summary>
        public Constants.CARD_TYPE Type
        {
            get { return _cardType; }
        }

        /// <summary>
        /// Row array position of the 2D array
        /// </summary>
        public int RowPos
        {
            get { return _rowPos; }
        }

        /// <summary>
        /// Column array position of the 2D array
        /// </summary>
        public int ColPos
        {
            get { return _colPos; }
        }

        /// <summary>
        /// Card referenced
        /// </summary>
        public Card Card
        {
            get { return _card; }
        }

        /// <summary>
        /// Whether or not this block on board is an obstacle
        /// </summary>
        public bool Obstacle
        {
            get { return _obstacle; }
            set { _obstacle = value; }
        }

        /// <summary>
        /// Draws the referenced card with its position at the position of the block on board
        /// </summary>
        /// <param name="paper">Graphics to draw on</param>
        public void DrawBlockOnBoard(Graphics paper, int blockWidth, int blockHeight)
        {
            Card.XPos = ColPos * blockWidth;
            Card.YPos = RowPos * blockHeight;
            Card.Draw(paper, blockWidth, blockHeight);
        }   
    }
}
