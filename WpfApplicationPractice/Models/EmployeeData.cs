using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApplicationPractice.Models
{
    public class EmployeeData:IData<EmployeeEntity>
    {
        private readonly List<EmployeeEntity> _employees;

        public EmployeeData()
        {
            DepartmentData departments = new DepartmentData();
            JobData jobs = new JobData();
            _employees = new List<EmployeeEntity>()
            {
                new EmployeeEntity() {
                    Id = 1,
                    Name = "Rene Cruz Ballinas",
                    Age = 37,
                    Department = departments.GetFirst(d => d.Name.Equals("Sistemas")),
                    Job = jobs.GetFirst(d=> d.Name.Equals("Gerente"))
                },
                new EmployeeEntity() {
                    Id = 2,
                    Name = "Carlos Moctezuma Flores",
                    Age = 38,
                    Department = departments.GetFirst(d => d.Name.Equals("Finanzas")),
                    Job = jobs.GetFirst(d=> d.Name.Equals("Gerente"))
                },
                new EmployeeEntity() {
                    Id = 3,
                    Age = 32,
                    Name = "Gerardo Raymundo Mendez Castillo",
                    Department = departments.GetFirst(d => d.Name.Equals("Sistemas")),
                    Job = jobs.GetFirst(d=> d.Name.Equals("Auxiliar"))
                },
                new EmployeeEntity() {
                    Id = 4,
                    Age = 25,
                    Name = "Yari Guerrero",
                    Department = departments.GetFirst(d => d.Name.Equals("Finanzas")),
                    Job = jobs.GetFirst(d=> d.Name.Equals("Supervisor"))
                },
                new EmployeeEntity() {
                    Id = 5,
                    Age = 22,
                    Name = "Rosa Nely Mejia",
                    Department = departments.GetFirst(d => d.Name.Equals("Finanzas")),
                    Job = jobs.GetFirst(d=> d.Name.Equals("Auxiliar"))
                },
            };
        }

        public List<EmployeeEntity> GetAll()
        {
            return _employees;
        }

        public List<EmployeeEntity> Get(Func<EmployeeEntity, bool> predicate)
        {
            return _employees.Where(predicate).ToList();
        }

        public EmployeeEntity GetFirst(Func<EmployeeEntity, bool> predicate)
        {
            return _employees.FirstOrDefault(predicate);
        }

        public void AddOrUpdate(EmployeeEntity entity)
        {
            if (!Exists(entity))
            {
                entity.Id = _employees.Max(e => e.Id) + 1;
                _employees.Add(entity);
            }
            else
            {
                EmployeeEntity entityToUpdate = _employees.FirstOrDefault(e => e.Id == entity.Id);
                entityToUpdate.Name = entity.Name;
                entityToUpdate.Age = entity.Age;
                entityToUpdate.Department = entity.Department;
                entityToUpdate.Job = entity.Job;
            }
        }

        public bool Exists(EmployeeEntity entity)
        {
            return _employees.Any(e => e.Id == entity.Id);
        }

        public void Remove(EmployeeEntity entity)
        {
            _employees.RemoveAt(_employees.IndexOf(entity));
        }
    }
}