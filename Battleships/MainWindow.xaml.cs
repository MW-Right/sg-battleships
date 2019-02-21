using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace Battleships
{
    public partial class MainWindow : Window
    {
        //Computer array - 1x4, 3x3, 3x2
        public static int[,] computerShips = new int[,] {
            { 1,1,0,2,0,0,0,0,0,0 },
            { 0,0,0,2,0,0,0,0,0,0 },
            { 3,0,0,2,0,0,0,0,0,0 },
            { 3,0,0,0,0,0,0,0,0,0 },
            { 3,0,0,0,0,0,0,0,0,0 },
            { 3,0,0,0,0,4,4,4,4,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 5,5,5,5,5,0,0,0,0,0 } };
        //Added a board with two ships for presentation demo
        public static int[,] demoArr = new int[,]
        {
            { 1,1,0,2,0,0,0,0,0,0 },
            { 0,0,0,2,0,0,0,0,0,0 },
            { 0,0,0,2,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 }
        };
        //Player array - blank
        public static int[,] playerShips = new int[10, 10];
        public static int playColumnNumber = 0;

        public static int columnNumber = 0;
        public static int rowNumber = 0;
        public static int computerRow = 0;
        public static int computerColumn = 0;
        //Length of piece
        public static int[] length = { 4, 3, 3, 3, 2, 2, 2 };
        //Direction for pieces
        public static string direction = "v";
        //Piece counter
        public static int pieceIndex = 0;
        //Alphabet Array
        public static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static char[] alpha = alphabet.ToCharArray();
        //Setting Miss image brush
        public static ImageBrush miss = new ImageBrush();
        //Setting Hit image brush
        public static ImageBrush hit = new ImageBrush();
        //Counter for win condition
        public static int counter = 0;


        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Rectangle myRect = new Rectangle();
                    ButtonGrid.Children.Add(myRect);
                    //myRect.Name = "Cell" + Convert.ToString((int)(i * 10 + j + 1));
                    Grid.SetColumn(myRect, i + 14);
                    Grid.SetRow(myRect, j + 3);
                    myRect.Fill = (new SolidColorBrush(Color.FromArgb(92, 0, 0, 255)));
                    Trace.WriteLine(myRect.Name);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Rectangle myRect = new Rectangle();
                    ButtonGrid.Children.Add(myRect);
                    //myRect.Name = "Cell" + Convert.ToString((int)(i * 10 + j + 101));
                    Grid.SetColumn(myRect, i + 1);
                    Grid.SetRow(myRect, j + 3);
                    myRect.Fill = (new SolidColorBrush(Color.FromArgb(92, 0, 0, 255)));
                    Trace.WriteLine(myRect.Name);
                }
            }
            HitOrMiss();
            ShipSelection();
        }
        public void HitOrMiss()
        {
            miss.ImageSource = new BitmapImage(new Uri(@"Images/miss.png", UriKind.Relative));
            hit.ImageSource = new BitmapImage(new Uri(@"Images/hit.png", UriKind.Relative));
        }
        public void PlacePiece()
        //Draw player piece onto board
        {
            System.Windows.Shapes.Rectangle ship = new System.Windows.Shapes.Rectangle();
            ButtonGrid.Children.Add(ship);
            Grid.SetColumn(ship, columnNumber + 1);
            Grid.SetRow(ship, rowNumber + 3);
            bool taken = false;
            ImageBrush imgBrush = new ImageBrush();
            if (direction == "v")
            {
                switch (length[pieceIndex])
                {
                    case 4:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/battleship4V.png", UriKind.Relative));
                        break;
                    case 3:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/longship3V.png", UriKind.Relative));
                        break;
                    case 2:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/ShortShipV.png", UriKind.Relative));
                        break;
                }
                for (int i = 0; i < length[pieceIndex]; i++)
                    //For each position in a piece, check if the position is taken by comparing it to the player ships array
                {
                    if (playerShips[rowNumber + i, columnNumber] != 0)
                    {
                        taken = true;
                    }
                    else
                    {
                        taken = false;
                    }
                }
                if (taken == false)
                    //If the position isn't taken, draw the pieces
                {
                    for (int i = 0; i < length[pieceIndex]; i++)
                    {
                        playerShips[rowNumber + i, columnNumber] = 1;
                    }
                    Grid.SetRowSpan(ship, length[pieceIndex]);
                    Grid.SetColumnSpan(ship, 1);
                    ship.Fill = imgBrush;
                }
                else if (taken == true)
                {
                    System.Windows.Forms.MessageBox.Show("There is already a ship there");
                }
            }
            else if (direction == "h")
            {
                switch (length[pieceIndex])
                {
                    case 4:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/battleship4.png", UriKind.Relative));
                        break;
                    case 3:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/longship3.png", UriKind.Relative));
                        break;
                    case 2:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/ShortShip.png", UriKind.Relative));
                        break;
                }
                for (int i = 0; i < length[pieceIndex]; i++)
                //For each position in a piece, check if the position is taken by comparing it to the player ships array
                {
                    if (playerShips[rowNumber, columnNumber + i] != 0)
                    {
                        taken = true;
                    }
                    else
                    {
                        taken = false;
                    }
                }
                if (taken == false)
                {
                    for (int i = 0; i < length[pieceIndex]; i++)
                    {
                        playerShips[rowNumber, columnNumber + i] = 1;
                    }
                    Grid.SetRowSpan(ship, 1);
                    Grid.SetColumnSpan(ship, length[pieceIndex]);
                    ship.Fill = imgBrush;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("There is already a ship there");
                }
            }
        }
        public void ShipSelection()
        //Player Ship selection
        {
            //Clear Grid
            ClearGrid2();
            //Draw Piece
            Rectangle shipRect = new Rectangle();
            Grid2.Children.Add(shipRect);
            Grid.SetColumn(shipRect, 1);
            Grid.SetRow(shipRect, 1);
            //Set image background for ships in placement phase
            ImageBrush imgBrush = new ImageBrush();
            if (direction == "v")
            {
                Grid.SetRowSpan(shipRect, length[pieceIndex]);
                Grid.SetColumnSpan(shipRect, 1);
                switch (length[pieceIndex])
                {
                    case 4:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/battleship4V.png", UriKind.Relative));
                        break;
                    case 3:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/longship3V.png", UriKind.Relative));
                        break;
                    case 2:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/ShortShipV.png", UriKind.Relative));
                        break;
                }
            }
            else if (direction == "h")
            {
                Grid.SetRowSpan(shipRect, 1);
                Grid.SetColumnSpan(shipRect, length[pieceIndex]);
                switch (length[pieceIndex])
                {
                    case 4:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/battleship4.png", UriKind.Relative));
                        break;
                    case 3:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/longship3.png", UriKind.Relative));
                        break;
                    case 2:
                        imgBrush.ImageSource = new BitmapImage(new Uri(@"Images/ShortShip.png", UriKind.Relative));
                        break;
                }
            }            
            shipRect.Fill = imgBrush;
        }
        public void ClearGrid2()
        //Clear Selection grid
        {
            Rectangle clearRect = new Rectangle();
            Grid2.Children.Add(clearRect);
            Grid.SetColumn(clearRect, 1);
            Grid.SetRow(clearRect, 1);
            Grid.SetColumnSpan(clearRect, 4);
            Grid.SetRowSpan(clearRect, 4);
            clearRect.Fill = (new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)));
        }
        public void CheckImpactPlayer()
        //Checks impact on Player turn
        {
            if (computerShips[rowNumber, columnNumber] > 0)
            {
                Rectangle myRect = new Rectangle();
                myRect.Width = 30;
                myRect.Height = 30;
                ButtonGrid.Children.Add(myRect);
                Grid.SetColumn(myRect, columnNumber + 14);
                Grid.SetRow(myRect, rowNumber + 3);
                myRect.Fill = hit;
                counter++;
            }
            else
            {
                Rectangle myRect = new Rectangle();
                myRect.Width = 30;
                myRect.Height = 30;
                ButtonGrid.Children.Add(myRect); 
                Grid.SetColumn(myRect, columnNumber + 14);
                Grid.SetRow(myRect, rowNumber + 3);
                myRect.Fill = miss;
            }
            if (counter == 5)
            {
                System.Windows.Forms.MessageBox.Show("You Win!");
            }
        }
        public void CheckImpactComputer()
        //Checks impact on computer turn
        {
            Random rnd = new Random();
            int computerRow = rnd.Next(0, 10);
            int computerColumn = rnd.Next(0, 10);
            computerPicks.Text = $"{alpha[computerColumn]}{computerRow + 1}";
            if (playerShips[computerRow, computerColumn] > 0)
            {
                Rectangle myRect = new Rectangle();
                ButtonGrid.Children.Add(myRect);
                Grid.SetColumn(myRect, computerColumn + 1);
                Grid.SetRow(myRect, computerRow + 3);
                myRect.Fill = hit;
            }
            else
            {
                Rectangle myRect = new Rectangle();
                ButtonGrid.Children.Add(myRect);
                Grid.SetColumn(myRect, computerColumn + 1);
                Grid.SetRow(myRect, computerRow + 3);
                myRect.Fill = miss;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerPicks_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void PlayerPicks_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PlayerPicks_GotFocus;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Selection Conversion for columns
            char columnLetter = Convert.ToChar(playerPicks.Text.Substring(0, 1).ToUpper());
            foreach (var i in alphabet)
            {
                if (columnLetter == i)
                {
                    columnNumber = alphabet.IndexOf(i);
                }
            }
            //Selection Row number
            rowNumber = Convert.ToInt32(playerPicks.Text.Substring(1, 1)) - 1;
            CheckImpactPlayer();
            CheckImpactComputer();
        }

        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            if (direction == "v")
            {
                direction = "h";
            }
            else if (direction == "h")
            {
                direction = "v";
            }
            Trace.WriteLine(direction);
            ShipSelection();
        }

        private void Place_Click(object sender, RoutedEventArgs e)
        {
            if (positionTextBox.Text.Length == 2)
            {
                //Selection Conversion for columns
                char columnLetter = Convert.ToChar(positionTextBox.Text.Substring(0, 1).ToUpper());
                columnNumber = 0;
                foreach (var i in alphabet)
                {
                    if (columnLetter == i)
                    {
                        columnNumber = alphabet.IndexOf(i);
                    }
                }
                rowNumber = Convert.ToInt32(positionTextBox.Text.Substring(1, 1)) - 1;
                PlacePiece();
                if (pieceIndex < 6)
                {
                    pieceIndex++;
                }
                else if (pieceIndex == 6)
                {
                    positionTextBox.IsEnabled = false;
                }
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Trace.Write(playerShips[i, j]);
                    }
                    Trace.Write("\n");
                }
                Trace.WriteLine("\n");
                ShipSelection();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Enter a location");
            }
        }

        private void PositionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PositionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PositionTextBox_GotFocus;
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            computerShips = MainWindow.demoArr;
        }
    }
}
