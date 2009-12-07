using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PensioMoto
{
    public partial class MotoForm : Form
    {
        public MotoForm()
        {
            InitializeComponent();

            ExpiryMonth.DataSource = new List<int>() { 1,2,3,4,5,6,7,8,9,10,11,12 };
            ExpiryYear.DataSource = new List<int>() { 2009,2010,2011,2012,2013,2014,2015,2016,2017,2018,2019,2020 };
        }
    }
}
