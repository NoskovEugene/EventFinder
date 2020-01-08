
namespace EventFinder.Models.Entity
{
    public class Image : EntitiesAbstraction.EntityBase
    {
        public string FileName { get; set; }

        public byte[] File { get; set; }

        public virtual User User { get; set; }
    }
}
