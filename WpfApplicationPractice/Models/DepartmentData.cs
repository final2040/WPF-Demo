using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApplicationPractice.Models
{
    public class DepartmentData:IData<DepartmentEntity>
    {
        private readonly List<DepartmentEntity> _departments;

        public DepartmentData()
        {
            _departments = new List<DepartmentEntity>()
            {
                new DepartmentEntity() {Id = 1, Name = "Sistemas"},
                new DepartmentEntity() {Id = 2, Name = "Finanzas"},
                new DepartmentEntity() {Id = 3, Name = "Operaciones"},
                new DepartmentEntity() {Id = 4, Name = "Cumplimiento"}
            };

        }

        public List<DepartmentEntity> GetAll()
        {
            return _departments.ToList();
        }

        public List<DepartmentEntity> Get(Func<DepartmentEntity, bool> predicate)
        {
            return _departments.Where(predicate).ToList();
        }

        public DepartmentEntity GetFirst(Func<DepartmentEntity, bool> predicate)
        {
            return _departments.FirstOrDefault(predicate);
        }

        public void AddOrUpdate(DepartmentEntity entity)
        {
            if (!Exists(entity))
                _departments.Add(entity);
            else
            {
                DepartmentEntity entityToUpdate = _departments.FirstOrDefault(d => d.Id == entity.Id);
                entityToUpdate.Name = entity.Name;
            }
        }

        public bool Exists(DepartmentEntity entity)
        {
            return _departments.Any(d => d.Id == entity.Id);
        }

        public void Remove(DepartmentEntity entity)
        {
            _departments.RemoveAt(_departments.IndexOf(entity));
        }
    }
}