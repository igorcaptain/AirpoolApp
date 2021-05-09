import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AirLocation } from '../models';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  public locations$ = new BehaviorSubject<AirLocation[]>([]);

  constructor(private httpClient: HttpClient) { }

  public getLocations$(): Observable<AirLocation[]> {
    return this.httpClient.get<AirLocation[]>('/api/v1/Scanner/locations');
  }

  public loadLocations() {
    const result = this.getLocations$().pipe(
      map(locations => this.locations$.next(locations))
    );
    result.subscribe();
  }
}
