using DataContract.Validator;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContract.Models
{
    [Serializable]
    [PhysicalPersonConnectionValidator]
    public class PhysicalPersonConnection
    {
        public long Id { get; set; }

        public long PhysicalPersonId { get; set; }

        [ForeignKey("PhysicalPersonId")]
        public PhysicalPerson PhysicalPerson { get; set; }

        public long ConnectedPhysicalPersonId { get; set; }

        [ForeignKey("ConnectedPhysicalPersonId")]
        public PhysicalPerson ConnectedPhysicalPerson { get; set; }

        public int PhysicalPersonConnectionTypeId { get; set; }

        [ForeignKey("PhysicalPersonConnectionTypeId")]
        public PhysicalPersonConnectionType PhysicalPersonConnectionType { get; set; }
    }
}
