using Interpolation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class BarycentricRationalInterpolation : BaseInterpolation
    {
        float[] w;
        int d;

        public BarycentricRationalInterpolation(float[] x, float[] y, int dd) : base(x, y, x.Length)
        {
            d = dd;
            w = new float[N];
            if (N < d)
            {
                Console.WriteLine("d too large for number of points in barycentric rational interpolation");
                return;
            }
            for(int k = 0; k < N; k++)
            {
                int imin = Math.Max(k - d, 0);
                int imax = k >= N - d ? N - d - 1 : k;
                float temp = (imin & 1) > 0 ? -1.0f : 1.0f;
                float sum = 0.0f;
                for(int i = imin; i <= imax; i++)
                {
                    int jmax = Math.Min(i + d, N - 1);
                    float term = 1.0f;
                    for (int j = i; j <= jmax; j++)
                    {
                        if (j == k) { continue; }
                        term *= (xx[k] - xx[j]);
                    }
                    term = temp / term;
                    temp = -temp;
                    sum += term;
                }
                w[k] = sum;
            }
        }
        public override float interpolate(float x)
        {
            return RawInterpolation(1,x);
        }
        public override float RawInterpolation(int jlo, float x)
        {
            float num = 0, den = 0;
            for(int i = 0; i < N; i++)
            {
                float h = x - xx[i];
                if (h == 0.0f) // need epsilon comparison
                {
                    return yy[i];
                } 
                else
                {
                    float temp = w[i]/h;
                    num += temp * yy[i];
                    den += temp;
                }
            }
            return num/den;
        }
    }
}
