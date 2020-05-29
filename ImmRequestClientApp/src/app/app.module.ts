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
import { LogoutComponent } from './modals/logout/logout.component';

declare var require: any;

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    RequestManagementComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent, canActivate: [AdminLoggedIn] },
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'home-page', component: RequestManagementComponent, canActivate: [AdminNotLoggedIn] },
    ])
  ],
  providers: [ AdminLoggedIn, AdminNotLoggedIn ],
  bootstrap: [AppComponent],
  entryComponents: [LogoutComponent]
})
export class AppModule { }
