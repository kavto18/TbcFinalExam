using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContract.Models
{
    [Serializable]
    public class City
    {
        [ForeignKey("CityId")]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
