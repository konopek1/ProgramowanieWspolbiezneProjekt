using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.com;
using WindowsFormsApp1.com.WorkShops;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private BufferedGraphicsContext _ctx;
        private BufferedGraphics _buffer;
        private int x = 0;
        private Worker _worker = new Worker();
        private Painter _painter;

        public Form1()
        {
            InitializeComponent();
            prepareBuffer();
            _painter = new Painter(_buffer,_worker);
            initWorkShops();
        }

        private void initWorkShops()
        {
            var _parking = new WorkShopParking( Damage.MECHANICZNE,
                _worker,
                _painter,
                Board.parking.pos
            );
            new WorkShopLakier(Damage.LAKIER,
                _worker,
                _painter,
                Board.workshops[0].pos
            ).Run();
            new WorkShopMechanika(Damage.MECHANICZNE,
                    _worker,
                    _painter,
                    _parking,
                    Board.workshops[1].pos
            ).Run();
            new WorkShopMechanika(Damage.MECHANICZNE,
                _worker,
                _painter,
                _parking,
                Board.workshops[3].pos
            ).Run();
            new WorkShopDiagnostyk(Damage.DIAGNOSTYKA,
                _worker,
                _painter,
                Board.workshops[2].pos).Run();
        }

        private void prepareBuffer()
        {
            _ctx = BufferedGraphicsManager.Current;
            _buffer = _ctx.Allocate(this.CreateGraphics(), this.DisplayRectangle);
        }
    }
}