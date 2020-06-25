import { Component, OnInit, ViewChild } from '@angular/core';
import { Column, Button, Topic, TopicType } from 'src/app/models/models';
import { MatTableDataSource } from '@angular/material';
import { ManagementService } from 'src/app/services/management.service';
import { Router } from '@angular/router';
import { JsonPipe } from '@angular/common';
import { ManagementComponent } from '../management/management.component';
import { BehaviorSubject } from 'rxjs';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { HtmlHelpers } from 'src/app/helpers/html.helper';
import { TypeService } from 'src/app/services/type.service';

@Component({
  selector: 'app-topic-management',
  templateUrl: './topic-management.component.html',
  styleUrls: ['./topic-management.component.css']
})
export class TopicManagementComponent implements OnInit {

  constructor(private managementService: ManagementService,
    private router: Router, private snackbarService: SnackbarService,
    private typeService: TypeService) { }

  @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;

  public columns: Column[];

  public buttons: Button[];

  public name: BehaviorSubject<string>;

  public topic: Topic;

  public datasource: BehaviorSubject<MatTableDataSource<TopicType>>;

  public shouldDisable: boolean;

  ngOnInit() {

    this.initializeButtons();
    this.initializeColumns();
    this.InitializeDataContainers();

    try {
      let ids = JSON.parse(history.state.data);
      this.managementService.getAllTopicsFromArea(ids.areaId)
        .subscribe((topics) => {
          let thisTopic = topics.find(t => t.id == ids.topicId);
          this.topic = thisTopic;
          this.topic.areaId = ids.areaId;
          this.name.next(this.topic.name);
          let newSource = new MatTableDataSource<TopicType>(this.topic.types);
          newSource.paginator = this.managementeComponent.paginator;
          this.datasource.next(newSource);
          this.shouldDisable = false;
        }, (error) => {
          this.ShowError(HtmlHelpers.getHtmlErrorMessage(error))
        })
    } catch (exception) {
      this.shouldDisable = true;
      this.ShowError("Please select an Area first");
    }
  }

  private ShowError(message: string) {
    this.snackbarService.notifications$.next({
      message: message,
      action: "Error !",
      config: Object.assign({}, { duration: 3000 }, this.snackbarService.configError)
    });
  }

  private InitializeDataContainers() {
    this.name = new BehaviorSubject("");
    let source = new MatTableDataSource<TopicType>();
    source.paginator = this.managementeComponent.paginator;
    this.datasource = new BehaviorSubject(source);
  }

  initializeColumns() {
    this.columns = []
    let headerName: Column = {
      columnClass: "name",
      columnName: "Types",
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
      callback: (element) => { this.redirectToEdit(element) }
    }
    let deleteButton: Button = {
      buttonTooltip: "delete",
      iconName: "delete",
      callback: (element) => { this.deleteType(element) }
    }
    this.buttons = [editButton, deleteButton];
  }

  deleteType(element) {
    this.typeService.deleteType(element.id).subscribe(
      (res) => {
        this.topic.types = this.topic.types.filter(t => t.id != element.id);
        let source = new MatTableDataSource(this.topic.types);
        source.paginator = this.managementeComponent.paginator;
        this.datasource.next(source);
        this.snackbarService.notifications$.next({
          message: res,
          action: "Success!",
          config: this.snackbarService.configSuccess
        });
      }, (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: "Error!",
          config: this.snackbarService.configError
        });
      });
  }

  redirectToCreate() {
    let typeInfo = { areaId: this.topic.areaId, topicId: this.topic.id, type: null };
    this.router.navigate(['/Type'], { state: { data: JSON.stringify(typeInfo) } });
  }

  redirectToEdit(element: TopicType) {
    let typeInfo = { areaId: this.topic.areaId, topicId: this.topic.id, type: element };
    this.router.navigate(['/Type'], { state: { data: JSON.stringify(typeInfo) } });
  }

  goBack() {
    this.managementService.getAllAreas().subscribe((areas) => {
      let area = areas.find(a => a.id == this.topic.areaId);
      this.router.navigate(['/Area'], { state: { data: JSON.stringify(area) } });
    });
  }

}
