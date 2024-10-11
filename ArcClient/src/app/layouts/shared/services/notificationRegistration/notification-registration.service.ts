import { Injectable } from '@angular/core';
import { getToken, Messaging } from 'firebase/messaging';
import { FirebaseService } from '../firebase/firebase.service';
import { HttpClient } from '@angular/common/http';
import { API_ENDPOINTS } from '../../../../core/config/endpoints';
import { jwtDecode } from 'jwt-decode';
import { CustomJwtPayload } from './CustomJwtPayload';

@Injectable({
  providedIn: 'root'
})
export class NotificationRegistrationService {

  private _firebaseService: FirebaseService;
  private _httpClient: HttpClient;

  constructor(firebaseService: FirebaseService, httpClient: HttpClient) {
    this._firebaseService = firebaseService;
    this._httpClient = httpClient;
  }

  async requestNotificationsPermission() {
    console.log('Requesting notifications permission...');
    const permission = await Notification.requestPermission();

    if (permission === 'granted') {
      console.log('Notification permission granted.');
    } else {
      console.log('Unable to get permission to notify.');
    }
  }

  public async requestPermissionAndRegisterForPushNotification() {
    if (Notification.permission !== 'granted') {
      await this.requestNotificationsPermission();
      this.requestPermissionAndRegisterForPushNotification();
    }
    else {
      const swRegistration = await navigator.serviceWorker.getRegistration("./ngsw-worker.js");
      const messaging: Messaging = await this._firebaseService.messaging();
      const uid = this.getUserIdFromJWT();

      getToken(messaging,
        {
          vapidKey: "",
          serviceWorkerRegistration: swRegistration
        })
        .then((currentToken) => {
          this.recordToken(uid, currentToken).subscribe((response) => {
            console.log(response);
          });
        }).catch((err) => {
          console.log('An error occurred while retrieving token. ', err);
        });
    }
  }

  private recordToken(uid: string, fcmToken: string) {
    return this._httpClient.post(
      `${API_ENDPOINTS.BASE_URL}${API_ENDPOINTS.PushNotifications.RecordToken}?userId=${uid}&fcmToken=${fcmToken}`, '');
  }

  private getUserIdFromJWT(): string {
    const auth_token = localStorage.getItem('auth_token');
    if (auth_token === null)
      throw new Error("Token not found");
    var jwtPayload = jwtDecode<CustomJwtPayload>(auth_token);
    return jwtPayload.preferred_username;
  }

}
