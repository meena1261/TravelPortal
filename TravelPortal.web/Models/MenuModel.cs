using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class MenuModel : MenuRights
    {
        [Required(ErrorMessage = "Required")]
        public string MenuName { get; set; }
        public string URL { get; set; }
        public string PageName { get; set; }
        public string Icon { get; set; }
        public string ParentID { get; set; }
        public string ParentMenuName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Position { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string MenuStatus { get; set; }
    }
    public class MenuRights
    {
        public int Usrno { get; set; }
        public int ID { get; set; }
        public int Ischeck { get; set; }
        public int IsAdd { get; set; }
        public int IsEdit { get; set; }
        public int IsDelete { get; set; }
    }
}