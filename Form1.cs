using Interpolation;
using System.Collections;

namespace WinFormsApp1
{
	public partial class Form1 : Form
	{
		readonly ScottPlot.WinForms.FormsPlot formsPlot1;
	
		public Form1()
		{
			InitializeComponent();
			
			// Add the FormsPlot
			formsPlot1 = new() { Dock = DockStyle.Fill };
			this.Controls.Add(formsPlot1);
			//float[] datax = new float[360];
			//float[] datay = new float[360];
			//for (int i = 0; i < 360; i++)
			//{
			//	datax[i] = (float)i;
			//	datay[i] = (float)i;
			//}

			float[] datax =
			{
				0,
				80,
				90,
				100,
				360
			};

			float[] datay =
			{
				0,
				85,
				90,
				95,
				360
			};

			var interpolation = new LinearInterpolation(datax, datay, 4);
			var dx = new List<float>();
            var dy = new List<float>();
            for (int i = 0;i < 360; i++)
			{
				if(i > 70 && i < 110)
				{
					continue;
				}

				dx.Add(i);
				dy.Add(interpolation.interpolate(i));
			}
			dx.Add(80);
			dy.Add(85);
			dx.Add(90);
			dy.Add(90);
			dx.Add(100);
			dy.Add(95);

			

			float[] ddx = dx.ToArray();
			float[] ddy = dy.ToArray();

			Array.Sort(ddx);
			Array.Sort(ddy);

			var pol = new SplineInterpolation(ddx, ddy);
			float[] res = new float[360];
			for (int i = 0; i < res.Length; i++) {
				res[i] = pol.interpolate(i);
			}

			formsPlot1.Plot.Add.Signal(res);
			formsPlot1.Refresh();
		}
	}
}
