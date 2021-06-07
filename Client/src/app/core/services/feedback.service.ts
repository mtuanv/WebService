import { Feedback } from "./../models/feedback.model";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

const url = "https://localhost:44359/api";
const currentData = "feedbacks";

@Injectable({
  providedIn: "root",
})
export class FeedbackService {
  constructor(private http: HttpClient) {}

  /** GET ALL feedback by place*/
  getAllPlaceFeedback(id: number): Observable<Feedback[]> {
    return this.http
      .get<Feedback[]>(`${url}` + `${currentData}` + `/${id}`)
      .pipe();
  }
  /** CREATE: add a new feedback*/
  createFeedback(newfeedback: Feedback): Observable<Feedback> {
    return this.http
      .post<Feedback>(`${url}` + `${currentData}`, newfeedback)
      .pipe();
  }
  /** UPDATE: edit existed feedback*/
  editFeedback(feedback: Feedback): Observable<Feedback> {
    return this.http
      .put<Feedback>(`${url}` + `${currentData}/${feedback.id}`, feedback)
      .pipe();
  }
  /** DELETE: delete existed feedback by id */
  deleteFeedback(id: number): Observable<Feedback> {
    return this.http
      .delete<Feedback>(`${url}` + `${currentData}` + `/${id}`)
      .pipe();
  }
}
