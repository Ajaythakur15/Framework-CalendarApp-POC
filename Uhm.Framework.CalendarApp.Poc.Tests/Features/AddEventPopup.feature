Feature: Add Event Popup
  As a calendar administrator
  I want to open the Add Event popup
  So that I can create a new calendar event

  @smoke @calendar
  Scenario: Open Add Event popup
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    And I click the Add Event button
    Then the Add Event popup should be displayed