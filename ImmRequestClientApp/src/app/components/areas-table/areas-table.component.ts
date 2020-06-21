import { Component, OnInit, ViewChild } from '@angular/core';
import { Area, Column, Button } from 'src/app/models/models';
import { MatTableDataSource } from '@angular/material';
import { ManagementService } from 'src/app/services/management.service';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { HtmlHelpers } from 'src/app/helpers/html.helper';
import { ManagementComponent } from '../management/management.component';

@Component({
    selector: 'app-areas-table',
    templateUrl: './areas-table.component.html',
    styleUrls: ['./areas-table.component.css']
})
export class AreasTableComponent implements OnInit {

    constructor(private managementService: ManagementService,
        private router: Router, private snackbarService: SnackbarService) { }

    @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;

    public areas: Area[];
    public datasource: MatTableDataSource<Area>;

    public columns: Column[];
    public buttons: Button[];

    public title: string;

    ngOnInit() {
        this.initializeButtons();
        this.initializeColumns();
        this.title = "Areas"
        this.datasource = new MatTableDataSource<Area>();
        this.managementService.getAllAreas()
            .subscribe((areas) => {
                this.areas = areas
                this.datasource = new MatTableDataSource<Area>(areas);
                this.datasource.paginator = this.managementeComponent.paginator;
            }, (error) => {
                this.snackbarService.notifications$.next({
                    message: HtmlHelpers.getHtmlErrorMessage(error),
                    action: "Error !",
                    config: this.snackbarService.configError
                });
            });
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
            columnName: "Areas",
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
        let seeMoreButton: Button = {
            buttonTooltip: "see more",
            iconName: "visibility",
            callback: (element) => { this.redirect(element) }
        }
        this.buttons = [seeMoreButton];
    }
    redirect(element: Area) {
        let area = JSON.stringify(element);
        this.router.navigate(['/Area'], { state: { data: area } });
    }
}
