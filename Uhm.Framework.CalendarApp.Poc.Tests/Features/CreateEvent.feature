Feature: Create Event
  As a calendar administrator
  I want to create a new event
  So that it appears in the team calendar

  @regression @calendar
  Scenario: Create a new calendar event
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    And I click the Add Event button
    And I enter event details
    And I save the event
    Then the Add Event popup should close