using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeletedLanguageCommand : IRequest<DeletedLanguageDto>
    {
        public int Id { get; set; }

        public class DeletedLanguageCommandHandler : IRequestHandler<DeletedLanguageCommand, DeletedLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public DeletedLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<DeletedLanguageDto> Handle(DeletedLanguageCommand request, CancellationToken cancellationToken)
            {
                await _languageBusinessRules.LanguageShouldExistWhenRequested(request.Id);
                Language? language = await _languageRepository.GetAsync(p => p.Id == request.Id);
                Language deletedLanguage = await _languageRepository.DeleteAsync(language);
                DeletedLanguageDto deletedLanguageDto =
                    _mapper.Map<DeletedLanguageDto>(deletedLanguage);
                return deletedLanguageDto;
            }
        }
    }
}
