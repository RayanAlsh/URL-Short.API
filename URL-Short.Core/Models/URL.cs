namespace URL_Short.Core
{
    public class URL
    {
        public Guid Id { get; set; }
        public string Default_URL { get; set; }
        public string Short_URL { get; set; }
        public DateTime CreatedAt { get; set; }

        public URL()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
