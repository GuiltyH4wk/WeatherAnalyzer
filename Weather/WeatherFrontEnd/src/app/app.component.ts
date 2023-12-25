import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title(title: any) {
    throw new Error('Method not implemented.');
  }
  
  headerTitle = 'WeatherAnalyzer ';
  menuItems : string[] = ["Menu","WeatherAnalyzer"];
  slideToggle = new FormControl<boolean>(false, Validators.required);
  constructor(private routerLink : Router){}


  setValueOfSlideToggle($event: MatSlideToggleChange){
    console.log($event.checked);
    this.slideToggle.setValue($event.checked);
  }

  homePage(){
    this.routerLink.navigate(['/home'])
  }
}
