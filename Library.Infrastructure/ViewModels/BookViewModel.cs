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

        public string writerfirstname { get; set; }

        public string writesecondrname { get; set; }

        public string writerlastname { get; set; }
    }
}
