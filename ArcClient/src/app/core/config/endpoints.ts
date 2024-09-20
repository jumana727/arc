export const API_ENDPOINTS = {
  // BASE_URL: 'http://matrix.keycloak:4200/api', // Base URL for the web client
  AUTH: {
      LOGIN: 'http://localhost:5174/api/Auth/login', // Authentication service
      VALIDATE_TOKEN: 'http://localhost:5174/api/Auth/validatetoken',
  },
  DEVICE: {
      ADD: 'http://localhost:5011/api/Device/add', // Device service
      GET: 'http://localhost:5011/api/Device/get',
      UPDATE: 'http://localhost:5011/api/Device/update',
      DELETE: 'http://localhost:5011/api/Device/delete',
      GETNAMELIST: 'http://localhost:5011/api/Device/devicecomponent',
  },
  DEVICECOMPONENT: {
      GETBYDEVICE: 'http://localhost:5011/api/Component/get/byDevice',
      GETSTREAMPROFILEBYCOPONENT: 'http://localhost:5011/api/Component/getStreamProfile/byComponent',
  },
  STREAMPROFILE: {
      GETBYCOMPONENT: 'http://localhost:5011/api/Component/getStreamProfiles/byComponent',
      UPDATE: 'http://localhost:5011/api/Component/addUpdateStreamProfile',
  },
  GETWEBRTCURL: 'http://localhost:5011/api/Stream/getstreamurl',
};
