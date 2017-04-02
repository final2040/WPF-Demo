using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApplicationPractice.Models
{
    public class DepartmentData:IData<DepartmentEntity>
    {
        private static readonly List<DepartmentEntity> Departments= new List<DepartmentEntity>()
            {
                new DepartmentEntity() {Id = 1, Name = "Sistemas"},
                new DepartmentEntity() {Id = 2, Name = "Finanzas"},
                new DepartmentEntity() {Id = 3, Name = "Operaciones"},
                new DepartmentEntity() {Id = 4, Name = "Cumplimiento"}
            };

        public DepartmentData()
        {

        }

        public List<DepartmentEntity> GetAll()
        {
            return Departments.ToList();
        }

        public List<DepartmentEntity> Get(Func<DepartmentEntity, bool> predicate)
        {
            return Departments.Where(predicate).ToList();
        }

        public DepartmentEntity GetFirst(Func<DepartmentEntity, bool> predicate)
        {
            return Departments.FirstOrDefault(predicate);
        }

        public void AddOrUpdate(DepartmentEntity entity)
        {
            if (!Exists(entity))
                Departments.Add(entity);
            else
            {
                DepartmentEntity entityToUpdate = Departments.FirstOrDefault(d => d.Id == entity.Id);
                entityToUpdate.Name = entity.Name;
            }
        }

        public bool Exists(DepartmentEntity entity)
        {
            return Departments.Any(d => d.Id == entity.Id);
        }

        public void Remove(DepartmentEntity entity)
        {
            Departments.RemoveAt(Departments.IndexOf(entity));
        }
    }
}