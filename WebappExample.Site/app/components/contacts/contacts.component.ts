import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
	moduleId: module.id,
	selector: 'contacts',
	templateUrl: 'contacts.component.html'
})

export class ContactsComponent implements OnInit{
	contacts: Contact[] = [];
	rowsOnPage = 4;
	
	constructor(private contactService: ContactService, private router: Router) {}
	
	ngOnInit(): void {
		this.contactService
			.getAll()
			.subscribe(h => this.contacts = h);
	}
	
	remove(contact Contact): void {
		this.contactService
			.delete(contact.id)
			.subscribe(h => {
				this.contacts = this.contacts.filter(c => c !== contact);
				alert(h);
			});
	}
	
	addNew(): void {
		this.router.navigate(['/contact']);
	}
	
	goToDetail(contact: Contact): void {
		this.router.navigate(['/contact', contact.id]);
	}
}