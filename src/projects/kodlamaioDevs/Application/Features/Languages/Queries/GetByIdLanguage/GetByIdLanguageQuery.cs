using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Queries.GetByIdLanguageQuery
{
    public class GetByIdLanguageQuery : IRequest<LanguageGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdLanguageQueryHandler : IRequestHandler<GetByIdLanguageQuery, LanguageGetByIdDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public GetByIdLanguageQueryHandler(ILanguageRepository codingRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = codingRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<LanguageGetByIdDto> Handle(GetByIdLanguageQuery request, CancellationToken cancellationToken)
            {
                await _languageBusinessRules.LanguageShouldExistWhenRequested(request.Id);
                Language? language = await _languageRepository.GetAsync(p => p.Id == request.Id);
                LanguageGetByIdDto languageGetByIdDto =
                    _mapper.Map<LanguageGetByIdDto>(language);
                return languageGetByIdDto;
            }
        }
    }
}
