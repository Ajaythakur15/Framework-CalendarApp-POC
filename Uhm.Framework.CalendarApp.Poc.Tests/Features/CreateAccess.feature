Feature: Create Access
  As a calendar administrator
  I want to add access for a team member
  So that the team member can use the calendar module

  @regression @manageaccess
  Scenario: Create new access from Manage Access
    Given I navigate to the calendar application
    When I open the Manage Access module
    And I click the Add Access button
    And I enter access details
    And I save the access
    Then the access popup should close