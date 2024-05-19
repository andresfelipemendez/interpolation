using Interpolation;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class SplineInterpolation : BaseInterpolation
    {
        float[] y2;
      
        public SplineInterpolation(float[] x, float[] y, float yp1 = 1e30f, float ypn = 1e30f) : base(x, y, 2)
        {
            y2 = new float[x.Length];
            sety2(x, y, yp1, ypn);
        }

        public void sety2(float[] xv, float[] yv, float yp1, float ypn)
        {
            int i, k;
            float p, qn, sig, un;
            float[] u = new float[N-1];
            if (yp1 > 0.99)
            {
                y2[0] = u[0] = 0.0f;
            }
            else
            {
                y2[0] = -0.5f;
                u[0] = (3.0f / (xv[1] - xv[0])) * ((yv[1] - yv[0]) / (xv[1] - xv[0]) - yp1);
            }
            /*
             * This is the decomposition loop of the tridiagonal al -
gorithm.y2 and u are used for tem -
porary storage of the decomposed
factors.
             */
            for (i = 1; i < N - 1; i++)
            {
                sig = (xv[i] - xv[i - 1]) / (xv[i + 1] - xv[i - 1]);
                p = sig * y2[i - 1] + 2.0f;
                y2[i] = (sig - 1.0f) / p;
                u[i] = (yv[i + 1] - yv[i]) / (xv[i + 1] - xv[i]) - (yv[i] - yv[i - 1]) / (xv[i] - xv[i - 1]);
                u[i] = (6.0f * u[i] / (xv[i + 1] - xv[i - 1]) - sig * u[i - 1]) / p;
            }
            if (ypn > 0.99f)
            {
                qn = un = 0.0f;

            }
            else
            {
                qn = 0.5f;
                un = (3.0f / (xv[N - 1] - xv[N - 2])) * (ypn - (yv[N - 1] - yv[N - 2]) / (xv[N - 1] - xv[N - 2]));
            }
            y2[N - 1] = (un - qn * u[N - 2]) / (qn * y2[N - 2] + 1.0f);
            for (k = N - 2; k >= 0; k--)
            {
                y2[k] = y2[k] * y2[k + 1] + u[k];
            }
        }

        public override float RawInterpolation(int jlo, float x)
        {
            int klo = jlo, khi = jlo + 1;
            float y, h, b, a;
            h = xx[khi] - xx[klo];
            if (h == 0.0)
            {
                Console.WriteLine ("Bad input to routine splint");
            }
            a = (xx[khi] - x) / h;
            b = (x - xx[klo]) / h;
            y = a * yy[klo] + b * yy[khi] + ((a * a * a - a) * y2[klo] + (b * b * b - b) * y2[khi]) * (h * h) / 6.0f;
            return y;
        }
        
    }
}
