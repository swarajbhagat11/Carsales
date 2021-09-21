using Carsales.Models.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carsales.Models
{
    [Table(name: "cars", Schema = "dbo")]
    public class CarDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NonUpdatable]
        public Guid id { get; set; }

        public int year { get; set; }

        public string make { get; set; }

        public string model { get; set; }

        public string comments { get; set; }

        // use this field if advertisedPriceType is DAP
        public double driveAwayPrice { get; set; }

        // use this field if advertisedPriceType is EGC
        public double excludingGovernmentCharges { get; set; }

        // values: DAP or EGC
        public string advertisedPriceType { get; set; }

        public bool isDealer { get; set; }

        // Use this field if the flagged as a non-dealer(private) vehicle
        public string name { get; set; }

        // Use this field if the flagged as a non-dealer(private) vehicle
        public string phone { get; set; }

        public string email { get; set; }

        public string dealerABN { get; set; }

        [NonUpdatable]
        public DateTime updatedOn { get; set; }

        [NonUpdatable]
        public DateTime createdOn { get; set; }
    }
}
