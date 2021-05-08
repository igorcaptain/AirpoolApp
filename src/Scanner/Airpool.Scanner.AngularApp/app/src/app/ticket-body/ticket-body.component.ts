import { Component, Input, OnInit } from '@angular/core';
import { FlightResponse } from '../models';

@Component({
  selector: 'app-ticket-body',
  templateUrl: './ticket-body.component.html',
  styleUrls: ['./ticket-body.component.scss']
})
export class TicketBodyComponent implements OnInit {

  @Input() flightData: FlightResponse | undefined;

  flightTime: string = '00:00';

  constructor() { }

  ngOnInit(): void {   
    const diffInMillis = new Date(this.flightData!.endDateTime)!.getTime() - new Date(this.flightData!.startDateTime)!.getTime();
    const diffInHoursFloat = diffInMillis / (1000 * 3600);
    const diffInHours = Math.round( diffInHoursFloat < 1.0 ? 0 : diffInHoursFloat );
    const diffInMinutes = Math.round(diffInMillis / (1000 * 60));
    this.flightTime = `${this.normalizeTimeOutput(diffInHours)}:${(diffInHours != 0) 
      ? this.normalizeTimeOutput((diffInMinutes % (diffInHours * 60)))
      : this.normalizeTimeOutput(diffInMinutes)}`;
  }

  private normalizeTimeOutput(value: number): string {
    return (value < 10) ? '0' + value : value.toString();
  }

}
