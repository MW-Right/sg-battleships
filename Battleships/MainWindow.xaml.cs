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
using System.Windows.Forms;


namespace Battleships
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class ProgramVariables
    {
        //Computer array - 1x2, 1x3, 2x4
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
        //Player array - 1x2, 1x3, 2x4
        public static int[,] playerShips = new int[,] {
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 } };
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
    }

    public partial class MainWindow : Window
    {
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
            Trace.WriteLine(ProgramVariables.pieceIndex);
            ShipSelection();
        }
        public void PlacePiece()
        //Draw player piece onto board
        {
            Rectangle ship = new Rectangle();
            ButtonGrid.Children.Add(ship);
            Grid.SetColumn(ship, ProgramVariables.columnNumber + 1);
            Grid.SetRow(ship, ProgramVariables.rowNumber + 3);
            bool taken = false;
            if (ProgramVariables.direction == "v")
            {
                for (int i = 0; i < ProgramVariables.length[ProgramVariables.pieceIndex]; i++)
                    //For each position in a piece, check if the position is taken by comparing it to the player ships array
                {
                    if (ProgramVariables.playerShips[ProgramVariables.columnNumber + i, ProgramVariables.rowNumber] != 0)
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
                    for (int i = 0; i < ProgramVariables.length[ProgramVariables.pieceIndex]; i++)
                    {
                        ProgramVariables.playerShips[ProgramVariables.columnNumber + i, ProgramVariables.rowNumber] = 1;
                    }
                    Grid.SetRowSpan(ship, ProgramVariables.length[ProgramVariables.pieceIndex]);
                    Grid.SetColumnSpan(ship, 1);
                }
                else if (taken == true)
                {
                    System.Windows.Forms.MessageBox.Show("There is already a ship there");
                }
            }
            else if (ProgramVariables.direction == "h")
            {
                for (int i = 0; i < ProgramVariables.length[ProgramVariables.pieceIndex]; i++)
                //For each position in a piece, check if the position is taken by comparing it to the player ships array
                {
                    if (ProgramVariables.playerShips[ProgramVariables.columnNumber + i, ProgramVariables.rowNumber] != 0)
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
                    for (int i = 0; i < ProgramVariables.length[ProgramVariables.pieceIndex]; i++)
                    {
                        ProgramVariables.playerShips[ProgramVariables.columnNumber + i, ProgramVariables.rowNumber] = 1;
                    }
                    Grid.SetRowSpan(ship, 1);
                    Grid.SetColumnSpan(ship, ProgramVariables.length[ProgramVariables.pieceIndex]);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("There is already a ship there");
                }
            }
            ship.Fill = Brushes.Gray;
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
            if (ProgramVariables.direction == "v")
            {
                Grid.SetRowSpan(shipRect, ProgramVariables.length[ProgramVariables.pieceIndex]);
                Grid.SetColumnSpan(shipRect, 1);
            }
            else if (ProgramVariables.direction == "h")
            {
                Grid.SetRowSpan(shipRect, 1);
                Grid.SetColumnSpan(shipRect, ProgramVariables.length[ProgramVariables.pieceIndex]);
            }
            shipRect.Fill = Brushes.Gray;
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
            if (ProgramVariables.computerShips[ProgramVariables.columnNumber, ProgramVariables.rowNumber] > 0)
            {
                Rectangle myRect = new Rectangle();
                myRect.Width = 30;
                myRect.Height = 30;
                ButtonGrid.Children.Add(myRect);
                Grid.SetColumn(myRect, ProgramVariables.columnNumber + 14);
                Grid.SetRow(myRect, ProgramVariables.rowNumber + 3);
                myRect.Fill = Brushes.Red;
            }
            else
            {
                Rectangle myRect = new Rectangle();
                ButtonGrid.Children.Add(myRect);
                Grid.SetColumn(myRect, ProgramVariables.columnNumber + 14);
                Grid.SetRow(myRect, ProgramVariables.rowNumber + 3);
                myRect.Fill = Brushes.White;
            }
        }
        public void CheckImpactComputer()
        //Checks impact on computer turn
        {
            Random rnd = new Random();
            int computerRow = rnd.Next(0, 9);
            int computerColumn = rnd.Next(0, 9);
            if (ProgramVariables.playerShips[computerColumn, computerRow] > 0)
            {
                Rectangle myRect = new Rectangle();
                ButtonGrid.Children.Add(myRect);
                Grid.SetColumn(myRect, computerColumn + 2);
                Grid.SetRow(myRect, computerRow + 4);
                myRect.Fill = Brushes.Red;
            }
            else
            {
                Rectangle myRect = new Rectangle();
                ButtonGrid.Children.Add(myRect);
                Grid.SetColumn(myRect, computerColumn + 1);
                Grid.SetRow(myRect, computerRow + 3);
                myRect.Fill = Brushes.White;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerPicks_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Selection Conversion for columns
            char columnLetter = Convert.ToChar(playerPicks.Text.Substring(0, 1));
            foreach (var i in ProgramVariables.alphabet)
            {
                if (columnLetter == i)
                {
                    ProgramVariables.columnNumber = ProgramVariables.alphabet.IndexOf(i);
                }
            }
            //Selection Row number
            ProgramVariables.rowNumber = Convert.ToInt32(playerPicks.Text.Substring(1, 1)) - 1;
            CheckImpactPlayer();
            CheckImpactComputer();
        }

        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramVariables.direction == "v")
            {
                ProgramVariables.direction = "h";
            }
            else if (ProgramVariables.direction == "h")
            {
                ProgramVariables.direction = "v";
            }
            Trace.WriteLine(ProgramVariables.direction);
            ShipSelection();
        }

        private void Place_Click(object sender, RoutedEventArgs e)
        {
            //Selection Conversion for columns
            char columnLetter = Convert.ToChar(positionTextBox.Text.Substring(0, 1));
            ProgramVariables.columnNumber = 0;
            foreach (var i in ProgramVariables.alphabet)
            {
                if (columnLetter == i)
                {
                    ProgramVariables.columnNumber = ProgramVariables.alphabet.IndexOf(i);
                }
            }
            ProgramVariables.rowNumber = Convert.ToInt32(positionTextBox.Text.Substring(1, 1)) - 1;
            PlacePiece();
            if (ProgramVariables.pieceIndex < 6)
            {
                ProgramVariables.pieceIndex++;
            }
            else if (ProgramVariables.pieceIndex == 6)
            {
                playButton.IsEnabled = true;
            }
            ShipSelection();
        }

        private void PositionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
