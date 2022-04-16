using System.Collections.Generic;

namespace Models
{
    public class ReadBookResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public List<CreateBookRequest> Books { get; set; }
    }
}