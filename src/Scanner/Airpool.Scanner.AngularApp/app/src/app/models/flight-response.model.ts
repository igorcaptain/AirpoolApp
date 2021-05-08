export interface FlightResponse {
    id: string;
    name: string;
    startDateTime: Date;
    endDateTime: Date;
    startLocationId: string;
    startLocationAirportName: string;
    startLocationIATA: string;
    startLocationCountry: string;
    startLocationCity: string;
    endLocationId: string;
    endLocationAirportName: string;
    endLocationIATA: string;
    endLocationCountry: string;
    endLocationCity: string;
}