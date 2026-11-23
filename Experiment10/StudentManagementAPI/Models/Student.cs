namespace StudentManagementAPI.Models;

public class Student
{
    public int Id { get; set; }
    public required string EnrollmentNo { get; set; }
    public required string FullName { get; set; }
    public string Branch { get; set; } = "CE";
    public int Semester { get; set; }
    public decimal CGPA { get; set; }
}
