using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new DatabaseContext())
            {
                //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                var employeeChihuahua = ctx.Employees
                                        .Where(employee => employee.City.Name == "Chihuahua")
                                        .Select(s => new { s.FirstName, s.City.Name });

                Console.WriteLine("EJERCICIO 1:");
                foreach (var employee in employeeChihuahua)
                {
                    Console.WriteLine("Name: {0}, City: {1}", employee.FirstName, employee.Name);
                }

                //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.
                var employeeDepartment = ctx.Employees
                     .GroupBy(employee => employee.Department.Name)
                     .Select(s => new { DepartmentName = s.Key, Count = s.Count() });

                Console.WriteLine();
                Console.WriteLine("EJERCICIO 2:");
                foreach (var employee in employeeDepartment)
                {
                    Console.WriteLine("Department: {0}, count: {1}", employee.DepartmentName, employee.Count);
                }

                //3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departament
                var employeeRemote = ctx.Employees
                    .Where(employee => employee.Department.Cities.Contains(employee.City))
                    .Select(s => new {Name = s.FirstName, City = s.City.Name, Department = s.Department.Name });

                Console.WriteLine();
                Console.WriteLine("EJERCICIO 3:");
                foreach (var employee in employeeRemote)
                {
                    Console.WriteLine("Name: {0}, City: {1}, Departmen: {2}", employee.Name, employee.City, employee.Department);
                }

                //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.
                var employeeAni = ctx.Employees
                    .Where(employee => employee.HireDate.Month - DateTime.Now.Month <= 1 && employee.HireDate.Month - DateTime.Now.Month > 0 )
                    .Select(s => new { Name = s.FirstName, HireDay = s.HireDate.Day, HireMonth = s.HireDate.Month });

                Console.WriteLine();
                Console.WriteLine("EJERCICIO 4");
                foreach (var employee in employeeAni)
                {
                    Console.WriteLine("Name: {0}, Day: {1}, Month: {2}", employee.Name, employee.HireDay, employee.HireMonth);
                }

                //Listar los 12 meses del año y el numero de empleados contratados por cada mes.
                var employeeYear = ctx.Employees
                    .GroupBy(employee => employee.HireDate.Month)
                    .OrderBy(s => s.Key)
                    .Select(s => new { Month = s.Key, Count = s.Count() });

                Console.WriteLine();
                Console.WriteLine("EJERCICIO 5");
                foreach (var employee in employeeYear)
                {
                    Console.WriteLine("Month: {0}, amount = {1}", employee.Month, employee.Count);
                }
            }


            Console.Read();
        }
    }
}
