namespace Colony_Management_System.Models
{
    namespace Colony_Management_System.Models
    {
        public class Opiekun
        {
            public int Id { get; set; }
            public int KontoId { get; set; }
            public string Telefon { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }

            public Konto Konto { get; set; }
            public ICollection<OpiekunGrupa> OpiekunowieGrup { get; set; }
        }
    }

}
