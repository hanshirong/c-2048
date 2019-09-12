using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smallgame
{
    class Logic
    {
        Map map1 = new Map();
        private string[] drewsNums = new string[16];
        public string[] DrewsNums
        {
            get { return Drew(); }
            set { drewsNums = value; }
        }
        public string[] Drew()
        {
            map1.ShowStr = Map.Nums;
            string[] arr = new string[16];
            int box = 0;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    arr[box] = map1.ShowStr[map1.Pos[i, j]];
                    box += 1;
                }
            }
            return arr;

        }
        //随机产生一个数 产生数为2或者4
        public void Create()
        {
            int i;
            int j;
            int num;
            Random ran = new Random();
            while (true)
            {
                i = ran.Next(0, 4);//0 1 2 3
                j = ran.Next(0, 4);//0 1 2 3
                num = ran.Next(1, 3);//1 2 即2或者4
                if (map1.Pos[i, j] == 0)
                {
                    map1.Pos[i, j] = num;
                    return;
                }
            }
   
        }
        //判断是否失败
        public bool Lose()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (map1.Pos[i, j] == 0)//有空的格子
                    {
                        return false;
                    }
                    else
                    {
                        if (i == 0 && j == 0)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i + 1, j] || map1.Pos[i, j] == map1.Pos[i, j + 1])//有可合并的
                            {
                                return false;
                            }
                        }
                        else if (i == 3 && j == 0)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i - 1, j] || map1.Pos[i, j] == map1.Pos[i, j + 1])
                            {
                                return false;
                            }
                        }
                        else if (i == 0 && j == 3)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i + 1, j] || map1.Pos[i, j] == map1.Pos[i, j - 1])
                            {
                                return false;
                            }
                        }
                        else if (i == 3 && j == 3)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i - 1, j] || map1.Pos[i, j] == map1.Pos[i, j - 1])
                            {
                                return false;
                            }
                        }
                        else if (i != 0 && i != 3 && j != 0 && j != 3)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i + 1, j] || map1.Pos[i, j] == map1.Pos[i - 1, j] || map1.Pos[i, j] == map1.Pos[i, j + 1] || map1.Pos[i, j] == map1.Pos[i, j - 1])
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
        }
        //再来一次
        public void ReGame()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    map1.Pos[i, j] = 0;//清零
                }
            }
            Create();
        }
        public void RunLose()
        {
            if (Lose())
            {
                DialogResult dr = MessageBox.Show("你输了", "结果", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (dr == DialogResult.OK)//确定
                {
                    ReGame();//再来一次
                }
            }
            else
            {
                for(int i = 0; i < 4; i++)
                {
                    for(int j = 0; j < 4; j++)
                    {
                        if (map1.Pos[i, j] == 11)//有1024
                        {
                            MessageBox.Show("你赢了");
                        }
                    }
                }
            }
        }
        //数字上移事件
        public bool CanUp()
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((map1.Pos[i, j] != 0 && map1.Pos[i - 1, j] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i - 1, j]))
                    {
                        return true;

                    }
                }
            }
            return false;
        }
        //左移
        public bool CanLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if ((map1.Pos[i, j] != 0 && map1.Pos[i, j - 1] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i, j - 1]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //右移
        public bool CanRight()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((map1.Pos[i, j] != 0 && map1.Pos[i, j + 1] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i, j + 1]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //下移
        public bool CanDown()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((map1.Pos[i, j] != 0 && map1.Pos[i + 1, j] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i + 1, j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //上事件
        public void Up()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map1.Pos[i, j] != 0)
                    {
                        for (int k = i + 1; k < 4; k++)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[k, j])
                                {
                                    map1.Pos[i, j] += 1;//增加
                                    map1.Pos[k, j] = 0;//清零
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = i + 1; k < 4; k++)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == 0)
                                {
                                    map1.Pos[i, j] = map1.Pos[k, j];
                                    map1.Pos[k, j] = 0;
                                }
                                for (int l = k + 1; l < 4; l++)
                                {
                                    if (map1.Pos[l, j] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[l, j])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[l, j] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        //左事件
        public void Left()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (map1.Pos[i, j] != 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[i, k])
                                {
                                    map1.Pos[i, j] += 1;
                                    map1.Pos[i, k] = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] == 0)
                                {
                                    map1.Pos[i, j] = map1.Pos[i, k];
                                    map1.Pos[i, k] = 0;
                                }

                                for (int l = k + 1; l < 4; l++)
                                {
                                    if (map1.Pos[i, l] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[i, l])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[i, l] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        //右事件
        public void Right()
        {
            for (int j = 3; j > 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (map1.Pos[i, j] != 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[i, k])
                                {
                                    map1.Pos[i, j] += 1;
                                    map1.Pos[i, k] = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] == 0)
                                {
                                    map1.Pos[i, j] = map1.Pos[i, k];
                                    map1.Pos[i, k] = 0;
                                }

                                for (int l = k - 1; l >= 0; l--)
                                {
                                    if (map1.Pos[i, l] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[i, l])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[i, l] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        //下事件
        public void Down()
        {
            for (int i = 3; i > 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map1.Pos[i, j] != 0)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[k, j])
                                {
                                    map1.Pos[i, j] += 1;
                                    map1.Pos[k, j] = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == 0)
                                {
                                    map1.Pos[i, j] = map1.Pos[k, j];
                                    map1.Pos[k, j] = 0;
                                }

                                for (int l = k - 1; l >= 0; l--)
                                {
                                    if (map1.Pos[l, j] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[l, j])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[l, j] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }



    }
}
