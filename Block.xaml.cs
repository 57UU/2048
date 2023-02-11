namespace _2048;

public partial class Block : ContentView
{
	public Block(int num)
	{
		InitializeComponent();
		lable.Text = num.ToString();
	}
}