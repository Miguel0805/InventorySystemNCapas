using InventorySystemNCapas.Presentation.Controller;
using System.Windows.Forms;

namespace InventorySystemNCapas.Presentation.View
{
    public partial class SupplierView : Form
    {
        private SupplierController _controller;
        private MenuView _menuView;
        public SupplierView(MenuView menuView)
        {
            InitializeComponent();
            _menuView = menuView;
            _controller = new SupplierController(_menuView, this);
        }
    }
}
