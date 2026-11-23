namespace StudentManagementAPI.Repositories;

using StudentManagementAPI.Models;

public interface IStudentRepository
{
    IEnumerable<Student> GetAll(string? branch = null);
    Student? GetById(int id);
    Student Add(Student student);
    bool Update(int id, Student student);
    bool Delete(int id);
}
