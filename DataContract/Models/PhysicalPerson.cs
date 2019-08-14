using DataContract.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContract.Models
{
    [PhysicalPersonValidator]
    [Serializable]
    public class PhysicalPerson
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        public int GenderId { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        [StringLength(11, MinimumLength = 11)]
        public string PersonalId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        
        public int TelephoneTypeId { get; set; }

        [ForeignKey("TelephoneTypeId")]
        public virtual TelephoneType TelephoneType { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string TelephoneNumber { get; set; }

        public string Photo { get; set; }

        [NotMapped]
        public virtual List<PhysicalPersonConnection> PhysicalPersonConnections { get; set; }

        [NotMapped]
        public List<PhysicalPersonConnetionReportContract> ConnectionsReport { get; set; }
    }
}
