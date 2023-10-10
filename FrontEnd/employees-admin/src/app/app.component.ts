import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
 
  title = 'Play.FrontEnd';

  

  datepickerOptions: any = {
    containerClass: 'theme-dark-blue',
    showWeekNumbers: false,
    dateInputFormat: 'YYYY-MM-DD HH:mm:ss',
  };
}
