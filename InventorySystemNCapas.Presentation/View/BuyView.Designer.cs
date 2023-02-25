namespace InventorySystemNCapas.Presentation.View
{
    partial class BuyView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuyView));
            this.buyDGV = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBuyId = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblSupplierId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClearItemInputs = new System.Windows.Forms.PictureBox();
            this.btnSaveItem = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUnits = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.itemsDGV = new System.Windows.Forms.DataGridView();
            this.Customer = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelUp = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.buyDGV)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearItemInputs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDGV)).BeginInit();
            this.panelUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // buyDGV
            // 
            this.buyDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.buyDGV.Location = new System.Drawing.Point(552, 118);
            this.buyDGV.Name = "buyDGV";
            this.buyDGV.ReadOnly = true;
            this.buyDGV.Size = new System.Drawing.Size(530, 307);
            this.buyDGV.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBuyId);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.lblUserId);
            this.panel1.Controls.Add(this.lblSupplierId);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSupplier);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtUsername);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.itemsDGV);
            this.panel1.Location = new System.Drawing.Point(12, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 406);
            this.panel1.TabIndex = 4;
            // 
            // lblBuyId
            // 
            this.lblBuyId.AutoSize = true;
            this.lblBuyId.Location = new System.Drawing.Point(15, 44);
            this.lblBuyId.Name = "lblBuyId";
            this.lblBuyId.Size = new System.Drawing.Size(0, 13);
            this.lblBuyId.TabIndex = 22;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(400, 374);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(17, 18);
            this.lblTotal.TabIndex = 21;
            this.lblTotal.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(324, 374);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 18);
            this.label10.TabIndex = 20;
            this.label10.Text = "Total: ";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(347, 24);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(100, 20);
            this.dtpDate.TabIndex = 19;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(19, 26);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(0, 13);
            this.lblUserId.TabIndex = 14;
            // 
            // lblSupplierId
            // 
            this.lblSupplierId.AutoSize = true;
            this.lblSupplierId.Location = new System.Drawing.Point(184, 28);
            this.lblSupplierId.Name = "lblSupplierId";
            this.lblSupplierId.Size = new System.Drawing.Size(0, 13);
            this.lblSupplierId.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(348, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(201, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Supplier";
            // 
            // txtSupplier
            // 
            this.txtSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSupplier.Location = new System.Drawing.Point(201, 25);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(100, 20);
            this.txtSupplier.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(36, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(39, 24);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.btnClearItemInputs);
            this.panel2.Controls.Add(this.btnSaveItem);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtSubtotal);
            this.panel2.Controls.Add(this.txtDiscount);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtUnits);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtPrice);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtProductName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtSku);
            this.panel2.Location = new System.Drawing.Point(0, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(492, 129);
            this.panel2.TabIndex = 1;
            // 
            // btnClearItemInputs
            // 
            this.btnClearItemInputs.Image = global::InventorySystemNCapas.Presentation.Properties.Resources.clear;
            this.btnClearItemInputs.Location = new System.Drawing.Point(454, 60);
            this.btnClearItemInputs.Name = "btnClearItemInputs";
            this.btnClearItemInputs.Size = new System.Drawing.Size(37, 25);
            this.btnClearItemInputs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClearItemInputs.TabIndex = 26;
            this.btnClearItemInputs.TabStop = false;
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSaveItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveItem.Location = new System.Drawing.Point(381, 100);
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Size = new System.Drawing.Size(97, 28);
            this.btnSaveItem.TabIndex = 25;
            this.btnSaveItem.Text = "Add Item";
            this.btnSaveItem.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(274, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 15);
            this.label9.TabIndex = 24;
            this.label9.Text = "Subtotal";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(139, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 15);
            this.label11.TabIndex = 22;
            this.label11.Text = "Discount";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(274, 86);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(100, 20);
            this.txtSubtotal.TabIndex = 23;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(142, 88);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(100, 20);
            this.txtDiscount.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(22, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "Units";
            // 
            // txtUnits
            // 
            this.txtUnits.Location = new System.Drawing.Point(22, 86);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.Size = new System.Drawing.Size(100, 20);
            this.txtUnits.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(378, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Price";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(378, 29);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(100, 20);
            this.txtPrice.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(139, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "Name";
            // 
            // txtProductName
            // 
            this.txtProductName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductName.Location = new System.Drawing.Point(142, 29);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(220, 20);
            this.txtProductName.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(22, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "SKU";
            // 
            // txtSku
            // 
            this.txtSku.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSku.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSku.Location = new System.Drawing.Point(22, 29);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(114, 20);
            this.txtSku.TabIndex = 13;
            // 
            // itemsDGV
            // 
            this.itemsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemsDGV.Location = new System.Drawing.Point(0, 199);
            this.itemsDGV.Name = "itemsDGV";
            this.itemsDGV.Size = new System.Drawing.Size(492, 172);
            this.itemsDGV.TabIndex = 0;
            // 
            // Customer
            // 
            this.Customer.AutoSize = true;
            this.Customer.Font = new System.Drawing.Font("Sans Serif Collection", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Customer.ForeColor = System.Drawing.Color.Black;
            this.Customer.Location = new System.Drawing.Point(766, 62);
            this.Customer.Name = "Customer";
            this.Customer.Size = new System.Drawing.Size(110, 47);
            this.Customer.TabIndex = 5;
            this.Customer.Text = "BUYS";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // panelUp
            // 
            this.panelUp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelUp.Controls.Add(this.btnClose);
            this.panelUp.Location = new System.Drawing.Point(-1, 0);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(1095, 25);
            this.panelUp.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1066, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(29, 24);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Brown;
            this.btnDelete.Location = new System.Drawing.Point(826, 437);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Brown;
            this.btnEdit.Location = new System.Drawing.Point(730, 437);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Brown;
            this.btnSave.Location = new System.Drawing.Point(216, 463);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 29);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save Buy";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnBack.Image = global::InventorySystemNCapas.Presentation.Properties.Resources.previous;
            this.btnBack.Location = new System.Drawing.Point(-1, 25);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(34, 26);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnBack.TabIndex = 21;
            this.btnBack.TabStop = false;
            // 
            // BuyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(1094, 524);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.panelUp);
            this.Controls.Add(this.Customer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buyDGV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BuyView";
            this.Text = "BuyView";
            ((System.ComponentModel.ISupportInitialize)(this.buyDGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearItemInputs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDGV)).EndInit();
            this.panelUp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView buyDGV;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Customer;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.Panel panelUp;
        public System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtSubtotal;
        public System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtUnits;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtSku;
        public System.Windows.Forms.Label lblSupplierId;
        public System.Windows.Forms.Label lblUserId;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnSaveItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label lblTotal;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DateTimePicker dtpDate;
        public System.Windows.Forms.Label lblBuyId;
        public System.Windows.Forms.DataGridView itemsDGV;
        public System.Windows.Forms.PictureBox btnClearItemInputs;
        public System.Windows.Forms.PictureBox btnBack;
    }
}