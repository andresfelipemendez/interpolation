using System;

namespace Interpolation
{

    public abstract class BaseInterpolation
    {
        public int N, mm, jsav, cor, dj;
        public float[] xx, yy;

        public BaseInterpolation(float[] x, float[] y, int m)
        {
            N = x.Length;
            mm = m;
            jsav = 0;
            cor = 0;
            xx = x;
            yy = y;
            dj = Math.Max(1, (int)Math.Pow(N, 0.25f));
        }

        public int locate(float x)
        {
            int ju, jm, jl;
            if (N < 2 || mm < 2 || mm > N)
            {
                //Debug.LogError("locate size error");
                Console.WriteLine("locate size error");
            }

            // might be redundant I expect the input to be ascendant from 1 to 360
            bool ascnd = xx[N - 1] >= xx[0];
            jl = 0;
            ju = N - 1;
            while (ju - jl > 1)
            {
                jm = (ju + jl) >> 1;
                if ((x >= xx[jm]) == ascnd)
                {
                    jl = jm;
                }
                else
                {
                    ju = jm;
                }
            }
            cor = Math.Abs(jl - jsav) > dj ? 0 : 1;
            jsav = jl;
            return Math.Max(0, Math.Min(N - mm, jl - ((mm - 2) >> 1)));
        }

        public int hunt(float x)
        {
            int jl = jsav, jm, ju, inc = 1;
            if (N < 2 || mm < 2 || mm > N)
            {
                Console.WriteLine("hunt size error");
            }
            bool ascnd = (xx[N - 1] >= xx[0]);

            if (jl < 0 || jl > N - 1) // skip hunt, go to bisect
            {
                jl = 0;
                ju = N - 1;
            }
            else
            {
                if (x >= xx[jl] == ascnd)
                {
                    for (; ; )
                    {
                        ju = jl + inc;
                        if (ju >= N - 1)
                        {
                            ju = N - 1; break; // end of table
                        }
                        else if (x < xx[ju] == ascnd)
                        {
                            break; // found bracket
                        }
                        else
                        {
                            // continue searching, double step size
                            jl = ju;
                            inc += inc;
                        }
                    }
                }
                else
                {
                    ju = jl;
                    for (; ; )
                    {
                        jl = jl - inc;
                        if (jl <= 0)
                        {
                            // beginning of table
                            jl = 0; break;
                        }
                        else if (x >= xx[jl] == ascnd)
                        {
                            // found bracket, begin bisect
                            break;
                        }
                        else
                        {
                            // continue searching, double step size
                            ju = jl;
                            inc += inc;
                        }
                    }
                }
            }
            // bisection, binary search
            while (ju - jl > 1)
            {
                // bit shift is division
                jm = (ju + jl) >> 1;
                if (x >= xx[jm] == ascnd)
                {
                    jl = jm;
                }
                else
                {
                    ju = jm;
                }
            }
            // set flag to choose hunt or locate next time
            cor = Math.Abs(jl - jsav) > dj ? 0 : 1;
            jsav = jl;
            return Math.Max(0, Math.Min(N - mm, jl - ((mm - 2) >> 1)));
        }
        public float interpolate(float x)
        {
            int jlo = cor == 1 ? hunt(x) : locate(x);
           // return locate(x);
            return RawInterpolation(jlo, x);
        }

        public abstract float RawInterpolation(int jlo, float x);
    }

}
