using InventorySystemNCapas.BLL;
using InventorySystemNCapas.Presentation.View;
using InventorySystemNCapas.Models;
using System;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections.Generic;

namespace InventorySystemNCapas.Presentation.Controller
{
    public class CustomerController
    {
        private CustomerView _view;
        public MenuView _menuView;
        private CustomerDAO _customerDAO;
        private bool _edit;

        private int _posX = 0;
        private int _posY = 0;

        public CustomerController(MenuView menuView, CustomerView view)
        {
            _menuView = menuView;
            _view = view;
            _customerDAO = new CustomerDAO();
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

        #region actions of components
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

        public void BtnClear()
        {
            ClearInputFields();
            _view.btnSave.Text = "Create";
        }

        public void BtnClose()
        {
            if (_menuView != null)
            {
                _menuView.Dispose();
            }

            _view.Dispose();
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
            var rowIsSelected = _view.customerDGV.SelectedRows.Count > 0;

            if (!rowIsSelected)
            {
                MessageBox.Show("Please, select a row to edit it.");
                _edit = false;
                _view.btnSave.Text = "Create";
                return;
            }
            _view.btnSave.Text = "Update";

            DataGridViewRow registerSelected = _view.customerDGV.SelectedRows[0];
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
            var rowIsSelected = _view.customerDGV.SelectedRows.Count > 0;

            if (!rowIsSelected)
            {
                MessageBox.Show("Please, select a row to delete it.");
                return;
            }

            DialogResult confirmation = MessageBox.Show("¿Are you sure you want delete this register?", "Confirmation", MessageBoxButtons.OKCancel);
            
            if (confirmation == DialogResult.OK)
            {
                int id = int.Parse(_view.customerDGV.SelectedRows[0].Cells[0].Value.ToString());
                DeleteRegister(id);
            }
        }
        #endregion

        #region methods
        private void InsertRegister()
        {
            if (FieldsRequiredAreEmpty())
            {
                MessageBox.Show("All field inputs are required.");
                return;
            }

            try
            {
                var customer = BuildCustomerModel();
                var insert = _customerDAO.Insert(customer);

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
                var customerUpdated = BuildCustomerModel();
                var update = _customerDAO.Update(customerUpdated.Id, customerUpdated);

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

        private Customer BuildCustomerModel()
        {
            Customer customer = new Customer();

            customer.Id = (!string.IsNullOrEmpty(_view.lblId.Text)) ?
                int.Parse(_view.lblId.Text) : 0;
            customer.Name = _view.txtName.Text;
            customer.Address = _view.txtAddress.Text;
            customer.Email = _view.txtEmail.Text;
            customer.Phone = _view.txtPhone.Text;

            return customer;
        }

        public void FillDataGridView()
        {
            var customerDAO2 = new CustomerDAO();
            _view.customerDGV.DataSource = customerDAO2.GetAll();
        }

        private void DeleteRegister(int id)
        {
            try
            {
                var delete = _customerDAO.Delete(id);

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
                MessageBox.Show("Creando nueva instancia de menu view");
            }

            _view.Hide();
            _menuView.FormClosed += (s, args) => _view.Close();
            _menuView.Show();
        }
        #endregion
    }
}
