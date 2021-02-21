import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from './Components/modal/modal.component';
import { SynonymService } from './service/synonym.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  items: any;
  errorMessage: any;
  noResult = false;


  constructor(private synonymService: SynonymService) {

  }

  @ViewChild(ModalComponent) modal = new ModalComponent(this.synonymService);

  ngOnInit(): void {

  }

  async chosenOption(option: any) {
    this.resetResult();
    try {
      if (option !== undefined) {
        this.items = await this.synonymService.getSynonyms(option);
      } else {
        this.noResult = true;
      }
    } catch (error) {
      this.errorMessage = error.message;
    }
  }

  resetResult() {
    this.noResult = false;
    this.items = [];
  }
}
