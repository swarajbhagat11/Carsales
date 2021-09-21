using System;

namespace Carsales.ViewModels
{
    public class Car
    {
        public Guid id { get; set; }

        public int year { get; set; }

        public string make { get; set; }

        public string model { get; set; }

        public string comments { get; set; }

        public double price { get; set; }

        // values: DAP or EGC
        public string advertisedPriceType { get; set; }

        public bool isDealer { get; set; }

        // Use this field if the flagged as a non-dealer(private) vehicle
        public string name { get; set; }

        // Use this field if the flagged as a non-dealer(private) vehicle
        public string phone { get; set; }

        public string email { get; set; }

        public string dealerABN { get; set; }
    }
}
