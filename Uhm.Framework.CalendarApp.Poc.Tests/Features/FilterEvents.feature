Feature: Filter Events
  As a calendar user
  I want to filter calendar events by category
  So that I can view only the relevant event type

  @regression @calendar
  Scenario: Filter calendar events by category
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    And I select an event filter category
    Then filtered calendar events should be displayed