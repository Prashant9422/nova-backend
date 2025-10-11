using System.Runtime;
using AutoMapper;
using NovaApp.DTOs;
using NovaApp.Models;


namespace NovaApp.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Product
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductDto, Product>();

        // Service
        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<CreateServiceDto, Service>();

        // Employee
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<CreateEmployeeDto, Employee>();

        // Auth / User mappings
        // RegisterDto -> User (map incoming registration data to entity)
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // don't map password as hash here
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())    // will be set by model default or DB
            .ForMember(dest => dest.Role, opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.Role))); // map role only if provided

        // User -> UserDto (safe public projection)
        CreateMap<User, UserDto>();
    }
}
