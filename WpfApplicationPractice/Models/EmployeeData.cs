using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApplicationPractice.Models
{
    public class EmployeeData:IData<EmployeeEntity>
    {
        private static readonly DepartmentData Departments = new DepartmentData();
        private static readonly JobData Jobs = new JobData();
        private static readonly List<EmployeeEntity> Employees = new List<EmployeeEntity>()
        {
            new EmployeeEntity()
            {
                Id = 1,
                Name = "Rene Cruz Ballinas",
                Age = 37,
                Department = Departments.GetFirst(d => d.Name.Equals("Sistemas")),
                Job = Jobs.GetFirst(d => d.Name.Equals("Gerente"))
            },
            new EmployeeEntity()
            {
                Id = 2,
                Name = "Carlos Moctezuma Flores",
                Age = 38,
                Department = Departments.GetFirst(d => d.Name.Equals("Finanzas")),
                Job = Jobs.GetFirst(d => d.Name.Equals("Gerente"))
            },
            new EmployeeEntity()
            {
                Id = 3,
                Age = 32,
                Name = "Gerardo Raymundo Mendez Castillo",
                Department = Departments.GetFirst(d => d.Name.Equals("Sistemas")),
                Job = Jobs.GetFirst(d => d.Name.Equals("Auxiliar"))
            },
            new EmployeeEntity()
            {
                Id = 4,
                Age = 25,
                Name = "Yari Guerrero",
                Department = Departments.GetFirst(d => d.Name.Equals("Finanzas")),
                Job = Jobs.GetFirst(d => d.Name.Equals("Supervisor"))
            },
            new EmployeeEntity()
            {
                Id = 5,
                Age = 22,
                Name = "Rosa Nely Mejia",
                Department = Departments.GetFirst(d => d.Name.Equals("Finanzas")),
                Job = Jobs.GetFirst(d => d.Name.Equals("Auxiliar"))
            },
        };


        public List<EmployeeEntity> GetAll()
        {
            return Employees;
        }

        public List<EmployeeEntity> Get(Func<EmployeeEntity, bool> predicate)
        {
            return Employees.Where(predicate).ToList();
        }

        public EmployeeEntity GetFirst(Func<EmployeeEntity, bool> predicate)
        {
            return Employees.FirstOrDefault(predicate);
        }

        public void AddOrUpdate(EmployeeEntity entity)
        {
            if (!Exists(entity))
            {
                entity.Id = Employees.Max(e => e.Id) + 1;
                Employees.Add(entity);
            }
            else
            {
                EmployeeEntity entityToUpdate = Employees.FirstOrDefault(e => e.Id == entity.Id);
                entityToUpdate.Name = entity.Name;
                entityToUpdate.Age = entity.Age;
                entityToUpdate.Department = entity.Department;
                entityToUpdate.Job = entity.Job;
            }
        }

        public bool Exists(EmployeeEntity entity)
        {
            return Employees.Any(e => e.Id == entity.Id);
        }

        public void Remove(EmployeeEntity entity)
        {
            if (Exists(entity))
                Employees.RemoveAt(Employees.IndexOf(entity));
        }
    }
}