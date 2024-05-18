using ClassLibrary1;

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
            float[] datax =
            {
                0, 5,10
            };
            float[] datay =
			{
				2.0f, 4.0f,6.0f
			};

			LinearInterpolation interpolation = new LinearInterpolation(datax, datay, 2);

			float[] res = new float[10];
			for (int i = 0; i < res.Length; i++) {
				res[i] = interpolation.interpolate(i);
			}

			formsPlot1.Plot.Add.Signal(res);
			formsPlot1.Refresh();
		}
	}
}
