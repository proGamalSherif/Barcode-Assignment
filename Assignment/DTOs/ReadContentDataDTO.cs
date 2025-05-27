namespace Assignment.DTOs
{
    public class ReadContentDataDTO
    {
        public int ContentId { get; set; }
        public string ContentTitle { get; set; }
        public string ContentDescription { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedIn { get; set; }
    }
}
