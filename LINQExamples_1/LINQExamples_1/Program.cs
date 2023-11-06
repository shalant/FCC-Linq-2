using LINQExamples_1;
using System.Linq;


List<Employee> employeeList = Data.GetEmployees();
List<Department> departmentList = Data.GetDepartments();

//Select and Where Operators - Method Syntax
//var results = employeeList.Select(e => new
//{
//    FullName = e.FirstName + " " + e.LastName,
//    AnnualSalary = e.AnnualSalary
//}).Where(e => e.AnnualSalary >= 5000);

//foreach (var item in results)
//{
//    Console.WriteLine($"{item.FullName, -20}, {item.AnnualSalary, 10}");
//}

//Console.ReadKey();



//Select and Where Operators - QUERY SYNTAX
//var results = from emp in employeeList
//              where emp.AnnualSalary >= 50000
//              select new
//              {
//                  FullName = emp.FirstName + " " + emp.LastName,
//                  AnnualSalary = emp.AnnualSalary
//              };

//employeeList.Add(new Employee
//{
//    Id = 5,
//    FirstName = "Sam",
//    LastName = "Davis",
//    AnnualSalary = 100000.20m,
//    DepartmentId = 2
//});

//foreach (var item in results)
//{
//    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
//}


//Deferred execution example
//var results = from emp in employeeList.GetHighSalariedEmployees()
//              select new
//              {
//                  FullName = emp.FirstName + " " + emp.LastName,
//                  AnnualSalary = emp.AnnualSalary
//              };


//employeeList.Add(new Employee
//{
//    Id = 5,
//    FirstName = "Sam",
//    LastName = "Davis",
//    AnnualSalary = 100000.20m,
//    DepartmentId = 2
//});

//foreach (var item in results)
//{
//    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
//}


//Immediate execution example
//var results = (from emp in employeeList.GetHighSalariedEmployees()
//              select new
//              {
//                  FullName = emp.FirstName + " " + emp.LastName,
//                  AnnualSalary = emp.AnnualSalary
//              }).ToList();


//employeeList.Add(new Employee
//{
//    Id = 5,
//    FirstName = "Sam",
//    LastName = "Davis",
//    AnnualSalary = 100000.20m,
//    DepartmentId = 2
//});

//foreach (var item in results)
//{
//    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
//}


// Join Operation Example - Method Syntax
//var results = departmentList.Join(employeeList,
//    department => department.Id,
//    employee => employee.DepartmentId,
//    (department, employee) => new
//    {
//        FullName = employee.FirstName + " " + employee.LastName,
//        AnnualSalary = employee.AnnualSalary,
//        DepartmentName = department.LongName
//    }
//    );

//foreach (var item in results)
//{
//    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}\t{item.departmentName}");
//}


//Join Operation Example- Query Syntax
//var results = from dept in departmentList
//              join emp in employeeList
//              on dept.Id equals emp.DepartmentId
//              select new
//              {
//                  FullName = emp.FirstName + " " + emp.LastName,
//                  AnnualSalary = emp.AnnualSalary,
//                  DepartmentName = dept.LongName
//              };

//foreach (var item in results)
//{
//    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}\t{item.DepartmentName}");
//}



var results = departmentList.GroupJoin(employeeList,
    dept => dept.Id,
    emp => emp.DepartmentId,
    (dept, employeesGroup) => new
    {
        Employees = employeesGroup,
        DepartmentName = dept.LongName
    }
    );

foreach (var item in results)
{
    Console.WriteLine($"DepartmentName: {item.DepartmentName}");
    foreach(var emp in item.Employees)
    {
        Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
    }
}

Console.ReadKey();


public static class EnumerableCollectionExtensionMethods
{
    public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
    {
        foreach (Employee emp in employees)
        {
            Console.WriteLine($"Accessing employee: {emp.FirstName + " " + emp.LastName}");
            if (emp.AnnualSalary >= 50000)
                yield return emp;
        }
    }
}