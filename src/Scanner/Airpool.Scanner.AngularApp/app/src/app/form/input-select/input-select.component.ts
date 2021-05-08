import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { SearchOption, SearchOptionEnum } from 'src/app/models';

@Component({
  selector: 'app-input-select',
  templateUrl: './input-select.component.html',
  styleUrls: ['./input-select.component.scss']
})
export class InputSelectComponent implements OnInit {
  
  searchOptions: SearchOption[] = [
    { id: SearchOptionEnum.Default, name: 'Default' },
    { id: SearchOptionEnum.FromCache, name: 'From cache' },
    { id: SearchOptionEnum.Greedy, name: 'Greedy' },
  ];
  selectedId = 1;

  @Output() selectChange = new EventEmitter<SearchOptionEnum>();

  constructor() { }

  ngOnInit(): void {
  }

  onSelectionChange(): void {
    this.selectChange.emit(this.selectedId);
  }
}
