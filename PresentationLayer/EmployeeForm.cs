﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoodFoodSystem.BusinessLayer;
using GoodFoodSystem.DatabaseLayer;

namespace GoodFoodSystem.PresentationLayer
{
    public partial class EmployeeForm : Form
    {

        #region Data Members
        private Employee employee;
        private EmployeeController employeeController;//
        private Role.RoleType roleValue;
        public bool employeeFormClosed = false;

        #endregion

        #region Property Method

        public Role.RoleType RoleValue
        {

            set
            {

                roleValue = value;
            }

        }

        #endregion

        #region Constructor
        public EmployeeForm(EmployeeController aController)
        {
            InitializeComponent();
            employeeController = aController;
        }

        #endregion

        #region Utility Methods
        private void ShowAll(bool value, Role.RoleType roleType)
        {
            idLabel.Visible = value;
            empIDLabel.Visible = value;
            nameLabel.Visible = value;
            phoneLabel.Visible = value;
            paymentLabel.Visible = value;
            idTextBox.Visible = value;
            empIDTextBox.Visible = value;
            nameTextBox.Visible = value;
            phoneTextBox.Visible = value;
            paymentTextBox.Visible = value;
            submitButton.Visible = value;
            cancelButton.Visible = value;
            label2.Visible = value;
            if (!(value))
            {
                headWaitronRadioButton.Checked = false;
                waitronRadioButton.Checked = false;
                runnerRadioButton.Checked = false;
            }
            if ((roleType == Role.RoleType.Waiter) || (roleType == Role.RoleType.Runner) && value)
            {
                tipsLabel.Visible = value;
                tipsTextBox.Visible = value;
                hoursLabel.Visible = value;
                hoursTextBox.Visible = value;
            }
            else
            {
                tipsLabel.Visible = false;
                tipsTextBox.Visible = false;
                hoursLabel.Visible = false;
                hoursTextBox.Visible = false;
            }
        }
        private void ClearAll()
        {
            idTextBox.Text = "";
            empIDTextBox.Text = "";
            nameTextBox.Text = "";
            phoneTextBox.Text = "";
            paymentTextBox.Text = "";
            hoursTextBox.Text = "";
            tipsTextBox.Text = "";
        }
        private void PopulateObject(Role.RoleType roleType)
        {
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            employee = new Employee(roleType);
            employee.ID = idTextBox.Text;
            employee.EmployeeID = empIDTextBox.Text;
            employee.Name = nameTextBox.Text;
            employee.Telephone = phoneTextBox.Text;

            switch (employee.role.getRoleValue)
            {
                case Role.RoleType.Headwaiter:
                    headW = (HeadWaiter)(employee.role);
                    headW.SalaryAmount = decimal.Parse(paymentTextBox.Text);
                    break;
                case Role.RoleType.Waiter:
                    waiter = (Waiter)(employee.role);
                    waiter.getRate = decimal.Parse(paymentTextBox.Text);
                    waiter.getShifts = int.Parse(hoursTextBox.Text);
                    waiter.getTips = decimal.Parse(tipsTextBox.Text);
                    break;
                case Role.RoleType.Runner:
                    runner = (Runner)(employee.role);
                    runner.getRate = decimal.Parse(paymentTextBox.Text);
                    runner.getShifts = int.Parse(hoursTextBox.Text);
                    break;
            }
        }

        #endregion

        #region Form Events
        private void headWaitronRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Add Head Waiter";
            roleValue = Role.RoleType.Headwaiter;
            paymentLabel.Text = "Salary";
            ShowAll(true, roleValue);
            idTextBox.Focus();
        }

        private void waitronRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Add Waiter";
            roleValue = Role.RoleType.Waiter;
            paymentLabel.Text = "Rate";
            ShowAll(true, roleValue);
            idTextBox.Focus();
        }

        private void runnerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Add Runner";
            roleValue = Role.RoleType.Runner;
            paymentLabel.Text = "Rate";
            ShowAll(true, roleValue);
            idTextBox.Focus();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {

            PopulateObject(roleValue);
            MessageBox.Show("To be submitted to the Database!");
            employeeController.DataMaintenance(employee, DB.DBOperation.Add);
            employeeController.FinalizeChanges(employee);
            ClearAll();
            ShowAll(false, roleValue);
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            ShowAll(false, roleValue);
           
        }

        private void EmployeeForm_Activated(object sender, EventArgs e)
        {
            ShowAll(false, roleValue);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void extBtn_Click(object sender, EventArgs e)
        {
            employeeFormClosed = true;
            this.Close();
        }
        #endregion
    }
}
