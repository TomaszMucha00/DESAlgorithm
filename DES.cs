using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DESAlgoritm
{
    class DES
    {
        public static string plaintext;
        public static string key;

        public DES()
        {
            DESBegin("","");
            Keys();
            DESCycle();
        }

        //Początek działania programu
        public static void DESBegin(string plaintext, string key)
        {
            //Console.Write("Podaj wiadomosc do zakodowania: ");
            //plaintext = //Console.ReadLine();
            //plaintext = "KOT";
            int[] temp = Array.ConvertAll<char,int>(StringToBitStream(plaintext).ToCharArray(), element=>(int)element-48);
            Table.plainTextBin = new PermutationMatrix(temp);
            //Console.Write("Podaj klucz: ");
            //plaintext = //Console.ReadLine();
            //key = "0001001100110100010101110111100110011011101111001101111111110001";
            temp = Array.ConvertAll<char, int>(key.ToCharArray(), element => (int)element - 48);
            Table.KeyBin = new PermutationMatrix(temp);
        }

        public static void SboxInit()
        {
            Table.S[0] = new int[,]{ {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
                               {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
                               {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
                               {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13} };
            Table.S[1] = new int[,]{ {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                               {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                               {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                               {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9} };
            Table.S[2] = new int[,]{ {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                               {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                               {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                               {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12} };
            Table.S[3] = new int[,]{ {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                               {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                               {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                               {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14} };
            Table.S[4] = new int[,]{ {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                               {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                               {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                               {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3} };
            Table.S[5] = new int[,]{ {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                               {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                               {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                               {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13} };
            Table.S[6] = new int[,]{ {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                               {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                               {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                               {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12} };
            Table.S[7] = new int[,]{ {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                               {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                               {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                               {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11} };
        }

        //Utworzenie wszystkich 16 podkluczy
        public static void Keys()
        {
            Table.C[0] = Table.KeyBin * Table.PC1L;
            Table.D[0] = Table.KeyBin * Table.PC1R;
            for (int i = 1; i < 17; i++)
            {
                if (i==1|| i == 2 || i == 9 || i == 16)
                {
                    Table.C[i] = new PermutationMatrix(ShiftBit(Table.C[i-1].matrix, 1));
                    Table.D[i] = new PermutationMatrix(ShiftBit(Table.D[i-1].matrix, 1));
                }
                else
                {
                    Table.C[i] = new PermutationMatrix(ShiftBit(Table.C[i - 1].matrix, 2));
                    Table.D[i] = new PermutationMatrix(ShiftBit(Table.D[i - 1].matrix, 2));
                }
            }
            int counter = 0;
            foreach (var item in Table.C)
            {
                counter++;
                string temp = "";
                foreach (var item2 in item.matrix)
                {
                    temp += item2.ToString();
                }
                //Console.WriteLine(counter-1 + " " + temp + "\n");
            }
            counter = 0;
            foreach (var item in Table.D)
            {
                counter++;
                string temp = "";
                foreach (var item2 in item.matrix)
                {
                    temp += item2.ToString();
                }
                //Console.WriteLine(counter-1 + " " + temp + "\n");
            }

            for (int i = 0; i < 16; i++)
            {

                Table.Keys[i] = new PermutationMatrix(Table.C[i+1].matrix.ToList().Concat(Table.D[i+1].matrix.ToList()).ToArray());
                Table.Keys[i] = Table.Keys[i] * Table.PC2;
            }

            counter = 0;
            foreach (var item in Table.Keys)
            {
                counter++;
                string temp = "";
                foreach (var item2 in item.matrix)
                {
                    temp += item2.ToString();
                }
                //Console.WriteLine(counter + " " + temp + "\n");
            }

        }

        //Glowny cykl 
        public static int[] DESCycle()
        {
            Table.plainTextBin = Table.plainTextBin * Table.IP;
            Table.L[0] = new PermutationMatrix(new int[32]);
            Table.R[0] = new PermutationMatrix(new int[32]);
            for (int i = 0; i < 32; i++)
            {
                Table.L[0].matrix[i] = Table.plainTextBin.matrix[i];
                Table.R[0].matrix[i] = Table.plainTextBin.matrix[i + 32];
            }
            for (int i = 1; i < 16; i++)
            {
                Table.L[i] = new PermutationMatrix(new int[32]);
                Table.R[i] = new PermutationMatrix(new int[32]);
                Table.L[i] = FeistelFunction(Table.R[i - 1], i);
                for (int j = 0; j < Table.L[i].matrix.Length; j++)
                {
                    Table.R[i].matrix[j] = (Table.L[i].matrix[j] + Table.Keys[i].matrix[j]) % 2;
                }
            }
            int[] temp = new int[32];
            temp = Table.R[15].matrix;
            Table.R[15].matrix = Table.L[15].matrix;
            Table.L[15].matrix = temp;

            for (int i = 0; i < 32; i++)
            {
                Table.plainTextBin.matrix[i] = Table.R[15].matrix[i];
                Table.plainTextBin.matrix[i+32] = Table.L[15].matrix[i];
            }

            Table.plainTextBin = Table.plainTextBin * Table.FP;
            string answer = "";
            foreach (var item in Table.plainTextBin.matrix)
            {
                answer += item.ToString();
            }
            //Console.WriteLine("Encripted message: " + answer);
            return Table.plainTextBin.matrix;
        }

        //Siatka Feistel'a
        public static PermutationMatrix FeistelFunction(PermutationMatrix R, int cycle)
        {
            PermutationMatrix FeistelMatrix = R;
            FeistelMatrix = FeistelMatrix * Table.E;
            for (int i = 0; i < FeistelMatrix.matrix.Length; i++)
            {
                FeistelMatrix.matrix[i] = (FeistelMatrix.matrix[i] + Table.Keys[cycle].matrix[i]) % 2;
            }
            FeistelMatrix = SBox(FeistelMatrix);
            FeistelMatrix = FeistelMatrix * Table.P;
            return FeistelMatrix;
        }

        //Procedura SBox
        public static PermutationMatrix SBox(PermutationMatrix M)
        {
            int[] Coordinates = new int[16];
            for (int i = 0; i < M.matrix.Length; i+=6)
            {
                Coordinates[i / 3] = M.matrix[i] * 2 + M.matrix[i+5];
                Coordinates[(i / 3) + 1] = M.matrix[i + 1] * 8 + M.matrix[i + 2] * 4 + M.matrix[i + 3] * 2 + M.matrix[i + 4]; 
            }
            int[] resoults = new int[8];
            for (int i = 0; i < Coordinates.Length; i+=2)
            {
                resoults[i/2] = Table.S[i/2][Coordinates[i], Coordinates[i+1]];
            }
            string temp = "";
            foreach (var item in resoults)
            {
                temp += Convert.ToString(item, 2).PadLeft(8, '0');
            }
            M.matrix = Array.ConvertAll<char, int>(temp.ToCharArray(), element => (int)element - 48);

            return M;
        }

        //Przesunięcie dla tablicy o n pozycji
        public static int[] ShiftBit(int[] tab, int howMuch)
        {
            int counter = 0;
            int temp = 0;
            int[] tempTab = new int[tab.Length];
            for (int i = 0; i < tab.Length; i++)
            {
                tempTab[i] = tab[i];
            }
            do
            {
                counter++;
                temp = tempTab[0];
                for (int i = 0; i < tempTab.Length-1; i++)
                {
                    tempTab[i] = tempTab[i+1];
                }
                tempTab[tab.Length-1] = temp;
            } while (counter != howMuch);
            return tempTab;
        }

        //Konwersja z tekstu do formy binarnej
        public static string StringToBitStream(string text)
            {
                string bitText = "";
                foreach (var item in Encoding.UTF8.GetBytes(text))
                {
                    bitText += Convert.ToString(item, 2).PadLeft(8,'0');
                }
                bitText = bitText.PadRight(bitText.Count()+64 - bitText.Count()%64, '0');
                return bitText;
            }
    }
}
