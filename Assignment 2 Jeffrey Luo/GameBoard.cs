using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    class GameBoard
    {
        //Instantiate a new 2D "block on board" array
        public BlockOnBoard[,] blocks;
        //Instantiate a new 2D "rectangle" array to detect whether user has clicked on them
        Rectangle[,] _rectanglesToClick;
        //Pen for drawing grid
        Pen dottedLinePen = new Pen(Color.LightGray, 2);
        //Pen for marking player regions
        Pen regionPen = new Pen(Color.Khaki, 4);
        //The width of a block on board
        protected int _blockWidthSize;
        //The length of a block on board
        protected int _blockHeightSize;
        /// <summary>
        /// Defines maximum sizes of 2D "block on board" array and "rectangle" array. Assigns placeholder blocks with 
        /// cardtype depending on either top or bottom side of the gameboard
        /// </summary>
        public GameBoard()
        {
            //Specifies the maximum dimensions of the 2D arrays
            blocks = new BlockOnBoard[Constants.BLOCK_ROW_NUM, Constants.BLOCK_COLUMN_NUM];
            _rectanglesToClick = new Rectangle[Constants.BLOCK_ROW_NUM, Constants.BLOCK_COLUMN_NUM];
            for(int i = 0; i< Constants.BLOCK_ROW_NUM; i++)
            {
                for(int j=0; j<Constants.BLOCK_COLUMN_NUM; j++)
                {
                    //Path player region to be placeholders (upper half)
                    if (i<Constants.BLOCK_ROW_NUM/2)
                    {
                        blocks[i, j] = new BlockOnBoard(Constants.CARD_TYPE.Path, i, j);
                    }
                    //Path player region to be placeholders (lower half)
                    else if (i > Constants.BLOCK_ROW_NUM / 2 - 1)
                    {
                        blocks[i, j] = new BlockOnBoard(Constants.CARD_TYPE.Pipe, i, j);
                    }
                }
            }
        }

        /// <summary>
        /// The width of a block on board
        /// </summary>
        public int BlockWidthSize
        {
            get { return _blockWidthSize; }
            set { _blockWidthSize = value; }
        }

        /// <summary>
        /// The height of a block on board
        /// </summary>
        public int BlockHeightSize
        {
            get { return _blockHeightSize; }
            set { _blockHeightSize = value; }
        }

        /// <summary>
        /// Finds the blocks array posiion of the place the user is clicking
        /// </summary>
        /// <param name="x">x position of mouse click</param>
        /// <param name="y">y position of mouse click</param>
        /// <returns>The row and column array number which corresponds to the blocks array</returns>
        public (int i, int j) GetArrayPositionOfClickedSquare(int x, int y)
        {
            for (int i = 0; i < Constants.BLOCK_ROW_NUM; i++)
            {
                for (int j = 0; j < Constants.BLOCK_COLUMN_NUM; j++)
                {
                    //Determines whether the mouse clicked position is within a particular rectangle
                    if (_rectanglesToClick[i, j].Contains(x, y))
                    {
                        return (i, j); 
                    }
                }
            }
            //Code after will be an appropriate error message (expected in the player class)
            return (-1, -1);
        }

        /// <summary>
        /// To determine whether the array position of the new block on board is valid.
        /// To be valid, it checks if there are no obstacles in the current block on board position,
        /// if there is an adjacent block on board on either 4 sides of the board, 
        /// if the referenced card is higher than the current card referenced on the block on board & 
        /// if there is a path of its own cardtype connecting back to its region of the board. 
        /// Also, a (referenced) gamecard cannot solely just connect to an obstacle block.
        /// All these conditions must be met to be valid
        /// </summary>
        /// <param name="placedCard">The referenced card from the player's selection</param>
        /// <param name="rowPos">The row array position of the 2D array</param>
        /// <param name="colPos">The column array position of the 2D array</param>
        /// <param name="playerMove">Display message boxes only for the human player if a move is not valid</param>
        /// <returns>Whether or not the clicked square is valid to place a new block on board on</returns>
        public bool ValidateClickedSquare(Card placedCard, int rowPos, int colPos, bool playerMove)
        {
            int emptySquare = 0;
            bool connectsToPlayerRegion = false;

            //Check if already placed card is an obstacle card
            if (blocks[rowPos,colPos].Obstacle) 
            {
                if (playerMove == true)
                {
                    MessageBox.Show("Card cannot be placed here because of an obstacle");
                }
                return false;
            }

            //Adjacent blocks checking for block placement NOT on side of gameboard display

            //Check row which is not part of top and bottom side 
            if (rowPos>0 && rowPos<Constants.BLOCK_ROW_NUM-1)
            {
                //Check top and bottom if there are blocks
                if (blocks[rowPos - 1, colPos].Placeholder == true && blocks[rowPos+1,colPos].Placeholder == true)
                {
                    //Above and below has no block
                    emptySquare += 2;
                }
            }

            //Check column which is not part of left and right side
            if (colPos > 0 && colPos < Constants.BLOCK_COLUMN_NUM-1)
            {
                //Check left and right if there are blocks
                if (blocks[rowPos, colPos-1].Placeholder == true && blocks[rowPos, colPos+1].Placeholder == true)
                {
                    //Left and right has no block
                    emptySquare += 2; 
                }
            }

            //Specialised adjacent block checking for block placement on side of gameboard display

            //Check top row
            if (rowPos == 0)
            {
                //Check if block in row below
                if (blocks[rowPos + 1, colPos].Placeholder == true)
                {
                    //Above and below has no block
                    emptySquare += 2;
                }
            }

            //Check bottom row
            if (rowPos == Constants.BLOCK_ROW_NUM - 1)
            {
                //Check if block in row above
                if (blocks[rowPos - 1, colPos].Placeholder == true)
                {
                    //Above and below has no block
                    emptySquare += 2;
                }
            }

            //Check left-most column
            if (colPos == 0)
            {
                //Check if block in column right
                if (blocks[rowPos, colPos + 1].Placeholder == true)
                {
                    //Left and right has no block
                    emptySquare += 2;
                }
            }

            //Check right-most column
            if (colPos == Constants.BLOCK_COLUMN_NUM - 1)
            {
                //Check if block in column left
                if (blocks[rowPos, colPos - 1].Placeholder == true)
                {
                    //Left and right has no block
                    emptySquare += 2;
                }
            }

            //The card is not higher than the already placed card
            if (blocks[rowPos, colPos].Placeholder != true)
            {
                if (placedCard.Number <= blocks[rowPos, colPos].Card.Number)
                {
                    if (playerMove == true)
                    {
                        MessageBox.Show("Card is not high enough to override (or is an obstacle card)");
                    }
                    return false;
                }
            }

            //There are no adjacent blocks top, bottom, left and right
            if (emptySquare == 4)
            {
                if (playerMove == true)
                {
                    MessageBox.Show("There are no adjacent cards");
                }
                return false;
            }

            //For placing a card on the player's own side
            if (placedCard.CardType == Constants.CARD_TYPE.Path && rowPos < Constants.BLOCK_ROW_NUM / 2)
            {
                connectsToPlayerRegion = true;
            }
            else if (placedCard.CardType == Constants.CARD_TYPE.Pipe && rowPos > Constants.BLOCK_ROW_NUM / 2 - 1)
            {
                connectsToPlayerRegion = true;
            }

            //Checks all the obstacle and placeholder cards
            DefaultBlocksToCheck();

            //Check valid path in opposition region back to player region
            if (connectsToPlayerRegion == false)
            {
                connectsToPlayerRegion = CheckValidPath(placedCard.CardType, rowPos, colPos);
            }

            //There is no block that connects to the player region
            if (connectsToPlayerRegion == false)
            {
                if (playerMove == true)
                {
                    MessageBox.Show("Card is not connected to your region (also cannot connect with obstacle cards)");
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks all the obstacle and placeholder cards
        /// </summary>
        public void DefaultBlocksToCheck()
        {
            //Reset all block's "block checked" properties to false to check next move's paths
            for (int i = 0; i < Constants.BLOCK_ROW_NUM - 1; i++)
            {
                for (int j = 0; j < Constants.BLOCK_COLUMN_NUM - 1; j++)
                {
                    blocks[i, j].BlockBeenChecked = false;
                    //Checks off the obstacles to check next move's paths
                    if (blocks[i, j].Obstacle == true || blocks[i, j].Placeholder == true)
                    {
                        blocks[i, j].BlockBeenChecked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Checks a valid path from the opposition region back to the middle edge row of the player's region.
        /// To check for a winning path, the finish or end row is passed to this method as the current row
        /// </summary>
        /// <param name="playerCardType">Can be pipes or paths</param>
        /// <param name="currentRow">Current row position in the 2D array</param>
        /// <param name="currentCol">Current column position in the 2D array</param>
        /// <returns>Whether or not the path connects to the player region</returns>
        public bool CheckValidPath(Constants.CARD_TYPE playerCardType, int currentRow, int currentCol)
        {
            //Set the new block to check to true
            blocks[currentRow, currentCol].BlockBeenChecked = true;

            //Centre or middle edge row according to what cardtype the player is
            if (playerCardType == Constants.CARD_TYPE.Path && currentRow == Constants.BLOCK_ROW_NUM / 2 - 1)
            {
                return true;
            }
            else if (playerCardType == Constants.CARD_TYPE.Pipe && currentRow == Constants.BLOCK_ROW_NUM / 2)
            {
                return true;
            }

            //Check adjacent rows and columns. If there is an adjacent block that is the same cardtype and it has not been checked,
            //then recursively call the method until the centre row is reached (return true) or until the paths are blocked (return false)

            try
            {
                if (blocks[currentRow, currentCol + 1].Type == playerCardType && blocks[currentRow, currentCol + 1].BlockBeenChecked == false)
                {
                    if (CheckValidPath(playerCardType, currentRow, currentCol + 1))
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            try
            {
                if (blocks[currentRow, currentCol - 1].Type == playerCardType && blocks[currentRow, currentCol - 1].BlockBeenChecked == false)
                {
                    if (CheckValidPath(playerCardType, currentRow, currentCol - 1))
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            try
            {
                if (blocks[currentRow + 1, currentCol].Type == playerCardType && blocks[currentRow + 1, currentCol].BlockBeenChecked == false)
                {
                    if (CheckValidPath(playerCardType, currentRow + 1, currentCol))
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            try
            {
                if (blocks[currentRow - 1, currentCol].Type == playerCardType && blocks[currentRow - 1, currentCol].BlockBeenChecked == false)
                {
                    if (CheckValidPath(playerCardType, currentRow - 1, currentCol))
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// Draws the gameboard and the clickable rectangles from the "rectangle" array
        /// </summary>
        /// <param name="paper">Graphics to draw on</param>
        public void DrawBoard(Graphics paper)
        {
            //x and y positions to draw grid squares
            int x = 0;
            int y = 0;
            dottedLinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //Marks the regions for the players
            paper.DrawRectangle(regionPen, x, y, Constants.BLOCK_COLUMN_NUM * BlockWidthSize, Constants.BLOCK_ROW_NUM/2 * BlockHeightSize);
            paper.DrawRectangle(regionPen, x, Constants.BLOCK_ROW_NUM / 2 * BlockHeightSize, Constants.BLOCK_COLUMN_NUM * BlockWidthSize, Constants.BLOCK_ROW_NUM / 2 * BlockHeightSize);
            //For each row to draw
            for (int i = 0; i<Constants.BLOCK_ROW_NUM; i++)
            {
                //For each column to draw
                for (int j = 0; j < Constants.BLOCK_COLUMN_NUM; j++)
                {
                    //Assigns rectangles for the user to click on to each square
                    _rectanglesToClick[i,j] = new Rectangle(x, y, BlockWidthSize, BlockHeightSize);
                    //Draws the grid square
                    paper.DrawRectangle(dottedLinePen, _rectanglesToClick[i,j]);
                    //Shifts the current position of x one square right
                    x += BlockWidthSize;
                }
                //Shifts the current position of y one square downwards for drawing the next row
                y += BlockHeightSize;
                //Resets the position of x to be at the start of the new row
                x = 0;
            }
        }

        /// <summary>
        /// Draws all blocks on boards with a non-null card reference
        /// </summary>
        /// <param name="paper">Graphics to draw on</param>
        public void RedrawBlocksOnGameBoard(Graphics paper)
        {
            for (int i = 0; i<Constants.BLOCK_ROW_NUM; i++)
            {
                for(int j=0; j<Constants.BLOCK_COLUMN_NUM; j++)
                {
                    if (blocks[i, j].Card != null)
                    {
                        blocks[i, j].DrawBlockOnBoard(paper, BlockWidthSize, BlockHeightSize);
                    }
                }
            }
        }
    }
}
