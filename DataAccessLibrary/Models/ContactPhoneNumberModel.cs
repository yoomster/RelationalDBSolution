﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ContactPhoneNumberModel
    {
        public int Id { get; set; }
        public int ContactID { get; set; }
        public int PhoneNumberId { get; set; }
    }
}
