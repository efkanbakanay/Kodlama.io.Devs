using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Githubs.Models
{
    public class GithubListModel
    {
        public IList<Github> Items { get; set; }
    }
}
