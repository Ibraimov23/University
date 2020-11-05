using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models.Events
{
    public abstract class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime  StartDateTime { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
