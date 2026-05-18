Feature: Calendar Smoke
  As a user
  I want to open the calendar application
  So that I can verify it loads successfully

  @smoke
  Scenario: Open calendar home page
    Given I navigate to the calendar application
    Then the calendar home page should be displayed