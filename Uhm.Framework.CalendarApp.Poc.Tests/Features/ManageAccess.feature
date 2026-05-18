Feature: Manage Access
  As a calendar administrator
  I want to access the Manage Access module
  So that I can manage user access for teams

  @smoke @manageaccess
  Scenario: Open Add Access popup from Manage Access
    Given I navigate to the calendar application
    When I open the Manage Access module
    And I click the Add Access button
    Then the Add Access popup should be displayed