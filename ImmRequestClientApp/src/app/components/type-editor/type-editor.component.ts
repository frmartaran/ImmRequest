import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseField, Button, Column, TopicType } from 'src/app/models/models';
import { NgForm } from '@angular/forms';
import { MatTableDataSource, MatDialog, MatPaginator } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { FieldEditorDialogComponent } from 'src/app/modals/field-editor-dialog/field-editor-dialog.component';
import { TypeService } from 'src/app/services/type.service';
import { HtmlHelpers } from 'src/app/helpers/html.helper';
import { ManagementComponent } from '../management/management.component';
import { Router } from '@angular/router';

@Component({
    selector: 'app-type-editor',
    templateUrl: './type-editor.component.html',
    styleUrls: ['./type-editor.component.css']
})

export class TypeEditorComponent implements OnInit {

    constructor(public dialog: MatDialog,
        private snackBarService: SnackbarService,
        private typeService: TypeService,
        private router: Router) { }

    @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;

    public action: BehaviorSubject<string>;

    public fieldsTitle: string;

    public name: string;

    public buttons: Button[];

    public columns: Column[];

    public allFields: BehaviorSubject<BaseField[]>;

    public datasource: BehaviorSubject<MatTableDataSource<BaseField>>;

    public type: TopicType;

    private parentTopicId: number;

    private areaId: number;

    public shouldDisable: boolean;

    private id: number;

    ngOnInit() {
        
        this.InitializeDataContainers();
        try {
            let typeInfo = JSON.parse(history.state.data);
            this.parentTopicId = typeInfo.topicId;
            this.areaId = typeInfo.areaId;
            this.type = typeInfo.type;
            this.id = typeInfo.type.id;
            this.type.id = this.id;
            this.shouldDisable = false;
        } catch (exception) {
            this.shouldDisable = true;
            this.snackBarService.notifications$.next({
                message: 'Please Select an Area and Topic First',
                action: 'Error !',
                config: Object.assign({}, {duration:3000}, this.snackBarService.configError)
            });
        }


        if (this.type != null) {
            this.InitializeType();
        }

        this.initializeButtons();
        this.initializeColumns();
    }


    private InitializeType() {
        this.action.next("Edit");
        this.name = this.type.name;
        let source = new MatTableDataSource<BaseField>(this.type.fields);
        source.paginator = this.managementeComponent.paginator;
        this.datasource.next(source);
        this.allFields.next(this.type.fields);
    }

    private InitializeDataContainers() {
        this.fieldsTitle = "Fields";
        this.action = new BehaviorSubject("Create");
        let source = new MatTableDataSource<BaseField>();
        this.datasource = new BehaviorSubject(source);
        this.allFields = new BehaviorSubject([]);
    }

    Send(typeEditorForm: NgForm) {
        let type: TopicType = {
            name: typeEditorForm.value.name,
            fields: []
        }
        let fields = [];
        this.allFields.subscribe((currentFields) => {
            fields = currentFields
        });
        type.fields = fields;
        this.type = type;
        let action = "";
        this.action.subscribe((intention) => {
            action = intention;
        })
        if (action == "Create") {
            this.Create();
        } else {
            this.Edit();
        }
    }

    private Create() {
        this.typeService.createType(this.parentTopicId, this.type)
            .subscribe((res) => {
                var successObject = JSON.parse(res);
                this.type.id = successObject.id;
                this.action.next("Edit");
                this.snackBarService.notifications$.next({
                    message: successObject.message,
                    action: 'Success!',
                    config: Object.assign({}, {duration:3000}, this.snackBarService.configSuccess)
                });
            }, (error) => {
                this.snackBarService.notifications$.next({
                    message: HtmlHelpers.getHtmlErrorMessage(error),
                    action: 'Error!',
                    config: Object.assign({}, {duration:3000}, this.snackBarService.configError)
                });
            });
    }

    private Edit() {
        this.type.id = this.id;
        this.typeService.updateType(this.type)
            .subscribe((res) => {
                this.snackBarService.notifications$.next({
                    message: res,
                    action: 'Success!',
                    config: Object.assign({}, {duration:3000}, this.snackBarService.configSuccess)
                });
            }, (error) => {
                this.snackBarService.notifications$.next({
                    message: HtmlHelpers.getHtmlErrorMessage(error),
                    action: 'Error!',
                    config: Object.assign({}, {duration:3000}, this.snackBarService.configError)
                });
            });
    }

    initializeColumns() {
        this.columns = []
        let headerName: Column = {
            columnClass: "name",
            columnName: "Fields",
            hasButtons: false
        }
        let headerButtons: Column = {
            columnClass: "buttons",
            columnName: "Actions",
            hasButtons: true
        }
        this.columns.push(headerName);
        this.columns.push(headerButtons);
    }

    initializeButtons() {
        let editButton: Button = {
            buttonTooltip: "edit",
            iconName: "edit",
            callback: (element) => { this.editField(element) }
        }
        let deleteButton: Button = {
            buttonTooltip: "delete",
            iconName: "delete",
            callback: (element) => { this.deleteField(element) }
        }
        this.buttons = [editButton, deleteButton];
    }

    deleteField(field: BaseField) {
        let updatedFields = [];
        this.allFields.subscribe((fields) => {
            updatedFields = fields
        });
        let foundIndex = updatedFields.findIndex(f => f.name == field.name);
        updatedFields = updatedFields.filter((_, index) => index !== foundIndex);
        this.allFields.next(updatedFields);
        this.UpdateDatasource(updatedFields);
    }


    editField(field: BaseField) {
        let action = "Edit";
        let editDialog = this.dialog.open(FieldEditorDialogComponent, {
            data: {
                action: action,
                field: field
            },
            autoFocus: false
        });
        editDialog.afterClosed().subscribe((res) => {
            if (res) {
                let newField = res as BaseField;
                let currentFields = [];
                this.allFields.subscribe((fields) => {
                    currentFields = fields;
                });
                let index = currentFields.findIndex(f => f.name == field.name);
                currentFields[index] = newField;
                this.allFields.next(currentFields);
                this.UpdateDatasource(currentFields);
            }
        });
    }

    createField() {
        let action = "Create"
        let createDialog = this.dialog.open(FieldEditorDialogComponent, {
            data: {
                action: action,
            },
            autoFocus: false
        });
        createDialog.afterClosed().subscribe((res) => {
            if (res) {
                let newField = res as BaseField;
                let currentFields = [];
                this.allFields.subscribe((fields) => {
                    currentFields = fields;
                });
                currentFields.push(newField);
                this.allFields.next(currentFields);
                this.UpdateDatasource(currentFields);
            }
        });
    }

    private UpdateDatasource(currentFields: any[]) {
        let currentDisplay = new MatTableDataSource<BaseField>(currentFields);
        currentDisplay.paginator = this.managementeComponent.paginator;
        this.datasource.next(currentDisplay);
    }

    goBack() {
        let topic = { areaId: this.areaId, topicId: this.parentTopicId };
        this.router.navigate(['/Topic'], { state: { data: JSON.stringify(topic) } });
    }
}
