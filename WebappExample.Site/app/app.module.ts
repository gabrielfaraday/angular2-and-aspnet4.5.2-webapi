import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { DataTableModule } from "angular2-datatable";

import { ContactService } from './services/contact.service';
import { LoginService } from './services/login.service';
import { AppRoutingModule } from './app-routing.module';
import { AppLoggedInGuard } from './app-logged-in.guard';

import { AppComponent } from './components/app.component';
import { ContactDetailComponent } from './components/contacts/contact-detail.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { LoginComponent } from './components/login/login.component';

@NgModule({
	imports:      [ 
			BrowserModule,
			FormsModule,
			AppRoutingModule,
			HttpModule,
			DataTableModule
		],
	declarations: [
			AppComponent,
			ContactDetailComponent,
			ContactsComponent,
			LoginComponent
		],
	providers: [
			ContactService,
			LoginService,
			AppLoggedInGuard
		],
	bootstrap: [ AppComponent]
})

export class AppModule { }