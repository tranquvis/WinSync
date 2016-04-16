using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSync.Controls
{
    public class AnimatedProgressBar : ProgressBar
    {
        const int frameRate = 200; //in frames per second
        const float animationStep = 1; //in percent

        float _value = 0;
        float _displayValue = 0;

        int _lastProgressBarPaintWidth = 0;
        bool _animationRunning;

        public AnimatedProgressBar()
        {
            Margin = new Padding(5, 5, 5, 5);
            BackColor = Color.White;
            ForeColor = Color.FromArgb(0,100,100);

            SetStyle(ControlStyles.UserPaint, true);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            int height = e.ClipRectangle.Height;
            int width = (int)(e.ClipRectangle.Width * DisplayValue / 100);

            if(_lastProgressBarPaintWidth < width)
                e.Graphics.FillRectangle(new SolidBrush(ForeColor), _lastProgressBarPaintWidth, 0, width, height);
            else if (_lastProgressBarPaintWidth > width)
                e.Graphics.FillRectangle(new SolidBrush(BackColor), width, 0, _lastProgressBarPaintWidth, height);

            _lastProgressBarPaintWidth = width;
        }

        /// <summary>
        /// paint background only on initialisation
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if(Value == 0 && !_animationRunning)
                base.OnPaintBackground(pevent);
        }

        /// <summary>
        /// progress value in percent
        /// </summary>
        public new float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Animate();
            }
        }

        /// <summary>
        /// displayed progress value in percent
        /// </summary>
        public float DisplayValue
        {
            get { return _displayValue; }
            protected set { _displayValue = value; }
        }

        /// <summary>
        /// start animation
        /// </summary>
        protected async void Animate()
        {
            if (_animationRunning) return;

            _animationRunning = true;

            await Task.Run(new Action(() =>
            {
                while (DisplayValue != Value)
                {
                    if (Math.Abs(DisplayValue - Value) < animationStep)
                        DisplayValue = Value;
                    else
                        DisplayValue += DisplayValue < Value ? animationStep : -animationStep;

                    Invalidate();
                    
                    System.Threading.Thread.Sleep((int)(1000f / frameRate));
                }
            }));

            _animationRunning = false;
        }
    }
}
