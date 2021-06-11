import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Place } from '../Models/Place.model';

export const url = 'https://localhost:44360/api';
var currentData = 'place';
@Injectable({
  providedIn: 'root',
})
export class PlaceService {
  constructor(private http: HttpClient) {}

  /** GET ALL place*/
  getAllPlace(): Observable<Place[]> {
    return this.http.get<Place[]>(`${url}` + `/${currentData}`).pipe();
  }
  /** GET 1 place by id*/
  getById(id: number): Observable<Place> {
    return this.http.get<Place>(`${url}` + `/${currentData}` + `/${id}`).pipe();
  }
  /** GET ALL place by search key*/
  search(searchText: string): Observable<Place[]> {
    return this.http
      .get<Place[]>(`${url}` + `${currentData}` + `/search` + `/${searchText}`)
      .pipe();
  }
  /** CREATE: add a new place*/
  createFeedback(place: Place): Observable<Place> {
    return this.http.post<Place>(`${url}` + `/${currentData}`, place).pipe();
  }
  /** UPDATE: edit existed place*/
  editFeedback(place: Place): Observable<Place> {
    return this.http
      .put<Place>(`${url}` + `/${currentData}/${place.id}`, place)
      .pipe();
  }
  /** DELETE: delete existed place by id */
  deleteFeedback(id: number): Observable<Place> {
    return this.http
      .delete<Place>(`${url}` + `/${currentData}` + `/${id}`)
      .pipe();
  }
}
