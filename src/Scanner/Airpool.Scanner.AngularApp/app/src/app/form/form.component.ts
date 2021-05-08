import { Component, OnInit } from '@angular/core';
import { AirLocation } from '../models';
import { FlightFilter } from '../models/flight-filter.model';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {

  flightFilter: FlightFilter = {
    originLocationId: '',
    originDateTime: '',
    destinationLocationId: '',
    returnDateTime: ''
  };

  constructor() { }

  ngOnInit(): void { }

  public onStartLocationChange(location: AirLocation): void {
    //console.log('selected start location: ', location);
    this.flightFilter.originLocationId = location.id;
  }

  public onEndLocationChange(location: AirLocation): void {
    //console.log('selected end location: ', location);
    this.flightFilter.destinationLocationId = location.id;
  }

  public onWhenDateChange(date: string): void {
    //console.log('selected when date: ', date);
    this.flightFilter.originDateTime = date;
  }

  public onReturnDateChange(date: string): void {
    //console.log('selected return date: ', date);
    this.flightFilter.returnDateTime = date;
  }

  isInputValid(): boolean {
    return this.flightFilter.originLocationId.length > 0
      && this.flightFilter.destinationLocationId.length > 0
      && this.flightFilter.originDateTime.length > 0;
  }

}
