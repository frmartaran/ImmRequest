// import { Component, OnInit } from '@angular/core';
// import { Button, Column, Area, Topic } from 'src/app/models/models';
// import { MatTableDataSource } from '@angular/material';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-areas-displayer',
//   templateUrl: './areas-displayer.component.html',
//   styleUrls: ['./areas-displayer.component.css']
// })
// export class AreasDisplayerComponent implements OnInit {

//   constructor( private router: Router) { }

//   public area: Area;

//   public buttons: Button[];
//   public columns: Column[];

//   public datasource: MatTableDataSource<Topic>;

//   ngOnInit() {
//     this.initializeButtons();
//     this.initializeColumns();
//     this.area = JSON.parse(history.state.data);
//     this.datasource = new MatTableDataSource<Topic>(this.area.topics);
//   }

//   initializeColumns() {
//     this.columns = []
//     let headerName: Column = {
//       columnClass: "Topics",
//       columnName: "Topics",
//       hasButtons: false
//     }
//     let headerButtons: Column = {
//       columnClass: "buttons",
//       columnName: "Actions",
//       hasButtons: true
//     }
//     this.columns.push(headerName);
//     this.columns.push(headerButtons);
//   }

//   initializeButtons() {
//     let editButton: Button = {
//       buttonTooltip: "edit",
//       iconName: "edit",
//       callback: (element) => { }
//     }
//     this.buttons = [editButton];
//   }

//   redirect(element: Topic){

//   }
  

// }
