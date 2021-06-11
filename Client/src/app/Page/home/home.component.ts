import { Place } from './../../Core/Models/Place.model';
import { PlaceService } from './../../Core/Services/place.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  places: Place[] = [];
  constructor(private placeService: PlaceService) {}

  ngOnInit(): void {
    this.placeService.getAllPlace().subscribe((res) => {
      this.places = res;
    });
  }

  // logOut() {
  //   localStorage.removeItem('jwt');
  // }
}
