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
    public partial class MenuView : Form
    {
        private MenuController _controller;
        public MenuView()
        {
            InitializeComponent();
            _controller = new MenuController(this);
        }

       
    }
}
