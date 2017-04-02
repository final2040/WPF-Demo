using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WpfApplicationPractice.Models
{
    public class JobData : IData<JobEntity>
    {
        private readonly List<JobEntity> _jobs;

        public JobData()
        {
            _jobs = new List<JobEntity>()
            {
                new JobEntity() {Id = 1, Name = "Gerente"},
                new JobEntity() {Id = 2, Name = "Supervisor"},
                new JobEntity() {Id = 3, Name = "Auxiliar"}
            };
        }

        public List<JobEntity> GetAll()
        {
            return _jobs.ToList();
        }

        public List<JobEntity> Get(Func<JobEntity, bool> predicate)
        {
            return _jobs.Where(predicate).ToList();
        }

        public JobEntity GetFirst(Func<JobEntity, bool> predicate)
        {
            return _jobs.FirstOrDefault(predicate);
        }

        public void AddOrUpdate(JobEntity entity)
        {
            if (!Exists(entity))
                _jobs.Add(entity);
            else
            {
                JobEntity jobToUpdate = _jobs.FirstOrDefault(j => j.Id == entity.Id);
                jobToUpdate.Name = entity.Name;
            }
        }

        public bool Exists(JobEntity entity)
        {
            return _jobs.Any(j => j.Id == entity.Id);
        }

        public void Remove(JobEntity entity)
        {
            _jobs.RemoveAt(_jobs.IndexOf(entity));
        }
    }
}