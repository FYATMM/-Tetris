using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace 俄罗斯方块Tetris
{
    /// <summary>
    /// 显示积分信息的控件
    /// </summary>
    public partial class youxiScore : Control
    {
        #region 字段
        ////自定义背景图
        private Bitmap backBitmap;
        #endregion

        #region 构造函数
        //创建组件后，自动生成，构造函数
        public youxiScore()
        {
            //InitializeComponent();//自动生成的构造函数定义的方法
            ////自定义背景色，风格
            BackColor = Color.Black;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            Graphics vGraphics = Graphics.FromImage(backBitmap);
            vGraphics.FillRectangle(new SolidBrush(BackColor), vGraphics.ClipBounds);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="youxiScore"></param>
        public void Update(youxiScore youxiScore)
        {
            if (youxiScore == null) return;
            Clear();
            youxiScore.DrawScore(Graphics.FromImage(backBitmap));
            Invalidate();
        }

        /// <summary>
        /// 设置尺寸
        /// </summary>
        /// <param name="AWidth"></param>
        /// <param name="AHeight"></param>
        public void SetSize(int AWidth, int AHeight)
        {
            Width = AWidth;
            Height = AHeight;
            backBitmap = new Bitmap(AWidth, AHeight);
        }

        /// <summary>
        /// 绘制积分框
        /// </summary>
        /// <param name="AGraphics"></param>
        public void DrawScore(Graphics AGraphics)
        {
            if (AGraphics == null) return;
            RectangleF vRectangleF = new Rectangle(0, 0, GameSetting.brickWidth * 4, GameSetting.brickHeight);

            StringFormat vStringFormat = new StringFormat();
            vStringFormat.FormatFlags |= StringFormatFlags.LineLimit;
            vStringFormat.Alignment = StringAlignment.Center;

            AGraphics.DrawString("Score", new Font(Font, FontStyle.Bold), Brushes.White, vRectangleF, vStringFormat);

            vRectangleF.Offset(0, GameSetting.brickHeight);

            AGraphics.DrawString(GameSetting.score.ToString(), Font, Brushes.White, vRectangleF, vStringFormat);

            vRectangleF.Offset(0, GameSetting.brickHeight);

            AGraphics.DrawString("Level", new Font(Font, FontStyle.Bold),Brushes.White, vRectangleF, vStringFormat);

            vRectangleF.Offset(0, GameSetting.brickHeight);

            AGraphics.DrawString(GameSetting.level.ToString(), Font,Brushes.White, vRectangleF, vStringFormat);

            vRectangleF.Offset(0, GameSetting.brickHeight);

            AGraphics.DrawString("Lines", new Font(Font, FontStyle.Bold), Brushes.White, vRectangleF, vStringFormat);

            vRectangleF.Offset(0, GameSetting.brickHeight);

            AGraphics.DrawString(GameSetting.lines.ToString(), Font,Brushes.White, vRectangleF, vStringFormat);
        }

        #endregion

        #region 事件
        //创建组件后，自动生成，用户调用画图
        protected override void OnPaint(PaintEventArgs pe)
        {
            //Raises the Paint event.
            //
            //Raising an event invokes the event handler through a delegate. 
            //The OnPaint method also enables derived classes to handle the event without attaching a delegate. 
            //This is the preferred technique for handling the event in a derived class.
            //
            //When overriding OnPaint(PaintEventArgs) in a derived class, be sure to call the base class's OnPaint(PaintEventArgs) method 
            //so that registered delegates receive the event.
            base.OnPaint(pe);//自动生成的用户调用画图

            ////画用户自定义图形,DrawImage有30多个重载，这个是DrawImage(Image, Int32, Int32)
            //Draws the specified image, using its original physical size, at the location specified by a coordinate pair.
            if (backBitmap != null) pe.Graphics.DrawImage(backBitmap, 0, 0);
        }
        #endregion
    }
}
