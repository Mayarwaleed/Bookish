using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models;

public partial class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } 
    public int AuthorId { get; set; }  

    public DateTime? PublicationDate { get; set; }
    public bool AvailabilityBookStatus { get; set; }
    public int CopiesAvailable { get; set; }
    public bool IsSeries { get; set; }
    public int? SeriesId { get; set; }
    public int? CategoryId { get; set; }
   
    public virtual Category? Category { get; set; } = null!;
    public virtual Series? Series { get; set; }
    public virtual Author? Author { get; set; }
}
    

