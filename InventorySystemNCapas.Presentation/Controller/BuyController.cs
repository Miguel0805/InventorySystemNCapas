using InventorySystemNCapas.BLLL;
using InventorySystemNCapas.Presentation.View;
using System.Windows.Forms;
using System;
using InventorySystemNCapas.BLL;
using InventorySystemNCapas.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace InventorySystemNCapas.Presentation.Controller
{
    public class BuyController
    {
        private BuyView _view;
        private MenuView _menuView;
        private BuyDAO _buyDAO;
        private SupplierDAO _supplierDAO;
        private ProductDAO _productDAO;

        private int _posX = 0;
        private int _posY = 0;
        private int _rowIndexItemSelected = 0;
        private decimal _total = 0;
        private bool _edit = false;
        private bool _editItem = false;

        private List<Supplier> _suppliers;
        private List<Product> _products;
        private List<BuyDetail> _buyDetails;
 
        private AutoCompleteStringCollection _list;

        public BuyController(MenuView menuView, BuyView view)
        {
            _view = view;
            _menuView = menuView;
            _buyDAO = new BuyDAO();
            _supplierDAO = new SupplierDAO();
            _productDAO = new ProductDAO();
            _buyDetails = new List<BuyDetail>();
            _list = new AutoCompleteStringCollection();
            Events();
            FillDataGridView();
            //AutocompleteSupplier();
        }

        public void Events()
        {
            _view.panelUp.MouseMove += new MouseEventHandler(MouseMoveEvent);

            _view.btnClose.Click += new EventHandler((s, args) => BtnClose());
            _view.btnBack.Click += new EventHandler((s, args) => GoToMenu());
            _view.txtSupplier.GotFocus += new EventHandler((s, args) => AutocompleteSupplier());
            _view.txtSupplier.Leave += new EventHandler((s, args) => VerifySupplierAutocompletSelection());
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
                BuildBuyDetailDataGridView();
                CalculateTotal();

                _view.btnSaveItem.Text = "Add Item";
            }
            else // add item
            {
                var item = BuildBuyDetailItem();
                _buyDetails.Add(item);
                BuildBuyDetailDataGridView();
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
            var rowSelected = _view.buyDGV.SelectedRows.Count > 0;

            if (!rowSelected)
            {
                MessageBox.Show("Please select a row to edit it.");
                return;
            }
            _edit = true;
            int id = Int32.Parse(_view.buyDGV.SelectedCells[0].Value.ToString());
            
            Buy buy = _buyDAO.GetById(id);
            FillBuyInputs(buy);

            _view.btnSave.Text = "Update Buy";
        }
    
        public void BtnDelete()
        {
            var rowSelected = _view.buyDGV.SelectedRows.Count > 0;

            if (!rowSelected)
            {
                MessageBox.Show("Please select a row to delete it.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Are your sure you want delete this register?", "Confirmation", MessageBoxButtons.OKCancel);

            if(result == DialogResult.OK)
            {
                int id = int.Parse(_view.buyDGV.SelectedCells[0].Value.ToString());
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
                DialogResult confirm =  MessageBox.Show("¿Are you sure you want delete it?", "Confirmation", MessageBoxButtons.OKCancel);

                if (confirm == DialogResult.OK)
                {
                    _buyDetails.RemoveAt(e.RowIndex);
                    BuildBuyDetailDataGridView();
                    CalculateTotal();
                }
            }
        }
        #endregion

        #region

        public void AutocompleteSupplier()
        {
            _list.Clear();

            _suppliers = _supplierDAO.GetSuppliers();

            foreach (var supplier in _suppliers)
            {
                _list.Add(supplier.Name);
            }

            _view.txtSupplier.AutoCompleteCustomSource = _list;
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
        public void VerifySupplierAutocompletSelection()
        {
            bool match = false;
            string supplierName = _view.txtSupplier.Text.Trim();

            foreach (var supplier in _suppliers)
            {
                if (supplier.Name.Equals(supplierName))
                {
                    _view.lblSupplierId.Text = supplier.Id.ToString();
                    match = true;
                    break;
                }
            }

            if (!match && supplierName.Length > 0)
            {
                MessageBox.Show("Please, select a item from autocomplete list");
                _view.txtSupplier.Text = "";
                _view.lblSupplierId.Text = "";
                _view.txtSupplier.Focus();
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

            foreach (var item in _buyDetails)
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

        public BuyDetail BuildBuyDetailItem()
        {
            string sku = _view.txtSku.Text;
            string name = _view.txtProductName.Text;
            string txtDiscount = _view.txtDiscount.Text;
            decimal price = decimal.Parse(_view.txtPrice.Text);
            int units = Int32.Parse(_view.txtUnits.Text);
            decimal discount = (string.IsNullOrEmpty(txtDiscount)) ? 0
                : decimal.Parse(txtDiscount);
            decimal subtotal = decimal.Parse(_view.txtSubtotal.Text);

            var buyDetail = new BuyDetail();

            buyDetail.ProductSku = sku;
            buyDetail.ProductName = name;
            buyDetail.Price = price;
            buyDetail.Units = units;
            buyDetail.Discount = discount;
            buyDetail.Subtotal = subtotal;

            return buyDetail;
        }

        public void BuildBuyDetailDataGridView()
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
            
            foreach (var item in _buyDetails)
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
                var buyModel = BuildBuyModel();
                var insert = _buyDAO.Insert(buyModel);

                if (insert)
                {
                    MessageBox.Show("Register added successfully.");
                    FillDataGridView();
                    ClearInputFields();
                    _buyDetails.Clear();
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
                var buyModel = BuildBuyModel();
                bool update = _buyDAO.Update(buyModel.Id, buyModel);

                if (update)
                {
                    MessageBox.Show("Register updated successfully.");
                    FillDataGridView();
                    ClearInputFields();
                    _buyDetails.Clear();
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
                bool delete = _buyDAO.Delete(id);

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
            _view.lblSupplierId.Text = "";
            _view.lblUserId.Text = "";
            _view.txtSupplier.Text = "";
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

        public void FillBuyInputs(Buy buy)
        {
            _view.lblBuyId.Text = buy.Id.ToString();
            _view.lblSupplierId.Text = buy.SupplierId.ToString();
            _view.lblUserId.Text = buy.UserId.ToString();
            _view.txtUsername.Text = buy.Username.ToString();
            _view.txtSupplier.Text = buy.SupplierName;
            _view.dtpDate.Value = buy.Date;
            _total = buy.Total;
            _view.lblTotal.Text = buy.Total.ToString();
            _buyDetails = buy.BuyDetail;

            BuildBuyDetailDataGridView();
        }

        public void UpdateItemBuyDetail()
        {
            var buyDetail = BuildBuyDetailItem();

            _buyDetails.RemoveAt(_rowIndexItemSelected);
            _buyDetails.Insert(_rowIndexItemSelected, buyDetail);
        }

        public void FillDataGridView()
        {
            var buyDAO2 = new BuyDAO();
            _view.buyDGV.DataSource = buyDAO2.GetAll();
        }

        public bool BuyInputsAreValid()
        {
            return !(string.IsNullOrEmpty(_view.lblSupplierId.Text))
                    && !(string.IsNullOrEmpty(_view.dtpDate.Text))
                    && (decimal.Parse(_view.lblTotal.Text) > 0)
                    && (_buyDetails.Count > 0);
        }
        public Buy BuildBuyModel()
        {
            var buyModel = new Buy();

            buyModel.Id = (string.IsNullOrEmpty(_view.lblBuyId.Text)) ? 0 
                : Int32.Parse(_view.lblBuyId.Text);
            buyModel.SupplierId = Int32.Parse(_view.lblSupplierId.Text);
            buyModel.UserId = 1;
            buyModel.Date = DateTime.Parse(_view.dtpDate.Text);
            buyModel.Date = DateTime.Parse(buyModel.Date.ToString("yyyy-MM-dd"));
            buyModel.Total = _total;
            buyModel.BuyDetail = _buyDetails;

            Console.WriteLine(buyModel.Date);

            return buyModel;
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
