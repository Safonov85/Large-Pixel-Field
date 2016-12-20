using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CloseToCubeProbability
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        SolidBrush brush;
        SolidBrush mapBrush;
        Color backgroundColor = Color.FromArgb(230, 230, 230);
        int xPosition, yPosition;
        Rectangle mainCube;
        Graphics graphics;
        Graphics graphicsOverride;

        private void Form1_Load(object sender, EventArgs e)
        {
            brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, 50, 50));
            mapBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 150, 150));
            bitmap = new Bitmap(this.Width, this.Height);
            graphics = Graphics.FromImage(bitmap);
            graphicsOverride = this.CreateGraphics();
            mainCube = new Rectangle(xPosition, yPosition, 9, 9);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Hide();

            graphics.Clear(backgroundColor);

            int sideRow = 0;

            // draw field of large "pixels"
            while (sideRow < 460)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    mainCube = new Rectangle(i, sideRow, 9, 9);
                    graphics.FillRectangle(mapBrush, mainCube);
                }
                sideRow += 10;
            }

            xPosition = ConvertToHackyPositionX(e.X);
            yPosition = ConvertToHackyPositionY(e.Y);

            int sphereMaker = 0;
            int decreaseBall = 100;
            int size = 9;

            // create/draw the "ball" in the field where the mouse is moving
            while(sphereMaker < 50)
            {
                for (int i = 0; i < decreaseBall; i += 10)
                {
                    // upper right
                    mainCube = new Rectangle(ConvertToHackyPositionX(e.X) + i, ConvertToHackyPositionY(e.Y) - sphereMaker, size, size);
                    brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50 + i, 50 + i, 50 + i));
                    graphics.FillRectangle(brush, mainCube);

                    // lower right
                    mainCube = new Rectangle(ConvertToHackyPositionX(e.X) + i, ConvertToHackyPositionY(e.Y) + sphereMaker, size, size);
                    graphics.FillRectangle(brush, mainCube);

                    // lower left
                    mainCube = new Rectangle(ConvertToHackyPositionX(e.X) - i, ConvertToHackyPositionY(e.Y) + sphereMaker, size, size);
                    graphics.FillRectangle(brush, mainCube);

                    // upper left
                    mainCube = new Rectangle(ConvertToHackyPositionX(e.X) - i, ConvertToHackyPositionY(e.Y) - sphereMaker, size, size);
                    graphics.FillRectangle(brush, mainCube);
                }
                sphereMaker += 10;
                decreaseBall -= 20;
            }

            graphicsOverride.DrawImage(bitmap, 0, 0, this.Width, this.Height);
        }

        int ConvertToHackyPositionX(int positionX) // read the name
        {
            string convertXToString = positionX.ToString();
            convertXToString = convertXToString.Remove(convertXToString.Length - 1);
            if (convertXToString == "")
            {
                convertXToString = "0";
            }
            int backToIntX = Int32.Parse(convertXToString);
            backToIntX = backToIntX * 10;

            return backToIntX;
        }
        
        int ConvertToHackyPositionY(int positionY) // read the name
        {
            string convertYToString = positionY.ToString();
            convertYToString = convertYToString.Remove(convertYToString.Length - 1);
            if (convertYToString == "")
            {
                convertYToString = "0";
            }
            int backToIntY = Int32.Parse(convertYToString);
            backToIntY = backToIntY * 10;

            return backToIntY;
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
