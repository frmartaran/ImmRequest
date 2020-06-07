import { Component, OnInit } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseField, Button, DataType } from 'src/app/models/models';
import { NgForm } from '@angular/forms';

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
    let field: BaseField = {
      name: "Test",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    this.fields = [field]
  }

  Send(typeEditorForm: NgForm){

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
    
    this.snackBarService.notifications$.next({
      message: "Delete button click",
      action: 'Success!',
      config: this.snackBarService.configSuccess
    });
  }

  openFieldDetails(field: BaseField){

    this.snackBarService.notifications$.next({
      message: "Edit button click",
      action: 'Success!',
      config: this.snackBarService.configSuccess
    });
  }

}
