using Application.Features.Githubs.Models;
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

namespace Application.Features.Githubs.Queries.GetListGithub
{
    public class GetListGithubQuery : IRequest<GithubListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListGithubQueryHandler : IRequestHandler<GetListGithubQuery, GithubListModel>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;

            public GetListGithubQueryHandler(IGithubRepository githubRepository, IMapper mapper)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
            }

            public async Task<GithubListModel> Handle(GetListGithubQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Github> githubs = await _githubRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                GithubListModel mappedGithubListModel =
                    _mapper.Map<GithubListModel>(githubs);
                return mappedGithubListModel;
            }
        }
    }
}
