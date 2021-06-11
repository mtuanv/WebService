import { FeedbackService } from './../../Core/Services/feedback.service';
import { Feedback } from './../../Core/Models/Feedback.model';
import { Place } from './../../Core/Models/Place.model';
import { PlaceService } from './../../Core/Services/place.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.scss'],
})
export class BlogComponent implements OnInit {
  place = new Place();
  feedbacks: Feedback[] = [];
  constructor(
    private placeService: PlaceService,
    private route: ActivatedRoute,
    private feedbackService: FeedbackService
  ) {}

  ngOnInit(): void {
    this.getRoute(this.route.snapshot.params['id']);
    this.feedbackService
      .getAllPlaceFeedback(this.route.snapshot.params['id'])
      .subscribe((res) => {
        this.feedbacks = res;
      });
  }
  getRoute(id: any) {
    this.placeService.getById(id).subscribe((res: any) => {
      this.place = res;
    });
  }
}
