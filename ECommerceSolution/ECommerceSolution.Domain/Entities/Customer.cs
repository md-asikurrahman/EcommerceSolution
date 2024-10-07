﻿using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeMobile {  get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ImageUrl { get; set; }
        public IList<Address> AddressList { get; set; } = new List<Address>();
    }
}
