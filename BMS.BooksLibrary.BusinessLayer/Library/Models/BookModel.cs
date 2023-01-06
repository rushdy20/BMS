namespace BMS.BooksLibrary.BusinessLayer.Models
{
    public class BookModel
    {
        public BooksCategoryModel BookCategory { get; set; }
        public string Barcode { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string MainImageFileName { get; set; }
        public int NumberOfCopies { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public bool IsAvailable { get; set; }
        public string Location { get; set; }

    }
}