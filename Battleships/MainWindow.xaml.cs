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
            { 0,0,0,2,0,0,0,1,0,0 },
            { 0,0,0,2,0,0,0,1,0,0 },
            { 0,0,0,2,0,0,0,0,0,0 },
            { 0,0,0,0,0,3,3,3,3,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,4,0,0,0,0 },
            { 0,0,0,0,0,4,0,0,0,0 },
            { 0,0,0,0,0,4,0,0,0,0 },
            { 0,0,0,0,0,4,0,0,0,0 },
            { 0,0,0,0,5,5,5,5,5,0 } };
        public static int columnNumber = 0;
        public static int rowNumber = 0;
        public static int computerRow = 0;
        public static int computerColumn = 0;
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
                    myRect.Name = "Cell" + Convert.ToString((int)(i * 10 + j + 1));
                    Grid.SetColumn(myRect, i + 14);
                    Grid.SetRow(myRect, j + 3);
                    myRect.Fill = Brushes.Blue;
                    Trace.WriteLine(myRect.Name);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Rectangle myRect = new Rectangle();
                    ButtonGrid.Children.Add(myRect);
                    myRect.Name = "Cell" + Convert.ToString((int)(i * 10 + j + 101));
                    Grid.SetColumn(myRect, i + 1);
                    Grid.SetRow(myRect, j + 3);
                    myRect.Fill = Brushes.Blue;
                    Trace.WriteLine(myRect.Name);
                }
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
            //Alphabet Array
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alpha = alphabet.ToCharArray();
            //Selection Conversion for columns
            char columnLetter = Convert.ToChar(playerPicks.Text.Substring(0, 1));
            ProgramVariables.columnNumber = 0;
            foreach (var i in alphabet)
            {
                if (columnLetter == i)
                {
                    ProgramVariables.columnNumber = alphabet.IndexOf(i);
                }
            }
            //Selection Row number
            ProgramVariables.rowNumber = Convert.ToInt32(playerPicks.Text.Substring(1, 1)) - 1;
            CheckImpactPlayer();
            CheckImpactComputer();
        }
        public void CheckImpactPlayer()
            //Checks impact on Player turn
        {
            if (ProgramVariables.computerShips[ProgramVariables.columnNumber, ProgramVariables.rowNumber] > 0)
            {
                Rectangle myRect = new Rectangle();
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
    }
}
