import { Component, OnInit } from '@angular/core';
import { SynonymService } from 'src/app/service/synonym.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {

  visible = false;
  buttonDisabled = false;
  wordOne = '';
  wordTwo = '';
  wordThree = '';
  wordFour = '';
  errorMsg = false;
  synonyms: string[] = [];
  errorMessage: any;
  added = false;

  constructor(private synonymService: SynonymService) { }

  ngOnInit() {
  }

  show() {
    this.visible = true;
  }

  hide() {
    this.buttonDisabled = this.visible = this.errorMsg = this.added = false;
    this.wordOne = this.wordTwo = this.wordThree = this.wordFour = '';
    this.synonyms = [];
  }

  async addSynonyms() {
    if (!this.wordOne || !this.wordTwo) {
      this.errorMsg = true;
      return;
    }
    this.synonyms.push(this.wordOne, this.wordTwo);
    if (this.wordThree) {
      this.synonyms.push(this.wordThree);
      if (this.wordFour) {
        this.synonyms.push(this.wordFour);
      }
    }
    try {
     await this.synonymService.addSynonyms(this.synonyms);
     this.added =  true;
    } catch (error) {
      this.errorMessage = error.message;
    }
  }

}
