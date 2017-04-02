using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WpfApplicationPractice.Models
{
    public class JobData : IData<JobEntity>
    {
        private static readonly List<JobEntity> Jobs = new List<JobEntity>()
            {
                new JobEntity() {Id = 1, Name = "Gerente"},
                new JobEntity() {Id = 2, Name = "Supervisor"},
                new JobEntity() {Id = 3, Name = "Auxiliar"}
            };
        
        public List<JobEntity> GetAll()
        {
            return Jobs.ToList();
        }

        public List<JobEntity> Get(Func<JobEntity, bool> predicate)
        {
            return Jobs.Where(predicate).ToList();
        }

        public JobEntity GetFirst(Func<JobEntity, bool> predicate)
        {
            return Jobs.FirstOrDefault(predicate);
        }

        public void AddOrUpdate(JobEntity entity)
        {
            if (!Exists(entity))
                Jobs.Add(entity);
            else
            {
                JobEntity jobToUpdate = Jobs.FirstOrDefault(j => j.Id == entity.Id);
                jobToUpdate.Name = entity.Name;
            }
        }

        public bool Exists(JobEntity entity)
        {
            return Jobs.Any(j => j.Id == entity.Id);
        }

        public void Remove(JobEntity entity)
        {
            Jobs.RemoveAt(Jobs.IndexOf(entity));
        }
    }
}