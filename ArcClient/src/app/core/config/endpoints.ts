export const API_ENDPOINTS = {
    BASE_URL: 'https://matrixcomsec.com/api',
    // BASE_URL: 'https://172.16.0.42:44347/api',
    AUTH: {
      LOGIN: '/Auth/login',
      VALIDATE_TOKEN: '/Auth/validatetoken',
    },
    DEVICE: {
      ADD: '/Device/add',
      GET: '/Device/get',
      UPDATE: '/Device/update',
      DELETE: '/Device/delete',
      GETNAMELIST: '/Device/devicecomponent',
    },
    DEVICECOMPONENT: {
      GETBYDEVICE: '/Component/get/byDevice',
      GETSTREAMPROFILEBYCOPONENT: '/Component/getStreamProfile/byComponent'
    },
    STREAMPROFILE: {
      GETBYCOMPONENT: '/Component/getStreamProfiles/byComponent',
      UPDATE: '/Component/addUpdateStreamProfile',
    },
    GETWEBRTCURL : '/Stream/getstreamurl',
  };
