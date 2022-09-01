using Application.Features.Codings.Dtos;
using Application.Features.Codings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Codings.Commands.CreateCoding
{
    public partial class CreateCodingCommand : IRequest<CreatedCodingDto>
    {
        public string Name { get; set; }

        public class CreateCodingCommandHandler : IRequestHandler<CreateCodingCommand, CreatedCodingDto>
        {
            private readonly ICodingRepository _codingRepository;
            private readonly IMapper _mapper;
            private readonly CodingBusinessRules _codingBusinessRules;

            public CreateCodingCommandHandler(ICodingRepository brandRepository, IMapper mapper, CodingBusinessRules codingBusinessRules)
            {
                _codingRepository = brandRepository;
                _mapper = mapper;
                _codingBusinessRules = codingBusinessRules;
            }

            public async Task<CreatedCodingDto> Handle(CreateCodingCommand request, CancellationToken cancellationToken)
            {
                await _codingBusinessRules.CodingNameCanNotBeDuplicatedWhenInserted(request.Name);

                Coding mappedCoding = _mapper.Map<Coding>(request);
                Coding createdCoding = await _codingRepository.AddAsync(mappedCoding);
                CreatedCodingDto createdCodingDto = _mapper.Map<CreatedCodingDto>(createdCoding);

                return createdCodingDto;

            }
        }
    }
}
