Feature: BudgetFeature
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: add budget
	When I add a buget 2000 for "2017-10"
	Then it should display 2000 for "2017-10"
