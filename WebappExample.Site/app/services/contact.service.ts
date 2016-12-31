import { Injectable } from '@angular/core';
import { Headers, Http, Response, RequestOptions ) from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { Contact } from '../models/contact';

@Injectable()
export class ContactService {
	private contactsUrl = 'http://localhost:63160/api/contacts';
	
	constructor(private http: Http) {}
	
	getAll(): Observable<Contact[]> {
		return this.http
			.get(this.contactsUrl, this.setupOptions())
			.map((res: Response) => res.json())
			.catch((error: any) => Observable.throw(error.json().error || 'Server error');
	}
	
	getContact(id: number): Observable<Contact> {
		return this.http
			.get(`${this.contactsUrl}/${id}`, this.setupOptions())
			.map((res: Response) => res.json())
			.catch((error: any) => Observable.throw(error.json().error || 'Server error');
	}
	
	delete(id: number): Observable<Contact[]> {
		return this.http.delete(`${this.contactsUrl}?id=${id}`, this.setupOptions())
			.map((res:Response) => res.json())
			.catch((error:any) => Observable.throw(error.json().error || 'Server error'));
    }
	
	update(contact: Contact): Observable<Contact[]> {
        let bodyString = JSON.stringify(contact);
        
        return this.http.put(this.contactsUrl, bodyString, this.setupOptions())
                         .map((res:Response) => res.json())
                         .catch((error:any) => Observable.throw(error.json().error || 'Server error'));
    }
	
	add(contact: Contact): Observable<Contact[]> {
        let bodyString = JSON.stringify(contact);
        
        return this.http.post(this.contactsUrl, bodyString, this.setupOptions())
                         .map((res:Response) => res.json())
                         .catch((error:any) => Observable.throw(error.json().error || 'Server error'));
    }
	
	private setupOptions(): RequestOptions {
		let headers = new Headers();
		let authToken = localStorage.getItem('auth_token');
		
		headers.append('Content-Type', 'application/json');
		headers.append('Authorization', `Bearer ${authToken}`);
		
		return new RequestOptions({ headers: headers });
	}
}