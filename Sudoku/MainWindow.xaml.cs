﻿using System;
using System.Windows;
using System.Windows.Controls;
using Sudoku.Calculators;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Solver _solver = new Solver();
        private Button[][] _button;
        public MainWindow()
        {
            InitializeComponent();

            _button = new Button[9][];
            for (int x = 0; x < 9; x++)
            {
                _button[x] = new Button[9];
                for (int y = 0; y < 9; y++)
                {
                    Button btn = new Button();
                    btn.Content = "";
                    btn.Click += BtnSudoku_OnClick;

                    int leftMargin = 1;
                    int topMargin = 1;
                    int rightMargin = 1;
                    int bottomMargin = 1;
                    if (x % 3 == 0) leftMargin = 3;
                    if (x % 3 == 2) rightMargin = 3;
                    if (y % 3 == 0) topMargin = 3;
                    if (y % 3 == 2) bottomMargin = 3;
                    
                    btn.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
                    
                    GridSudoku.Children.Add(btn);
                    Grid.SetColumn(btn, x);
                    Grid.SetRow(btn, y);

                    _button[x][y] = btn;
                }
            }
        }

        private string increaseNumber(string original)
        {
            int result;
            int.TryParse(original, out result);

            result++;
            if (result > 9) return "";
            
            return result.ToString();
        }

        private void BtnSudoku_OnClick(object sender, RoutedEventArgs e)
        {
            Button currentButton = (sender as Button);
            if(currentButton == null) return;
            currentButton.Content = increaseNumber(currentButton.Content.ToString());
        }

        private void btnSolve_OnClick(object sender, RoutedEventArgs e)
        {
            int[][] inputNumbers = new int[9][];
            for (var i = 0; i < 9; i++)
            {
                inputNumbers[i] = new int[9];
                for (var j = 0; j < 9; j++)
                {
                    int number;
                    int.TryParse(_button[i][j].Content.ToString(), out number);
                    inputNumbers[i][j] = number;
                }
            }
            int[][] resultNumbers = _solver.Solve(inputNumbers);

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (resultNumbers[i][j] == 0) _button[i][j].Content = "";
                    else _button[i][j].Content = resultNumbers[i][j];
                }
            }
        }

        private void btnClear_OnClick(object sender, RoutedEventArgs e)
        {
            _solver = new Solver();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    _button[x][y].Content = "";
                }
            }
        }
    }
}