using static System.Net.Mime.MediaTypeNames;

namespace Colony_Management_System.Models
{
    public class Administrator
    {
        public int Id { get; set; }
        public int KontoId { get; set; }
        public int FirmaId { get; set; }

        // Navigation properties
        public Konto Konto { get; set; }
        public Firma Firma { get; set; }
    }
}
