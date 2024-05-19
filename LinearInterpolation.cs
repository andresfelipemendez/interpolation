
namespace Interpolation
{
    public class LinearInterpolation : BaseInterpolation
    {
        public LinearInterpolation(float[] x, float[] y, int m) : 
            base(x, y, 2)
        {
        }

        public override float RawInterpolation(int j, float x)
        {
            if (xx[j] == xx[j + 1])
            {
                return yy[j];
            }
            else
            {
                return yy[j] + ((x - xx[j]) / (xx[j + 1] - xx[j])) * (yy[j + 1] - yy[j]);
            }
        }
    }
}
