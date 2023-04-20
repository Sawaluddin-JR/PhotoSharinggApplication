using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoSharinggApplication.Models
{
    public class Email
    {
        public string NamaClient { get; set; }
        public int Port { get; set; }
        public string EmailKita { get; set; }
        public string PasswordEmail { get; set; }
    }
}
