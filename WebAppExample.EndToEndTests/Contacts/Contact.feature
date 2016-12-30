Feature: ContactFeature

Scenario: Adding new contact
	Given I want to add a new contact
		| Name | Email         | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com | carlton st, los angeles, CA | 55123456 | 08/01/1970 |
	When I call the post method
	Then the result should be a reponse message with status 'OK' and content with contact
		| Name | Email         | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com | carlton st, los angeles, CA | 55123456 | 08/01/1970 |

Scenario: Editing a contact
	Given I have a contact
		| Name | Email         | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com | carlton st, los angeles, CA | 55123456 | 08/01/1970 |
	And I edit its name to 'Charles'
	And email to 'charles@gmail.com'
	And phone to empty
	When I call the put method
	Then the result should be a reponse message with status 'OK' and content with contact
		| Name    | Email             | Address                     | Phone | Birth Date |
		| Charles | charles@gmail.com | carlton st, los angeles, CA |       | 08/01/1970 |

Scenario: Deleting a contact
	Given I have a contact
		| Name | Email         | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com | carlton st, los angeles, CA | 55123456 | 08/01/1970 |
	When I call the delete method for that contact id
	Then the result should be a reponse message with status 'OK' 
	And with content 'Contact successfully deleted'


Scenario: Getting existing contact
	Given I have a contact
		| Name | Email         | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com | carlton st, los angeles, CA | 55123456 | 08/01/1970 |
	When I call the get method for for that contact id
	Then the result should be a reponse message with status 'OK' and content with contact
		| Name | Email         | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com | carlton st, los angeles, CA | 55123456 | 8/1/1970   |


Scenario: Getting all contacts
	Given I have following contacts
		| Name | Email          | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com  | carlton st, los angeles, CA | 55123456 | 08/01/1970 |
		| Will | will@gmail.com |                             |          |            |
	When I call the get method
	Then the result should be a reponse message with status 'OK' and content with contacts
		| Name | Email          | Address                     | Phone    | Birth Date |
		| Bob  | bob@gmail.com  | carlton st, los angeles, CA | 55123456 | 8/1/1970   |
		| Will | will@gmail.com |                             |          |            |