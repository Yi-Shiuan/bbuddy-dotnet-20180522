Feature: budget_query
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Get Total Budget
	

	Given
		| month | amount |
		| 2018-05 | 310 |
		| 2018-06 | 600 |
		| 2018-07 | 620 |
	When I query total budget from "2018-05-16" to "2018-07-15"
	Then I will get total budget 1060