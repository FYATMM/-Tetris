using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
        public static class GameSetting
        {
            public static Random random = new Random(); // 随即数

            public static bool playing = false; // 玩家是否正在游戏
            public static bool extended = false; // 扩展方块
                                                 //布局
            public const int rowCount = 21; // 行数
            public const int colCount = 11; // 列数

            //小方块
            public static int brickWidth = 16; // 小块宽度
            public static int brickHeight = 16; // 小块高度

            //形状
            public static byte[,] points = new byte[colCount, rowCount]; // 点阵
            public static byte brickIndex = 0; // 模板序号
            public static byte facingIndex = 0; // 当前变化号
            public static byte afterBrickIndex = 0; // 下一个模板序号
            public static byte afterFacingIndex = 0; // 下一个变化号

            public static int lines; // 消行数

            //速度
            public static int level = 0; // 当前速度            
                                         /// 下落速度，数值表示每次下落的时间差，以毫秒为单位  
            public static int[] speeds = new int[] { 700, 500, 400, 300, 200, 200, 100, 80, 70, 60, 50 };

            //积分
            public static int score = 0; // 成绩 
            public static int[] scoress = new int[] { 0001, 0100, 0300, 0500, 1000, 2000 };/// 每次消除行所增加的积分

            //回放
            public static int stepIndex = -1; // 当前回放的步数
            public static bool reviewing = false; // 是否正在回放中
            public static int reviewSpeed = 1; // 回放的速度，数值表示倍数

            //记录
            public static int lastRecordTime = 0; // 最后记录的时间
            public static bool recordMode = false; // 是否采用记录模式

            public static int Lines { get { return lines; } }/// 消除的行数

            public static int Score { get { return score; } }/// 当前的积分

            public static int Level { get { return level; } } /// 当前的关数
        }
    }


