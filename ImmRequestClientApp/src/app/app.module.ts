import { MaterialModule } from './material.icons.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './components/app-component/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { RequestManagementComponent } from './components/request-management/request-management.component';
import { AdminLoggedIn } from './guards/admin-logged-in.guard';
import { AdminNotLoggedIn } from './guards/admin-not-logged-in.guard';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ConfirmationComponent } from './modals/confirmation/confirmation.component';
import { ModifyAdminComponent } from './components/modify-admin/modify-admin.component';
import { ManagementComponent } from './components/management/management.component';
import { ButtonComponent } from './components/button/button.component';
import { DummyComponent } from './components/dummy/dummy.component';
import { TypeEditorComponent } from './components/type-editor/type-editor.component';
import { FieldEditorDialogComponent } from './modals/field-editor-dialog/field-editor-dialog.component';

declare var require: any;

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    RequestManagementComponent,
    ConfirmationComponent,
    ModifyAdminComponent,
    ManagementComponent,
    ButtonComponent,
    DummyComponent,
    TypeEditorComponent,
    FieldEditorDialogComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: '', redirectTo: 'home-page', pathMatch: 'full' },
      { path: 'home-page', component: RequestManagementComponent},
      { path: 'modify-admin', component: ModifyAdminComponent, canActivate: [AdminNotLoggedIn]},
      { path: 'dummy', component: DummyComponent},
      { path: 'type-editor', component: TypeEditorComponent}
    ])
  ],
  providers: [ AdminLoggedIn, AdminNotLoggedIn ],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmationComponent, FieldEditorDialogComponent]
})
export class AppModule { }
