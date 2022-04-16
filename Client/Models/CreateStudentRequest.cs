namespace Models
{
    public class CreateStudentRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public CreateSchoolRequest SchoolRequest { get; set; }
        public CreateBookRequest BookRequest { get; set; }
    }
}
