import { Feedback } from './../Models/Feedback.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

var url = 'https://localhost:44360/api/place';
var currentData = 'feedback';
@Injectable({
  providedIn: 'root',
})
export class FeedbackService {
  constructor(private http: HttpClient) {}

  /** GET ALL feedback by place*/
  getAllPlaceFeedback(pid: number): Observable<Feedback[]> {
    return this.http
      .get<Feedback[]>(`${url}` + `/${pid}` + `/${currentData}`)
      .pipe();
  }
  /** CREATE: add a new feedback*/
  createFeedback(pid: number, newfeedback: Feedback): Observable<Feedback> {
    return this.http
      .post<Feedback>(`${url}` + `/${pid}` + `/${currentData}`, newfeedback)
      .pipe();
  }
  /** UPDATE: edit existed feedback*/
  editFeedback(pid: number, feedback: Feedback): Observable<Feedback> {
    return this.http
      .put<Feedback>(`${url}` + `/${pid}` + `/${currentData}`, feedback)
      .pipe();
  }
  /** DELETE: delete existed feedback by id */
  deleteFeedback(pid: number, id: number): Observable<Feedback> {
    return this.http
      .delete<Feedback>(`${url}` + `/${pid}` + `/${currentData}` + `/${id}`)
      .pipe();
  }
}
