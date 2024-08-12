using EntryLogManagement.SchoolDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolBLL
{
    internal class EntryLogService
    {
        private readonly EntryLogRepository entrylogRepository;

        public EntryLogService()
        {
            this.entrylogRepository = new EntryLogRepository();
        }

        public List<Entrylog> GetEtryLogAll()
        {
            return entrylogRepository.GetEntryLogAll();
        }

        public List<Entrylog> GetEtryLogID(int id)
        {
            return entrylogRepository.GetEntryLogId(id);
        }

        public List<Entrylog> GetEntryLogRangeTime(DateTime timeStart , DateTime timeEnd)
        {
            return entrylogRepository.GetEntryLogRangeTime(timeStart, timeEnd);
        }

     

    }
}
