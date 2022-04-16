using System.Collections.Generic;

namespace Models
{
    public class CreateSchoolResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
