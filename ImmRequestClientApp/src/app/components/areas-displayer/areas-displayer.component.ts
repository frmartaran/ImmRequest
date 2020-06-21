import { Component, OnInit, ViewChild } from '@angular/core';
import { Button, Column, Area, Topic } from 'src/app/models/models';
import { MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { ManagementComponent } from '../management/management.component';

@Component({
    selector: 'app-areas-displayer',
    templateUrl: './areas-displayer.component.html',
    styleUrls: ['./areas-displayer.component.css']
})
export class AreasDisplayerComponent implements OnInit {

    constructor(private router: Router,
        private snackbarService: SnackbarService) { }

    @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;

    public area: Area;

    public buttons: Button[];
    public columns: Column[];

    public datasource: MatTableDataSource<Topic>;
    ngOnInit() {
        this.initializeButtons();
        this.initializeColumns();
        try {
            this.area = JSON.parse(history.state.data);
            this.datasource = new MatTableDataSource<Topic>(this.area.topics);
            this.datasource.paginator = this.managementeComponent.paginator;
        } catch (exception) {
            this.snackbarService.notifications$.next({
                message: "Please select an Area first",
                action: "Error !",
                config: this.snackbarService.configError
            });
        }
    }

    initializeColumns() {
        this.columns = []
        let idColumn: Column = {
            columnClass: "id",
            columnName: "#",
            hasButtons: false
        }
        let headerName: Column = {
            columnClass: "name",
            columnName: "Topics",
            hasButtons: false
        }
        let headerButtons: Column = {
            columnClass: "buttons",
            columnName: "Actions",
            hasButtons: true
        }
        this.columns.push(idColumn);
        this.columns.push(headerName);
        this.columns.push(headerButtons);
    }

    initializeButtons() {
        let editButton: Button = {
            buttonTooltip: "edit",
            iconName: "edit",
            callback: (element) => { this.redirect(element) }
        }
        this.buttons = [editButton];
    }

    redirect(element: Topic) {
        var ids = { areaId: this.area.id, topicId: element.id };
        this.router.navigate(['/Topic'], { state: { data: JSON.stringify(ids) } });
    }

    goBack(){
        this.router.navigate(['/Areas']);
    }
}
