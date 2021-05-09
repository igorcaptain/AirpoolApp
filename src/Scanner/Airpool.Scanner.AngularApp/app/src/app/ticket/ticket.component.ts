import { Component, Input, OnInit } from '@angular/core';
import { FlightResponse } from '../models';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss']
})
export class TicketComponent implements OnInit {

  @Input() departureFlight: FlightResponse | undefined;
  @Input() arrivalFlight: FlightResponse | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
