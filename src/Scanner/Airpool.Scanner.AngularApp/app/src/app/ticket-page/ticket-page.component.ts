import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightFilter } from '../models/flight-filter.model';
import { FilterFlightsService } from '../services/filter-flights.service';
import { FlightResponse } from '../models';

@Component({
  selector: 'app-ticket-page',
  templateUrl: './ticket-page.component.html',
  styleUrls: ['./ticket-page.component.scss']
})
export class TicketPageComponent implements OnInit {

  flightFilter: FlightFilter = {
    originLocationId: '',
    originDateTime: '',
    destinationLocationId: '',
    returnDateTime: ''
  };

  departureFlights: FlightResponse[] = [];
  arrivalFlights: FlightResponse[] = [];

  constructor(private filterFlightsService: FilterFlightsService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {

      this.flightFilter.originLocationId = params['originLocationId'];
      this.flightFilter.originDateTime = params['originDateTime'];
      this.flightFilter.destinationLocationId = params['destinationLocationId'];
      this.flightFilter.returnDateTime = params['returnDateTime'];

      this.filterFlightsService.getMergedFilteredFlights$(this.flightFilter).subscribe(response => {
        //console.log('response: ', response);

        if ((response as FlightResponse[][])[0][0] != null) {
          const twoWaysArray = (response as FlightResponse[][]);
          twoWaysArray.map(da => {
            this.departureFlights.push(da[0]);
            this.arrivalFlights.push(da[1]);
          });
        } else {
          const oneWayArray = (response as FlightResponse[]);
          this.departureFlights = oneWayArray;
        }

        //console.log('dep and arr ', this.departureFlights, this.arrivalFlights);
        
      });
    });
  }

}
