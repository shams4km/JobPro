// JobPro.API/MappingProfile.cs
using AutoMapper;
using JobPro.Core.Models;
using JobPro.Core.DTOs;

namespace JobPro
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vacancy, VacancyDTO>();
        }
    }
}