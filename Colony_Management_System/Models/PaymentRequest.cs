namespace Colony_Management_System.Models
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int RodzicId { get; set; }
        public int KoloniaDieckoId { get; set; }
        public int RodzajPlatnosciId { get; set; }
        public int WalutaId { get; set; } // Added missing property
    }
}

