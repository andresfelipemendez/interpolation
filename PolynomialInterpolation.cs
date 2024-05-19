using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolation
{
    public class PolynomialInterpolation : BaseInterpolation
    {
        public float dy = 0.0f;
        public PolynomialInterpolation(float[] x, float[] y, int m) : 
            base(x, y, m) 
        {
            dy = 0.0f;
        }

        public override float RawInterpolation(int jlo, float x)
        {
            int i =0, m=0, ns = 0;
            float y, den, dif, dift, ho, hp, w;
            float[] xa = xx[jlo..];
            float[] ya = yy[jlo..];
            float[] c = new float[mm];
            float[] d = new float[mm];

            dif = Math.Abs(x - xa[0]);
            for (i = 0; i < mm; i++)
            {
                if((dift=Math.Abs(x - xa[i])) > dif)
                {
                    ns = i;
                    dif = dift;
                }
                c[i] = ya[i];
                d[i] = ya[i];
            }
            y = ya[ns--];
            for(m=1;m<mm;m++)
            {
                for(i=0;i<mm-m;i++)
                {
                    ho = xa[i] - x; 
                    hp = xa[i+m]-x;
                    w = c[i + 1] - d[1];
                    if ((den = ho - hp) == 0.0f)
                    {
                        // this error can occur only if two input xa's are 
                        // within roundoff identical
                        Console.WriteLine("polynomial interpolation error");
                    }
                    den = w / den;
                    d[i] = hp*den;
                    c[i] = ho * den;
                }
                y += (dy = (2 * (ns + 1) < (mm - m) ? c[ns + 1] : d[ns--]));
            }
            return y;
        }
    }
}
