using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.ViewModels
{
    public class GroupViewModel
    {
        public GroupViewModel()
        {

        }
        public int ID { get; set; }
        public string WorkSpaceName { get; set; }
        public string Description { get; set; }
        public string User_ID { get; set; }
        public string ImageWS { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Updatedate { get; set; }
    }
}