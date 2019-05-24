using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 俄罗斯方块Tetris
{
    public partial class youxiNext : Control
    {
        /// <summary>
        /// 显示下一个出现的方块控件
        /// </summary>

        #region 字段
        private Bitmap backBitmap;
        #endregion

        #region 构造函数
        public youxiNext()
        {
            //InitializeComponent();
            BackColor = Color.Black;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion

        #region 方法
        public void Clear()
        {
            Graphics vGraphics = Graphics.FromImage(backBitmap);
            vGraphics.FillRectangle(new SolidBrush(BackColor), vGraphics.ClipBounds);
        }

        public void Update(youxiControl AyouxiControl)
        {
            if (AyouxiControl == null) return;
            Clear();
            AyouxiControl.DrawNext(Graphics.FromImage(backBitmap));
            Invalidate();
        }

        public void SetSize(int AWidth, int AHeight)
        {
            Width = AWidth;
            Height = AHeight;
            backBitmap = new Bitmap(AWidth, AHeight);
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
