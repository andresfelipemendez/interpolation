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

			// Add sample data to the plot
			double[] data = ScottPlot.Generate.Sin();
			formsPlot1.Plot.Add.Signal(data);
			formsPlot1.Refresh();
		}
	}
}
