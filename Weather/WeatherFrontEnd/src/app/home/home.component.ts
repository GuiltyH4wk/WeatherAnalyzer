import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Weather } from '../data/weather';
import { Router, RouterEvent, RouterLink } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { FormControl, FormGroup } from '@angular/forms';


export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}


const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements  OnInit {

  @Input() slideToggle!: boolean | null;

  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  dataSource = ELEMENT_DATA;

  weather = new FormGroup({
    id : new FormControl ()
  })


  constructor(private routerLink : Router){}


  ngOnInit(): void {
    if(this.slideToggle == null) this.slideToggle = false;
  }
  

  title = 'Weather Analyzer'

  menuItems : number[] | undefined
 


}
