using InventorySystemNCapas.BLL;
using InventorySystemNCapas.BLLL;
using InventorySystemNCapas.Models;
using InventorySystemNCapas.Models.DTOs;
using InventorySystemNCapas.Presentation.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace InventorySystemNCapas.Presentation.Controller
{
    public class SaleController
    {
        private SaleView _view;
        private MenuView _menuView;
        private SaleDAO _saleDAO;
        private CustomerDAO _customerDAO;
        private ProductDAO _productDAO;

        private int _posX = 0;
        private int _posY = 0;
        private int _rowIndexItemSelected = 0;
        private decimal _total = 0;
        private bool _edit = false;
        private bool _editItem = false;

        private List<Customer> _customers;
        private List<Product> _products;
        private List<SaleDetail> _saleDetails;

        private AutoCompleteStringCollection _list;

        public SaleController(MenuView meuView, SaleView view)
        {
            _view = view;
            _menuView = meuView;
            _saleDAO = new SaleDAO();
            _customerDAO = new CustomerDAO();
            _productDAO = new ProductDAO();
            _saleDetails = new List<SaleDetail>();
            _list = new AutoCompleteStringCollection();
            Events();
            FillDataGridView();
        }

        public void Events()
        {
            _view.panelUp.MouseMove += new MouseEventHandler(MouseMoveEvent);

            _view.btnClose.Click += new EventHandler((s, args) => BtnClose());
            _view.btnBack.Click += new EventHandler((s, args) => GoToMenu());
            _view.txtCustomer.GotFocus += new EventHandler((s, args) => AutocompleteCustomer());
            _view.txtCustomer.Leave += new EventHandler((s, args) => VerifyCustomerAutocompletSelection());
            _view.txtSku.GotFocus += new EventHandler((s, args) => AutocompleteProductSku());
            _view.txtSku.LostFocus += new EventHandler((s, args) => VerifySkuAutocompleteSelection());
            _view.txtProductName.GotFocus += new EventHandler((s, args) => AutocompleteProductName());
            _view.txtProductName.LostFocus += new EventHandler((s, args) => VerifyProductNameAutocompleteSelection());
            _view.txtPrice.LostFocus += new EventHandler((s, args) => VerifyDecimalFormat(_view.txtPrice));
            _view.txtUnits.LostFocus += new EventHandler((s, args) => VerifyDecimalFormat(_view.txtUnits));
            _view.txtDiscount.LostFocus += new EventHandler((s, args) => VerifyDecimalFormat(_view.txtDiscount));
            _view.btnSaveItem.Click += new EventHandler((s, args) => BtnSaveItem());
            _view.btnSave.Click += new EventHandler((s, args) => BtnSave());
            _view.btnEdit.Click += new EventHandler((s, args) => BtnEdit());
            _view.btnDelete.Click += new EventHandler((s, args) => BtnDelete());
            _view.itemsDGV.CellClick += new DataGridViewCellEventHandler((s, args) => CellClickEvent(s, args));
            _view.btnClearItemInputs.Click += new EventHandler((s, args) => ClearItemInputs());
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
            if (_menuView != null)
            {
                _menuView.Dispose();
            }

            _view.Dispose();
        }

        public void BtnSaveItem()
        {
            if (!ProductInputsAreValid())
            {
                MessageBox.Show("Please, enter all input values.");
                return;
            }

            if (_editItem) //edit item
            {
                UpdateItemBuyDetail();
                ClearItemInputs();
                BuildSaleDetailDataGridView();
                CalculateTotal();

                _view.btnSaveItem.Text = "Add Item";
            }
            else // add item
            {
                var item = BuildSaleDetailItem();
                _saleDetails.Add(item);
                BuildSaleDetailDataGridView();
                CalculateTotal();
            }
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
            var rowSelected = _view.saleDGV.SelectedRows.Count > 0;

            if (!rowSelected)
            {
                MessageBox.Show("Please select a row to edit it.");
                return;
            }
            _edit = true;
            int id = Int32.Parse(_view.saleDGV.SelectedCells[0].Value.ToString());

            try
            {
                Sale sale = _saleDAO.GetById(id);
                FillBuyInputs(sale);

                _view.btnSave.Text = "Update Sale";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} \n {ex.InnerException}");
            }
            
        }

        public void BtnDelete()
        {
            var rowSelected = _view.saleDGV.SelectedRows.Count > 0;

            if (!rowSelected)
            {
                MessageBox.Show("Please select a row to delete it.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Are your sure you want delete this register?", "Confirmation", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                int id = int.Parse(_view.saleDGV.SelectedCells[0].Value.ToString());
                DeleteRegister(id);
            }
        }

        public void CellClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            // click on edit button
            if (e.ColumnIndex == _view.itemsDGV.Columns["Edit"].Index)
            {
                _view.btnSaveItem.Text = "Update Item";
                FillInputsWithItemToEdit(e.RowIndex);
                _editItem = true;
            }
            else if (e.ColumnIndex == _view.itemsDGV.Columns["Delete"].Index)
            {
                DialogResult confirm = MessageBox.Show("¿Are you sure you want delete it?", "Confirmation", MessageBoxButtons.OKCancel);

                if (confirm == DialogResult.OK)
                {
                    _saleDetails.RemoveAt(e.RowIndex);
                    BuildSaleDetailDataGridView();
                    CalculateTotal();
                }
            }
        }
        #endregion

        #region

        public void AutocompleteCustomer()
        {
            _list.Clear();

            _customers = _customerDAO.GetCustomers();

            foreach (var customer in _customers)
            {
                _list.Add(customer.Name);
            }

            _view.txtCustomer.AutoCompleteCustomSource = _list;
        }
        public void AutocompleteProductSku()
        {
            _list.Clear();

            _products = _productDAO.GetProducts();

            foreach (var product in _products)
            {
                _list.Add(product.Sku);
            }

            _view.txtSku.AutoCompleteCustomSource = _list;
        }
        public void AutocompleteProductName()
        {
            _list.Clear();

            _products = _productDAO.GetProducts();

            foreach (var product in _products)
            {
                _list.Add(product.Name);
            }

            _view.txtProductName.AutoCompleteCustomSource = _list;
        }
        public void VerifyCustomerAutocompletSelection()
        {
            bool match = false;
            string customerName = _view.txtCustomer.Text.Trim();

            foreach (var customer in _customers)
            {
                if (customer.Name.Equals(customerName))
                {
                    _view.lblCustomerId.Text = customer.Id.ToString();
                    match = true;
                    break;
                }
            }

            if (!match && customerName.Length > 0)
            {
                MessageBox.Show("Please, select a item from autocomplete list");
                _view.txtCustomer.Text = "";
                _view.lblCustomerId.Text = "";
                _view.txtCustomer.Focus();
            }

        }

        public void VerifySkuAutocompleteSelection()
        {
            bool match = false;
            string productName = "", price = "";
            string sku = _view.txtSku.Text.Trim();

            foreach (var product in _products)
            {
                if (product.Sku.Equals(sku))
                {
                    match = true;
                    productName = product.Name;
                    price = product.Price.ToString();
                    break;
                }
            }

            if (!match && sku.Length > 0)
            {
                MessageBox.Show("Please, select a item from autocomplete list");
                _view.txtSku.Text = "";
                _view.txtProductName.Text = "";
                _view.txtSku.Focus();
                return;
            }

            _view.txtProductName.Text = productName;
            _view.txtPrice.Text = price;
            CalculateSubtotal();
        }

        public void VerifyProductNameAutocompleteSelection()
        {
            bool match = false;
            string sku = "", price = "";
            string name = _view.txtProductName.Text;

            foreach (var product in _products)
            {
                if (product.Name.Equals(name))
                {
                    match = true;
                    sku = product.Sku;
                    price = product.Price.ToString();
                    break;
                }
            }

            if (!match && name.Length > 0)
            {
                MessageBox.Show("Please, select a item from autocomplete list");
                _view.txtSku.Text = "";
                _view.txtProductName.Text = "";
                _view.txtProductName.Focus();
                return;
            }

            _view.txtSku.Text = sku;
            _view.txtPrice.Text = price;
            CalculateSubtotal();
        }

        public void VerifyDecimalFormat(TextBox txtBox)
        {
            var value = txtBox.Text;

            decimal decimalValue;

            if (value.Length > 0 && !decimal.TryParse(value, out decimalValue))
            {
                MessageBox.Show("Please, enter a valid number format");
                txtBox.Focus();
            }
            else
            {
                CalculateSubtotal();
            }
        }

        public void CalculateSubtotal()
        {
            decimal price = (string.IsNullOrEmpty(_view.txtPrice.Text)) ? 0
                : decimal.Parse(_view.txtPrice.Text);
            int units = (string.IsNullOrEmpty(_view.txtUnits.Text)) ? 0
                : int.Parse(_view.txtUnits.Text);
            var discount = (string.IsNullOrEmpty(_view.txtDiscount.Text)) ? 0
                : decimal.Parse(_view.txtDiscount.Text);

            decimal subtotal = (price * units) - discount;

            _view.txtSubtotal.Text = subtotal.ToString();
        }



        public void CalculateTotal()
        {
            _total = 0;

            foreach (var item in _saleDetails)
            {
                _total += item.Subtotal;
            }

            _view.lblTotal.Text = _total.ToString();
        }


        public bool ProductInputsAreValid()
        {
            return !string.IsNullOrEmpty(_view.txtSku.Text)
                && !string.IsNullOrEmpty(_view.txtPrice.Text)
                && !string.IsNullOrEmpty(_view.txtUnits.Text)
                && (decimal.Parse(_view.txtSubtotal.Text) > 0);
        }

        public SaleDetail BuildSaleDetailItem()
        {
            string sku = _view.txtSku.Text;
            string name = _view.txtProductName.Text;
            string txtDiscount = _view.txtDiscount.Text;
            decimal price = decimal.Parse(_view.txtPrice.Text);
            int units = Int32.Parse(_view.txtUnits.Text);
            decimal discount = (string.IsNullOrEmpty(txtDiscount)) ? 0
                : decimal.Parse(txtDiscount);
            decimal subtotal = decimal.Parse(_view.txtSubtotal.Text);

            var saleDetail = new SaleDetail();

            saleDetail.ProductSku = sku;
            saleDetail.ProductName = name;
            saleDetail.Price = price;
            saleDetail.Units = units;
            saleDetail.Discount = discount;
            saleDetail.Subtotal = subtotal;

            return saleDetail;
        }

        public void BuildSaleDetailDataGridView()
        {
            _view.itemsDGV.Columns.Clear();
            _view.itemsDGV.Rows.Clear();

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnEdit.Name = "Edit";
            btnEdit.Text = "Edit";
            btnDelete.Name = "Delete";
            btnDelete.Text = "Delete";

            _view.itemsDGV.Columns.Add("sku", "SKU");
            _view.itemsDGV.Columns.Add("name", "Name");
            _view.itemsDGV.Columns.Add("price", "Price");
            _view.itemsDGV.Columns.Add("units", "Units");
            _view.itemsDGV.Columns.Add("discount", "Discount");
            _view.itemsDGV.Columns.Add("subtotal", "Subtotal");
            _view.itemsDGV.Columns.Add(btnEdit);
            _view.itemsDGV.Columns.Add(btnDelete);


            _view.itemsDGV.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            _view.itemsDGV.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            _view.itemsDGV.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            _view.itemsDGV.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            _view.itemsDGV.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //_view.itemsDGV.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            _view.itemsDGV.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            _view.itemsDGV.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            foreach (var item in _saleDetails)
            {
                _view.itemsDGV.Rows.Add(new object[] {
                    item.ProductSku,
                    item.ProductName,
                    item.Price,
                    item.Units,
                    item.Discount,
                    item.Subtotal,
                });
            }
        }

        public void InsertRegister()
        {
            if (!BuyInputsAreValid())
            {
                MessageBox.Show("Please, complete all input fields.");
                return;
            }

            try
            {
                var buyModel = BuildSaleModel();
                var insert = _saleDAO.Insert(buyModel);

                if (insert)
                {
                    MessageBox.Show("Register added successfully.");
                    FillDataGridView();
                    ClearInputFields();
                    _saleDetails.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public void UpdateRegister()
        {
            if (!BuyInputsAreValid())
            {
                MessageBox.Show("Please, complete all input fields");
                return;
            }

            try
            {
                var buyModel = BuildSaleModel();
                bool update = _saleDAO.Update(buyModel.Id, buyModel);

                if (update)
                {
                    MessageBox.Show("Register updated successfully.");
                    FillDataGridView();
                    ClearInputFields();
                    _saleDetails.Clear();
                    _view.btnSave.Text = "Save Buy";
                    _edit = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteRegister(int id)
        {
            try
            {
                bool delete = _saleDAO.Delete(id);

                if (delete)
                {
                    MessageBox.Show("Register deleted successfully.");
                    FillDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} \n {ex.InnerException}");
            }
        }
        public void ClearInputFields()
        {
            _view.lblBuyId.Text = "";
            _view.lblCustomerId.Text = "";
            _view.lblUserId.Text = "";
            _view.txtCustomer.Text = "";
            _view.dtpDate.Text = DateTime.Now.ToString("dd/MM/yy");
            _view.lblTotal.Text = "";

            ClearItemInputs();

            _view.itemsDGV.Rows.Clear();
            _view.itemsDGV.Columns.Clear();

        }

        public void FillInputsWithItemToEdit(int rowIndex)
        {
            _rowIndexItemSelected = rowIndex;
            _view.txtSku.Text = _view.itemsDGV.Rows[rowIndex].Cells["sku"].Value.ToString();
            _view.txtProductName.Text = _view.itemsDGV.Rows[rowIndex].Cells["name"].Value.ToString();
            _view.txtPrice.Text = _view.itemsDGV.Rows[rowIndex].Cells["price"].Value.ToString();
            _view.txtUnits.Text = _view.itemsDGV.Rows[rowIndex].Cells["units"].Value.ToString();
            _view.txtDiscount.Text = _view.itemsDGV.Rows[rowIndex].Cells["discount"].Value.ToString();
            _view.txtSubtotal.Text = _view.itemsDGV.Rows[rowIndex].Cells["subtotal"].Value.ToString();
        }

        public void ClearItemInputs()
        {
            _view.txtSku.Text = "";
            _view.txtProductName.Text = "";
            _view.txtPrice.Text = "";
            _view.txtUnits.Text = "";
            _view.txtDiscount.Text = "";
            _view.txtSubtotal.Text = "0";
        }

        public void FillBuyInputs(Sale sale)
        {
            _view.lblBuyId.Text = sale.Id.ToString();
            _view.lblCustomerId.Text = sale.CustomerId.ToString();
            _view.lblUserId.Text = sale.UserId.ToString();
            _view.txtUsername.Text = sale.Username.ToString();
            _view.txtCustomer.Text = sale.CustomerName;
            _view.dtpDate.Value = sale.Date;
            _total = sale.Total;
            _view.lblTotal.Text = sale.Total.ToString();
            _saleDetails = sale.SaleDetail;

            BuildSaleDetailDataGridView();
        }

        public void UpdateItemBuyDetail()
        {
            var buyDetail = BuildSaleDetailItem();

            _saleDetails.RemoveAt(_rowIndexItemSelected);
            _saleDetails.Insert(_rowIndexItemSelected, buyDetail);
        }

        public void FillDataGridView()
        {
            var saleDAO2 = new SaleDAO();
            _view.saleDGV.DataSource = saleDAO2.GetAll();
        }

        public bool BuyInputsAreValid()
        {
            return !(string.IsNullOrEmpty(_view.lblCustomerId.Text))
                    && !(string.IsNullOrEmpty(_view.dtpDate.Text))
                    && (decimal.Parse(_view.lblTotal.Text) > 0)
                    && (_saleDetails.Count > 0);
        }
        public Sale BuildSaleModel()
        {
            var saleModel = new Sale();

            saleModel.Id = (string.IsNullOrEmpty(_view.lblBuyId.Text)) ? 0
                : Int32.Parse(_view.lblBuyId.Text);
            saleModel.CustomerId = Int32.Parse(_view.lblCustomerId.Text);
            saleModel.UserId = 1;
            saleModel.Date = DateTime.Parse(_view.dtpDate.Text);
            saleModel.Date = DateTime.Parse(saleModel.Date.ToString("yyyy-MM-dd"));
            saleModel.Total = _total;
            saleModel.SaleDetail = _saleDetails;

            return saleModel;
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
        #endregion



    }
}
