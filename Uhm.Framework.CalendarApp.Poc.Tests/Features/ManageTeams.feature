Feature: Manage Teams
  As a calendar administrator
  I want to access the Manage Teams module
  So that I can manage team records in the calendar application

  @smoke @manageteams
  Scenario: Open Add Team popup from Manage Teams
    Given I navigate to the calendar application
    When I open the Manage Teams module
    And I click the Add Team button
    Then the Add Team popup should be displayed