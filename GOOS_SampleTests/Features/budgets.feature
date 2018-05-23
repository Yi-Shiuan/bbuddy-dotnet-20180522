@web
Feature: budgets
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Add budget
	When I add buget for "2018-05" with amount 500
	Then I will see the following list
	| month     | amount |
	| "2018-05" | 500    |

