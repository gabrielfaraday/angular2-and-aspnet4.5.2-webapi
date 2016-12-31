import { NgModule }      from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppLoggedInGuard } from './app-logged-in.guard';

import { ContactDetailComponent } from './components/contacts/contact-detail.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
	{ path: '', redirectTo: '/contacts', pathMatch: 'full', canActivate: [AppLoggedInGuard] },
	{ path: 'contacts', component: ContactsComponent, canActivate: [AppLoggedInGuard]  },
	{ path: 'contact/:id', component: ContactDetailComponent, canActivate: [AppLoggedInGuard]  },
	{ path: 'contact', component: ContactDetailComponent, canActivate: [AppLoggedInGuard]  },
	{ path: 'login', component: LoginComponent }
];

@NgModule({
	imports: [ RouterModule.forRoot(routes) ],
	exports: [ RouterModule ]
})

export class AppRoutingModule {}