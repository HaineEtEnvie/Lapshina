using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.ViewModels
{
    public class BookViewModel
    {
        public long id { get; set; }

        public string name { get; set; }

        public string publishinghouse { get; set; }

        public string genre { get; set; }

        public string writerfullname { get; set; }
    }
}
