using Npgsql;
using Dapper;
using Library.Model;

namespace Library.Service;


public class EmployeeService
{
    string connectionString = "Server=127.0.0.1;Port=5432;Database=CompanyDb;User Id=postgres;Password=280806;";

    public bool InsertEmployee(Employee employee)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sql = @"insert into employees (fullname,Age,Phone,Email,Position,department_id) values(@fullname,@Age,@Phone,@Email,@Position,@DepartmentId)";
            var effect = connection.Execute(sql, employee);
            return effect > 0;
        }
    }
    public bool UpdateEmployee(Employee employee)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sql = @"update employees set fullname = @fullname,Age = @Age,Phone = @Phone,Email = @Email,Position = @Position,department_id=@DepartmentId
            where id = @id";
            var effect = connection.Execute(sql, employee);
            return effect > 0;
        }
    }
    public bool Delete(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sql = "delete from employees where id=@Id";
            var affected = connection.Execute(sql, new { Id = id });
            return affected > 0;
        }
    }
    public List<Employee> GetEmployees()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sql = @"
            SELECT 
                employees.FullName,
                employees.Age,
                employees.Phone,
                employees.Email,
                employees.Position,
                departments.name AS DepartmentName
            FROM employees
            JOIN departments 
                ON employees.department_id = departments.id";
            List<Employee> employees = connection.Query<Employee>(sql).ToList();
            return employees;
        }
    }
    public Employee GetEmployeeById(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sql = "select * from employees where id=@Id;";
            var students = connection.QuerySingle<Employee>(sql, new { Id = id });
            return students;
        }
    }
    public bool InsertDepartment(Department department)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sql = "INSERT INTO departments (name) VALUES (@Name)";
            var affectedRows = connection.Execute(sql, department);
            return affectedRows > 0;
        }
    }
    public List<Department> GetDepartments()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sql = "SELECT * FROM departments";
            return connection.Query<Department>(sql).ToList();
        }
    }
}

