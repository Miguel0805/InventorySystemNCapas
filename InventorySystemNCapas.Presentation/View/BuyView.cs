using InventorySystemNCapas.Presentation.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystemNCapas.Presentation.View
{
    public partial class BuyView : Form
    {
        private BuyController controller;
        private MenuView _menuView;
        public BuyView(MenuView menuView)
        {
            InitializeComponent();
            _menuView = menuView;
            controller = new BuyController(_menuView, this);
        }
    }
}
