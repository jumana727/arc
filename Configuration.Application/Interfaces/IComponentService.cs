using Configuration.Application.DTOs;

namespace Configuration.Application.Interfaces
{

    public interface IComponentService
    {


        #region Components

        List<Component> GetComponents(int id);

        Component? GetComponent(int deviceId, int componentId);

        int AddUpdateComponent(Component device);

        bool DeleteComponent(int componentId);

        bool DeleteComponents(int deviceId);

        #endregion

        #region Stream Profiles

        List<StreamProfile> GetStreamProfiles(int deviceId, int componentId);

        StreamProfile? GetStreamProfile(int deviceId, int componentId, int profileId);

        int AddUpdateStreamProfile(StreamProfile network);

        bool DeleteStreamProfile(int id);

        bool DeleteStreamProfiles(int deviceId);

        bool DeleteStreamProfiles(int deviceId, int componentId);

        #endregion

    }

}