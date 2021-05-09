import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FormComponent } from './form/form.component';
import { MatInputModule } from '@angular/material/input';
import { TicketComponent } from './ticket/ticket.component';
import { SearchPageComponent } from './search-page/search-page.component';
import { InfoBlocksComponent } from './info-blocks/info-blocks.component';
import { TicketPageComponent } from './ticket-page/ticket-page.component';
import { TicketBodyComponent } from './ticket-body/ticket-body.component';
import { MatIconModule } from '@angular/material/icon';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputAutocompleteComponent } from './form/input-autocomplete/input-autocomplete.component';
import { InputDatepickerComponent } from './form/input-datepicker/input-datepicker.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { InputSelectComponent } from './form/input-select/input-select.component';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    FormComponent,
    TicketComponent,
    SearchPageComponent,
    InfoBlocksComponent,
    TicketPageComponent,
    TicketBodyComponent,
    InputAutocompleteComponent,
    InputDatepickerComponent,
    InputSelectComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule, 
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatToolbarModule,
    MatInputModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
