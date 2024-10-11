import { Injectable } from '@angular/core';
import { FirebaseApp, initializeApp } from "firebase/app";
import { getMessaging, isSupported, Messaging } from 'firebase/messaging';
import { firebaseConfig } from './firebase.config';

@Injectable({
  providedIn: 'root'
})
export class FirebaseService {

  public app: FirebaseApp;

  constructor() {
    this.app = initializeApp(firebaseConfig);
  }

  public async messaging(): Promise<Messaging> {
    var supported = await isSupported();
    if (supported === false)
      throw new Error(`Browser does not support all required APIs for FCM(Firebase Cloud Messaging).`);
    return getMessaging(this.app);
  }
}
