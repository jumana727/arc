using AutoMapper;
using Configuration.Application.DTOs;
using Configuration.Domain.Entities;

namespace Configuration.Application.Mapper
{

    internal class Mapping : Profile
    {

        #region Ctor

        /// <summary>
        /// Constructor for initialize Mapping.
        /// </summary>
        public Mapping()
        {
            CreateMap<DeviceConfig, Device>()
            .ForMember(dest => dest.CameraCount, opt => opt.MapFrom(src => src.CameraCount))
            .ForMember(dest => dest.AlarmCount, opt => opt.MapFrom(src => src.AlarmCount))
            .ForMember(dest => dest.SensorCount, opt => opt.MapFrom(src => src.SensorCount)).ReverseMap();

            CreateMap<ComponentConfig, Component>()
            .ForMember(dest => dest.ProfileCount, opt => opt.MapFrom(src => src.ProfileCount))
            .ForMember(dest => dest.Profiles, opt => opt.MapFrom(src => src.Profiles)).ReverseMap();

            CreateMap<NetworkConfig, Network>().ReverseMap();
            CreateMap<StreamProfileConfig, StreamProfile>().ReverseMap();

            CreateMap<DeviceConfig, ComponentDevice>().ReverseMap();
            CreateMap<ComponentConfig, DeviceComponent>().ReverseMap();
        }

        #endregion

    }

}
