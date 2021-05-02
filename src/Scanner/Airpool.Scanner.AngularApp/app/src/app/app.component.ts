import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  serverString: string = "";
  locations: any[] = [];
  subscriptions: Subscription[] = [];
  
  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.subscriptions.push(this.httpClient.get("/api/v1/Scanner")
      .subscribe((response: any) => this.serverString = response?.value)
    );

    this.subscriptions.push(this.httpClient.get("/api/v1/Scanner/locations")
      .subscribe((response: any) => this.locations = response)
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.map(sub => sub.unsubscribe);
  }
}
