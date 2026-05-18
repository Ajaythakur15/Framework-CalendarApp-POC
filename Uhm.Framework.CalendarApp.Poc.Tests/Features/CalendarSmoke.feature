Feature: Calendar Smoke
  As a user of the calendar application
  I want to open the application successfully
  So that I can confirm the Calendar module is available

  @smoke @calendar
  Scenario: Open calendar home page
    Given I navigate to the calendar application
    Then the calendar home page should be displayed