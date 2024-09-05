using AutoMapper;
using Authentication.Application.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Application.Mapper
{

    /// <summary>
    /// Mapping binding mechanism for DTO and Entities.
    /// </summary>
    public class Mapping : Profile
    {

        #region Ctor

        /// <summary>
        /// Constructor for initialize Mapping.
        /// </summary>
        public Mapping()
        {
            CreateMap<AuthSchema, AuthRequest>().ReverseMap();
            CreateMap<AuthSchema, AuthResponse>().ReverseMap();
        }

        #endregion

    }

}
