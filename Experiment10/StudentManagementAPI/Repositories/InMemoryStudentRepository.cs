namespace StudentManagementAPI.Repositories;

using System.Collections.Concurrent;
using StudentManagementAPI.Models;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly ConcurrentDictionary<int, Student> _students = new();
    private int _currentId;

    public InMemoryStudentRepository()
    {
        var initialStudents = new List<Student>
        {
            new() { Id = 1, EnrollmentNo = "21CE001", FullName = "Aarav Sharma", Branch = "CE", Semester = 5, CGPA = 8.75m },
            new() { Id = 2, EnrollmentNo = "21CE015", FullName = "Priya Patel", Branch = "CE", Semester = 5, CGPA = 9.20m },
            new() { Id = 3, EnrollmentNo = "21IT023", FullName = "Rohan Mehta", Branch = "IT", Semester = 5, CGPA = 8.40m },
            new() { Id = 4, EnrollmentNo = "22CE045", FullName = "Ananya Verma", Branch = "CE", Semester = 3, CGPA = 8.95m },
            new() { Id = 5, EnrollmentNo = "21EC012", FullName = "Dev Shah", Branch = "EC", Semester = 5, CGPA = 7.80m }
        };

        foreach (var student in initialStudents)
        {
            _students[student.Id] = student;
        }

        _currentId = 5;
    }

    public IEnumerable<Student> GetAll(string? branch = null)
    {
        if (string.IsNullOrWhiteSpace(branch))
        {
            return _students.Values.OrderBy(s => s.Id);
        }

        return _students.Values
            .Where(s => string.Equals(s.Branch, branch.Trim(), StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Id);
    }

    public Student? GetById(int id)
    {
        _students.TryGetValue(id, out var student);
        return student;
    }

    public Student Add(Student student)
    {
        student.Id = Interlocked.Increment(ref _currentId);
        _students[student.Id] = student;
        return student;
    }

    public bool Update(int id, Student student)
    {
        if (!_students.ContainsKey(id))
        {
            return false;
        }

        student.Id = id;
        _students[id] = student;
        return true;
    }

    public bool Delete(int id)
    {
        return _students.TryRemove(id, out _);
    }
}
