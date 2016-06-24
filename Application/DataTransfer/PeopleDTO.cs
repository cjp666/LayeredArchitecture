﻿using System.Collections.Generic;

namespace CJSoftware.Application.DataTransfer
{
    public class PeopleDTO
    {
        public int Id { get; set; }

        public string EmployeeReference { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public List<PersonAddressDTO> Addresses { get; set; } = new List<PersonAddressDTO>();
    }
}