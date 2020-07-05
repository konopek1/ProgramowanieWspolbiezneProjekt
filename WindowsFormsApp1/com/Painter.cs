using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1.com
{
    public class Painter
    {
        private List<Animation> _animations;
        private BufferedGraphics _buffer;
        private Timer timer;
        private Dictionary<Point, String> _strings;
        private Worker _worker;

        internal Painter(BufferedGraphics buffer, Worker worker)
        {
            _animations = new List<Animation>();
            _strings = new  Dictionary<Point, string>();
            _buffer = buffer;
            _worker = worker;
            initTimer();
            initStaticStrings();
        }

        private void initStaticStrings()
        {
            foreach (var pair in Board.staticStrings)
            {
                _strings.Add(pair.pos,pair.value);
            }

        }

        public void AddAnimation(Animation animation)
        {
            Monitor.Enter(_animations);
            _animations.Add(animation);
            Monitor.Exit(_animations);
        }
        
        public void AddString(Point key, string value)
        {
            Monitor.Enter(_strings);
            _strings.Add(key, value);
            Monitor.Exit(_strings);
        }
        
        public void UpadateString(Point key, string value)
        {
            Monitor.Enter(_strings);
            _strings.Remove(key);
            _strings.Add(key, value);
            Monitor.Exit(_strings);
        }

        void PropagateAnimations()
        {
            for (int i = 0; i < _animations.Count; i++)
            {
                var animation = _animations[i];

                _buffer.Graphics.FillRectangle(animation.brush, new Rectangle(animation.pos, animation.target));

                if (animation.isDone())
                {
                    _animations.Remove(animation);
                    i = 0;
                }

                animation.Update();
            }
        }

        public void DrawStrings()
        {
            Font font = new Font("Arial",16);
            SolidBrush brush = new SolidBrush(Color.Black);
            
            for (var i = 0; i < _strings.Count; i++)
            {
                var pair = _strings.ElementAt(i);
                _buffer.Graphics.DrawString(pair.Value,font,brush,pair.Key);
            }
           
        }


        private void DrawBoard()
        {
            Font font = new Font("Arial",16);
            SolidBrush brush = new SolidBrush(Color.Black);
            
            _buffer.Graphics.Clear(Color.Azure);
            for (int i = 0; i < 4; i++)
            {
                var rect = Board.workshops[i];
                _buffer.Graphics.DrawRectangle(new Pen(Brushes.Black, 10), new Rectangle(rect.pos,rect.value));
            }

            _buffer.Graphics.DrawRectangle(new Pen(Brushes.DarkBlue), new Rectangle(Board.parking.pos, Board.parking.value));
            _buffer.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(Board.que.pos, Board.que.value));
            _buffer.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(Board.finish.pos, Board.finish.value));
            _buffer.Graphics.DrawRectangle(new Pen(Brushes.DarkRed), new Rectangle(Board.prioQue.pos, Board.prioQue.value));
            _buffer.Graphics.DrawString(_worker.queCounter.ToString(),font,brush,Board.que.pos);
            _buffer.Graphics.DrawString(_worker.prioQueCounter.ToString(),font,brush,Board.prioQue.pos);
        }

        private void initTimer()
        {
            timer = new Timer();
            timer.Tick += this.NextTick;
            timer.Interval = 1000 / 120; // 20 klatek na sekund
            timer.Start();
        }

        private void NextTick(object sender, EventArgs e)
        {
            DrawBoard();
            PropagateAnimations();
            DrawStrings();
            _buffer.Render();
        }
        
    }
}