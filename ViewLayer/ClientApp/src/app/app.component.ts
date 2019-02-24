import { Component } from '@angular/core';
import { DataService } from './data.service';
import { YTRepsone } from './YTRepsone';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [
    './app.component.less'
  ]
})
export class AppComponent {
  repsonse: YTRepsone;
  constructor(private dataCall: DataService) {
  
  }
  
  ngOnInit() {
    
      }
  onSearch(searchTerm: string) {
    if (searchTerm != "") {
      this.dataCall.callApi(searchTerm).subscribe((result) => {
        this.repsonse = result;
        console.log(result);
      }
      );
    }
  }
}
