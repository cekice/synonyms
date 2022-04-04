import { Component, OnInit } from '@angular/core';
import { Form, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LoaderService } from 'src/app/service/loader.service';
import { SynonymService } from 'src/app/service/synonym.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {

  visible = false;
  buttonDisabled = false;
  errorMsg = false;
  errorMessage: any;
  added = false;
  form = this.fb.group({
    word: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z\-']+$")]),
    synonyms: this.fb.array([
      this.fb.control('', [Validators.required, Validators.pattern("^[a-zA-Z\-']+$")]),
    ])
  })

  constructor(private synonymService: SynonymService,
              private fb: FormBuilder,
              private loaderService: LoaderService) {
               }

  ngOnInit() {
  }

  show() {
    this.visible = true;
  }

  hide() {
    this.visible = this.errorMsg = this.added = false;
    this.errorMessage =  '';
    this.form.reset();
    this.clearFormArray(this.form.controls.synonyms as FormArray);
  }

  async onSubmit() {
    this.loaderService.show()
    if(!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    };
    try {
      let synonym = this.form.get('synonyms')?.value;
      if(!synonym.includes(this.form.controls.word.value))
          synonym.unshift(this.form.controls.word.value)
      await this.synonymService.addSynonyms(synonym);
      this.added =  true;
      this.errorMessage = '';
     } catch (error: any) {
       this.errorMessage = error.Message;
     }
     this.loaderService.hide();
  }

  addSynonym() {
    (this.form.controls.synonyms as FormArray).push(this.fb.control('',[Validators.required, Validators.pattern("^[a-zA-Z\-']+$")]));
  }

  removeSynonym(i: number) {
    (this.form.controls.synonyms as FormArray).removeAt(i);
  }

  get formArr() {
    return this.form.get("synonyms") as FormArray;
  }

  clearFormArray = (formArray: FormArray) => {
    while (formArray.length !== 1) {
      formArray.removeAt(1)
    }
  }
}
