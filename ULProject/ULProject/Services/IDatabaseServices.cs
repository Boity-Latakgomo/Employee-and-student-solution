using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ULProject.Services
{
    public interface IDatabaseServices
    {
        Task<bool> AddLeaveApplicationDetails(string userEmail, string inputLeave, string inputDescription, string startDate, string endDate, string leaveID);
    }
}
