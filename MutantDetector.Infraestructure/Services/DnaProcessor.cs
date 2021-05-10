using MutantDetector.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MutantDetector.Infraestructure.Services
{
    public class DnaProcessor : IDnaProcessor
    {

        private readonly MutantCondition condition = new MutantCondition();
        private List<string> dna;
        private int matrixSize;
        int matchesQty = 0;

        public DnaProcessor()
        {
        }

        public bool isMutant(IEnumerable<string> Dna)
        {

            dna = Dna.ToList();
            matrixSize = dna.First().Length;
            matchesQty = 0;

            ProcessHorizontally();
            if ( matchesQty > condition.MaxSecuenceQty)
                return true;

            ProcessVertically();
            if (matchesQty > condition.MaxSecuenceQty)
                return true;

            ProcessDiagonally();
            if ( matchesQty > condition.MaxSecuenceQty)
                return true;

            return false;
        }

        private void ProcessHorizontally()
        {
            for (int i = 0; i < matrixSize; i++)
            {
                ProcessLine(dna[i]);
            }
        }
        private void ProcessVertically()
        {
            for (int i = 0; i < matrixSize; i++)
            {
                ProcessLine(getColumn(i));
            }
        }

        private string getColumn(int colnumber)
        {
            string column = "";
            for (int i = 0; i < matrixSize; i++)
            {
                column += dna[i].ToCharArray()[colnumber];
            }
            return column;
        }

        private void ProcessDiagonally()
        {
            //process from top/left to right/down
            ProcessDiagonals(0, matrixSize - 3, 0, matrixSize -3);
            if (matchesQty > condition.MaxSecuenceQty)
                return;

            //process from bottom
            ProcessDiagonals(0, matrixSize - 3, 3, matrixSize);
            if (matchesQty > condition.MaxSecuenceQty)
                return;

        }

        /// <summary>
        /// Iterates over diagonals. When increments = 1, iterates from top/left to bottom/right.
        /// When increment = -1, iterates from bottom/left to top/right. Avoid three last columns/ rows.
        /// </summary>
        private void ProcessDiagonals(int firstRow, int lastRow, int firstColumn, int lastColumn)
        {
            bool left2Right = firstColumn == 0;

            //iterates over first row
            for (int i = firstColumn; i < lastColumn; i++)
            {
                ProcessLine(getDiagonal(i, firstRow, left2Right ));
            }

            //iterates over first column, begining from de second row (first was processed in last step)
            for (int i = firstRow + 1; i < lastRow; i++)
            {
                if (left2Right)
                {
                    ProcessLine(getDiagonal(firstColumn, i, left2Right));
                    if (matchesQty > condition.MaxSecuenceQty)
                        return;
                }
                else
                {
                    ProcessLine(getDiagonal(lastColumn -1, i, left2Right));
                    if (matchesQty > condition.MaxSecuenceQty)
                        return;
                }
                
            }
        }
        private string getDiagonal(int colnumber, int rownumber, bool Left2Right)
        {
            string column = "";
            int increment = Left2Right ? 1 : -1;

            do
            {
                do
                {
                    column += dna[rownumber].ToCharArray()[colnumber];
                    colnumber = colnumber + increment;
                    rownumber++;
                } while (colnumber < matrixSize && rownumber < matrixSize && colnumber >= 0);

            } while (rownumber < matrixSize && colnumber < matrixSize && colnumber >= 0);

            return column;
        }
        private void ProcessLine(string line)
        {
            foreach (string dnabase in condition.DnaBases)
            {
                matchesQty += Regex.Matches(line, dnabase).Count;
                if (matchesQty > condition.MaxSecuenceQty)
                    return;
            }
        }

    }
}
