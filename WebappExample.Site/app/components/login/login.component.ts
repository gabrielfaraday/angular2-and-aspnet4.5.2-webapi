import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { LoginService } from '../../services/login.service';

import { User } from '../../models/user';

@Component({
	moduleId: module.id
	selector: 'login',
	templateUrl: 'login.component.html'
})

export class LoginComponent {
	user: User = new User();
	
	constructor(private loginService: LoginService, private router: Router) {}

	login() {
		this.loginService
			.login(this.user.userName, this.user.password)
			.subscribe((result) => {
				if (result)
					this.router.navigate(['']);
			});
	}
}