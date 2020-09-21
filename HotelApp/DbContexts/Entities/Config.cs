namespace HotelApp.API.DbContexts.Entities
{
    public class Config : IDeleteable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
