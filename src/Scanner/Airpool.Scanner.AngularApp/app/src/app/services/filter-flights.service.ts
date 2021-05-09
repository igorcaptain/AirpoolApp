import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { FlightResponse, SearchOptionEnum } from '../models';
import { FlightFilter } from '../models/flight-filter.model';
import { FilteredFlightResponse } from '../models/filtered-flight-response.model';

@Injectable({
  providedIn: 'root'
})
export class FilterFlightsService {

  constructor(private httpClient: HttpClient) { }

  public getFilteredFlights$(filter: FlightFilter, searchOption: SearchOptionEnum): Observable<FilteredFlightResponse> {
    return this.httpClient.get<FilteredFlightResponse>('/api/v1/Scanner/flights', { params: {...filter, searchOption: searchOption.toString() } });
  }

  public getMergedFilteredFlights$(inputFilter: FlightFilter, searchOption: SearchOptionEnum): Observable<FlightResponse[] | FlightResponse[][]> {
    return this.getFilteredFlights$(inputFilter, searchOption).pipe(
      filter(res => res != null),
      map(response => {
        //console.log('hello, ', response)
        return response.isOneWay
          ? response.departure
          : this.cartesian(response.departure, response.arrival) as FlightResponse[][];
      })
    );
  }

  cartesian(a: any, b: any): any {
    return [].concat(...a.map((d: any) => b.map((e: any) => [].concat(d, e))));
  }

}
