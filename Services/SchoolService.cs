using CrudDapperMultiQuerySchoolApi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;

namespace CrudDapperMultiQuerySchoolApi.Services
{
    public class SchoolService : ISchoolService
    {
        public string Delete(int studentId, int teacherId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = @"Delete From Student Where StudentId = " + studentId + ";" +
                                    "Delete From Teacher Where TeacherId = " + teacherId;
                    con.QueryMultiple(query);

                    return "Deleted";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public object Get(int studentId, int teacherId)
        {
            dynamic obj = new ExpandoObject();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = @"Select * From Student Where StudentId = " + studentId + ";" +
                                "Select * From Teacher Where TeacherId = " + teacherId;
                using (var multi = con.QueryMultiple(query, null))
                {
                    obj.Students = multi.Read<Student>().Single();
                    obj.Teachers = multi.Read<Teacher>().Single();
                }
            }
            return obj;
        }

        public object Gets()
        {
            dynamic obj = new ExpandoObject();
            using(IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = @"Select * From Student;
                                 Select * From Teacher";

                using (var multi = con.QueryMultiple(query, null))
                {
                    obj.Students = multi.Read<Student>().ToList();
                    obj.Teachers = multi.Read<Teacher>().ToList();
                }
            }
            return obj;
        }

        public object Save(Student oStudent, Teacher oTeacher)
        {
            dynamic obj = new ExpandoObject();
            try
            {
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = @"EXEC sp_Student @StudentId, @Name, @Roll;
                                     EXEC spTeacher @TeacherId, @FullName, @SubjectName;";

                    using (var multi = con.QueryMultiple(query, new
                    {
                        oStudent.StudentId,
                        oStudent.Name,
                        oStudent.Roll,
                        oTeacher.TeacherId,
                        oTeacher.FullName,
                        oTeacher.SubjectName
                    }))
                    {
                        obj.Students = multi.Read<Student>().Single();
                        obj.Teachers = multi.Read<Teacher>().Single();
                    }
                }
            }
            catch(Exception ex)
            {
                obj.Message = ex.Message;
            }

            return obj;
        }
    }
}
