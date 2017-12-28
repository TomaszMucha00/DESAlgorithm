using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgoritm
{
    class CommonFunction
    {
        public void Print2DMatrix(bool[,] bool2DMatrix)
        {
            for (int i = 0; i < bool2DMatrix.Length; i++)
            {
                for (int j = 0; j < bool2DMatrix.LongLength/bool2DMatrix.Length; j++)
                {
                    Console.WriteLine(bool2DMatrix[i,j]+" ");
                }
                Console.WriteLine("\n");
            }
        }

        public void Print2DMatrix(int[,] bool2DMatrix)
        {
            for (int i = 0; i < bool2DMatrix.Length; i++)
            {
                for (int j = 0; j < bool2DMatrix.LongLength / bool2DMatrix.Length; j++)
                {
                    Console.WriteLine(bool2DMatrix[i, j] + " ");
                }
                Console.WriteLine("\n");
            }
        }

        public void Print2DMatrix(char[,] bool2DMatrix)
        {
            for (int i = 0; i < bool2DMatrix.Length; i++)
            {
                for (int j = 0; j < bool2DMatrix.LongLength / bool2DMatrix.Length; j++)
                {
                    Console.WriteLine(bool2DMatrix[i, j] + " ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
