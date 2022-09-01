using Application.Features.Codings.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Codings.Queries.GetListCoding
{
    public class GetListCodingQuery : IRequest<CodingListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListCodingQueryHandler : IRequestHandler<GetListCodingQuery, CodingListModel>
        {
            private readonly ICodingRepository _codingRepository;
            private readonly IMapper _mapper;

            public GetListCodingQueryHandler(ICodingRepository codingRepository, IMapper mapper)
            {
                _codingRepository = codingRepository;
                _mapper = mapper;
            }

            public async Task<CodingListModel> Handle(GetListCodingQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Coding> codings = await _codingRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                CodingListModel mappedCodingListModel = _mapper.Map<CodingListModel>(codings);

                return mappedCodingListModel;
            }
        }
    }
}
