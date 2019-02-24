import { Injectable } from '@angular/core';
import { YTRepsone } from 'src/app/models/YTRepsone';
import { throwError, Observable, BehaviorSubject, observable } from 'rxjs';
import { catchError, map, mapTo, mergeMap } from 'rxjs/operators';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private _Response: BehaviorSubject<YTRepsone>;
  currentURL = '';
  constructor(private http: HttpClient) {
    this.currentURL = window.location.href;
  }
  ytresponse: YTRepsone;
  public GetFirstResult(searchTerm): Observable<YTRepsone> {
    const result = this.http.get<YTRepsone>(this.currentURL + 'api/YouTubeData/q='+ searchTerm).pipe(
      catchError(this.handleError.bind(this)));
    return result;
  }

  public PageResult(searchTerm): Observable<YTRepsone> {
    const result = this.http.get<YTRepsone>(this.currentURL + 'api/YouTubeData/'+ searchTerm).pipe(
      catchError(this.handleError.bind(this)));
    return result;
  }

   
  handleError(errorResponse: HttpErrorResponse) {
    if (errorResponse.error instanceof ErrorEvent) {
      console.error('Client Side Error :', errorResponse.error.message);
    } else {
      console.error('Server Side Error :', errorResponse);
    }
    // return an observable with a meaningful error message to the end user
    return throwError('There is a problem with the service.We are notified & working on it.Please try again later.');
  }
}

