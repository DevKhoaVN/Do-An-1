using EntryLogManagement.SchoolDAL;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolBLL
{
    internal class UserService 
    {
        private readonly HandleLogRepository handleLogRepository;

        public UserService()
        {
            handleLogRepository = new HandleLogRepository();
        }

        public bool RegisterUser(string username, string password, int parentId)
        {
           
            return handleLogRepository.HandleRegister(username, password, parentId);
        }

        public User LoginUser(string username , string password)
        {
            
             return  handleLogRepository.HandleLogin(username, password);

                       
        }
    }
}
