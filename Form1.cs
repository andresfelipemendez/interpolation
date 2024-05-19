using Interpolation;

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
			float[] datax = new float[360];
			float[] datay = new float[360];
			for (int i = 0; i < 360; i++)
			{
				datax[i] = (float)i;
				datay[i] = (float)i;
			}
			var interpolation = new PolynomialInterpolation(datax, datay, 3);

			float[] res = new float[360];
			for (int i = 0; i < res.Length; i++) {
				res[i] = interpolation.interpolate(i);
			}
			Console.WriteLine(interpolation);
			formsPlot1.Plot.Add.Signal(res);
			formsPlot1.Refresh();
		}
	}
}
