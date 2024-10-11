export const API_ENDPOINTS = {
  BASE_URL: 'http://localhost:5039',
  AUTH: {
    LOGIN: '/api/Auth/login',
    VALIDATE_TOKEN: '/api/Auth/validatetoken',
  },
  DEVICE: {
    ADD: '/api/Device/add',
    GET: '/api/Device/get',
    UPDATE: '/api/Device/update',
    DELETE: '/api/Device/delete',
    GETNAMELIST: '/api/Device/devicecomponent',
  },
  DEVICECOMPONENT: {
    GETBYDEVICE: '/api/Component/get/byDevice',
    GETSTREAMPROFILEBYCOPONENT: '/api/Component/getStreamProfile/byComponent',
  },
  STREAMPROFILE: {
    GETBYCOMPONENT: '/api/Component/getStreamProfiles/byComponent',
    UPDATE: '/api/Component/addUpdateStreamProfile',
  },
  GETWEBRTCURL: '/api/Stream/getstreamurl',
  PushNotifications: {
    RecordToken: '/recordToken',
    SendNotifications: '/sendNotifications'
  },
  HLS_BASE_URL : 'localhost:8888',
  MEDIAMTX_URL : 'mediamtx'
};
