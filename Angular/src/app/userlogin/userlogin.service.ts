import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserloginService {
 
  private apiUrls = 'https://localhost:7247/api/Userlogin/login';
  constructor(private http : HttpClient) { }

  login(credentials: any): Observable<any> {
    return this.http.post<any>(this.apiUrls, credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'json'
    });
  }
  
}
