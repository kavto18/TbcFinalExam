using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContract.Models
{
    [Serializable]
    public class PhysicalPersonConnectionType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<PhysicalPersonConnection> PhysicalPersonConnections { get; set; }
    }
}
