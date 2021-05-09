import { DatePipe } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-input-datepicker',
  templateUrl: './input-datepicker.component.html',
  styleUrls: ['./input-datepicker.component.scss']
})
export class InputDatepickerComponent implements OnInit {

  control = new FormControl();
  @Output() dateChange = new EventEmitter<string>();
  
  constructor(private datePipe: DatePipe) { }

  ngOnInit(): void {
  }

  onDateChange(data: any): void {
    this.dateChange.emit(this.datePipe.transform(data.value, 'yyyy-MM-dd') as string)
  }
}
