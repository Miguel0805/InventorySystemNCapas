﻿using InventorySystemNCapas.Presentation.Controller;
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
    public partial class ProductView : Form
    {
        private ProductController _controller;
        private MenuView _menuView;
        public ProductView(MenuView menuView)
        {
            InitializeComponent();
            _menuView = menuView;
            _controller = new ProductController(_menuView, this);
        }
    }
}
