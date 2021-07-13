using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Classes.Helpers;
using NorthWindCoreLibrary.Projections;

namespace SortingByStringPropertyName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Shown += OnShown;

            dataGridView1.AutoGenerateColumns = false;
        }

        private void OnShown(object sender, EventArgs e)
        {
            PropertyNamesListBox.DataSource = TypeHelper.PropertyNamesOrderedByName(new CustomerItemSort());
        }

        private async void ReadCustomersButton_Click(object sender, EventArgs e)
        {
            List<CustomerItemSort> customers = await CustomersOperations
                .GetCustomersWithProjectionSortAsync(); 
            
            List<CustomerItemSort> customersSortedSortByPropertyName = 
                customers.SortByPropertyName(PropertyNamesListBox.Text,
                    AscendingRadioButton.Checked ? 
                        SortDirection.Ascending : 
                        SortDirection.Descending);

            dataGridView1.DataSource = customersSortedSortByPropertyName;

        }
    }
}
