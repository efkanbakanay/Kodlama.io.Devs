using Application.Features.Codings.Commands.CreateCoding;
using Application.Features.Codings.Dtos;
using Application.Features.Codings.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Codings.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Coding, CreatedCodingDto>().ReverseMap();
            CreateMap<Coding, CreateCodingCommand>().ReverseMap();
            CreateMap<IPaginate<Coding>, CodingListModel>().ReverseMap();
            CreateMap<Coding, CodingListDto>().ReverseMap();
            CreateMap<Coding, CodingGetByIdDto>().ReverseMap();
        }
       
    }
}
