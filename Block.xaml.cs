namespace _2048;

public partial class Block : ContentView
{
	static string[] blockColors =new string[] {"#F09F8D","#FFE4C4", "#ffb366", 
		"#f4a460", "#ff7300", "#cc5500", "#ffff99",
		"#ffff4d", "#ffd700", "#e6b800", "#b88608" };
	static Color[] realBlockColor ;
	static Block()
	{
        realBlockColor = new Color[blockColors.Length];
        for (int i = 0; i < blockColors.Length; i++)
        {
            realBlockColor[i] = Color.FromRgba(blockColors[i]);
        }
    }
	public Block(int num)
	{
		InitializeComponent();

		lable.Text = num.ToString();

#if ANDROID
		lable.TextColor = Colors.Violet;

		
#endif
		
		int value = 2;
		int i=0;
		for(; i < blockColors.Length;i++)
		{
			
			if(value==num)
			{
				break;
			}
            value *= 2;
        }
		frame.BackgroundColor = realBlockColor[i];
		
    }
}