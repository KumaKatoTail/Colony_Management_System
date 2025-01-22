namespace Colony_Management_System.Models
{
    namespace Colony_Management_System.Models
    {
        public class Konto
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Haslo { get; set; }
            public int UprId { get; set; }

            public Upr Upr { get; set; }
            public ICollection<Administrator> Administratorzy { get; set; }
            public ICollection<Rodzic> Rodzice { get; set; }
            public ICollection<Opiekun> Opiekunowie { get; set; }
        }
    }

}
