import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Router } from '@angular/router';

@Injectable()
export class LoginService {
	private loginUrl = 'http://localhost:63160/api/login';
	private loggedIn = false;

	constructor(private http: Http, private router: Router) {
		this.loggedIn = !!localStorage.getItem('auth_token');
	}

	login(userName, password) {
		let bodyString = JSON.stringify({ userName, password });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http
			.post(this.loginUrl, bodyString, options)
			.map(res => res.json())
			.map((res) => {
				if (res.success) {
					localStorage.setItem('auth_token', res.token);
					this.loggedIn = true;
				}

				return res.success;
			})
			.catch((error: any) => Observable.throw(error.json().error || 'Server error');
	}

	logout() {
		localStorage.removeItem('auth_token');
		this.loggedIn = false;
		this.router.navigate(['/login']);
	}

	isLoggedIn() {
		return this.loggedIn;
	}
}