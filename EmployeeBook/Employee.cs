using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBook
{
    class Employee
    {
        private const string TEXT_FILE_NAME = "EmployeeTextFile.txt"; // Constant means the value will not change once we initialize. Generally, when you create a constant, you name use uppercase letters.

        public string Name { get; set; }

        public string Title { get; set; }

        public static Employee GetEmployee()
        {
            var employee = new Employee
            {
                Name = "John Doe",
                Title = "CEO"
            };
            return employee;
        }

        public static void WriteEmployee(Employee employee) // Here we are writing to a flat file, text file
        {
            var employeeData = $"{employee.Name}, {employee.Title}"; 
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, employeeData); // This is another layer of read action in here. We created FileHelper for this method to write to.
        }

        // Write employee to file

        public async static Task<ICollection<Employee>> GetEmployeesAsync() // This is going to give us a collection. If you make a method async, you have to return a task. Wratp it in a Task<>. Task is a thread.
            // async is the keyword that says it's going to run in a separate thread. You're always going to get that thread back. A thread will contain its thread value.
            // The return of a method should be a thread with its return. 
        {
            var employees = new List<Employee>(); // List is also another type of array. Arrays are fixed in size but behind the scenes will continue to add.
            var fileContent = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME); // This is the file where we'll read from, which we previously created. It will come back with the string of content from the text file.
            var lines = fileContent.Split('\r', '\n'); // '\r' is carriage return and '\n' is a new line. This will split by these parameters, which will split by more than one value. 
            foreach (var line in lines) //
            {
                if (string.IsNullOrEmpty(line)) // Exception handling; If there's an empty line or null, need to take this into account. Here we're saying we want to continue if there is one. 
                    continue;

                var lineParts = line.Split(','); // Now that we have a line, we want to split it by comma. This will also give us an array.

                var employee = new Employee // Since we know part zero is the name and the one is the title, we'll create an employee object
                {
                    Name = lineParts[0], // And we'll initialize it here. 
                    Title = lineParts[1] // And with these, now we've created an object.
                };
                employees.Add(employee);
            }
            return employees;  
        }
    }    
}