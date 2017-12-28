namespace DESAlgoritm
{
    class PermutationMatrix
    {
        public int[] matrix;

        public PermutationMatrix(int[] matrix)
        {
            this.matrix = new int[matrix.Length];
            this.matrix = matrix;
        }

        public static PermutationMatrix operator * (PermutationMatrix p1, PermutationMatrix p2)
        {
            int[] temp = new int[p2.matrix.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = p1.matrix[p2.matrix[i] - 1];
            }
            PermutationMatrix answer = new PermutationMatrix(temp);
            return answer;
        }
    }
}
