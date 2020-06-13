import { Component, OnInit, ViewChild } from '@angular/core';
import { Column, Button, Topic, TopicType } from 'src/app/models/models';
import { MatTableDataSource } from '@angular/material';
import { ManagementService } from 'src/app/services/management.service';
import { Router } from '@angular/router';
import { JsonPipe } from '@angular/common';
import { ManagementComponent } from '../management/management.component';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-topic-management',
  templateUrl: './topic-management.component.html',
  styleUrls: ['./topic-management.component.css']
})
export class TopicManagementComponent implements OnInit {

  constructor(private managementService: ManagementService,
    private router: Router) { }

  @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;

  public columns: Column[];

  public buttons: Button[];

  public name: BehaviorSubject<string>;

  public topic: Topic;

  public datasource: MatTableDataSource<TopicType>;

  ngOnInit() {
    this.initializeButtons();
    this.initializeColumns();
    this.name = new BehaviorSubject("");

    this.datasource = new MatTableDataSource<TopicType>([]);
    let ids = JSON.parse(history.state.data);
    this.managementService.getAllTopicsFromArea(ids.areaId)
      .subscribe((topics) => {
        let thisTopic = topics.find(t => t.id == ids.topicId);
        this.topic = thisTopic;
        this.name.next(this.topic.name);
        this.datasource = new MatTableDataSource<TopicType>(this.topic.types);
        this.datasource.paginator = this.managementeComponent.paginator;
      })
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
    this.buttons = [editButton];
  }

  redirectToCreate() {
    let typeInfo = { topicId: this.topic.id, type: null };
    this.router.navigate(['/Type'], { state: { data: JSON.stringify(typeInfo) } });
  }

  redirectToEdit(element: TopicType) {
    let typeInfo = { topicId: this.topic.id, type: element };
    this.router.navigate(['/Type'], { state: { data: JSON.stringify(typeInfo) } });
  }

}
