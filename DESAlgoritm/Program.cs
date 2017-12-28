using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgoritm
{
    class Program
    {
        static void Main(string[] args)
        {
            DES.SboxInit();
            DES des = new DES();
            int[] pmpmpm = new int[] { 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1};
            PermutationMatrix pmpm = new PermutationMatrix(pmpmpm);
            DES.FeistelFunction(pmpm, 0);
        }
    }
}
