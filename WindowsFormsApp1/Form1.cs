using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void ReadButton_Click(object sender, EventArgs e)
        {
            await ReadContacts();
        }

        /// <summary>
        /// Simple read, get count demo
        /// </summary>
        /// <returns></returns>
        public static async Task ReadContacts()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1);
                using (var context = new NorthWindEntities())
                {
                    var contacts = await context.Contacts.ToListAsync();
                    MessageBox.Show($@"Record count for contacts {contacts.Count}");
                }
            });
        }
    }
}
