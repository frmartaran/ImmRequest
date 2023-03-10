import { MaterialModule } from './material.icons.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './components/app-component/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { AdminLoggedIn } from './guards/admin-logged-in.guard';
import { AdminNotLoggedIn } from './guards/admin-not-logged-in.guard';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ConfirmationComponent } from './modals/confirmation/confirmation.component';
import { ModifyAdminComponent } from './components/modify-admin/modify-admin.component';
import { ManagementComponent } from './components/management/management.component';
import { ButtonComponent } from './components/button/button.component';
import { DummyComponent } from './components/dummy/dummy.component';
import { TypeEditorComponent } from './components/type-editor/type-editor.component';
import { FieldEditorDialogComponent } from './modals/field-editor-dialog/field-editor-dialog.component';
import { DatePipe } from '@angular/common';
import { AreasDisplayerComponent } from './components/areas-displayer/areas-displayer.component';
import { AreasTableComponent } from './components/areas-table/areas-table.component';
import { AdminManagementComponent } from './components/admin-management/admin-management.component';
import { TopicManagementComponent } from './components/topic-management/topic-management.component';
import { ImporterComponent } from './components/importer/importer.component';
import { RequestSummaryReportComponent } from './components/request-summary-report/request-summary-report.component';
import { TypeSummaryReportComponent } from './components/type-summary-report/type-summary-report.component';
import { CreateCitizenRequestComponent } from './components/create-citizen-request/create-citizen-request.component';
import { RadioButtonsStepComponent } from './components/radio-buttons-step/radio-buttons-step.component';
import { FieldValuesComponent } from './modals/field-values/field-values.component';
import { GetRequestStatusComponent } from './components/get-request-status/get-request-status.component';
import { ManageRequestStatusComponent } from './components/manage-request-status/manage-request-status.component';
import { EditStatusComponent } from './modals/edit-status/edit-status.component';
import { ShowRequestValuesComponent } from './modals/show-request-values/show-request-values.component';

declare var require: any;

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    ConfirmationComponent,
    ModifyAdminComponent,
    ManagementComponent,
    ButtonComponent,
    DummyComponent,
    TypeEditorComponent,
    FieldEditorDialogComponent,
    AreasDisplayerComponent,
    AreasTableComponent,
    AdminManagementComponent,
    TopicManagementComponent,
    ImporterComponent,
    CreateCitizenRequestComponent,
    RadioButtonsStepComponent,
    FieldValuesComponent,
    RequestSummaryReportComponent,
    TypeSummaryReportComponent,
    GetRequestStatusComponent,
    ManageRequestStatusComponent,
    EditStatusComponent,
    ShowRequestValuesComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: '', redirectTo: 'home-page', pathMatch: 'full' },
      { path: 'home-page', component: CreateCitizenRequestComponent},
      { path: 'modify-admin', component: ModifyAdminComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'dummy', component: DummyComponent},
      { path: 'Type', component: TypeEditorComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'Area', component: AreasDisplayerComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'Areas', component: AreasTableComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'manage-admin', component: AdminManagementComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'Topic', component: TopicManagementComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'Import', component: ImporterComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'RequestReport', component: RequestSummaryReportComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'TypeReport', component: TypeSummaryReportComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'RequestStatus', component: GetRequestStatusComponent },
      { path: 'ManageRequestsStatus', component: ManageRequestStatusComponent, canActivate: [AdminNotLoggedIn]},
    ])
  ],
  providers: [ AdminLoggedIn, AdminNotLoggedIn, DatePipe],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmationComponent, FieldEditorDialogComponent, FieldValuesComponent, EditStatusComponent, ShowRequestValuesComponent]
})
export class AppModule { }
