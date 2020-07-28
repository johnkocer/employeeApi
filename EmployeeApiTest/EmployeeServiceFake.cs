using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartIT.Employee.MockDB;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApiTest
{
    public class EmployeeServiceFake : IEmployeeRepository
    {
        private readonly List<Employee> _employeeList;

        public EmployeeServiceFake()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1, DepartmentId = 1, Gender = "Male", Name = "Mike", Salary = 8000},
                new Employee() {Id = 2, DepartmentId = 1, Gender = "Male", Name = "Adam", Salary = 5000},
                new Employee() {Id = 3, DepartmentId = 1, Gender = "Female", Name = "Jacky", Salary = 9000}
            };
        }

        public Employee Update(Employee item)
        {
            var findItem = _employeeList.Find(e => e.Id == item.Id);
            if (findItem != null)
            {
                findItem.Name = item.Name;
                findItem.Gender = item.Gender;
                findItem.Salary = item.Salary;

                return findItem;
            }

            return null;
        }

        public async Task<Employee> UpdateAsync(Employee item)
        {
            var findItem = await Task.FromResult( _employeeList.Find(e => e.Id == item.Id));
            if (findItem != null)
            {
                findItem.Name = item.Name;
                findItem.Gender = item.Gender;
                findItem.Salary = item.Salary;

                return findItem;
            }

            return null;
        }

        public ICollection<Employee> FindbyId(int id)
        {
            var findItems = _employeeList.FindAll(e => e.Id == id);
            if (findItems != null)
            {
                return findItems;
            }
            return null;
        }

        public async Task<Employee> FindbyIdAsync(int id)
        {
            var findItem = await Task.FromResult( _employeeList.Find(e => e.Id == id));
            if (findItem != null)
                return findItem;
            return null;
        }

        public ICollection<Employee> FindbyName(string name)
        {
            var findItem = _employeeList.Find(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            if (findItem != null)
            {
                var list = new List<Employee>();
                list.Add(findItem);
                return list;
            }
            return null;
        }

        public async Task<Employee> FindbyNameAsync(string name)
        {
            var findItem = await Task.FromResult( _employeeList.Find(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
            if (findItem != null)
                return findItem;
            return null;
        }

        public ICollection<Employee> GetAll()
        {
            return _employeeList;
        }

        public async Task<ICollection<Employee>> GetAllAsync()
        {
            return await Task.FromResult( _employeeList);
        }

        public List<Employee> Items { get; }
        public int Count { get; }
        public Employee Add(IBaseEntity t)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> AddAsync(Employee t)
        {
            t.Id = _employeeList.Count + 1;
            await Task.Run( ()=>_employeeList.Add(t));
            return t;
        }

        public void Delete(IBaseEntity t)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(IBaseEntity t)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
