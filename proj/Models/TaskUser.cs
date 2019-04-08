using System;

namespace proj.Models
{
    public class TaskUser
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public Login Login { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Check { get; set; }
    }
}