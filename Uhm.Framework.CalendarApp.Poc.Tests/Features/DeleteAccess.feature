Feature: Delete Access
  As a calendar administrator
  I want to delete an existing team member access
  So that invalid or unnecessary access can be removed

  @regression @manageaccess
  Scenario: Delete existing access from Manage Access
    Given I navigate to the calendar application
    When I open the Manage Access module
    And I delete an existing access record
    Then the access record should be removed successfully