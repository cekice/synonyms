import { Component } from '@angular/core';
import { LoaderService } from '../../service/loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss'],
})
export class LoaderComponent {
  loader: boolean | undefined;

  constructor(public loaderService: LoaderService) {
  }

  ngOnInit() {
    this.loaderService.isLoading.subscribe(val =>
      this.loader = val)
  }
}