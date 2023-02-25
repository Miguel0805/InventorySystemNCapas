using InventorySystemNCapas.BLLL;
using InventorySystemNCapas.Models;
using InventorySystemNCapas.Presentation.View;
using System;
using System.Windows.Forms;

namespace InventorySystemNCapas.Presentation.Controller
{
    public class ProductController
    {
        private ProductView _view;
        private MenuView _menuView;
        private ProductDAO _productDAO;

        private int _posX = 0;
        private int _posY = 0;
        private bool _edit = false;

        public ProductController(MenuView menuView, ProductView view)
        {
            _view = view;
            _menuView = menuView;
            _productDAO = new ProductDAO();

            Events();
            FillDataGridView();
        }

        public void Events()
        {
            _view.StartPosition = FormStartPosition.CenterScreen;

            _view.panelUp.MouseMove += new MouseEventHandler(MouseMoveEvent);
            _view.btnBack.Click += new EventHandler((s, args) => GoToMenu());
            _view.btnSave.Click += new EventHandler((s, args) => BtnSave());
            _view.btnEdit.Click += new EventHandler((s, args) => BtnEdit());
            _view.btnDelete.Click += new EventHandler((s, args) => BtnDelete());
            _view.btnClear.Click += new EventHandler((s, args) => BtnClear());
            _view.btnClose.Click += new EventHandler((s, args) => BtnClose());
            _view.txtPrice.LostFocus += new EventHandler((s, args) => VerifyPriceFormat());
        }

        #region action methods
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
            if (_menuView != null)
            {
                _menuView.Dispose();
            }

            _view.Dispose();
        }
        public void BtnClear()
        {
            ClearInputFields();
            _view.btnSave.Text = "Create";
        }

        public void BtnSave()
        {
            try
            {
                if (_edit)
                {
                    UpdateRegister();
                }
                else // insert
                {
                    InsertRegister();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.InnerException}");
            }
        }

        public void BtnEdit()
        {
            _edit = true;
            var rowIsSelected = _view.productDGV.SelectedRows.Count > 0;

            if (!rowIsSelected)
            {
                MessageBox.Show("Please, select a row to edit it.");
                _edit = false;
                _view.btnSave.Text = "Create";
                return;
            }
            _view.btnSave.Text = "Update";

            DataGridViewRow registerSelected = _view.productDGV.SelectedRows[0];
            Product product = new Product();

            product.Sku = registerSelected.Cells[0].Value.ToString();
            product.Name = registerSelected.Cells[1].Value.ToString();
            product.Description = registerSelected.Cells[2].Value.ToString();
            product.Price = Decimal.Parse(registerSelected.Cells[3].Value.ToString());

            FillCustomerInputs(product);
        }

        public void BtnDelete()
        {
            var rowIsSelected = _view.productDGV.SelectedRows.Count > 0;

            if (!rowIsSelected)
            {
                MessageBox.Show("Please, select a row to delete it.");
                return;
            }

            DialogResult confirmation = MessageBox.Show("¿Are you sure you want delete this register?", "Confirmation", MessageBoxButtons.OKCancel);

            if (confirmation == DialogResult.OK)
            {
                string sku = _view.productDGV.SelectedRows[0].Cells[0].Value.ToString();
                DeleteRegister(sku);
            }
        }

        #endregion

        #region
        private void InsertRegister()
        {
            if (FieldsRequiredAreEmpty())
            {
                MessageBox.Show("All field inputs are required.");
                return;
            }

            try
            {
                var product = BuildProductModel();
                var insert = _productDAO.Insert(product);

                if (insert)
                {
                    MessageBox.Show("Register added successfully.");
                    FillDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }

        }
        private void UpdateRegister()
        {
            if (FieldsRequiredAreEmpty())
            {
                MessageBox.Show("All field inputs are required.");
                return;
            }

            try
            {
                var productUpdated = BuildProductModel();
                string sku = _view.lblId.Text; 
                var update = _productDAO.Update(sku, productUpdated);

                if (update)
                {
                    MessageBox.Show("Register updated successfully.");
                    FillDataGridView();
                    ClearInputFields();
                    _view.btnSave.Text = "Create";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
        private bool FieldsRequiredAreEmpty()
        {
            return string.IsNullOrEmpty(_view.txtSku.Text) ||
                string.IsNullOrEmpty(_view.txtName.Text) ||
                string.IsNullOrEmpty(_view.txtDescription.Text) ||
                string.IsNullOrEmpty(_view.txtPrice.Text);
        }

        private Product BuildProductModel()
        {
            Product product = new Product();

            product.Sku = _view.txtSku.Text;
            product.Name = _view.txtName.Text;
            product.Description = _view.txtDescription.Text;
            product.Price = Decimal.Parse(_view.txtPrice.Text);

            return product;
        }
        private void FillDataGridView()
        {
            var productDAO2 = new ProductDAO();
            _view.productDGV.DataSource = productDAO2.GetAll();
        }

        private void DeleteRegister(string sku)
        {
            try
            {
                var delete = _productDAO.Delete(sku);

                if (delete)
                {
                    MessageBox.Show("Register deleted successfully.");
                    FillDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public void FillCustomerInputs(Product obj)
        {
            _view.lblId.Text = obj.Sku;
            _view.txtSku.Text = obj.Sku;
            _view.txtName.Text = obj.Name;
            _view.txtDescription.Text = obj.Description;
            _view.txtPrice.Text = obj.Price.ToString();
        }
        private void ClearInputFields()
        {
            _view.lblId.Text = "";
            _view.txtSku.Text = "";
            _view.txtName.Text = "";
            _view.txtDescription.Text = "";
            _view.txtPrice.Text = "";
        }
        private void GoToMenu()
        {
            if (_menuView == null)
            {
                _menuView = new MenuView();
            }

            _view.Hide();
            _menuView.FormClosed += (s, args) => _view.Close();
            _menuView.Show();
        }

        public void VerifyPriceFormat()
        {
            var txtPrice = _view.txtPrice.Text;
            decimal price;

            var parsed = decimal.TryParse(txtPrice, out price);

            if (!parsed && txtPrice.Length > 0)
            {
                MessageBox.Show($"Please, enter a valid price.");
                _view.txtPrice.Focus();
            }
        }
        #endregion


    }
}
