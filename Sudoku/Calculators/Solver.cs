using System.Collections.Generic;
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

            CheckColumns();

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
                            if(k == j) continue;
                            if (_possibleNumbers[i][k].Contains(_possibleNumbers[i][j].ElementAt(0))) _possibleNumbers[i][k].Remove(_possibleNumbers[i][j].ElementAt(0));
                        }
                    }
                }
            }
        }
    }
}