using AutoMapper;
using EmployeePostTrace.Api.Models.Requests;
using EmployeePostTrace.Api.Models.Responses;
using EmployeePostTrace.DataLayer.Models;

namespace EmployeePostTrace.Api.Infrastructure;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<RegistrationEmployeeRequest, EmployeeDto>();
        CreateMap<UpdateEmployeeRequest, EmployeeDto>();
        CreateMap<EmployeeDto, EmployeeMainInfoResponse>();
        CreateMap<EmployeeDto, EmployeeAllInfoResponse>();

        CreateMap<AddLetterRequest, LetterDto>();
        CreateMap<UpdateLetterRequest, LetterDto>();
        CreateMap<LetterDto, LetterAllInfoResponse>();
        CreateMap<LetterDto, LetterMainInfoResponse>();

        CreateMap<LoginRequest, EmployeeDto>();
    }
}
