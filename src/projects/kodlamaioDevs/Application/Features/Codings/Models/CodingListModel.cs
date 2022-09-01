using Application.Features.Codings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Codings.Models
{
    public class CodingListModel
    {
        public IList<CodingListDto> Items { get; set; }
    }
}
