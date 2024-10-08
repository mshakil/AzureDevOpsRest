Feature: CreateUser

Create a new user

@Smoke
Scenario: Create a new user with valid inputs
	Given User with name "Peter"
	And User with job "Manager"
	When Send request to create user
	Then Validate usr is created
