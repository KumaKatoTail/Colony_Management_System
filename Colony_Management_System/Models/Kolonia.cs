namespace Colony_Management_System.Models
{
    namespace Colony_Management_System.Models
    {
        public class Kolonia
        {
            public int Id { get; set; }
            public int FirmaId { get; set; }
            public int AdresId { get; set; }
            public int FormaId { get; set; }
            public DateTime TerminOd { get; set; }
            public DateTime TerminDo { get; set; }
            public string TrasaWedrowna { get; set; }
            public string Kraj { get; set; }

            public Firma Firma { get; set; }
            public Adres Adres { get; set; }
            public Forma Forma { get; set; }
            public ICollection<Grupa> Grupy { get; set; }
        }
    }

}
