using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages
{
    /// <summary>
    /// Page object class that represents the Calendar home page
    /// and provides actions and validations related to calendar search,
    /// Add Event popup behavior, category selection, and event creation.
    /// </summary>
    public class CalendarHomePage : BasePage
    {
        // Calendar Home Page Controls

        private readonly By _calendarHeader =
            By.XPath("//*[contains(text(),'CALENDAR')]");

        private readonly By _teamDropdown =
            By.XPath("//select");

        private readonly By _searchButton =
            By.XPath("//button[contains(text(),'SEARCH')]");

        // Calendar Results / Add Event Controls

        private readonly By _addEventButton =
            By.XPath("//button[contains(text(),'ADD EVENT')]");

        private readonly By _addEventPopupHeader =
            By.XPath("//*[contains(text(),'Add New Event')]");

        // Add Event Form Controls

        private readonly By _categoryDropdown =
            By.XPath("//label[contains(.,'CATEGORY')]/following::select[1]");

        private readonly By _titleInput =
            By.XPath("//label[contains(.,'TITLE')]/following::input[1]");

        private readonly By _startDateInput =
            By.XPath("//label[contains(.,'START DATE')]/following::input[1]");

        private readonly By _endDateInput =
            By.XPath("//label[contains(.,'END DATE')]/following::input[1]");

        private readonly By _remarkInput =
            By.XPath("//label[contains(.,'REMARK')]/following::textarea[1]");

        private readonly By _saveButton =
            By.XPath("//button[contains(text(),'Save') or contains(text(),'SAVE')]");

        private readonly By _eventValidationMessage =
            By.XPath("//*[contains(text(),'required') or contains(text(),'invalid') or contains(text(),'unavailable') or contains(text(),'already') or contains(text(),'error') or contains(text(),'Error')]");

        private readonly By _popupCloseMarker =
            By.XPath("//*[contains(text(),'Add New Event')]");

        // Filter Controls

        private readonly By _filterDropdown =
            By.XPath("//div[contains(@class,'filter-dropdown')]//div[contains(@class,'dropdown-container')]");

        private readonly By _calendarContentArea =
            By.XPath("//*[contains(text(),'Events')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarHomePage"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="waitSeconds">The explicit wait timeout in seconds.</param>
        public CalendarHomePage(IWebDriver driver, int waitSeconds)
            : base(driver, waitSeconds)
        {
        }

        /// <summary>
        /// Verifies whether the Calendar home page is loaded successfully.
        /// </summary>
        /// <returns>
        /// <c>true</c> if Calendar page controls are displayed; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLoaded()
        {
            try
            {
                return IsElementDisplayed(_calendarHeader)
                       && IsElementDisplayed(_teamDropdown)
                       && IsElementDisplayed(_searchButton);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Selects a team from the Team dropdown.
        /// </summary>
        /// <param name="teamName">The visible team name to select.</param>
        public void SelectTeam(string teamName)
        {
            var dropdownElement = WaitForElement(_teamDropdown);
            var dropdown = new SelectElement(dropdownElement);
            dropdown.SelectByText(teamName);
        }

        /// <summary>
        /// Clicks the Search button to load the selected team's calendar.
        /// </summary>
        public void ClickSearch()
        {
            var searchButton = WaitForElement(_searchButton);
            searchButton.Click();

            Wait.Until(d =>
                ((IJavaScriptExecutor)d)
                .ExecuteScript("return document.readyState")
                ?.ToString() == "complete");

            Wait.Until(d => d.FindElement(_addEventButton).Displayed);
        }

        /// <summary>
        /// Selects an event filter category from the filter dropdown.
        /// </summary>
        /// <param name="categoryName">The visible category name to select.</param>
        public void SelectFilterCategory(string categoryName)
        {
            var dropdown = WaitForElement(_filterDropdown);
            JsClick(dropdown);

            var option = Wait.Until(d =>
                d.FindElement(By.XPath(
                    $"//div[contains(@class,'filter-dropdown')]//label[normalize-space(.)='{categoryName}'] | " +
                    $"//div[contains(@class,'filter-dropdown')]//span[normalize-space(.)='{categoryName}'] | " +
                    $"//div[contains(@class,'filter-dropdown')]//div[normalize-space(.)='{categoryName}']")));

            JsClick(option);
        }

        /// <summary>
        /// Verifies whether filtered calendar content is displayed after applying a category filter.
        /// </summary>
        /// <returns>
        /// <c>true</c> if filtered calendar content remains visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFilteredCalendarDisplayed()
        {
            return IsElementDisplayed(_calendarContentArea);
        }

        /// <summary>
        /// Verifies whether the team calendar is displayed after search.
        /// </summary>
        /// <returns>
        /// <c>true</c> if Add Event button is visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCalendarDisplayed()
        {
            return IsElementDisplayed(_addEventButton);
        }

        /// <summary>
        /// Clicks the Add Event button on the Calendar page.
        /// </summary>
        public void ClickAddEvent()
        {
            var button = WaitForElement(_addEventButton);
            JsClick(button);
        }

        /// <summary>
        /// Verifies whether the Add Event popup is displayed.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the Add Event popup is visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddEventPopupDisplayed()
        {
            return IsElementDisplayed(_addEventPopupHeader);
        }

        /// <summary>
        /// Opens the event category dropdown inside the Add Event popup.
        /// </summary>
        public void OpenCategoryDropdown()
        {
            JsClick(WaitForElement(_categoryDropdown));
        }

        /// <summary>
        /// Gets the available event category values from the category dropdown.
        /// </summary>
        /// <returns>
        /// A collection of category option texts.
        /// </returns>
        public IReadOnlyCollection<string> GetAvailableCategories()
        {
            var options = Wait.Until(d =>
                d.FindElements(By.XPath("//label[contains(.,'CATEGORY')]/following::select[1]/option")));

            return options
                .Select(o => o.Text.Trim())
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();
        }

        /// <summary>
        /// Selects a specific event category from the category dropdown.
        /// </summary>
        /// <param name="categoryName">The visible category text to select.</param>
        public void SelectCategory(string categoryName)
        {
            var dropdown = new SelectElement(WaitForElement(_categoryDropdown));
            dropdown.SelectByText(categoryName);
        }

        /// <summary>
        /// Enters event details into the Add Event popup form.
        /// </summary>
        /// <param name="title">The event title.</param>
        /// <param name="startDateTime">The event start date and time.</param>
        /// <param name="endDateTime">The event end date and time.</param>
        /// <param name="remark">The event remark text.</param>
        public void EnterEventDetails(
            string title,
            string startDateTime,
            string endDateTime,
            string remark)
        {
            var titleElement = WaitForElement(_titleInput);
            titleElement.Clear();
            titleElement.SendKeys(title);

            var startElement = WaitForElement(_startDateInput);
            startElement.Clear();
            startElement.SendKeys(startDateTime);

            var endElement = WaitForElement(_endDateInput);
            endElement.Clear();
            endElement.SendKeys(endDateTime);

            var remarkElement = WaitForElement(_remarkInput);
            remarkElement.Clear();
            remarkElement.SendKeys(remark);
        }

        /// <summary>
        /// Clicks the Save button on the Add Event popup.
        /// </summary>
        public void ClickSaveEvent()
        {
            JsClick(WaitForElement(_saveButton));
        }

        /// <summary>
        /// Gets the event validation message shown after save attempt.
        /// </summary>
        /// <returns>
        /// The validation message text if present; otherwise empty string.
        /// </returns>
        public string GetEventValidationMessage()
        {
            return GetElementText(_eventValidationMessage);
        }

        /// <summary>
        /// Gets any save error or validation message shown in the Add Event popup.
        /// </summary>
        /// <returns>
        /// The error/validation text if present; otherwise empty string.
        /// </returns>
        public string GetEventSaveErrorMessage()
        {
            return GetElementText(_eventValidationMessage);
        }

        /// <summary>
        /// Verifies whether the Add Event popup is closed after save.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the popup is no longer visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddEventPopupClosed()
        {
            try
            {
                Wait.Until(_ =>
                {
                    var popups = Driver.FindElements(_popupCloseMarker);
                    return popups.Count == 0 || popups.All(p => !p.Displayed);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}