Feature: Event Categories
  As a calendar administrator
  I want to view available event categories
  So that I can create different types of calendar events

  @regression @calendar
  Scenario: View available event categories in Add Event popup
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    And I click the Add Event button
    And I open the event category dropdown
    Then the available event categories should be displayed