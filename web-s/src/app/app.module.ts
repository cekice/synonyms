import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { ModalComponent } from './Components/modal/modal.component';
import { SearchInputComponent } from './Components/search-input/search-input.component';
import { FormsModule } from '@angular/forms';
import { SynonymService } from './service/synonym.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ModalComponent,
    SearchInputComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  exports: [
    ModalComponent
  ],
  providers: [SynonymService],
  bootstrap: [AppComponent]
})
export class AppModule { }
