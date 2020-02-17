﻿using System;
using CompanyAPI.Domain.Enum;

namespace CompanyAPI.Domain.Models
{
    public class Address : Entity
    {
        public Address() { }

        public Address(string street,
            string number,
            string complement,
            string district,
            string city,
            string state,
            string country,
            string zipCode,
            EAddressType type,
            Guid? id) : base(id)
        {
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Type = type;
        }

        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public EAddressType Type { get; set; }
        public Guid? EmployeeId { get; set; }
        //public virtual List<EmployeeAddress> Employee { get; set; }

        public string FullAddress()
        {
            return $"{Street}, {Number} - {City}/{State}";
        }
    }
}