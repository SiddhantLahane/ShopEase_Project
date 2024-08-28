import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrls = 'https://localhost:7247/api/Adminlogin/login'; // Ensure this URL matches your backend API

  constructor(private http: HttpClient) { }

  login(credentials: any): Observable<any> {
    return this.http.post<any>(this.apiUrls, credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'json'
    });
  }
}

// registerUser(formData: any): Observable<RegistrationOptions> {
//   return this.http.post<RegistrationOptions>(this.apiUrl, formData)
//     .pipe(
//       catchError(this.handleError)
//     );
// }

// // Handle errors
// private handleError(error: HttpErrorResponse): Observable<never> {
//   console.error('An error occurred:', error);
//   return throwError('Something went wrong; please try again later.');
// }