namespace Colony_Management_System.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<KoloniaDziecko> KolonieDzieci { get; set; }
    }
}
