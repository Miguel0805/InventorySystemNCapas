
using InventorySystemNCapas.BLLL;
using InventorySystemNCapas.Models;
using InventorySystemNCapas.Presentation.View;
using System;
using System.Windows.Forms;

namespace InventorySystemNCapas.Presentation.Controller
{
    public class SupplierController
    {
        private SupplierView _view;
        private MenuView _menuView;
        private SupplierDAO _supplierDAO;

        private int _posX = 0;
        private int _posY = 0;
        private bool _edit = false;

        public SupplierController(MenuView menuView, SupplierView view)
        {
            _view = view;
            _menuView = menuView;
            _supplierDAO = new SupplierDAO();
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
            //ClearInputFields();
            //_view.btnSave.Text = "Create";
            var suppliers = _supplierDAO.GetSuppliers();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine(supplier.Name);
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
            _edit = true;
            var rowIsSelected = _view.supplierDGV.SelectedRows.Count > 0;

            if (!rowIsSelected)
            {
                MessageBox.Show("Please, select a row to edit it.");
                _edit = false;
                _view.btnSave.Text = "Create";
                return;
            }
            _view.btnSave.Text = "Update";

            DataGridViewRow registerSelected = _view.supplierDGV.SelectedRows[0];
            Customer customer = new Customer();

            customer.Id = int.Parse(registerSelected.Cells[0].Value.ToString());
            customer.Name = registerSelected.Cells[1].Value.ToString();
            customer.Address = registerSelected.Cells[2].Value.ToString();
            customer.Email = registerSelected.Cells[3].Value.ToString();
            customer.Phone = registerSelected.Cells[4].Value.ToString();

            FillCustomerInputs(customer);
        }

        public void BtnDelete()
        {
            var rowIsSelected = _view.supplierDGV.SelectedRows.Count > 0;

            if (!rowIsSelected)
            {
                MessageBox.Show("Please, select a row to delete it.");
                return;
            }

            DialogResult confirmation = MessageBox.Show("¿Are you sure you want delete this register?", "Confirmation", MessageBoxButtons.OKCancel);

            if (confirmation == DialogResult.OK)
            {
                int id = int.Parse(_view.supplierDGV.SelectedRows[0].Cells[0].Value.ToString());
                DeleteRegister(id);
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
                var supplier = BuildCustomerModel();
                var insert = _supplierDAO.Insert(supplier);

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
                var supplierUpdated = BuildCustomerModel();
                var update = _supplierDAO.Update(supplierUpdated.Id, supplierUpdated);

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
            return string.IsNullOrEmpty(_view.txtName.Text) ||
                string.IsNullOrEmpty(_view.txtAddress.Text) ||
                string.IsNullOrEmpty(_view.txtEmail.Text) ||
                string.IsNullOrEmpty(_view.txtPhone.Text);
        }

        private Supplier BuildCustomerModel()
        {
            Supplier supplier = new Supplier();

            supplier.Id = (!string.IsNullOrEmpty(_view.lblId.Text)) ?
                int.Parse(_view.lblId.Text) : 0;
            supplier.Name = _view.txtName.Text;
            supplier.Address = _view.txtAddress.Text;
            supplier.Email = _view.txtEmail.Text;
            supplier.Phone = _view.txtPhone.Text;

            return supplier;
        }
        private void FillDataGridView()
        {
            var supplierDAO2 = new SupplierDAO();
            _view.supplierDGV.DataSource = supplierDAO2.GetAll();
        }

        private void DeleteRegister(int id)
        {
            try
            {
                var delete = _supplierDAO.Delete(id);

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

        public void FillCustomerInputs(Customer obj)
        {
            _view.lblId.Text = obj.Id.ToString();
            _view.txtName.Text = obj.Name;
            _view.txtAddress.Text = obj.Address;
            _view.txtEmail.Text = obj.Email;
            _view.txtPhone.Text = obj.Phone;
        }
        private void ClearInputFields()
        {
            _view.lblId.Text = "";
            _view.txtName.Text = "";
            _view.txtAddress.Text = "";
            _view.txtEmail.Text = "";
            _view.txtPhone.Text = "";
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
