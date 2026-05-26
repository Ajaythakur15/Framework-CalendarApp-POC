Feature: Shift Management
  As a calendar administrator
  I want to manage shift overrides
  So that team shift timings can be viewed and updated

  @smoke @calendar @shiftmanagement
  Scenario: Open Shift Management tab
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    And I open the Shift Management tab
    Then the Shift Management view should be displayed

  @smoke @calendar @shiftmanagement
  Scenario: Open Add Shift popup
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    And I open the Shift Management tab
    And I click the Add Shift button
    Then the Add Shift popup should be displayed

  @regression @calendar @shiftmanagement
  Scenario: Create a new shift override
    Given I navigate to the calendar application
    When I select a team for calendar search
    And I click the Search button
    And I open the Shift Management tab
    And I click the Add Shift button
    And I enter shift override details
    And I save the shift override
    Then the Add Shift popup should close