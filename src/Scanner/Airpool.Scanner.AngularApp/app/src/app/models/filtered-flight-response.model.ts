import { FlightResponse } from './flight-response.model';

export interface FilteredFlightResponse {
    departure: FlightResponse[];
    arrival: FlightResponse[];
    isOneWay: boolean;
}