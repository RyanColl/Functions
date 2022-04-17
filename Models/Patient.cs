using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Functions.Models
{
    public partial class Patient
    {
        public int PatientId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DisplayName("Room Number")]
        public string RoomNumber { get; set; }
        [DisplayName("Date Checked In")]
        public DateTime InDate { get; set; }
        [DisplayName("Date Checked Out")]
        public DateTime OutDate { get; set; }   
    }
}
