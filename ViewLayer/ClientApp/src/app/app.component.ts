import { Component } from '@angular/core';
import { DataService } from './service/data.service';
import { YTRepsone } from './models/YTRepsone';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [
    './app.component.less'
  ]
})
export class AppComponent {
  response: YTRepsone;
  oldSearch: string;
  validate: boolean;
  constructor(private dataCall: DataService) {
  
  }
  
  ngOnInit() {
    
      }
  onSearch(searchTerm: string) {
    if (searchTerm != undefined) {
      this.oldSearch = searchTerm;
      this.validate = false;
      this.dataCall.GetFirstResult(searchTerm).subscribe((result) => {
        this.response = result;
        console.log(result);
      }
      );
    }
    else {
      this.validate = true;
    }
  }
  onTurnPage(pageToken: string) {
    if (pageToken != undefined) {
      let searchTerm = "pageToken=" + pageToken + "&q=" + this.oldSearch
      this.dataCall.PageResult(searchTerm).subscribe((result) => {
        this.response = result;
        console.log(result);
      }
      );
    }
  }

}
