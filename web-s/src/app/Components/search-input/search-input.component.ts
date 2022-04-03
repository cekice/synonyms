import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { ngDebounce } from 'src/app/decorators/debounce.decorator';
import { SynonymService } from 'src/app/service/synonym.service';

@Component({
  selector: 'app-search-input',
  templateUrl: './search-input.component.html',
  styleUrls: ['./search-input.component.scss']
})
export class SearchInputComponent implements OnInit {

  @Output() getValue = new EventEmitter();
  searchTerm$ = new Subject<string>();
  searchTerm = '';
  options: any;
  errorMessage: any;
  results: any;
  item: any;

  constructor(private synonymService: SynonymService) { }

  ngOnInit() {

}
  @ngDebounce(500)
  async findSynonym(event: any) {
    this.searchTerm = event.target.value;
    if (!this.searchTerm) {
      this.options = [];
      return;
    }
    try {
      this.options = await this.synonymService.findSynonyms(this.searchTerm);
    } catch (error: any) {
      this.errorMessage = error.message;
    }
  }


  async chosenOption(option: any, optionClick: boolean) {
    this.options = [];
    if (optionClick) {
      this.searchTerm = option.Key;
      this.getValue.emit(option.Value);
    } else {
      this.item = await this.synonymService.findSynonyms(option);
        this.getValue.emit(this.item[0]?.Value);
    }
  }
}