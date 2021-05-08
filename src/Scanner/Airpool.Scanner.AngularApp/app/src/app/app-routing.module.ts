import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchPageComponent } from './search-page/search-page.component';
import { TicketPageComponent } from './ticket-page/ticket-page.component';

const routes: Routes = [
  { path: '', component: SearchPageComponent },
  { path: 'result', component: TicketPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
