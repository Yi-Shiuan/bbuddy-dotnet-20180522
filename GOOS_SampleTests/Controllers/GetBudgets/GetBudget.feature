Feature: GetBudget
	
Scenario: 20180415 to 20180515
	Given Already input budget
		| month   | budget |
		| 2016-02 | 560    |
		| 2018-04 | 600    |
		| 2018-05 | 620    |
		| 2018-07 | 620    |
	When input 2018-04-15 and 2018-05-15
	Then get budget 620

Scenario: 20180415 to 20180630
	Given Already input budget
		| month   | budget |
		| 2016-02 | 560    |
		| 2018-04 | 600    |
		| 2018-05 | 620    |
		| 2018-07 | 620    |
	When input 2018-04-15 and 2018-06-30
	Then get budget 940

Scenario: 20180520 to 20180716
	Given Already input budget
		| month   | budget |
		| 2016-02 | 560    |
		| 2018-04 | 600    |
		| 2018-05 | 620    |
		| 2018-07 | 620    |
	When input 2018-05-20 and 2018-07-16
	Then get budget 560

Scenario: 20160115 to 20160213
	Given Already input budget
		| month   | budget |
		| 2016-02 | 560    |
		| 2018-04 | 600    |
		| 2018-05 | 620    |
		| 2018-07 | 620    |
	When input 2018-05-20 and 2018-07-16
	Then get budget 247