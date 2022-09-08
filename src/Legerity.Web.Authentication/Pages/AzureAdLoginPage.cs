namespace Legerity.Web.Authentication.Pages
{
    using System;
    using Legerity.Extensions;
    using Legerity.Pages;
    using Legerity.Web.Elements.Core;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// Defines a page object for the Azure Active Directory login page.
    /// </summary>
    public class AzureAdLoginPage : BasePage
    {
        /// <summary>
        /// The expected title of the Azure AD login page.
        /// </summary>
        public const string Title = "Sign in to your account";

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAdLoginPage"/> class using the <see cref="AppManager.App"/> instance that verifies the page has loaded within 2 seconds.
        /// </summary>
        public AzureAdLoginPage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAdLoginPage"/> class using a <see cref="RemoteWebDriver"/> instance that verifies the page has loaded within 2 seconds.
        /// </summary>
        /// <param name="app">
        /// The instance of the started application driver that will be used to drive the page interaction.
        /// </param>
        /// <exception cref="T:Legerity.Exceptions.DriverNotInitializedException">Thrown if AppManager.StartApp() has not been called.</exception>
        /// <exception cref="T:Legerity.Exceptions.PageNotShownException">Thrown if the page is not shown in 2 seconds.</exception>
        public AzureAdLoginPage(RemoteWebDriver app)
            : base(app)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAdLoginPage"/> class using the <see cref="AppManager.App"/> instance that verifies the page has loaded within the given timeout.
        /// </summary>
        /// <param name="traitTimeout">
        /// The amount of time the driver should wait when searching for the <see cref="Trait"/> if it is not immediately present.
        /// </param>
        /// <exception cref="T:Legerity.Exceptions.DriverNotInitializedException">Thrown if AppManager.StartApp() has not been called.</exception>
        /// <exception cref="T:Legerity.Exceptions.PageNotShownException">Thrown if the page is not shown in the given timeout.</exception>
        public AzureAdLoginPage(TimeSpan? traitTimeout)
            : base(traitTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAdLoginPage"/> class using a <see cref="RemoteWebDriver"/> instance that verifies the page has loaded within the given timeout.
        /// </summary>
        /// <param name="app">
        /// The instance of the started application driver that will be used to drive the page interaction.
        /// </param>
        /// <param name="traitTimeout">
        /// The amount of time the driver should wait when searching for the <see cref="Trait"/> if it is not immediately present.
        /// </param>
        /// <exception cref="T:Legerity.Exceptions.DriverNotInitializedException">Thrown if AppManager.StartApp() has not been called.</exception>
        /// <exception cref="T:Legerity.Exceptions.PageNotShownException">Thrown if the page is not shown in the given timeout.</exception>
        public AzureAdLoginPage(RemoteWebDriver app, TimeSpan? traitTimeout)
            : base(app, traitTimeout)
        {
        }

        /// <summary>
        /// Gets the input element for providing an email address.
        /// </summary>
        public virtual TextInput EmailInput => this.App.FindWebElement(WebByExtras.InputType("email"));

        /// <summary>
        /// Gets the input element for providing a password.
        /// </summary>
        public virtual TextInput PasswordInput => this.App.FindWebElement(WebByExtras.InputType("password"));

        /// <summary>
        /// Gets the button element for continuing the sign-in flow through the Azure AD login UI.
        /// </summary>
        public virtual Button SignInButton => this.App.FindWebElement(By.Id("idSIButton9"));

        /// <summary>
        /// Gets the optional "Use Password" button that appears when password-less authentication is enabled.
        /// </summary>
        public virtual Button UsePasswordButton => this.App.FindWebElement(By.Id("idA_PWD_SwitchToPassword"));

        /// <summary>
        /// Gets a given trait of the page to verify that the page is in view.
        /// </summary>
        protected override By Trait => By.ClassName("login-paginated-page");

        /// <summary>
        /// Login an Azure Active Directory user by email and password.
        /// </summary>
        /// <param name="email">The email address to authenticate with.</param>
        /// <param name="password">The password associated with the email address to authenticate with.</param>
        /// <returns>The <see cref="AzureAdLoginPage"/> instance.</returns>
        public AzureAdLoginPage Login(string email, string password)
        {
            (bool hasEmailInput, AzureAdLoginPage _) =
                this.TryWaitUntil(page => page.EmailInput.IsVisible, this.WaitTimeout);
            if (!hasEmailInput)
            {
                // Cannot login as email address input not shown (possibly logged in already?)
                return this;
            }

            this.EmailInput.SetText(email);

            // Azure AD login uses a 2-step process for email and password.
            this.SignInButton.Click();

            // Check to ensure that the user is not authenticating with password-less authentication.
            (bool isNotPasswordLogin, AzureAdLoginPage _) =
                this.TryWaitUntil(page => page.UsePasswordButton.IsVisible, this.WaitTimeout);
            if (isNotPasswordLogin)
            {
                this.UsePasswordButton.Click();
                this.TryWaitUntil(page => page.PasswordInput.IsVisible, this.WaitTimeout);
            }

            this.PasswordInput.SetText(password);
            this.SignInButton.Click();

            return this;
        }
    }
}