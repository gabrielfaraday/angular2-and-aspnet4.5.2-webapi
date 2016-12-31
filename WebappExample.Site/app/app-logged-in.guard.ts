import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

import { LoginService } from './services/login.service';

@Injectable()
export class AppLoggedInGuard implements CanActivate {
	constructor(private loginService: LoginService, private router: Router) {}

	canActivate() {
		if (this.loginService.isLoggedIn())
			return true;
		
		this.router.navigate(['/login']);
		return false;
	}
}