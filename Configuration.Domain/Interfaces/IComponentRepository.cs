using Configuration.Domain.Entities;

namespace Configuration.Domain.Interfaces
{

    public interface IComponentRepository
    {

        #region Components

        List<ComponentConfig> GetComponents(int id);

        ComponentConfig? GetComponent(int deviceId, int componentId);

        int AddUpdateComponent(ComponentConfig device);

        bool DeleteComponent(int id);

        bool DeleteComponents(int id);

        #endregion

        #region Stream Profiles

        List<StreamProfileConfig> GetStreamProfiles(int deviceId, int componentId);

        StreamProfileConfig? GetStreamProfile(int deviceId, int componentId, int profileId);

        int AddUpdateStreamProfile(StreamProfileConfig network);

        bool DeleteStreamProfile(int id);

        bool DeleteStreamProfiles(int deviceId);

        bool DeleteStreamProfiles(int deviceId, int componentId);

        #endregion

    }

}