using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartWeb
{
    public partial class Contact : Page
    {
        private int myVar;

        public int Test
        {
            get { return myVar; }
            set { myVar = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Test = 5;
        }
    }
}