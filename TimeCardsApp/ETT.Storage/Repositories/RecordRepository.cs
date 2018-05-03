using ETT.Storage.Context;
using ETT.Storage.Entities;
using ETT.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Storage.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private ApplicationContext context;

        public RecordRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Record item)
        {
            context.Records.Add(item);
        }

        public void Delete(int id)
        {
            Record record = context.Records.Find(id);
            if (record != null)
                context.Records.Remove(record);
        }

        public Record Get(int id)
        {
            return context.Records.Include(x => x.Task).Include(x => x.Task).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Record> GetAll()
        {
            return context.Records.Include(x => x.Task).Include(x => x.Task).AsEnumerable<Record>();
        }

        public void Update(Record item)
        {
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
