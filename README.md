# Zip.InstallmentsService
User Story
As a Zip Customer, I would like to establish a payment plan spread over 6 weeks that splits the original charge evenly over 4 installments.

Acceptance Criteria
Given it is the 1st of January, 2020
When I create an order of $100.00
And I select 4 Installments
And I select a frequency of 14 days
Then I should be charged these 4 installments
01/01/2020 - $25.00
01/15/2020 - $25.00
01/29/2020 - $25.00
02/12/2020 - $25.00
