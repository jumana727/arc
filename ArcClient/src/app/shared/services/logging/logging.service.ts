import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class LoggingService {
  private apiUrl = 'http://192.168.27.172:9200';

  constructor(private http: HttpClient) {}

  sendLogEntry(data: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    const currentDate = new Date();
    const indexFormat = `angular-${currentDate.getFullYear()}.${('0' + (currentDate.getMonth() + 1)).slice(-2)}.${('0' + currentDate.getDate()).slice(-2)}`;
    const fullUrl = `${this.apiUrl}/${indexFormat}/_doc/`;

    console.log('Sending log data to:', fullUrl);
    console.log('Log data:', JSON.stringify(data));

    return this.http.post(fullUrl, data, { headers }).pipe(
      catchError((error) => {
        console.error('Error sending log entry:', error);
        return throwError(() => error);
      })
    );
  }
}

export interface LogData {
  timestamp: string;
  level: 'info' | 'Warning' | 'Error'; // You can define the allowed levels here
  message: string;
  servicename : string;
  additionalInfo: AdditionalInfo;
}

export interface AdditionalInfo {
  user: string;
  location: string;
}