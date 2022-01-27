using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Utils;

namespace Services.Extensions
{
    public static class ConfigureMappingExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection service)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            IMapper mapper = mappingConfig.CreateMapper();

            service.AddSingleton(mapper);

            return service;
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;

            CreateMap<Blog, BlogDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>()
                .ForMember(x => x.Password, y => y.MapFrom(z => PasswordEncrypter.Encrypt(z.Password)))
                ;

            //CreateMap<Orders, OrderDTO>()
            //    .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
            //    .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName));

            //CreateMap<OrderDTO, Orders>();



            //CreateMap<OrderItems, OrderItemsDTO>()
            //    .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName))
            //    .ForMember(x => x.OrderName, y => y.MapFrom(z => z.Order.Name ?? ""));

            //CreateMap<OrderItemsDTO, OrderItems>();
        }
    }
}

