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

namespace Application.Features.Codings.Queries.GetByIdCoding
{
    public class GetByIdCodingQuery : IRequest<CodingGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdCodingQueryHandler : IRequestHandler<GetByIdCodingQuery, CodingGetByIdDto>
        {
            private readonly ICodingRepository _codingRepository;
            private readonly IMapper _mapper;
            private readonly CodingBusinessRules _codingBusinessRules;

            public GetByIdCodingQueryHandler(ICodingRepository codingRepository, IMapper mapper, CodingBusinessRules codingBusinessRules)
            {
                _codingRepository = codingRepository;
                _mapper = mapper;
                _codingBusinessRules = codingBusinessRules;
            }

            public async Task<CodingGetByIdDto> Handle(GetByIdCodingQuery request, CancellationToken cancellationToken)
            {
                Coding? coding = await _codingRepository.GetAsync(b => b.Id == request.Id);

                _codingBusinessRules.CodingShouldExistWhenRequested(coding);

                CodingGetByIdDto codingGetByIdDto = _mapper.Map<CodingGetByIdDto>(coding);
                return codingGetByIdDto;
            }
        }
    }
}
