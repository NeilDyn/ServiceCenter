using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Comment
    {
        public string Date { get; set; }
        public string CommentText { get; set; }

        public Comment(string dateP, string commentP)
        {
            Date = dateP;
            CommentText = commentP;
        }

        public Comment()
        {

        }
    }
}