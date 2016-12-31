import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
	moduleId: module.id
	selector: 'contact-detail',
	templateUrl: 'contact-detail.component.html'
})

export class ContactDetailComponent implements OnInit{
	contact: Contact;
	title: string;
	
	constructor(
		private contactService: ContactService,
		private route: ActivatedRoute,
		private location: Location
	) {}
	
	ngOnInit(): void {
		this.route.params.forEach((params : Params) => {
			let id = +params['id'];
			
			if (id)
			{
				this.contactService
					.getContact(id)
					.subscribe(c => this.contact = c);
				
				this.title = "Alterar Contato";
			}
			else
			{
				this.contact = new Contact();
				this.title = "Novo Contato";
			}
		});
	}
	
	goBack(): void {
		this.location.back();
	}
	
	add(): void {
		this.contactService
			.add(this.contact)
			.subscribe(() => this.goBack());
	}
	
	update(): void {
		this.contactService
			.update(this.contact)
			.subscribe(() => this.goBack());
	}
}