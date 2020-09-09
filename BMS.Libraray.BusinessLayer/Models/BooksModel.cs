namespace BMS.Library.BusinessLayer.Models
{
    public class BooksModel
    {
        public string Barcode { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string MainImageFileName { get; set; }
        public int NumberOfCopies { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
    }
}