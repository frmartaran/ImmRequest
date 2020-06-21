import { NavMenuComponent } from './../nav-menu/nav-menu.component';
import { CitizenRequest, RequestValue, BaseField, Button, Column } from './../../models/models';
import { TypeService } from 'src/app/services/type.service';
import { RadioButton } from 'src/app/models/models';
import { Subject, BehaviorSubject } from 'rxjs';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ManagementService } from 'src/app/services/management.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { HtmlHelpers } from 'src/app/helpers/html.helper';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { FieldValuesComponent } from 'src/app/modals/field-values/field-values.component';

@Component({
  selector: 'app-create-citizen-request',
  templateUrl: './create-citizen-request.component.html',
  styleUrls: ['./create-citizen-request.component.css']
})
export class CreateCitizenRequestComponent implements OnInit {

  public areaRadioButtons$: Subject<RadioButton[]>

  public topicRadioButtons$: Subject<RadioButton[]>

  public typeRadioButtons$: Subject<RadioButton[]>

  public typeFields$: Subject<BaseField[]>;

  public canAreaAdvanceToNextStep$: Subject<boolean>

  public canTopicAdvanceToNextStep$: Subject<boolean>

  public canTypeAdvanceToNextStep$: Subject<boolean>

  public selectedField: BaseField;

  public topicTypesFields: BaseField[];

  public requestFields$: Subject<BaseField[]>;

  public fieldsTableDataSource$: BehaviorSubject<MatTableDataSource<any>>;

  public requestToSend: CitizenRequest;

  public descriptionFormGroup: FormGroup;

  public userInfoFormGroup: FormGroup;

  public buttons: Button[];

  public columns: Column[];

  public valuesTitle:string;

  constructor(private managementService: ManagementService,
    private typeService: TypeService,
    private snackbarService: SnackbarService,
    private _formBuilder: FormBuilder,
    public dialog: MatDialog) { }

  ngOnInit() {
    this.canAreaAdvanceToNextStep$ = new Subject<boolean>();
    this.canTopicAdvanceToNextStep$ = new Subject<boolean>();
    this.canTypeAdvanceToNextStep$ = new Subject<boolean>();
    this.initializeStepper();
  }

  initializeStepper(){
    this.initializeFields();
    this.getAllAreas();
    this.initializeFieldsTable();
  }

  initializeFields(){
    let source = new MatTableDataSource<any>();
    this.fieldsTableDataSource$ = new BehaviorSubject<MatTableDataSource<any>>(source);
    this.descriptionFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.userInfoFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
    this.areaRadioButtons$ = new Subject<RadioButton[]>();
    this.topicRadioButtons$ = new Subject<RadioButton[]>();
    this.typeRadioButtons$ = new Subject<RadioButton[]>();
    this.canAreaAdvanceToNextStep$.next(false);
    this.canTopicAdvanceToNextStep$.next(false);
    this.canTypeAdvanceToNextStep$.next(false);
    this.typeFields$ = new Subject<BaseField[]>();
    this.topicTypesFields = [];
    this.requestFields$ = new Subject<BaseField[]>();
    this.valuesTitle = "Request Values";
    this.requestToSend = {
      description: '',
      citizenName: '',
      email: '',
      phone: '',
      areaId: 0,
      topicId: 0,
      topicTypeId: 0,
      values: []
    }
  }

  getAllAreas(){
    this.managementService.getAllAreas()
    .subscribe(
      (response) => {
        let radioButtons = response.map(area => <RadioButton>{
          id: area.id,
          checked: false,
          name: area.name
        });
        this.areaRadioButtons$.next(radioButtons)
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackbarService.configError
        });
      }
    )
  }

  getAllTopicsFromArea(id: number){
    this.managementService.getAllTopicsFromArea(id)
    .subscribe(
      (response) => {
        let radioButtons = response.map(topic => <RadioButton>{
          id: topic.id,
          checked: false,
          name: topic.name
        });
        this.topicRadioButtons$.next(radioButtons);
        this.canAreaAdvanceToNextStep$.next(true);
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackbarService.configError
        });
      }
    )
  }

  getAllTypesFromTopic(id: number){
    this.typeService.getAll(id)
    .subscribe(
      (response) => {
        let radioButtons = response.map(type => <RadioButton>{
          id: type.id,
          checked: false,
          name: type.name
        });
        response.forEach(type => Array.prototype.push.apply(this.topicTypesFields, type.fields));
        this.typeRadioButtons$.next(radioButtons);
        this.canTopicAdvanceToNextStep$.next(true);
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackbarService.configError
        });
      }
    )
  }

  setAreaTopics(id:number){
    this.getAllTopicsFromArea(id);
    this.requestToSend.areaId = id;
    this.requestToSend.topicId = 0;
    this.requestToSend.topicTypeId = 0;
    let source = new MatTableDataSource<any>();
    this.fieldsTableDataSource$ = new BehaviorSubject<MatTableDataSource<any>>(source);
    this.requestToSend.values = [];
  }

  setTopicTypes(id:number){
    this.getAllTypesFromTopic(id);
    this.requestToSend.topicId = id;
    this.requestToSend.topicTypeId = 0;
    let source = new MatTableDataSource<any>();
    this.fieldsTableDataSource$ = new BehaviorSubject<MatTableDataSource<any>>(source);
    this.requestToSend.values = [];
  }

  setType(id:number){
    this.requestToSend.topicTypeId = id;
    this.typeFields$.next(this.topicTypesFields.filter(field => field.parentTypeId == id))
    let source = new MatTableDataSource<any>();
    this.fieldsTableDataSource$ = new BehaviorSubject<MatTableDataSource<any>>(source);
    this.requestToSend.values = [];
    this.canTypeAdvanceToNextStep$.next(true);
  }

  addField(field:BaseField){
    let addField = this.dialog.open(FieldValuesComponent, {
      data: {
        dataType: field.dataType,
        isMultipleSelectEnabled: field.multipleValues
      }
    });
    addField.afterClosed().subscribe((res) => {
        if (res) {
            let value:RequestValue = {
              fieldId: field.id,
              name: field.name,
              value: res
            }
            let source = new MatTableDataSource<any>();
            this.requestToSend.values.push(value);
            source.data = this.requestToSend.values;
            this.fieldsTableDataSource$.next(source);
        }
    });
  }

  initializeFieldsTable(){
    this.initializeColumns();
    this.initializeButtons();
  }

  initializeColumns() {
      this.columns = []
      let fieldsName: Column = {
          columnClass: "name",
          columnName: "Fields",
          hasButtons: false
      }
      let fieldsValues: Column = {
        columnClass: "value",
        columnName: "Value",
        hasButtons: false
      }
      let headerButtons: Column = {
          columnClass: "buttons",
          columnName: "Actions",
          hasButtons: true
      }
      this.columns.push(fieldsName);
      this.columns.push(fieldsValues);
      this.columns.push(headerButtons);
  }

  initializeButtons() {
      let deleteButton: Button = {
          buttonTooltip: "delete",
          iconName: "delete",
          callback: (element) => { this.deleteField(element) }
      }
      this.buttons = [deleteButton];
  }

  deleteField(field: RequestValue) {
      this.requestToSend.values = this.requestToSend.values.filter(value => value.fieldId != field.fieldId);
      let source = new MatTableDataSource<any>();
      source.data = this.requestToSend.values;
      this.fieldsTableDataSource$.next(source);
  }

  submitCitizenRequest(){
    this.managementService.createCitizenRequest(this.requestToSend)
    .subscribe(
      (response) => {
        this.snackbarService.notifications$.next({
          message: response,
          action: 'Success!',
          config: this.snackbarService.configSuccess
        });
        this.initializeStepper();
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackbarService.configError
        });
        this.initializeStepper();
      }
    )
  }

}
