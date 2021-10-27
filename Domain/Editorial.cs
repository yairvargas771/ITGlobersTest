namespace Domain
{
    public class Editorial : Entity<int>
    {
        public string Nombre { get; set; }
        public string Sede { get; set; }

        private Editorial() { }
    }
}