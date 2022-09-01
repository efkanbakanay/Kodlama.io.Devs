using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Codings.Rules
{
    public class CodingBusinessRules
    {
        private readonly ICodingRepository _codingRepository;

        public CodingBusinessRules(ICodingRepository codingRepository)
        {
            _codingRepository = codingRepository;
        }

        public async Task CodingNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Coding> result = await _codingRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Coding name exists.");
        }

        public void CodingShouldExistWhenRequested(Coding coding)
        {
            if (coding == null) throw new BusinessException("Requested coding does not exist");
        }
    }
}
