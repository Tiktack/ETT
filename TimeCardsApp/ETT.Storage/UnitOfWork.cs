using ETT.Storage.Context;
using ETT.Storage.Interfaces;
using ETT.Storage.Repositories;
using ETT.Storage.Repositories.Projects;
using System;

namespace ETT.Storage
{
    /// <summary>
    /// Pattern "Unit of Work" https://metanit.com/sharp/mvc5/23.3.php
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationContext appContext = new ApplicationContext();


        private IUserRepository userRepository = null;
        public IUserRepository Users => userRepository ?? (userRepository = new UserRepository(appContext));


        private IProjectRepository projectRepository = null;
        public IProjectRepository Projects => projectRepository ?? (projectRepository = new ProjectRepository(appContext));

        private IProjectUserRepository projectUserRepository = null;
        public IProjectUserRepository ProjectUsers => projectUserRepository ?? (projectUserRepository = new ProjectUserRepository(appContext));

        private IProjectTaskRepository projectTaskRepository = null;
        public IProjectTaskRepository ProjectTasks => projectTaskRepository ?? (projectTaskRepository = new ProjectTaskRepository(appContext));


        private IRecordRepository recordRepository = null;
        public IRecordRepository Records => recordRepository ?? (recordRepository = new RecordRepository(appContext));


        public void Save()
        {
            appContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    appContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
