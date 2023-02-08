namespace CrudDapperMultiQuerySchoolApi.IServices
{
    public interface ISchoolService
    {
        object Save(Student oStudent, Teacher oTeacher);
        object Gets();
        object Get(int studentId, int teacherId);
        string Delete(int studentId, int teacherId);
    }
}
