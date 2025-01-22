namespace Colony_Management_System.Models
{
    namespace Colony_Management_System.Models
    {
        public class Grupa
        {
            public int Id { get; set; }
            public int KoloniaId { get; set; }
            public string Temat { get; set; }
            public string Opis { get; set; }
            public int Limit { get; set; }

            public Kolonia Kolonia { get; set; }
            public ICollection<KoloniaDziecko> KoloniaDzieci { get; set; }
            public ICollection<OpiekunGrupa> OpiekunowieGrupy { get; set; }
        }
    }

}
