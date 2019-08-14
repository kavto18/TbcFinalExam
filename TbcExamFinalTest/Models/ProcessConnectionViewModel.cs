using DataContract.Models;
using System.Collections.Generic;

namespace TbcExamFinalTest.Models
{
    public class ProcessConnectionViewModel
    {
        public PhysicalPerson PhysicalPerson { get; set; }

        public IEnumerable<PhysicalPersonConnection> Connections { get; set; }
    }
}
