namespace TunaPiano.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Artist_Id { get; set; }
        public string Album { get; set; }
        public string Length { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
