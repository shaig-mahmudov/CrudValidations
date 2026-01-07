using System.ComponentModel.DataAnnotations;

namespace WebAppPractiece.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage ="Bos ola bilmez!!")]
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
