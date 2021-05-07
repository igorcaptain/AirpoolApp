import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FormComponent } from './form/form.component';
import { MatInputModule } from '@angular/material/input';
import { TicketComponent } from './ticket/ticket.component';
import { SearchPageComponent } from './search-page/search-page.component';
import { InfoBlocksComponent } from './info-blocks/info-blocks.component';
import { TicketPageComponent } from './ticket-page/ticket-page.component';
import { TicketBodyComponent } from './ticket-body/ticket-body.component';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    AppComponent,
    FormComponent,
    TicketComponent,
    SearchPageComponent,
    InfoBlocksComponent,
    TicketPageComponent,
    TicketBodyComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule, 
    MatButtonModule, 
    MatToolbarModule,
    MatInputModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
