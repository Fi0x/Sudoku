using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sudoku.Calculators
{
    public class Solver
    {
        private List<int>[][] _possibleNumbers;

        public int[][] Solve(int[][] givenSudoku)
        {
            _possibleNumbers = new List<int>[9][];
            for (int i = 0; i < 9; i++)
            {
                _possibleNumbers[i] = new List<int>[9];
                for (int j = 0; j < 9; j++)
                {
                    _possibleNumbers[i][j] = new List<int>();
                    if (givenSudoku[i][j] == 0)
                    {
                        for (int k = 1; k <= 9; k++)
                        {
                            _possibleNumbers[i][j].Add(k);
                        }
                    }
                    else _possibleNumbers[i][j].Add(givenSudoku[i][j]);
                }
            }

            CheckSquares();
            CheckColumns();
            CheckRows();
            CheckNumberPerSquare();
            CheckNumberPerColumn();
            CheckNumberPerRow();

            int[][] returnSudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                returnSudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    if (_possibleNumbers[i][j].Count == 1) returnSudoku[i][j] = _possibleNumbers[i][j].ElementAt(0);
                    else returnSudoku[i][j] = 0;
                }
            }

            return returnSudoku;
        }

        private void CheckColumns()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_possibleNumbers[i][j].Count == 1)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            if (k == j) continue;
                            if(_possibleNumbers[i][k].Count == 1) continue;
                            if (_possibleNumbers[i][k].Contains(_possibleNumbers[i][j].ElementAt(0))) _possibleNumbers[i][k].Remove(_possibleNumbers[i][j].ElementAt(0));
                        }
                    }
                }
            }
        }

        private void CheckRows()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_possibleNumbers[j][i].Count == 1)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            if (k == j) continue;
                            if(_possibleNumbers[k][i].Count == 1) continue;
                            if (_possibleNumbers[k][i].Contains(_possibleNumbers[j][i].ElementAt(0))) _possibleNumbers[k][i].Remove(_possibleNumbers[j][i].ElementAt(0));
                        }
                    }
                }
            }
        }

        private void CheckSquares()
        {
            for (var squareX = 0; squareX < 9; squareX += 3)
            {
                for (var squareY = 0; squareY < 9; squareY += 3)
                {
                    List<int> containedNumbers = new List<int>();
                    for (var fieldX = 0; fieldX < 3; fieldX++)
                    {
                        for (var fieldY = 0; fieldY < 3; fieldY++)
                        {
                            if (_possibleNumbers[squareX + fieldX][squareY + fieldY].Count == 1) containedNumbers.Add(_possibleNumbers[squareX + fieldX][squareY + fieldY].ElementAt(0));
                        }
                    }

                    foreach (var num in containedNumbers)
                    {
                        for (var fieldX = 0; fieldX < 3; fieldX++)
                        {
                            for (var fieldY = 0; fieldY < 3; fieldY++)
                            {
                                if (_possibleNumbers[squareX + fieldX][squareY + fieldY].Count == 1) continue;
                                _possibleNumbers[squareX + fieldX][squareY + fieldY].Remove(num);
                            }
                        }
                    }
                }
            }
        }

        private void CheckNumberPerColumn()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int number = 1; number <= 9; number++)
                {
                    int foundY = 9;
                    for (int y = 0; y < 9; y++)
                    {
                        if (_possibleNumbers[x][y].Contains(number))
                        {
                            if (foundY == 9) foundY = y;
                            else
                            {
                                foundY = 9;
                                break;
                            }
                        }
                    }

                    if (foundY != 9)
                    {
                        _possibleNumbers[x][foundY].Clear();
                        _possibleNumbers[x][foundY].Add(number);
                    }
                }
            }
        }

        private void CheckNumberPerRow()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int number = 1; number <= 9; number++)
                {
                    int foundX = 9;
                    for (int x = 0; x < 9; x++)
                    {
                        if (_possibleNumbers[x][y].Contains(number))
                        {
                            if (foundX == 9) foundX = x;
                            else
                            {
                                foundX = 9;
                                break;
                            }
                        }
                    }

                    if (foundX != 9)
                    {
                        _possibleNumbers[foundX][y].Clear();
                        _possibleNumbers[foundX][y].Add(number);
                    }
                }
            }
        }

        private void CheckNumberPerSquare()
        {
            for (var squareX = 0; squareX < 9; squareX += 3)
            {
                for (var squareY = 0; squareY < 9; squareY += 3)
                {
                    for (int number = 1; number <= 9; number++)
                    {
                        int foundX = 3;
                        int foundY = 3;
                        for (int fieldX = 0; fieldX < 3; fieldX++)
                        {
                            for (int fieldY = 0; fieldY < 3; fieldY++)
                            {
                                if (_possibleNumbers[squareX + fieldX][squareY + fieldY].Contains(number))
                                {
                                    if (foundX == 3)
                                    {
                                        foundX = fieldX;
                                        foundY = fieldY;
                                    }
                                    else
                                    {
                                        foundX = -1;
                                        break;
                                    }
                                }
                            }
                            if(foundX == -1) break;
                        }

                        if (foundX != -1)
                        {
                            _possibleNumbers[squareX + foundX][squareY + foundY].Clear();
                            _possibleNumbers[squareX + foundX][squareY + foundY].Add(number);
                        }
                    }
                }
            }
        }
    }
}