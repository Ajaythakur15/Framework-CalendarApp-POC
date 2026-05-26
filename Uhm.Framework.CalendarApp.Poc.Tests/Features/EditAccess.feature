Feature: Edit Access
  As a calendar administrator
  I want to edit an existing team member access
  So that access details can be updated when needed

  @regression @manageaccess
  Scenario: Edit existing access from Manage Access
    Given I navigate to the calendar application
    When I open the Manage Access module
    And I edit an existing access record
    And I update the access details
    And I save the updated access
    Then the access record should be updated successfully