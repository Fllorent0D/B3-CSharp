using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DTOLibrary;

namespace SmartWeb
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public List<PostDTO> SelectNews()
        {
            return BLLVideotheque.getNews();

        }
    }
}