using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Engine;

namespace 俄罗斯方块Tetris
{
    public partial class youxiControl : Control
    {
        #region 字段
        /*
        //private const int rowCount = 21; // 行数
        //private const int colCount = 11; // 列数

        //private int brickWidth = 16; // 小块宽度
        //private int brickHeight = 16; // 小块高度

        //private byte[,] points = new byte[colCount, rowCount]; // 点阵
        //private byte brickIndex = 0; // 模板序号
        //private byte facingIndex = 0; // 当前变化号

        //private byte afterBrickIndex = 0; // 下一个模板序号
        //private byte afterFacingIndex = 0; // 下一个变化号
        //private int lines; // 消行数
        //private Random random = new Random(); // 随即数
        //private int level = 0; // 当前速度
        //private int score = 0; // 成绩        
        ///// 下落速度，数值表示每次下落的时间差，以毫秒为单位  
        //private int[] speeds = new int[] { 700, 500, 400, 300, 200, 200, 100, 80, 70, 60, 50 };     
        //private int[] scoress = new int[] { 0001, 0100, 0300, 0500, 1000, 2000 };/// 每次消除行所增加的积分
        //private bool playing = false; // 玩家是否正在游戏
        //private int stepIndex = -1; // 当前回放的步数
        //private bool reviewing = false; // 是否正在回放中
        //private int reviewSpeed = 1; // 回放的速度，数值表示倍数
        //private int lastRecordTime = 0; // 最后记录的时间
        //private bool recordMode = false; // 是否采用记录模式
        //private bool extended = false; // 扩展方块
        */

        public static ImageList imageList; // 方块素材
        public static Bitmap backBitmap; // 背景图片

        public static List<List<List<Point>>> brickTemplets = new List<List<List<Point>>>(); // 方块模板[模板序号，朝向]
        public static Point brickPoint = new Point(); // 方块的位子
        public static System.Windows.Forms.Timer timer; // 控制下落的时间器
        public static Thread threadReview = null; // 回放使用的线程
        public static List<StepInfo> StepInfos = new List<StepInfo>(); // 记录玩家每一步的操作

        public static ProgressBar progressBar; // 回放进度条
        private youxiNext youxiNext; // 下一个方块的显示控件
        private youxiScore youxiScore; // 积分显示控件
        #endregion

        #region   属性
        ///方块的操作
        public enum BrickOperates
        {
            boMoveLeft = 0, // 左移
            boMoveRight = 1, // 右移
            boMoveDown = 2, // 下移
            boMoveBottom = 3, // 直下
            boTurnLeft = 4, // 左旋
            boTurnRight = 5, // 右旋
        }

        /// 一个步骤地信息
        public struct StepInfo
        {
            public byte command; // 执行的命令 0: 初始方块 1:得到下一个形状 2:移动方块
            public ushort timeTick; // 该步骤花掉的时间
            public byte param1; // 参数1
            public byte param2; // 参数2
            public StepInfo(int ATimeTick, byte ACommand, byte AParam1, byte AParam2)
            {
                timeTick = (ushort)ATimeTick;
                command = ACommand;
                param1 = AParam1;
                param2 = AParam2;
            }
        }
        #endregion

        #region 构造函数
        public youxiControl()
        {
            //InitializeComponent();
            Width = GameSetting.colCount * GameSetting.brickWidth;
            Height = GameSetting.rowCount * GameSetting.brickHeight;
            BackColor = Color.Black;
            NewTemplets(false);

            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Selectable, true);

            GameSetting.afterBrickIndex = (byte)GameSetting.random.Next(brickTemplets.Count);
            GameSetting.afterFacingIndex = (byte)GameSetting.random.Next(brickTemplets[GameSetting.afterBrickIndex].Count);
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            DoChange();
            GameOver();
        }
        #endregion

        #region 方法
        private void NewTemplets(bool AExtended)
        {
            GameSetting.extended = AExtended;
            List<List<Point>> templets;
            List<Point> bricks;

            #region 添加默认方块模板数据
            brickTemplets.Clear();
            //添加正方形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][10][  ][  ]
            bricks.Add(new Point(1, 0)); //[01][11][  ][  ]
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            brickTemplets.Add(templets);

            //添加T形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(0, 1)); //[01][11][21][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][11][21][  ]
            bricks.Add(new Point(2, 1)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11][21][  ]
            bricks.Add(new Point(2, 1)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(0, 1)); //[01][11][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //添加左L形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][11][21][  ]
            bricks.Add(new Point(1, 2)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 3)); //[  ][13][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11][21][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][22][  ]
            bricks.Add(new Point(2, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(2, 0)); //[  ][  ][20][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][21][  ]
            bricks.Add(new Point(1, 2)); //[  ][12][22][  ]
            bricks.Add(new Point(2, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][11][  ][  ]
            bricks.Add(new Point(2, 2)); //[  ][12][22][32]
            bricks.Add(new Point(3, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //添加右L形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][11][21][  ]
            bricks.Add(new Point(2, 2)); //[  ][  ][22][  ]
            bricks.Add(new Point(2, 3)); //[  ][  ][23][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(0, 2)); //[  ][  ][21][  ]
            bricks.Add(new Point(1, 2)); //[02][12][22][  ]
            bricks.Add(new Point(2, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][11][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][12][22][  ]
            bricks.Add(new Point(2, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][11][21][31]
            bricks.Add(new Point(3, 1)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //添加直条
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][11][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 3)); //[  ][13][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11][21][31]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(3, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //添加左Z
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11][  ][  ]
            bricks.Add(new Point(0, 1)); //[02][  ][  ][  ]
            bricks.Add(new Point(0, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][10][  ][  ]
            bricks.Add(new Point(1, 0)); //[  ][11][21][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //添加右Z
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][  ][  ][  ]
            bricks.Add(new Point(0, 1)); //[01][11][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][20][  ]
            bricks.Add(new Point(2, 0)); //[01][11][  ][  ]
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);
            #endregion 添加默认方块模板数据
            if (!AExtended) return;
            #region 添加扩展方块模板数据
            //扩充"."形
            templets = new List<List<Point>>();
            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充"-"形
            templets = new List<List<Point>>();
            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10]
            bricks.Add(new Point(1, 1)); //[  ][11]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 1)); //[  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充示范 "v"形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(0, 1)); //[01][11][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][11][21][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][11][21][  ]
            bricks.Add(new Point(1, 2)); //[  ][12][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][12][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充示范 "|"形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][11][  ][  ]
            bricks.Add(new Point(1, 2)); //[  ][12][  ][  ]
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11][21][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充示范 "E"形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][  ][20][  ]
            bricks.Add(new Point(2, 0)); //[01][11][21][  ]
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 1));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][20][  ]
            bricks.Add(new Point(2, 0)); //[  ][11][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][12][22][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 2));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 1)); //[01][11][21][  ]
            bricks.Add(new Point(2, 1)); //[02][  ][22][  ]
            bricks.Add(new Point(0, 2)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 2));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][10][  ][  ]
            bricks.Add(new Point(1, 0)); //[  ][11][  ][  ]
            bricks.Add(new Point(1, 1)); //[02][12][  ][  ]
            bricks.Add(new Point(0, 2)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 2));
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充示范"T"形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][10][20][  ]
            bricks.Add(new Point(1, 0)); //[  ][11][  ][  ]
            bricks.Add(new Point(2, 0)); //[  ][12][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 2));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(2, 0)); //[  ][  ][20][  ]
            bricks.Add(new Point(0, 1)); //[01][11][21][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][22][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 2));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][11][  ][  ]
            bricks.Add(new Point(0, 2)); //[02][12][22][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 2));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][  ][  ][  ]
            bricks.Add(new Point(0, 1)); //[01][11][21][  ]
            bricks.Add(new Point(1, 1)); //[02][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(0, 2));
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充示范左"Z"形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][10][  ][  ]
            bricks.Add(new Point(1, 0)); //[  ][11][  ][  ]
            bricks.Add(new Point(1, 1)); //[  ][12][22][  ]
            bricks.Add(new Point(1, 2)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 2));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(2, 0)); //[  ][  ][20][  ]
            bricks.Add(new Point(0, 1)); //[01][11][21][  ]
            bricks.Add(new Point(1, 1)); //[02][  ][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(0, 2));
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充示范右"Z"形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][20][  ]
            bricks.Add(new Point(2, 0)); //[  ][11][  ][  ]
            bricks.Add(new Point(1, 1)); //[02][12][  ][  ]
            bricks.Add(new Point(0, 2)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 2));
            templets.Add(bricks);

            bricks = new List<Point>();
            bricks.Add(new Point(0, 0)); //[00][  ][  ][  ]
            bricks.Add(new Point(0, 1)); //[01][11][21][  ]
            bricks.Add(new Point(1, 1)); //[  ][  ][22][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(2, 2));
            templets.Add(bricks);
            brickTemplets.Add(templets);

            //扩充示范 "+"形
            templets = new List<List<Point>>();

            bricks = new List<Point>();
            bricks.Add(new Point(1, 0)); //[  ][10][  ][  ]
            bricks.Add(new Point(0, 1)); //[01][11][21][  ]
            bricks.Add(new Point(1, 1)); //[  ][12][  ][  ]
            bricks.Add(new Point(2, 1)); //[  ][  ][  ][  ]
            bricks.Add(new Point(1, 2));
            templets.Add(bricks);
            brickTemplets.Add(templets);
            #endregion 添加扩展方块模板数据
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            BrickOperate(BrickOperates.boMoveDown);
        }

        /// <summary>
        /// 从新开始游戏
        /// </summary>
        /// <param name="ARecordMode">是否记录玩家的操作</param>
        public void Replay(bool ARecordMode, bool AExtended)
        {
            if (threadReview != null)
            {
                threadReview.Abort();
                threadReview = null;
            }
            if (AExtended != GameSetting.extended)
                NewTemplets(AExtended);

            GameSetting.reviewing = false;
            GameSetting.playing = true;
            GameSetting.recordMode = ARecordMode;
            Clear();
            StepInfos.Clear();
            GameSetting.afterBrickIndex = (byte)GameSetting.random.Next(brickTemplets.Count);
            GameSetting.afterFacingIndex = (byte)GameSetting.random.Next(brickTemplets[GameSetting.afterBrickIndex].Count);
            if (youxiNext != null) youxiNext.Update(this);
            if (GameSetting.recordMode && !GameSetting.reviewing)
            {
                StepInfos.Add(new StepInfo(0, 0, GameSetting.afterBrickIndex, GameSetting.afterFacingIndex));
                GameSetting.lastRecordTime = Environment.TickCount;
            }
            GameSetting.level = 0;
            GameSetting.score = 0;
            GameSetting.lines = 0;
            GameSetting.stepIndex = -1;
            if (progressBar != null) progressBar.Value = 0;
            NextBrick();
            timer.Interval = GameSetting.speeds[GameSetting.level];
            timer.Enabled = true;
            if (youxiScore != null) youxiScore.Update();//if (youxiScore != null) youxiScore.Update(this);
            if (CanFocus) Focus();
        }

        /// <summary>
        /// 回放到下一步
        /// </summary>
        public void NextStep()
        {
            if (GameSetting.stepIndex < 0) return;
            if (GameSetting.stepIndex >= StepInfos.Count) return;
            switch (StepInfos[GameSetting.stepIndex].command)
            {
                case 0:
                    GameSetting.afterBrickIndex = StepInfos[GameSetting.stepIndex].param1;
                    GameSetting.afterFacingIndex = StepInfos[GameSetting.stepIndex].param2;
                    break;
                case 1:
                    GameSetting.brickIndex = GameSetting.afterBrickIndex;
                    GameSetting.facingIndex = GameSetting.afterFacingIndex;
                    brickPoint.X = GameSetting.colCount / 2 - 1;
                    brickPoint.Y = 0;

                    GameSetting.afterBrickIndex = StepInfos[GameSetting.stepIndex].param1;
                    GameSetting.afterFacingIndex = StepInfos[GameSetting.stepIndex].param2;
                    if (youxiNext != null && GameSetting.afterBrickIndex != GameSetting.brickIndex)
                        youxiNext.Update(this);
                    if (youxiScore != null)
                        youxiScore.Update(); //youxiScore.Update(this);
                    DrawCurrent(Graphics.FromImage(backBitmap), false);
                    Invalidate();
                    break;
                case 2:
                    BrickOperate((BrickOperates)StepInfos[GameSetting.stepIndex].param1);
                    Invalidate();
                    break;
                case 3:
                    GameOver();
                    Invalidate();
                    break;
            }
            GameSetting.stepIndex++;
        }

        /// <summary>
        /// 回放速度
        /// </summary>
        public int ReviewSpeed
        {
            set
            {
                GameSetting.reviewSpeed = value > 0 ? value : 1;
            }
            get
            {
                return GameSetting.reviewSpeed;
            }
        }

        /// <summary>
        /// 执行回放操作
        /// </summary>
        private void DoReview()
        {
            while (GameSetting.reviewing)
            {
                if (GameSetting.stepIndex < 0 || GameSetting.stepIndex >= StepInfos.Count)
                {
                    GameSetting.reviewing = false;
                    break;
                }
                Thread.Sleep((int)((double)StepInfos[GameSetting.stepIndex].timeTick / GameSetting.reviewSpeed));
                if (!GameSetting.reviewing) break;

                Invoke(new EventHandler(DoInvoke));
            }
            Invoke(new EventHandler(DoInvoke));
            threadReview = null;
        }

        /// <summary>
        /// 在线程中控制界面用
        /// </summary>
        private void DoInvoke(object sender, EventArgs e)
        {
            if (GameSetting.reviewing)
            {
                NextStep();
                if (progressBar != null) progressBar.Value = GameSetting.stepIndex;
            }
            else if (GameSetting.playing)
            {
                if (progressBar != null) progressBar.Value = 0;
                timer.Enabled = true;
                GameSetting.lastRecordTime = Environment.TickCount;
                if (CanFocus) Focus();
            }
        }

        /// <summary>
        /// 回放历史
        /// </summary>
        public void Review()
        {
            if (threadReview != null)
            {
                threadReview.Abort();
                threadReview = null;
            }
            timer.Enabled = false;
            GameSetting.reviewing = true;
            GameSetting.playing = true;
            Clear();
            GameSetting.level = 0;
            GameSetting.score = 0;
            GameSetting.lines = 0;
            GameSetting.stepIndex = 0;
            NextStep();
            NextStep();
            if (progressBar != null)
                progressBar.Maximum = StepInfos.Count;
            threadReview = new Thread(new ThreadStart(DoReview));
            threadReview.Start();
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        public void GameOver()
        {
            GameSetting.playing = false;
            timer.Enabled = false;
        }

        /// <summary>
        /// 处理重要信息变化
        /// </summary>
        public void DoChange()
        {
            Width = GameSetting.brickWidth * GameSetting.colCount;
            Height = GameSetting.brickHeight * GameSetting.rowCount;
            backBitmap = new Bitmap(Width, Height);
            Graphics vGraphics = Graphics.FromImage(backBitmap);
            vGraphics.FillRectangle(new SolidBrush(BackColor), vGraphics.ClipBounds);
            DrawPoints(vGraphics);
            DrawCurrent(vGraphics, false);
            Invalidate();
        }

        /// <summary>
        /// 清空画面和点阵信息
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < GameSetting.colCount; i++)
                for (int j = 0; j < GameSetting.rowCount; j++)
                    GameSetting.points[i, j] = 0;
            Graphics vGraphics = Graphics.FromImage(backBitmap);
            vGraphics.FillRectangle(new SolidBrush(BackColor), vGraphics.ClipBounds);
        }

        /// <summary>
        /// 从文件中载入玩家操作记录
        /// </summary>
        /// <param name="AFileName">文件名</param>
        public void LoadFromFile(string AFileName)
        {
            if (!File.Exists(AFileName)) return;
            FileStream vFileStream = new FileStream(AFileName,
                FileMode.Open, FileAccess.Read);
            LoadFromStream(vFileStream);
            vFileStream.Close();
        }

        /// <summary>
        /// 从流中载入玩家操作记录
        /// </summary>
        /// <param name="AStream">流</param>
        public void LoadFromStream(Stream AStream)
        {
            if (AStream == null) return;
            byte[] vBuffer = new byte[3];
            if (AStream.Read(vBuffer, 0, vBuffer.Length) != 3) return;
            if (vBuffer[0] != 116 || vBuffer[1] != 114 || vBuffer[2] != 102) return;
            if (GameSetting.colCount != (byte)AStream.ReadByte()) return;
            if (GameSetting.rowCount != (byte)AStream.ReadByte()) return;
            if (threadReview != null)
            {
                threadReview.Abort(); // 如果正在回放
                threadReview = null;
            }
            timer.Enabled = false;
            GameSetting.playing = false;
            GameSetting.reviewing = false;
            brickTemplets.Clear();
            if (progressBar != null) progressBar.Value = 0;
            int vTempletsCount = AStream.ReadByte();
            for (int i = 0; i < vTempletsCount; i++)
            {
                List<List<Point>> templets = new List<List<Point>>();

                int vPointsLength = AStream.ReadByte();
                for (int j = 0; j < vPointsLength; j++)
                {
                    List<Point> bricks = new List<Point>();
                    int vPointCount = AStream.ReadByte();
                    for (int k = 0; k < vPointCount; k++)
                    {
                        int vData = AStream.ReadByte();
                        if (vData < 0) break;

                        bricks.Add(new Point(vData & 3, vData >> 4 & 3));
                    }
                    templets.Add(bricks);
                }
                brickTemplets.Add(templets);
            }
            StepInfos.Clear();
            vBuffer = new byte[sizeof(int)];
            if (AStream.Read(vBuffer, 0, vBuffer.Length) != vBuffer.Length) return;
            int vStepCount = BitConverter.ToInt32(vBuffer, 0);

            for (int i = 0; i < vStepCount; i++)
            {
                StepInfo vStepInfo = new StepInfo();
                vStepInfo.param1 = (byte)AStream.ReadByte();
                vBuffer = new byte[sizeof(ushort)];
                if (AStream.Read(vBuffer, 0, vBuffer.Length) != vBuffer.Length) return;
                vStepInfo.timeTick = (ushort)BitConverter.ToInt16(vBuffer, 0);
                int vData = AStream.ReadByte();
                vStepInfo.command = (byte)(vData & 3);
                vStepInfo.param2 = (byte)(vData >> 4 & 3);
                StepInfos.Add(vStepInfo);
            }
            Clear();
            Invalidate();
        }

        /// <summary>
        /// 将玩家操作记录保存到文件中
        /// </summary>
        /// <param name="AFileName">文件名</param>
        public void SaveToFile(string AFileName)
        {
            FileStream vFileStream = new FileStream(AFileName,
                FileMode.Create, FileAccess.Write);
            SaveToStream(vFileStream);
            vFileStream.Close();
        }

        /// <summary>
        /// 将玩家操作记录保存到流中
        /// </summary>
        /// <param name="AStream"></param>
        public void SaveToStream(Stream AStream)
        {
            if (AStream == null) return;
            byte[] vBuffer = Encoding.ASCII.GetBytes("trf");
            AStream.Write(vBuffer, 0, vBuffer.Length); // 写头信息
            AStream.WriteByte((byte)GameSetting.colCount);
            AStream.WriteByte((byte)GameSetting.rowCount);
            byte vByte = (byte)brickTemplets.Count;
            AStream.WriteByte(vByte);
            foreach (List<List<Point>> vList in brickTemplets)
            {
                vByte = (byte)vList.Count;
                AStream.WriteByte(vByte);
                foreach (List<Point> vPoints in vList)
                {
                    vByte = (byte)vPoints.Count;
                    AStream.WriteByte(vByte);
                    foreach (Point vPoint in vPoints)
                    {
                        vByte = (byte)(vPoint.Y << 4 | vPoint.X);
                        AStream.WriteByte(vByte);
                    }
                }
            }
            AStream.Write(BitConverter.GetBytes(StepInfos.Count), 0, sizeof(int));
            foreach (StepInfo vStepInfo in StepInfos)
            {
                AStream.WriteByte(vStepInfo.param1);
                AStream.Write(BitConverter.GetBytes(vStepInfo.timeTick), 0, sizeof(ushort));
                vByte = (byte)(vStepInfo.param2 << 4 | vStepInfo.command);
                AStream.WriteByte(vByte);
            }
        }

        /// <summary>
        /// 绘制一个点的方块
        /// </summary>
        /// <param name="AGraphics">绘制的图像</param>
        /// <param name="APoint">绘制的坐标</param>
        /// <param name="ABrick">绘制的方块图案，如果为0则表示清除</param>
        public void DrawPoint(Graphics AGraphics, Point APoint, byte ABrick)
        {
            if (ImageList == null) return;
            if (ImageList.Images.Count <= 0) return;
            if (APoint.X < 0 || APoint.X >= GameSetting.colCount) return;
            if (APoint.Y < 0 || APoint.Y >= GameSetting.rowCount) return;

            Rectangle vRectangle = new Rectangle(
                APoint.X * GameSetting.brickWidth, APoint.Y * GameSetting.brickHeight,
                GameSetting.brickWidth, GameSetting.brickHeight);
            AGraphics.FillRectangle(new SolidBrush(BackColor), vRectangle);
            if (ABrick <= 0) return;
            ABrick = (byte)((ABrick - 1) % ImageList.Images.Count);
            Image vImage = ImageList.Images[ABrick];
            AGraphics.DrawImage(vImage, vRectangle.Location);
        }

        /// <summary>
        /// 从新绘制整个点阵
        /// </summary>
        /// <param name="AGraphics">绘制的图像</param>
        public void DrawPoints(Graphics AGraphics)
        {
            if (ImageList == null) return;
            if (ImageList.Images.Count <= 0) return;
            for (int i = 0; i < GameSetting.colCount; i++)
                for (int j = 0; j < GameSetting.rowCount; j++)
                    DrawPoint(AGraphics, new Point(i, j), GameSetting.points[i, j]);
        }

        /// <summary>
        /// 绘制当前被控制的方块
        /// </summary>
        /// <param name="AGraphics">所要绘制的图像</param>
        /// <param name="AClear">是否采用清除绘制</param>
        public void DrawCurrent(Graphics AGraphics, bool AClear)
        {
            if (ImageList == null) return;
            if (ImageList.Images.Count <= 0) return;
            foreach (Point vPoint in brickTemplets[GameSetting.brickIndex][GameSetting.facingIndex])
                DrawPoint(AGraphics, new Point(vPoint.X + brickPoint.X,
                    vPoint.Y + brickPoint.Y),
                    AClear ? (byte)0 : (byte)(GameSetting.brickIndex + 1));
        }

        /// <summary>
        /// 绘制下一个出现的方块
        /// </summary>
        /// <param name="AGraphics">绘制的图像</param>
        public void DrawNext(Graphics AGraphics)
        {
            if (AGraphics == null) return;
            if (ImageList == null) return;
            if (ImageList.Images.Count <= 0) return;
            foreach (Point vPoint in brickTemplets[GameSetting.afterBrickIndex][GameSetting.afterFacingIndex])
                DrawPoint(AGraphics, new Point(vPoint.X, vPoint.Y),
                    (byte)(GameSetting.afterBrickIndex + 1));
        }

        /// <summary>
        /// 检查方块是否可以移动或变化到此位置
        /// </summary>
        /// <param name="ABrickIndex">模板序号</param>
        /// <param name="AFacingIndex">朝向序号</param>
        /// <param name="ABrickPoint">位置坐标</param>
        /// <returns>返回是否可以移动</returns>
        public bool CheckBrick(byte ABrickIndex, byte AFacingIndex, Point ABrickPoint)
        {
            foreach (Point vPoint in brickTemplets[ABrickIndex][AFacingIndex])
            {
                if (vPoint.X + ABrickPoint.X < 0 ||
                    vPoint.X + ABrickPoint.X >= GameSetting.colCount) return false;
                if (vPoint.Y + ABrickPoint.Y < 0 ||
                    vPoint.Y + ABrickPoint.Y >= GameSetting.rowCount) return false;
                if (GameSetting.points[vPoint.X + ABrickPoint.X,
                    vPoint.Y + ABrickPoint.Y] != 0) return false;
            }
            return true;
        }

        /// <summary>
        /// 消行
        /// </summary>
        public void FreeLine()
        {
            int vFreeCount = 0;
            for (int j = GameSetting.rowCount - 1; j >= 0; j--)
            {
                bool vExistsFull = true; // 是否存在满行
                for (int i = 0; i < GameSetting.colCount && vExistsFull; i++)
                    if (GameSetting.points[i, j] == 0)
                        vExistsFull = false;
                if (!vExistsFull) continue;
                #region 图片下移
                Graphics vGraphics = Graphics.FromImage(backBitmap);
                Rectangle srcRect = new Rectangle(0, 0, backBitmap.Width, j * GameSetting.brickHeight);
                Rectangle destRect = srcRect;
                destRect.Offset(0, GameSetting.brickHeight);
                Bitmap vBitmap = new Bitmap(srcRect.Width, srcRect.Height);
                Graphics.FromImage(vBitmap).DrawImage(backBitmap, 0, 0);
                vGraphics.DrawImage(vBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                vGraphics.FillRectangle(new SolidBrush(BackColor), 0, 0,
                    backBitmap.Width, GameSetting.brickHeight);
                #endregion 图片下移
                GameSetting.lines++;
                vFreeCount++;
                for (int k = j; k >= 0; k--)
                {
                    for (int i = 0; i < GameSetting.colCount; i++)
                        if (k == 0)
                            GameSetting.points[i, k] = 0;
                        else GameSetting.points[i, k] = GameSetting.points[i, k - 1];
                }
                j++;
            }
            GameSetting.score += GameSetting.scoress[vFreeCount];
            if (vFreeCount > 0)
            {
                GameSetting.level = Math.Min(GameSetting.lines / 30, GameSetting.speeds.Length - 1);
                timer.Interval = GameSetting.speeds[GameSetting.level];
                Invalidate();
            }
            if (youxiScore != null) youxiScore.Update();//if (youxiScore != null) youxiScore.Update(this);
        }

        /// <summary>
        /// 执行一个变化操作
        /// </summary>
        /// <param name="ABrickOperates">变化指令</param>
        /// <returns>返回是否变化</returns>
        public bool BrickOperate(BrickOperates ABrickOperates)
        {

            byte vFacingIndex = GameSetting.facingIndex;
            Point vBrickPoint = brickPoint;

            switch (ABrickOperates)
            {
                case BrickOperates.boTurnLeft:
                    vFacingIndex = (byte)((vFacingIndex + 1) %
                        brickTemplets[GameSetting.brickIndex].Count);
                    break;
                case BrickOperates.boTurnRight:
                    vFacingIndex = (byte)((brickTemplets[GameSetting.brickIndex].Count +
                        vFacingIndex - 1) % brickTemplets[GameSetting.brickIndex].Count);
                    break;
                case BrickOperates.boMoveLeft:
                    vBrickPoint.Offset(-1, 0);
                    break;
                case BrickOperates.boMoveRight:
                    vBrickPoint.Offset(+1, 0);
                    break;
                case BrickOperates.boMoveDown:
                    vBrickPoint.Offset(0, +1);
                    break;
                case BrickOperates.boMoveBottom:
                    vBrickPoint.Offset(0, +1);
                    while (CheckBrick(GameSetting.brickIndex, vFacingIndex, vBrickPoint))
                        vBrickPoint.Offset(0, +1);
                    vBrickPoint.Offset(0, -1);
                    break;
            }
            if (CheckBrick(GameSetting.brickIndex, vFacingIndex, vBrickPoint))
            {
                if (GameSetting.playing && GameSetting.recordMode && !GameSetting.reviewing)
                {
                    StepInfos.Add(new StepInfo(Environment.TickCount - GameSetting.lastRecordTime,
                        2, (byte)ABrickOperates, 0));
                    GameSetting.lastRecordTime = Environment.TickCount;
                }

                Graphics vGraphics = Graphics.FromImage(backBitmap);
                DrawCurrent(vGraphics, true);
                GameSetting.facingIndex = vFacingIndex;
                brickPoint = vBrickPoint;
                DrawCurrent(vGraphics, false);
                if (ABrickOperates == BrickOperates.boMoveBottom)
                    Downfall();
                else Invalidate();
            }
            else if (ABrickOperates == BrickOperates.boMoveDown)
            {
                if (GameSetting.playing && GameSetting.recordMode && !GameSetting.reviewing)
                {
                    StepInfos.Add(new StepInfo(Environment.TickCount - GameSetting.lastRecordTime,
                        2, (byte)ABrickOperates, 0));
                    GameSetting.lastRecordTime = Environment.TickCount;
                }
                Downfall();
            }
            return true;
        }

        /// <summary>
        /// 得到下一个方块
        /// </summary>
        private void NextBrick()
        {
            GameSetting.brickIndex = GameSetting.afterBrickIndex;
            GameSetting.facingIndex = GameSetting.afterFacingIndex;
            brickPoint.X = GameSetting.colCount / 2 - 1;
            brickPoint.Y = 0;
            GameSetting.afterBrickIndex = (byte)GameSetting.random.Next(brickTemplets.Count);
            GameSetting.afterFacingIndex = (byte)GameSetting.random.Next(brickTemplets[GameSetting.afterBrickIndex].Count);
            if (GameSetting.playing && GameSetting.recordMode && !GameSetting.reviewing)
            {
                StepInfos.Add(new StepInfo(0, 1, GameSetting.afterBrickIndex, GameSetting.afterFacingIndex));
                GameSetting.lastRecordTime = Environment.TickCount;
            }
            if (youxiNext != null && GameSetting.afterBrickIndex != GameSetting.brickIndex)
                youxiNext.Update(this);
            DrawCurrent(Graphics.FromImage(backBitmap), false);
            if (!CheckBrick(GameSetting.brickIndex, GameSetting.facingIndex, brickPoint))
            {
                if (GameSetting.playing && GameSetting.recordMode && !GameSetting.reviewing)
                {
                    StepInfos.Add(new StepInfo(0, 3, 0, 0));
                    GameSetting.lastRecordTime = Environment.TickCount;
                }
                GameOver();
            }
            Invalidate();
        }

        /// <summary>
        /// 当前方块落底
        /// </summary>
        private void Downfall()
        {
            foreach (Point vPoint in brickTemplets[GameSetting.brickIndex][GameSetting.facingIndex])
                GameSetting.points[vPoint.X + brickPoint.X,
                    vPoint.Y + brickPoint.Y] = (byte)(GameSetting.brickIndex + 1);
            FreeLine();
            if (GameSetting.playing && !GameSetting.reviewing) NextBrick();
        }

        private void ImageListRecreateHandle(object sender, EventArgs e)
        {
            DoChange();
        }

        private void DetachImageList(object sender, EventArgs e)
        {
            ImageList = null;
        }

        public ImageList ImageList
        {
            get
            {
                return imageList;
            }
            set
            {
                if (value != imageList)
                {
                    EventHandler handler = new EventHandler(ImageListRecreateHandle);
                    EventHandler handler2 = new EventHandler(DetachImageList);
                    if (imageList != null)
                    {
                        imageList.RecreateHandle -= handler;
                        imageList.Disposed -= handler2;
                    }
                    imageList = value;
                    if (value != null)
                    {
                        GameSetting.brickWidth = ImageList.ImageSize.Width;
                        GameSetting.brickHeight = ImageList.ImageSize.Height;
                        DoChange();
                        if (!GameSetting.playing && !GameSetting.reviewing) GameOver();
                        if (youxiNext != null)
                        {
                            youxiNext.BackColor = BackColor;
                            youxiNext.SetSize(GameSetting.brickWidth * 4, GameSetting.brickHeight * 4);
                            youxiNext.Update(this);
                        }
                        value.RecreateHandle += handler;
                        value.Disposed += handler2;
                    }
                }
            }
        }

        private void DetachyouxiNext(object sender, EventArgs e)
        {
            youxiNext = null;
        }

        public youxiNext TetrisNext
        {
            get
            {
                return youxiNext;
            }

            set
            {
                if (value != youxiNext)
                {
                    EventHandler handler = new EventHandler(DetachyouxiNext);
                    if (youxiNext != null) youxiNext.Disposed -= handler;
                    youxiNext = value;
                    if (value != null)
                    {
                        value.SetSize(4 * GameSetting.brickWidth, 4 * GameSetting.brickHeight);
                        value.Update(this);
                        value.Disposed += handler;
                    }
                }
            }
        }

        private void DetachyouxiScore(object sender, EventArgs e)
        {
            youxiScore = null;
        }

        public youxiScore TetrisScore
        {
            get
            {
                return youxiScore;
            }

            set
            {
                if (value != youxiScore)
                {
                    EventHandler handler = new EventHandler(DetachyouxiScore);
                    if (youxiScore != null) youxiScore.Disposed -= handler;
                    youxiScore = value;
                    if (value != null)
                    {
                        value.SetSize(4 * GameSetting.brickWidth, 6 * GameSetting.brickHeight);
                        value.Update();//value.Update(this);
                        value.Disposed += handler;
                    }
                }
            }
        }

        private void DetachProgressBar(object sender, EventArgs e)
        {
            progressBar = null;
        }

        /// <summary>
        /// 回放进度条
        /// </summary>
        public ProgressBar ProgressBar
        {
            get
            {
                return progressBar;
            }

            set
            {
                if (value != progressBar)
                {
                    EventHandler handler = new EventHandler(DetachProgressBar);
                    if (progressBar != null) progressBar.Disposed -= handler;
                    progressBar = value;
                    if (value != null)
                    {
                        progressBar.Minimum = 0;
                        progressBar.Maximum = StepInfos.Count;
                        progressBar.Value = GameSetting.stepIndex < 0 ? 0 : GameSetting.stepIndex;
                        value.Disposed += handler;
                    }
                }
            }
        }

        protected override bool IsInputKey(Keys keydata)
        {
            return (keydata == Keys.Down) || (keydata == Keys.Up) ||
                (keydata == Keys.Left) || (keydata == Keys.Right) ||
                (keydata == Keys.Escape) || base.IsInputKey(keydata);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CanFocus) Focus();
        }

        protected override void Dispose(bool disposing)
        {
            if (threadReview != null) threadReview.Abort();
            base.Dispose(disposing);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!GameSetting.playing || GameSetting.reviewing) return;
            switch (e.KeyCode)
            {
                case Keys.A: // 左移
                case Keys.Left:
                    BrickOperate(BrickOperates.boMoveLeft);
                    break;
                case Keys.D: // 右移
                case Keys.Right:
                    BrickOperate(BrickOperates.boMoveRight);
                    break;
                case Keys.W: // 变化1
                case Keys.Up:
                    BrickOperate(BrickOperates.boTurnLeft);
                    break;
                case Keys.Back: // 变化2
                case Keys.F:
                    BrickOperate(BrickOperates.boTurnRight);
                    break;
                case Keys.Down: // 向下
                case Keys.S:
                    BrickOperate(BrickOperates.boMoveDown);
                    break;
                case Keys.Enter: // 直下
                case Keys.Space:
                case Keys.End:
                case Keys.J:
                    BrickOperate(BrickOperates.boMoveBottom);
                    break;
            }
        }
        #endregion

        #region 事件
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (backBitmap != null) pe.Graphics.DrawImage(backBitmap, 0, 0);
        }
        #endregion
    }
}
