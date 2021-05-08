import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { AirLocation } from 'src/app/models';
import { LocationService } from 'src/app/services/location.service';

@Component({
  selector: 'app-input-autocomplete',
  templateUrl: './input-autocomplete.component.html',
  styleUrls: ['./input-autocomplete.component.scss']
})
export class InputAutocompleteComponent implements OnInit {

  control = new FormControl();
  locations: AirLocation[] = [];
  filteredLocations: Observable<AirLocation[]> | undefined;

  private selectedLocationSubject$ = new BehaviorSubject<AirLocation | undefined>(undefined);
  get selectedLocation(): AirLocation | undefined {
    return this.selectedLocationSubject$.value;
  }
  set selectedLocation(value: AirLocation | undefined) {
    this.selectedLocationSubject$.next(value)
  }
  public selectedLocation$: Observable<AirLocation | undefined>;
  @Output() locationChange = new EventEmitter<AirLocation>();

  constructor(private locationService: LocationService) {
    this.selectedLocation$ = this.selectedLocationSubject$.asObservable();
  }

  ngOnInit(): void {
    this.locationService.locations$.subscribe((locations: AirLocation[]) => {
      this.locations = locations;
      this.filteredLocations = this.control.valueChanges.pipe(
        delay(200),
        map(value => (value.length > 0) ? this._filter(value) : [])
      );
    });
  }

  public onSelectedOption(locationId: string): void {
    this.selectedLocation = this.locations.find(location => location.id === locationId);
    this.locationChange.emit(this.selectedLocation);
  }

  private _filter(value: string): AirLocation[] {
    const filterValue = this._normalizeValue(value);
    return this.locations.filter(location => 
      this._normalizeValue(location.city).includes(filterValue) || this._normalizeValue(location.iata).includes(filterValue)
    );
  }

  private _normalizeValue(value: string): string {
    return value.toLocaleLowerCase().replace(/\s/g, '');
  }

}
