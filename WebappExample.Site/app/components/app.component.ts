import { Component } from '@angular/core';

import { LoginService } from '../services/login.service';

@Component({
	moduleId: module.id,
	selector: 'my-app',
	templateUrl: 'app.component.html'
})

export class AppComponent {
	constructor(private loginService: LoginService) {};
	
	isLoggedIn(): boolean {
		return this.loginService.isLoggedIn();
	}
	
	logout() {
		this.loginService.logout();
	}
}