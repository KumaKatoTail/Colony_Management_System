namespace Colony_Management_System.Models
{
    public class GrupaDto
    {
        public int Id { get; set; }
        public int KoloniaId { get; set; }
        public string Temat { get; set; }
        public string Opis { get; set; }
        public int Limit { get; set; }
        public int WolneMiejsca { get; set; }
    }

}
