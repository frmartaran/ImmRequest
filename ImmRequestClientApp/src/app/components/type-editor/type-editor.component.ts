import { Component, OnInit } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseField, Button } from 'src/app/models/models';

@Component({
  selector: 'app-type-editor',
  templateUrl: './type-editor.component.html',
  styleUrls: ['./type-editor.component.css']
})

export class TypeEditorComponent implements OnInit {

  constructor(private snackBarService: SnackbarService) { }

  public action: string;

  public id: number;

  public name: string;

  public fields: BaseField[];

  public buttons: Button[];

  ngOnInit() {
    this.initializeButtons();
  }

  initializeButtons(){
    let editButton: Button = {
      buttonTooltip: "edit",
      iconName: "edit",
      callback: (element) => {this.openFieldDetails(element)}
    }
    let deleteButton: Button = {
      buttonTooltip: "delete",
      iconName: "delete",
      callback: (element) => {this.deleteField(element)}
    }
    this.buttons = [editButton, deleteButton];
  }

  deleteField(field: BaseField){
  }

  openFieldDetails(field: BaseField){

  }

}
