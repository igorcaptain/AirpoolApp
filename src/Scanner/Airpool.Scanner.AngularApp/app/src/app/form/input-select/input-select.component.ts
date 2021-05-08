import { Component, OnInit } from '@angular/core';
import { SearchOption } from 'src/app/models';

@Component({
  selector: 'app-input-select',
  templateUrl: './input-select.component.html',
  styleUrls: ['./input-select.component.scss']
})
export class InputSelectComponent implements OnInit {
  
  searchOptions: SearchOption[] = [
    { id: 1, name: 'Default' },
    { id: 2, name: 'From cache' },
    { id: 3, name: 'Greedy' },
  ];
  selectedId = 1;

  constructor() { }

  ngOnInit(): void {
  }

}
