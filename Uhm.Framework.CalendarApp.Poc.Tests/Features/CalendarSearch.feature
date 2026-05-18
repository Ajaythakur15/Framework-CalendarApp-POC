Feature: Calendar Search
  As a calendar user
  I want to search the calendar for a selected team
  So that I can view team-wise calendar events

  @smoke @calendar
  Scenario: Search calendar by selected team
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    Then the team calendar should be displayed