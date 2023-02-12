namespace _2048;

public partial class MainPage : ContentPage
{
	static int rowMax = 4;
	static int columnMax = 4;
	Core2048 core;

	public MainPage()
	{
		InitializeComponent();

		core = new(rowMax, columnMax,draw);
        for (int i=0; i < rowMax; i++)
		{
			graph.RowDefinitions.Add(new RowDefinition() { });
		}
		for(int i=0; i < columnMax; i++)
		{
			graph.ColumnDefinitions.Add(new ColumnDefinition() {  });
		}
		//this.Loaded += (s, e) => { reSize(); };
		this.SizeChanged += (o, s) => { reSize(); };
		SwipeGestureRecognizer _left=new SwipeGestureRecognizer() { Direction=SwipeDirection.Left};
        SwipeGestureRecognizer _right = new SwipeGestureRecognizer() { Direction = SwipeDirection.Right };
        SwipeGestureRecognizer _up = new SwipeGestureRecognizer() { Direction = SwipeDirection.Up };
        SwipeGestureRecognizer _down = new SwipeGestureRecognizer() { Direction = SwipeDirection.Down };
		_left.Swiped += (o, e) => { left(); };
		_right.Swiped += (o, e) => { right(); };
		_up.Swiped += (o, e) => { up(); };
		_down.Swiped += (o, e) => { down(); };
		graph.GestureRecognizers.Add(_down);
		graph.GestureRecognizers.Add(_left);
		graph.GestureRecognizers.Add(_right);
		graph.GestureRecognizers.Add(_up);
		//autoResize();

		draw();
		reSize();
		entry.TextChanged += (o, e) => {
			if (e.NewTextValue == "")
			{
				return;
			}
			if (e.NewTextValue == "w")
			{
				up();
			}else if(e.NewTextValue == "a")
			{
				left();
			}else if (e.NewTextValue == "s")
			{
				down();
			}
			else if(e.NewTextValue=="d")
			{
				right();
			}
			entry.Text = "";
		};
		entry.Focus();
    }
	void left()
	{
		core.move(Direction.left);
	}
	void right()
	{
		core.move(Direction.right);
	}
	void up()
	{
		core.move(Direction.up);
	}
	void down()
	{
		core.move(Direction.down);
	}
	async void autoResize()
	{
		while(true)
		{
			await Task.Delay(1000);
			reSize();
		}
	}
	async void reSize()
	{
		double w = Width / columnMax;
		double h = (Height-score.Height-childLayout.Height) / rowMax;
		double r=Math.Min(w,h);
		if (r < 1)
		{
			await Task.Delay(100);
			reSize();
			return;
		}
		foreach(var i in graph.RowDefinitions)
		{
			i.Height = r;
		}
		foreach(var i in graph.ColumnDefinitions)
		{
			i.Width = r;
		}
	}

	private void draw()
	{
		graph.Children.Clear();
		score.Text = "Scores:"+core.getScore();
		for(int row = 0; row < rowMax; row++)
		{
			for(int column = 0; column < columnMax; column++)
			{
				var i = core.graph[row, column];
				if(i == 0)
				{
					continue;
				}
				Block block = new(i);
				Grid.SetRow(block, row);
				Grid.SetColumn(block, column);
				graph.Add(block);
			}
		}
		if (core.isDead())
		{
			DisplayAlert("O", "YOU DEAD", "OK");
		}
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		core.replay();
    }
}

