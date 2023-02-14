import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {  HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import {RegisterComponent  } from "./register/register.component";
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MembersComponent } from './members/members.component';
import { MemberListDetailComponent } from './members/member-list-detail/member-list-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './_modules/shared/shared.module';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { ErrorsInterceptor } from './_interceptor/errors.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    ListsComponent,
    MembersComponent,
    MemberListDetailComponent,
    MessagesComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SharedModule,
    BrowserAnimationsModule,

  ],
  providers: [
    {provide:HTTP_INTERCEPTORS, useClass: ErrorsInterceptor,multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
