using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ULProject.Models;

namespace ULProject.ViewModels
{
    public class TasksPageViewModel : BindableBase
    {
        public List<TaskType> Tasks { get; set; }
        public TasksPageViewModel()
        {
            Tasks = new List<TaskType> {
                new TaskType { Title = "Bursaries", Task = "Avail bursary to students view until 30 November"},
                new TaskType { Title = "October Documention", Task = "Finish with your 1 month documention and send it to Boity for review, please make sure you do that by the end on working hours and do notify Boity if she doesn't come back to you"},
                new TaskType { Title = "Archievements", Task = "Update your weekly archeivements for October"}, 
                new TaskType { Title = "Alert", Task = "Message Brian to drop me the file"}, 
                new TaskType { Title = "Alert", Task = "Ask Celine to check final designs"}, 
                new TaskType { Title = "Approval", Task = "Approve Boitumelo's request"}, 
                new TaskType { Title = "Grand access", Task = "Grand Boitumelo access to perfomance document"}, 
                new TaskType { Title = "Alert", Task = "Meet with Antony at 2.30."}, 
                new TaskType { Title = "Clearance", Task = "Clear all the approved tasks for this month after working hours"}

            };
        }
    }
}
