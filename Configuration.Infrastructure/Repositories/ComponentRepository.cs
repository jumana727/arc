using Configuration.Domain.Entities;
using Configuration.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Infrastructure.Repositories
{

    public class ComponentRepository : IComponentRepository
    {

        #region Declaration

        private readonly DatabaseContext dbContext;

        #endregion

        #region Ctor

        public ComponentRepository(DatabaseContext _dbContext)
        {
            dbContext = _dbContext;
        }

        #endregion

        #region Methods

        #region Components

        public List<ComponentConfig> GetComponents(int id) => dbContext.Components.Where(x => x.DeviceId == id).Include(x => x.Profiles).ToList();

        public ComponentConfig? GetComponent(int deviceId, int componentId) => dbContext.Components.Where(x => x.DeviceId == deviceId && x.ComponentId == componentId).Include(x => x.Profiles).FirstOrDefault();

        public int AddUpdateComponent(ComponentConfig component) => AddUpdate(component);

        public bool DeleteComponent(int id) => ComponentDelete(id);

        public bool DeleteComponents(int id) => ComponentsDelete(id);

        #region Private Methods

        private int AddUpdate(ComponentConfig component)
        {
            int result = 0;

            try
            {
                ComponentConfig? _component = dbContext.Components.FirstOrDefault(x => x.ComponentId == component.Id && x.DeviceId == component.DeviceId);

                if (_component == null)
                    dbContext.Components.Add(component);
                else
                {
                    _component.Id = component.Id;
                    _component.DeviceId = component.DeviceId;
                    _component.ComponentId = component.ComponentId;
                    _component.Name = component.Name;
                    _component.Type = component.Type;
                }

                dbContext.SaveChanges();
                result = component.Id;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private bool ComponentDelete(int id)
        {
            bool result = false;

            try
            {
                ComponentConfig? _component = dbContext.Components.Where(x => x.Id == id).FirstOrDefault();

                if (_component != null)
                    dbContext.Components.Remove(_component);

                dbContext.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private bool ComponentsDelete(int id)
        {
            bool result = false;

            try
            {
                List<ComponentConfig>? _component = dbContext.Components.Where(x => x.DeviceId == id).ToList();

                if (_component != null)
                    dbContext.Components.RemoveRange(_component);

                dbContext.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion

        #endregion

        #region Stream Profiles

        public List<StreamProfileConfig> GetStreamProfiles(int deviceId, int componentId) => dbContext.StreamProfiles.Where(x => x.DeviceId == deviceId && x.ComponentId == componentId).ToList();

        public StreamProfileConfig? GetStreamProfile(int deviceId, int componentId, int profileId) => dbContext.StreamProfiles.Where(x => x.DeviceId == deviceId && x.ComponentId == componentId && x.No == profileId).FirstOrDefault();

        public int AddUpdateStreamProfile(StreamProfileConfig streamProfile) => AddUpdate(streamProfile);

        public bool DeleteStreamProfile(int id) => StreamProfileDelete(id);

        public bool DeleteStreamProfiles(int deviceId) => StreamProfilesDelete(deviceId);

        public bool DeleteStreamProfiles(int deviceId, int componentId) => StreamProfilesDelete(deviceId, componentId);

        #region Private Methods

        private int AddUpdate(StreamProfileConfig streamProfile)
        {
            int result = 0;

            try
            {
                StreamProfileConfig? _streamProfile = dbContext.StreamProfiles.FirstOrDefault(x => x.DeviceId == streamProfile.DeviceId && x.ComponentId == streamProfile.ComponentId && x.No == streamProfile.No);

                if (_streamProfile == null)
                    dbContext.StreamProfiles.Add(streamProfile);
                else
                {
                    _streamProfile.Id = streamProfile.Id;
                    _streamProfile.No = streamProfile.No;
                    _streamProfile.DeviceId = streamProfile.DeviceId;
                    _streamProfile.ComponentId = streamProfile.ComponentId;
                    _streamProfile.Name = streamProfile.Name;
                    _streamProfile.VideoCodec = streamProfile.VideoCodec;
                    _streamProfile.Resolution = streamProfile.Resolution;
                    _streamProfile.AudioCodec = streamProfile.AudioCodec;
                    _streamProfile.BitrateControl = streamProfile.BitrateControl;
                    _streamProfile.Bitrate = streamProfile.Bitrate;
                    _streamProfile.FPS = streamProfile.FPS;
                    _streamProfile.Quality = streamProfile.Quality;
                    _streamProfile.GOP = streamProfile.GOP;
                    _streamProfile.EnableAudio = streamProfile.EnableAudio;
                    _streamProfile.Url = streamProfile.Url;
                }

                dbContext.SaveChanges();
                result = streamProfile.Id;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private bool StreamProfileDelete(int id)
        {
            bool result = false;

            try
            {
                StreamProfileConfig? _streamProfile = dbContext.StreamProfiles.FirstOrDefault(x => x.Id == id);

                if (_streamProfile != null)
                    dbContext.StreamProfiles.Remove(_streamProfile);

                dbContext.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private bool StreamProfilesDelete(int deviceId)
        {
            bool result = false;

            try
            {
                List<StreamProfileConfig>? _streamProfiles = dbContext.StreamProfiles.Where(x => x.DeviceId == deviceId).ToList();

                if (_streamProfiles != null)
                    dbContext.StreamProfiles.RemoveRange(_streamProfiles);

                dbContext.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private bool StreamProfilesDelete(int deviceId, int componentId)
        {
            bool result = false;

            try
            {
                List<StreamProfileConfig>? _streamProfiles = dbContext.StreamProfiles.Where(x => x.DeviceId == deviceId && x.ComponentId == componentId).ToList();

                if (_streamProfiles != null)
                    dbContext.StreamProfiles.RemoveRange(_streamProfiles);

                dbContext.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion

        #endregion

        #endregion

    }

}