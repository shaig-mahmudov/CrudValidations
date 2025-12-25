namespace WebAppPractiece.Models
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
