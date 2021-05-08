import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IconService } from './services/icon.service';
import { AirLocation } from '../app/models';
import { LocationService } from './services/location.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  subscriptions: Subscription[] = [];
  
  constructor(
    private iconService: IconService,
    private locationService: LocationService
  ) { }

  ngOnInit(): void {
    this.iconService.registerIcons();
    this.locationService.loadLocations();
  }

  ngOnDestroy(): void {
    this.subscriptions.map(sub => sub.unsubscribe);
  }
}
