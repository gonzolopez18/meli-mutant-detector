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

            GetMatchesDiagonally();
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

        private void GetMatchesDiagonally()
        {
            for (int i = 0; i < matrixSize; i++)
            {
                ProcessLine(getDiagonal(i, 0));
            }
            for (int i = 0; i < matrixSize; i++)
            {
                ProcessLine(getDiagonal(0, i));
            }
        }
        private string getDiagonal(int colnumber, int rownumber)
        {
            string column = "";
            for (int i = 0; i < matrixSize; i++)
            {
                column += dna[i].ToCharArray()[colnumber];
            }
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
