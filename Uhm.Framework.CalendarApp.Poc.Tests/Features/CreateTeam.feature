Feature: Create Team
  As a calendar administrator
  I want to add a new team
  So that the team can be managed in the calendar app

  @regression @manageteams
  Scenario: Create a new team from Manage Teams
    Given I navigate to the calendar application
    When I open the Manage Teams module
    And I click the Add Team button
    And I enter team details
    And I save the team
    Then the team popup should close