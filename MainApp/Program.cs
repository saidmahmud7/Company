using Library.Model;
using Library.Service;
using Npgsql;

EmployeeService employeeService = new EmployeeService();
Employee employee = new Employee();

while (true)
{
    Console.WriteLine("====== Меню управления сотрудниками ======");
    Console.WriteLine("1. Добавить сотрудника");
    Console.WriteLine("2. Обновить сотрудника");
    Console.WriteLine("3. Удалить сотрудника");
    Console.WriteLine("4. Показать всех сотрудников");
    Console.WriteLine("5. Найти сотрудника по ID");
    Console.WriteLine("6. Добавить отдел");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");

    int choice = int.Parse(Console.ReadLine());
    if (choice == 1)
    {
        try
        {
            Console.Write("Введите имю и фамилию сотрудника: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введите номер телефона: ");
            string phone = Console.ReadLine();
            Console.Write("Введите email: ");
            string email = Console.ReadLine();
            System.Console.Write("Введите должность: ");
            string position = Console.ReadLine();
            Console.WriteLine("Выберите ID отдела :");
            var departments = employeeService.GetDepartments();
            foreach (var dept in departments)
            {
                Console.WriteLine($"ID: {dept.Id}, Название: {dept.Name}");
            }
            Console.Write("Введите ID отдела: ");
            int departmentInput = int.Parse(Console.ReadLine());
            employeeService.InsertEmployee(new Employee { FullName = name, Age = age, Phone = phone, Email = email, Position = position, DepartmentId = departmentInput });
            System.Console.WriteLine("Успешно добавлено!");
            System.Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления сотрудника: {ex.Message}");
        }
    }
    if (choice == 2)
    {
        try
        {
            Console.Write("Введите ID сотрудника для обновления: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите новое имя и фамилию сотрудника: ");
            string name = Console.ReadLine();
            Console.Write("Введите новый возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введите новый номер телефона: ");
            string phone = Console.ReadLine();
            Console.Write("Введите новый email: ");
            string email = Console.ReadLine();
            Console.Write("Введите новую должность: ");
            string position = Console.ReadLine();
            employeeService.UpdateEmployee(new Employee { Id = id, FullName = name, Age = age, Phone = phone, Email = email, Position = position });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления сотрудника: {ex.Message}");
        }
    }
    if (choice == 3)
    {
        try
        {
            Console.Write("Введите ID сотрудника для удаления: ");
            int id = int.Parse(Console.ReadLine());
            employeeService.Delete(id);
            Console.WriteLine("Сотрудник успешно удалён!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления сотрудника: {ex.Message}");
        }

    }
    if (choice == 4)
    {
        try
        {
            foreach (var el in employeeService.GetEmployees())
            {
                Console.WriteLine($"""
                Имя: {el.FullName}
                Возраст: {el.Age}
                Телефон: {el.Phone}
                Email: {el.Email}
                Должность: {el.Position}
                Отдел: {el.Department.Name}
                """);
                System.Console.WriteLine(new String('-', 50));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения списка сотрудников: {ex.Message}");
        }
    }
    if (choice == 5)
    {
        try
        {
            Console.Write("Введите ID сотрудника для поиска: ");
            int id = int.Parse(Console.ReadLine());
            employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                Console.WriteLine($"""
                Имя: {employee.FullName}
                Возраст: {employee.Age}
                Телефон: {employee.Phone}
                Email: {employee.Email}
                Должность: {employee.Position}
                """);
            }
            else
            {
                Console.WriteLine("Сотрудник не найден!");
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка поиска сотрудника: {ex.Message}");
        }
    }
    if (choice == 6)
    {
        try
        {
            Console.Write("Введите название отдела: ");
            string departmentName = Console.ReadLine();

            employeeService.InsertDepartment(new Department { Name = departmentName });
            Console.WriteLine("Отдел успешно добавлен!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления отдела: {ex.Message}");
        }

    }
    if (choice == 0)
    {
        break;
    }
}
