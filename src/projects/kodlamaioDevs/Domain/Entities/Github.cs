using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Github : Entity
    {
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }
        public Github()
        {
           
        }
        public Github(int id, int userId, string profileUrl) : this()
        {
            Id = id;
            UserId = userId;
            ProfileUrl = profileUrl;
        }
    }
}
