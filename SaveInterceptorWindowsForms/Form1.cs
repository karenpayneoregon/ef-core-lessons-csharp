using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NorthWindCoreLibrary.Models;
using SaveChangesInterceptor.Classes;

namespace SaveChangesInterceptor
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Employee to work with
        /// </summary>
        private int identifier = 7;
        
        public Form1()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        /// <summary>
        /// When this form is displayed get employee with an id of <see cref="identifier"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnShown(object sender, EventArgs e)
        {
            await Task.Delay(500);

            try
            {
                var employee = await NorthWindOperations.ReadEmployee(identifier);

                FirstNameTextBox.Text = employee.FirstName;
                LastNameTextBox.Text = employee.LastName;


                FirstNameTextBox.SelectionStart = FirstNameTextBox.Text.Length;
                FirstNameTextBox.SelectionLength = 0;
                SaveButton.Enabled = true;
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }


        /// <summary>
        /// Save employee with id <see cref="identifier"/>
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"><see cref="EventArgs"/></param>
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            /*
             * Simple assertion to ensure both first and last name have values
             */
            if (Controls.OfType<TextBox>().Any(textbox => string.IsNullOrWhiteSpace(textbox.Text)))
            {
                MessageBox.Show("First and last name are reqired");
                ResetEmployee();

                return;
            }


            var employee = await NorthWindOperations.ReadEmployee(identifier);
            employee.FirstName = FirstNameTextBox.Text;
            employee.LastName = LastNameTextBox.Text;

            try
            {
                var success = NorthWindOperations.SaveEmployee(employee);
                MessageBox.Show(success ? "Saved" : "Failed");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Reset Employee from database current first and last names
        /// </summary>
        private void ResetEmployee()
        {
            Employees originalEmployee = NorthWindOperations.OriginalEmployee(identifier);
            
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                FirstNameTextBox.Text = originalEmployee.FirstName;
            }

            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                LastNameTextBox.Text = originalEmployee.LastName;
            }
        }

    }
}
