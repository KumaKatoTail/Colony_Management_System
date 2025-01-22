namespace Colony_Management_System.Models
{
    namespace Colony_Management_System.Models
    {
        public class OpiekunGrupa
        {
            public int Id { get; set; }
            public int GrupaId { get; set; }
            public int OpiekunId { get; set; }

            public Grupa Grupa { get; set; }
            public Opiekun Opiekun { get; set; }
        }
    }

}
