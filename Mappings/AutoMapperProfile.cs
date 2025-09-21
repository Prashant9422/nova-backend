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
    }
}
