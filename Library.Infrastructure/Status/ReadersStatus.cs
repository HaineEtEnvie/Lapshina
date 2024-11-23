using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Status
{
    public class ReadersStatus
    {
        public long Id { get; set; }

        public string Fullname { get; set; }

        public string Phonenumber { get; set; }

        public string Adress { get; set; }
    }
}
