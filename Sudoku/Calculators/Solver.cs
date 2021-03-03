using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Calculators
{
    public class Solver
    {
        public static int[][] Solve(int[][] givenSudoku)
        {
            List<int>[][] possibleNumbers = new List<int>[9][];
            for (int i = 0; i < 9; i++)
            {
                possibleNumbers[i] = new List<int>[9];
                for (int j = 0; j < 9; j++)
                {
                    possibleNumbers[i][j] = new List<int>();
                    if (givenSudoku[i][j] == 0)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            possibleNumbers[i][j].Add(k);
                        }
                    }
                    else possibleNumbers[i][j].Add(givenSudoku[i][j]);
                }
            }

            int[][] returnSudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                returnSudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    if (possibleNumbers[i][j].Count == 1) returnSudoku[i][j] = possibleNumbers[i][j].ElementAt(0);
                    else returnSudoku[i][j] = 0;
                }
            }

            return returnSudoku;
        }
    }
}