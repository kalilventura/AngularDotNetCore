﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Domain.Models
{
    public class EmployeeAddress : Entity
    {
        public EmployeeAddress()
        {

        }

        public EmployeeAddress(Address newAddress)
        {
            Address = newAddress;
        }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

    }
}
