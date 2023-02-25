using InventorySystemNCapas.Presentation.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystemNCapas.Presentation.Controller
{
    public class MenuController
    {
        private MenuView _view;
        private SupplierView _supplierView;
        private CustomerView _customerView;
        private ProductView _productView;
        private BuyView _buyView;
        private SaleView _saleView;

        private int _posX = 0;
        private int _posY = 0;

        public MenuController(MenuView view) 
        {
            _view = view;
            Events();
        }

        

        private void Events()
        {
            _view.StartPosition = FormStartPosition.CenterScreen;

            _view.panelUp.MouseMove += new MouseEventHandler(MouseMoveEvent);
            _view.btnClose.Click += new EventHandler((s, args) => BtnClose());
            _view.panelSuppliers.Click += new EventHandler((s, args) => GoToSuppliers());
            _view.lblSuppliers.Click += new EventHandler((s, args) => GoToSuppliers());
            _view.pbxSuppliers.Click += new EventHandler((s, args) => GoToSuppliers());
            _view.panelCustomers.Click += new EventHandler((s, args) => GoToCustomers());
            _view.lblCustomers.Click += new EventHandler((s, args) => GoToCustomers());
            _view.pbxCustomers.Click += new EventHandler((s, args) => GoToCustomers());
            _view.panelProducts.Click += new EventHandler((s, args) => GoToProducts());
            _view.lblProducts.Click += new EventHandler((s, args) => GoToProducts());
            _view.pbxProducts.Click += new EventHandler((s, args) => GoToProducts());
            _view.panelBuys.Click += new EventHandler((s, args) => GoToBuys());
            _view.lblBuys.Click += new EventHandler((s, args) => GoToBuys());
            _view.pbxBuys.Click += new EventHandler((s, args) => GoToBuys());
            _view.panelSales.Click += new EventHandler((s, args) => GoToSales());
            _view.lblSales.Click += new EventHandler((s, args) => GoToSales());
            _view.pbxSales.Click += new EventHandler((s, args) => GoToSales());
        }




        #region event methods
        public void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                _posX = e.X;
                _posY = e.Y;
            }
            else
            {
                _view.Left += (e.X - _posX);
                _view.Top += (e.Y - _posY);
            }
        }

        public void BtnClose()
        {
            if (_supplierView != null)
            {
                _supplierView.Dispose();
            }
            else if (_customerView != null)
            {
                _customerView.Dispose();
            }
            else if(_productView != null)
            {
                _productView.Dispose();
            }
            else if (_buyView != null)
            {
                _buyView.Dispose();
            }
            else if (_saleView != null)
            {
                _saleView.Dispose();
            }

            _view.Dispose();
        }

        #endregion

        #region methods
        private void GoToCustomers()
        {
            if (_customerView == null)
            {
                _customerView = new CustomerView(_view);
            }

            _view.Hide();
            _customerView.FormClosed += (s, args) => _view.Close();
            _customerView.Show();
        }

        private void GoToSuppliers()
        {
            if (_supplierView == null)
            {
                _supplierView = new SupplierView(_view);
            }

            _view.Hide();
            _supplierView.FormClosed += (s, args) => _view.Close();
            _supplierView.Show();
        }

        private void GoToProducts()
        {
            if (_productView == null)
            {
                _productView = new ProductView(_view);
            }

            _view.Hide();
            _productView.FormClosed += (s, args) => _view.Close();
            _productView.Show();
        }

        private void GoToBuys()
        {
            if (_buyView == null)
            {
                _buyView = new BuyView(_view);
            }

            _view.Hide();
            _buyView.FormClosed += (s, args) => _view.Close();
            _buyView.Show();
        }

        private void GoToSales()
        {
            if (_saleView == null)
            {
                _saleView = new SaleView(_view);
            }

            _view.Hide();
            _saleView.FormClosed += (s, args) => _view.Close();
            _saleView.Show();
        }

        #endregion


    }
}
