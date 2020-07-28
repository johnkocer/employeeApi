//Do not use in production, training, test and demo only
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SmartIT.Employee.MockDB
{
  public interface IBaseEntity
  {
    [Key]
    int Id { get; set; }
  }

  public interface IBaseRepository<T> where T : class
  {
      public ICollection<T> GetAll();
      public Task<ICollection<T>> GetAllAsync();
      public List<T> Items { get; }
      public int Count { get; }
      public T Add(IBaseEntity t);
      public Task<T> AddAsync(IBaseEntity t);
      public void Delete(IBaseEntity t);
      public Task<int> DeleteAsync(IBaseEntity t);
      public void DeleteAll();
  }

  public class BaseRepository<T> : IBaseRepository<T> where T : class
  {
    protected static List<T> _items = new List<T>();

    public ICollection<T> GetAll()
    {
      return Items;
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
      //return await Task.Run(() => Items);
      return await Task.FromResult( Items);
    }
    public List<T> Items { get { return _items; } }

    public T Add(IBaseEntity t)
    {
      t.Id = _items.Count + 1;
      _items.Add(t as T);
      return (T)t;
    }

    public async Task<T> AddAsync(IBaseEntity t)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> AddAsync(Employee t)
    {
      t.Id = _items.Count + 1;
      await Task.Run(() => _items.Add(t as T));
      return t ;
    }

    public void Delete(IBaseEntity t)
    {
      _items.Remove(t as T);
    }

    public async Task<int> DeleteAsync(IBaseEntity t)
    {
      await Task.FromResult( _items.Remove(t as T));
      return 1;
    }

    public void DeleteAll()
    {
      _items = new List<T>();
    }

    public int Count
    {
      get { return _items.Count; }
    }


  }

  public interface IEmployeeRepository
  {
      Employee Update(Employee item);
      Task<Employee> UpdateAsync(Employee item);
      ICollection<Employee> FindbyId(int id);
      Task<Employee> FindbyIdAsync(int id);
      ICollection<Employee> FindbyName(string name);
      Task<Employee> FindbyNameAsync(string name);
      ICollection<Employee> GetAll();
      Task<ICollection<Employee>> GetAllAsync();
      List<Employee> Items { get; }
      int Count { get; }
      Employee Add(IBaseEntity t);
      Task<Employee> AddAsync(Employee t);
      void Delete(IBaseEntity t);
      Task<int> DeleteAsync(IBaseEntity t);
      void DeleteAll();
  }

  public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
  {
    static EmployeeRepository()
    {

      _items.Add(new Employee() { Id = 1, DepartmentId = 1, Gender = "Male", Name = "Mike", Salary = 8000 });
      _items.Add(new Employee() { Id = 2, DepartmentId = 1, Gender = "Male", Name = "Adam", Salary = 5000 });
      _items.Add(new Employee() { Id = 3, DepartmentId = 1, Gender = "Female", Name = "Jacky", Salary = 9000 });
    }
    public Employee Update(Employee item)
    {
      var findItem = _items.Find(e => e.Id == item.Id);
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
      var findItem = await Task.FromResult( _items.Find(e => e.Id == item.Id));
      if (findItem != null)
      {
        findItem.Name = item.Name;
        findItem.Gender = item.Gender;
        findItem.Salary = item.Salary;

        return findItem;
      }

      return null;
    }

    //public Employee FindbyId(int id)
    //{
    //  var findItem = _items.Find(e => e.Id == id);
    //  if (findItem != null)
    //  {
    //    return findItem;
    //  }
    //  return null;
    //}
    public ICollection<Employee> FindbyId(int id)
    {
      var findItems = _items.FindAll(e => e.Id == id);
      if (findItems != null)
      {
        return findItems;
      }
      return null;
    }

    public async Task<Employee> FindbyIdAsync(int id)
    {
      var findItem = await Task.FromResult(_items.Find(e => e.Id == id));
      if (findItem != null)
        return findItem;
      return null;
    }

    public ICollection<Employee> FindbyName(string name)
    {
      var findItem = _items.Find(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
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
      var findItem = await Task.FromResult( _items.Find(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
      if (findItem != null)
        return findItem;
      return null;
    }
  }

  public class DepartmentRepository : BaseRepository<Department>
  {
    static DepartmentRepository()
    {
      _items.Add(new Department() { Id = 1, Name = "IT", Head = "Rick", Location = "Phoenix" });
      _items.Add(new Department() { Id = 2, Name = "HR", Head = "Jen", Location = "New York" });
      _items.Add(new Department() { Id = 3, Name = "RND", Head = "Sam", Location = "Los Angeles" });
    }
    public void Update(Department item)
    {
      var findItem = _items.Find(e => e.Id == item.Id);
      if (findItem != null)
      {
        findItem.Name = item.Name;
        findItem.Location = item.Location;
        findItem.Head = item.Head;
      }
    }

    public async Task<Department> UpdateAsync(Department item)
    {
      var findItem = await Task.Run(() => _items.Find(e => e.Id == item.Id));
      if (findItem != null)
      {
        findItem.Name = item.Name;
        findItem.Location = item.Location;
        findItem.Head = item.Head;
      }
      return findItem;
    }
  }

  public class TodoRepository : BaseRepository<Todo>
  {
    static TodoRepository()
    {
      _items.Add(new Todo() { Id = 1, Name = "Check eMails" });
      _items.Add(new Todo() { Id = 2, Name = "Do dishes" });
      _items.Add(new Todo() { Id = 3, Name = "Call Boss" });
    }


    public Todo Update(Todo item)
    {
      var findTodo = _items.Find(e => e.Id == item.Id);
      if (findTodo != null)
      {
        findTodo.Name = item.Name;
        return findTodo;
      }

      return null;
    }

    public async Task<Todo> UpdateAsync(Todo item)
    {
      var findTodo = await Task.FromResult( _items.Find(e => e.Id == item.Id));
      if (findTodo != null)
      {
        findTodo.Name = item.Name;
        return findTodo;
      }

      return null;
    }

    public Todo FindbyId(int id)
    {
      var findItem = _items.Find(e => e.Id == id);
      if (findItem != null)
      {
        return findItem;
      }
      return null;
    }

    public async Task<Todo> FindbyIdAsync(int id)
    {
      var findItem = await Task.FromResult( _items.Find(e => e.Id == id));
      if (findItem != null)
        return findItem;
      return null;
    }

    public ICollection<Todo> FindbyName(string name)
    {
      var findItem = _items.Find(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
      if (findItem != null)
      {
        var list = new List<Todo>();
        list.Add(findItem);
        return list;
      }
      return null;
    }

    public async Task<Todo> FindbyNameAsync(string name)
    {
      var findEmployee = await Task.FromResult( _items.Find(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
      if (findEmployee != null)
        return findEmployee;
      return null;
    }

  }
}
