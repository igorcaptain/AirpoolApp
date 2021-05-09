import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightFilter } from '../models/flight-filter.model';
import { FilterFlightsService } from '../services/filter-flights.service';
import { FlightResponse, SearchOptionEnum } from '../models';

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
  searchOption: SearchOptionEnum = SearchOptionEnum.Default;

  departureFlights: FlightResponse[] = [];
  arrivalFlights: FlightResponse[] = [];

  constructor(private filterFlightsService: FilterFlightsService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {

      this.flightFilter.originLocationId = params['originLocationId'];
      this.flightFilter.originDateTime = params['originDateTime'];
      this.flightFilter.destinationLocationId = params['destinationLocationId'];
      this.flightFilter.returnDateTime = params['returnDateTime'];
      this.searchOption = params['searchOption'];

      this.filterFlightsService.getMergedFilteredFlights$(this.flightFilter, this.searchOption).subscribe(response => {
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
