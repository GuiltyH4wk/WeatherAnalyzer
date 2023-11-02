import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Weather } from '../data/weather';
import { Router, RouterEvent, RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent{


  constructor(private routerLink : Router){}
  

  title = 'Weather Analyzer'

  menuItems : number[] | undefined
 


}
