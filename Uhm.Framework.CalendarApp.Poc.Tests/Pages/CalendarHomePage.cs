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
        private readonly By _teamDropdown = By.XPath("//select");
        private readonly By _searchButton = By.XPath("//button[contains(.,'SEARCH')]");
        private readonly By _calendarHeader = By.XPath("//*[contains(text(),'Events') or contains(text(),'Shift Management') or contains(text(),'May')]");
        private readonly By _addEventButton = By.XPath("//button[contains(.,'ADD EVENT')]");
        private readonly By _addEventPopupHeader = By.XPath("//*[contains(text(),'Add New Event')]");
        private readonly By _categoryDropdown = By.XPath("//label[contains(.,'CATEGORY')]/following::select[1]");
        private readonly By _titleInput = By.XPath("//label[contains(.,'TITLE')]/following::input[1]");
        private readonly By _startDateInput = By.XPath("//label[contains(.,'START DATE')]/following::input[1]");
        private readonly By _endDateInput = By.XPath("//label[contains(.,'END DATE')]/following::input[1]");
        private readonly By _remarkInput = By.XPath("//label[contains(.,'REMARK')]/following::textarea[1]");
        private readonly By _saveButton = By.XPath("//button[contains(.,'Save')]");
        private readonly By _popupCloseMarker = By.XPath("//*[contains(text(),'Add New Event')]");
        private readonly By _filterDropdown = By.XPath("//input[contains(@placeholder,'Filter')]/following::*[contains(@class,'select') or self::select][1] | //*[contains(text(),'Filter')]/following::*[contains(@class,'select') or self::select][1]");
        private readonly By _calendarContentArea = By.XPath("//*[contains(text(),'Events') or contains(text(),'Shift Management') or contains(text(),'May')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarHomePage"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="waitSeconds">The explicit wait timeout in seconds.</param>
        public CalendarHomePage(IWebDriver driver, int waitSeconds) : base(driver, waitSeconds)
        {
        }

        /// <summary>
        /// Verifies whether the Calendar home page is loaded successfully.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the page title is not null or empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLoaded()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        /// <summary>
        /// Selects a team from the team dropdown for calendar search.
        /// </summary>
        /// <param name="teamName">The visible team name to select.</param>
        public void SelectTeam(string teamName)
        {
            var dropdown = Wait.Until(d => d.FindElement(_teamDropdown));
            dropdown.Click();

            var option = Wait.Until(d =>
                d.FindElement(By.XPath($"//option[contains(text(),'{teamName}')]")));
            option.Click();
        }

        /// <summary>
        /// Clicks the Search button to load the selected team's calendar.
        /// </summary>
        public void ClickSearch()
        {
            Wait.Until(d => d.FindElement(_searchButton)).Click();
        }

        /// <summary>
        /// Selects an event filter category from the filter dropdown.
        /// </summary>
        /// <param name="categoryName">The visible category name to select.</param>
        public void SelectFilterCategory(string categoryName)
        {
            var dropdown = Wait.Until(d => d.FindElement(_filterDropdown));
            dropdown.Click();

            var option = Wait.Until(d =>
                d.FindElement(By.XPath($"//*[contains(@class,'option') or @role='option' or self::option][contains(.,'{categoryName}')]")));
            option.Click();
        }

        /// <summary>
        /// Verifies whether filtered calendar content is displayed after applying a category filter.
        /// </summary>
        /// <returns>
        /// <c>true</c> if filtered calendar content remains visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFilteredCalendarDisplayed()
        {
            return Wait.Until(d => d.FindElement(_calendarContentArea)).Displayed;
        }

        /// <summary>
        /// Verifies whether the team calendar is displayed after search.
        /// </summary>
        /// <returns>
        /// <c>true</c> if calendar content is visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCalendarDisplayed()
        {
            return Wait.Until(d => d.FindElement(_calendarHeader)).Displayed;
        }

        /// <summary>
        /// Clicks the Add Event button on the Calendar page.
        /// </summary>
        public void ClickAddEvent()
        {
            Wait.Until(d => d.FindElement(_addEventButton)).Click();
        }

        /// <summary>
        /// Verifies whether the Add Event popup is displayed.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the Add Event popup is visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddEventPopupDisplayed()
        {
            return Wait.Until(d => d.FindElement(_addEventPopupHeader)).Displayed;
        }

        /// <summary>
        /// Opens the event category dropdown inside the Add Event popup.
        /// </summary>
        public void OpenCategoryDropdown()
        {
            Wait.Until(d => d.FindElement(_categoryDropdown)).Click();
        }

        /// <summary>
        /// Gets the available event category values from the category dropdown.
        /// </summary>
        /// <returns>
        /// A collection of category option texts.
        /// </returns>
        public IReadOnlyCollection<string> GetAvailableCategories()
        {
            var options = Wait.Until(d => d.FindElements(By.XPath("//label[contains(.,'CATEGORY')]/following::select[1]/option")));
            return options.Select(o => o.Text.Trim())
                          .Where(text => !string.IsNullOrWhiteSpace(text))
                          .ToList();
        }

        /// <summary>
        /// Selects a specific event category from the category dropdown.
        /// </summary>
        /// <param name="categoryName">The visible category text to select.</param>
        public void SelectCategory(string categoryName)
        {
            var dropdown = new SelectElement(Wait.Until(d => d.FindElement(_categoryDropdown)));
            dropdown.SelectByText(categoryName);
        }

        /// <summary>
        /// Enters event details into the Add Event popup form.
        /// </summary>
        /// <param name="title">The event title.</param>
        /// <param name="startDateTime">The event start date and time.</param>
        /// <param name="endDateTime">The event end date and time.</param>
        /// <param name="remark">The event remark text.</param>
        public void EnterEventDetails(string title, string startDateTime, string endDateTime, string remark)
        {
            var titleElement = Wait.Until(d => d.FindElement(_titleInput));
            titleElement.Clear();
            titleElement.SendKeys(title);

            var startElement = Wait.Until(d => d.FindElement(_startDateInput));
            startElement.Clear();
            startElement.SendKeys(startDateTime);

            var endElement = Wait.Until(d => d.FindElement(_endDateInput));
            endElement.Clear();
            endElement.SendKeys(endDateTime);

            var remarkElement = Wait.Until(d => d.FindElement(_remarkInput));
            remarkElement.Clear();
            remarkElement.SendKeys(remark);
        }

        /// <summary>
        /// Clicks the Save button on the Add Event popup.
        /// </summary>
        public void ClickSaveEvent()
        {
            Wait.Until(d => d.FindElement(_saveButton)).Click();
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
                Wait.Until(_ => Driver.FindElements(_popupCloseMarker).Count == 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}