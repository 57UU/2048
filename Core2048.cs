using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _2048;

public class Core2048
{

    public int[,] graph;
    public int rowMax;
    public int columnMax;
    ThreadStart draw;
    
    //public
    public Core2048(int row,int column,ThreadStart draw)
    {
        this.draw = draw;
        rowMax = row;
        columnMax = column;
        graph = new int[row,column];
        GenerateRandom();
        GenerateRandom();
    }
    public void replay()
    {
        graph = new int[rowMax, columnMax];
        GenerateRandom();
        GenerateRandom();
        draw();
    }
    private Random random = new();
    public int getScore()
    {
        return score;
    }
    public void GenerateRandom()
    {
        List<Point_int> points = new();
        for (int row = 0; row < rowMax; row++)
        {
            for (int column = 0; column < columnMax; column++)
            {
                if (get(row, column) == 0)
                {
                    points.Add(new Point_int(row, column));
                }
            }
        }
        if (points.Count == 0)
        {
            return;
        }
        int index = (int)(random.NextDouble()*points.Count);
        var p = points[index];
        set(p.x, p.y,intBlock());
    }
    public void move(Direction direction)
    {
        if (direction == Direction.down)
        {
            
            for(int column = 0; column < columnMax; column++)
            {
                int value = 0;
                int index=0;
                for (int row = rowMax - 1; row > -1; row--)
                {
                    int i = get(row, column);
                    if (i == 0)
                    {
                        continue;
                    }
/*                    if (value == 0)
                    {
                        value = i;
                    }*/
                    

                    if (value != i)
                    {
                        value = i;
                        index = row;
                        
                    }
                    else
                    {
                        doubleValue(index, column);
                        clean(row, column);
                    }
                }
                List<int> ints = new(); ;
                for (int row = rowMax-1; row >-1; row--)
                {
                    
                    var i = get(row, column);
                    if (i!= 0)
                    {
                        ints.Add(i);
                    }
                    
                }
                for(int row = rowMax - 1; row > -1; row--)
                {
                    int ListIndex = rowMax - row-1;
                    if (ListIndex < ints.Count)
                    {
                        set(row, column, ints[ListIndex]);
                    }
                    else
                    {
                        set(row, column, 0);
                    }
                }
            }
        }else if (direction == Direction.up)
        {
            for (int column = 0; column < columnMax; column++)
            {
                int value = 0;
                int index = 0;
                for (int row = 0; row < rowMax; row++)
                {
                    int i = get(row, column);
                    if (i == 0)
                    {
                        continue;
                    }
                    /*                    if (value == 0)
                                        {
                                            value = i;
                                        }*/
                    if (value != i)
                    {
                        value = i;
                        index = row;

                    }
                    else
                    {
                        doubleValue(index, column);
                        clean(row, column);
                    }
                }
                List<int> ints = new(); ;
                for (int row = 0; row < rowMax; row++)
                {

                    var i = get(row, column);
                    if (i != 0)
                    {
                        ints.Add(i);
                    }

                }
                for (int row = 0; row < rowMax; row++)
                {

                    if (row < ints.Count)
                    {
                        set(row, column, ints[row]);
                    }
                    else
                    {
                        set(row, column, 0);
                    }
                }
            }
        }else if (direction == Direction.left)
        {
            for(int row = 0; row < rowMax; row++)
            {
                int value = 0;
                int index = 0;
                for (int column = 0; column < columnMax; column++)
                {
                    int i = get(row, column);
                    if (i == 0)
                    {
                        continue;
                    }
                    /*                    if (value == 0)
                                        {
                                            value = i;
                                        }*/
                    if (value != i)
                    {
                        value = i;
                        index = column;

                    }
                    else
                    {
                        doubleValue(row, index);
                        clean(row, column);
                    }
                }
                List<int> ints = new();
                for(int column = 0; column < columnMax; column++)
                {
                    var i = get(row, column);
                    if (i != 0)
                    {
                        ints.Add(i);
                    }
                }
                for(int column = 0; column < columnMax; column++)
                {
                    if (column < ints.Count)
                    {
                        set(row, column, ints[column]);
                    }
                    else
                    {
                        set(row, column, 0);
                    }
                }
            }
        }
        else//right
        {
            for (int row = 0; row < rowMax; row++)
            {
                int value = 0;
                int index = 0;
                for (int column = columnMax-1; column >-1; column--)
                {
                    int i = get(row, column);
                    if (i == 0)
                    {
                        continue;
                    }
                    /*                    if (value == 0)
                                        {
                                            value = i;
                                        }*/
                    if (value != i)
                    {
                        value = i;
                        index = column;

                    }
                    else
                    {
                        doubleValue(row, index);
                        clean(row, column);
                    }
                }
                List<int> ints = new();
                for (int column = columnMax-1; column >-1; column--)
                {
                    var i = get(row, column);
                    if (i != 0)
                    {
                        ints.Add(i);
                    }
                }
                for (int column =columnMax-1; column >-1; column--)
                {
                    int ListIndex = columnMax - column - 1;
                    if (ListIndex < ints.Count)
                    {
                        set(row, column, ints[ListIndex]);
                    }
                    else
                    {
                        set(row, column, 0);
                    }
                }
            }
        }//direction
        GenerateRandom();
        draw();
    }//move()
    public bool isDead()
    {
        for (int column = 0; column < columnMax; column++)
        {
            int value = 0;
            for (int row = 0; row < rowMax; row++)
            {
                int i = get(row, column);
                if (i == 0)
                {
                    return false;
                }
                if (value == 0 )
                {
                    value = i;
                    continue;
                }
                if (i == value)
                {
                    return false;
                }
                value = i;
            }
        }
        for (int row = 0; row < rowMax; row++)
        {
            int value=0 ;
            for (int column = 0; column < columnMax ; column++)
            {
                int i = get(row, column);
                if (i == 0)
                {
                    return false;
                }
                if(value==0 )
                {
                    value = i;
                    continue;
                }
                if (i == value)
                {
                    return false;
                }
                value = i;

            }
        }
        return true;
    }






    private int intBlock()
    {
        int num =(int)( random.NextDouble() * 10);
        if (num < 4)
        {
            return 2;
        }else if (num < 8)
        {
            return 4;
        }
        return 8;
    }
    private int get(int row,int column)
    {
        return graph[row, column];
    }
    private void set(int row,int column,int value)
    {
        graph[row, column] = value;
    }
    private void clean(int row,int column)
    {
        set(row, column, 0);
    }
    private int score=0;
    private void doubleValue(int row,int column)
    {
        int value = get(row, column)*2;
        score += value;
        set(row, column, value);
    }

}
public  struct Point_int
{
    public int x, y;
    public Point_int(int x,int y)
    {
        this.x = x;this.y = y;
    }
}
public enum Direction { up,down,left,right}