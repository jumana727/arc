using AutoMapper;
using Configuration.Application.DTOs;
using Configuration.Application.Interfaces;
using Configuration.Domain.Entities;
using Configuration.Domain.Interfaces;

namespace Configuration.Application.Services
{

    public class ComponentService : IComponentService
    {

        #region Declaration

        private readonly IComponentRepository componentRepository;
        private readonly IMapper mapper;

        #endregion

        #region Ctor

        public ComponentService(IComponentRepository _componentRepository, IMapper _mapper)
        {
            componentRepository = _componentRepository;
            mapper = _mapper;
        }

        #endregion

        #region Methods

        #region Components

        public List<Component> GetComponents(int id) => mapper.Map<List<Component>>(componentRepository.GetComponents(id));

        public Component? GetComponent(int deviceId, int componentId) => mapper.Map<Component>(componentRepository.GetComponent(deviceId, componentId));

        public int AddUpdateComponent(Component component) => componentRepository.AddUpdateComponent(mapper.Map<ComponentConfig>(component));

        public bool DeleteComponent(int id) => componentRepository.DeleteComponent(id);

        public bool DeleteComponents(int id) => componentRepository.DeleteComponents(id);

        #endregion

        #region Stream Profiles

        public List<StreamProfile> GetStreamProfiles(int deviceId, int componentId) => mapper.Map<List<StreamProfile>>(componentRepository.GetStreamProfiles(deviceId, componentId));

        public StreamProfile? GetStreamProfile(int deviceId, int componentId, int profileId) => mapper.Map<StreamProfile>(componentRepository.GetStreamProfile(deviceId, componentId, profileId));

        public int AddUpdateStreamProfile(StreamProfile streamProfile) => componentRepository.AddUpdateStreamProfile(mapper.Map<StreamProfileConfig>(streamProfile));

        public bool DeleteStreamProfile(int id) => componentRepository.DeleteStreamProfile(id);

        public bool DeleteStreamProfiles(int deviceId) => componentRepository.DeleteStreamProfiles(deviceId);

        public bool DeleteStreamProfiles(int deviceId, int componentId) => componentRepository.DeleteStreamProfiles(deviceId, componentId);

        #endregion

        #endregion

    }

}