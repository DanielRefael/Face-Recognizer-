using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Face_Recognizer_Exrecise.Model
{
    public class DotProductClass
    {
        // Function to compute Maximum
        // Dot Product and return it
        public float MaxDotProduct(float[] A, float[] B, int m, int n)
        {
            // Create 2D Matrix that stores dot product
            // dp[i+1][j+1] stores product considering B[0..i]
            // and A[0...j]. Note that since all m > n, we fill
            // values in upper diagonal of dp[][]
            float[,] dp = new float[n + 1, m + 1];

            // Traverse through all elements of B[]
            for (int i = 1; i <= n; i++)

                // Consider all values of A[] with indexes greater
                // than or equal to i and compute dp[i][j]
                for (int j = i; j <= m; j++)

                    // Two cases arise
                    // 1) Include A[j]
                    // 2) Exclude A[j] (insert 0 in B[])
                    dp[i, j] =
                        Math.Max((dp[i - 1, j - 1] +
                                (A[j - 1] * B[i - 1])), dp[i, j - 1]);

            // return Maximum Dot Product
            return dp[n, m];
        }

        public double GetClosetVector(float[] A, float[] B)
        {
            //Get mul between vectors after sqrt and pow for each vector
            var squaredArray = Math.Sqrt(A.Select(x => x * x).ToArray().Sum()) * Math.Sqrt(B.Select(x => x * x).ToArray().Sum());
            //return the normal between the vectors
            return MaxDotProduct(A, B, A.Length, B.Length) / squaredArray;
        }
    }
}
