using System;
using System.Collections.Generic;
using System.Text;
using BMS.BooksLibrary.BusinessLayer.Models;

namespace BMS.BusinessLayer.Library.Models
{
  public  class LendingRequestModel
    {
        public int LendingRequestId { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedEmail { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string LentBy { get; set; }
        public DateTime? LentOn { get; set; }
        public DateTime? ReturnedOn { get; set; }
        public List<BookModel> BooksLent { get; set; }
        public string Note { get; set; }
        public bool IsReadyToCollect { get; set; }
    }
}
