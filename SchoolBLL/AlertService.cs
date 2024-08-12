using EntryLogManagement.SchoolDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolBLL
{
    internal class AlertService
    {
        private readonly AlertRepository alertRepository;

        public AlertService()
        {
            alertRepository = new AlertRepository();
        }

        public List<Alert> GetAlertToday()
        {
            return alertRepository.GetAlert();
        }

        public List<Alert> GetAlertAll()
        {
            return alertRepository.GetAlertAll();
        }
    }
}
