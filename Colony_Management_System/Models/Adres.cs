namespace Colony_Management_System.Models
{
    public class Adres
    {
        public int Id { get; set; }
        public int UlicaId { get; set; }
        public int NrDomu { get; set; }
        public int? NrMieszkania { get; set; }

        public Ulica Ulica { get; set; }
    }
}
