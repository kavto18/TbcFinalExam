using Microsoft.AspNetCore.Http;

namespace DataContract.Models
{
    public class PhysicalPersonPictureViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }
    }
}
