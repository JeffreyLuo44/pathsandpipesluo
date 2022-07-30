using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    class Constants
    {
        //Expected for drawing number values, the "de-select" symbol and the number of countdown tokens
        public static Font TEXT_FONT = new Font("Times New Roman", 12);

        //The side length of the square card
        public const int CARD_SIZE = 51;
        //The gap between cards in hand
        public const int CARD_GAP = 6;

        //The two types of cards, path or pipe
        public enum CARD_TYPE { Path, Pipe };
        //The maximum size of a hand
        public const int HAND_SIZE = 6;
        //The highest number a card in the deck is
        public const int DECK_SIZE = 13;

        //The number of rows on the grid of blocks
        public const int BLOCK_ROW_NUM = 10;
        //The number of columns on the grid of blocks
        public const int BLOCK_COLUMN_NUM = 8;

        //X position where deselect area is in player interface
        public const int DESELECT_SQUARE_X_POSITION = 6 * CARD_SIZE + 7 * CARD_GAP;

        //X position where boulder card is in player interface
        public const int BOULDER_X_POSITION = 7 * CARD_SIZE + 8 * CARD_GAP;

        //Max number of obstacles a player can have
        public const int OBSTACLE_NUM = 2;
        //X position where obstacle card is in player interface
        public const int OBSTACLE_X_POSITION = 8 * CARD_SIZE + 9 * CARD_GAP;
        //The size of a countdown token drawn on an obstacle card
        public const int OBSTACLE_SYMBOL_SIZE = CARD_SIZE / 2;
        //The maximum number of countdown tokens that a obstacle card has
        public const int COUNT_DOWN_NUM = 4;

        //Pens/brushes whose use can depend on the cardtype
        public static Pen[] CARD_PENS = new Pen[] { new Pen(Color.YellowGreen, 6), new Pen(Color.Gray, 6) };
        public static Pen[] BORDER_PENS = new Pen[] { new Pen(Color.Yellow, 3), new Pen(Color.RosyBrown, 3), new Pen(Color.Black, 3) };
        public static SolidBrush[] CARD_BRUSH = new SolidBrush[] { new SolidBrush(Color.DarkGreen), new SolidBrush(Color.SaddleBrown) };
        public static SolidBrush DESELECT_BRUSH = new SolidBrush(Color.LightGray);
        public static SolidBrush BOULDER_BRUSH = new SolidBrush(Color.DarkGray);
        public static SolidBrush[] OBSTACLE_BRUSH = new SolidBrush[] { new SolidBrush(Color.YellowGreen), new SolidBrush(Color.Gray) };
        public static SolidBrush TEXT_BRUSH = new SolidBrush(Color.Black);
    }
}

        

