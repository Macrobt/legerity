namespace Legerity.Windows.Elements.WinUI;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Legerity.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

/// <summary>
/// Defines a <see cref="WindowsElement"/> wrapper for the WinUI UWP MenuBar control.
/// </summary>
public class MenuBar : WindowsElementWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuBar"/> class.
    /// </summary>
    /// <param name="element">
    /// The <see cref="WindowsElement"/> reference.
    /// </param>
    public MenuBar(WindowsElement element)
        : base(element)
    {
    }

    /// <summary>
    /// Gets the UI components associated with the menu items.
    /// </summary>
    public virtual IEnumerable<MenuBarItem> MenuItems =>
        this.Element.FindElements(By.ClassName("Microsoft.UI.Xaml.Controls.MenuBarItem"))
            .Select(element => new MenuBarItem(this, element as WindowsElement));

    /// <summary>
    /// Allows conversion of a <see cref="WindowsElement"/> to the <see cref="MenuBar"/> without direct casting.
    /// </summary>
    /// <param name="element">
    /// The <see cref="WindowsElement"/>.
    /// </param>
    /// <returns>
    /// The <see cref="MenuBar"/>.
    /// </returns>
    public static implicit operator MenuBar(WindowsElement element)
    {
        return new MenuBar(element);
    }

    /// <summary>
    /// Allows conversion of a <see cref="AppiumWebElement"/> to the <see cref="MenuBar"/> without direct casting.
    /// </summary>
    /// <param name="element">
    /// The <see cref="AppiumWebElement"/>.
    /// </param>
    /// <returns>
    /// The <see cref="MenuBar"/>.
    /// </returns>
    public static implicit operator MenuBar(AppiumWebElement element)
    {
        return new MenuBar(element as WindowsElement);
    }

    /// <summary>
    /// Allows conversion of a <see cref="RemoteWebElement"/> to the <see cref="MenuBar"/> without direct casting.
    /// </summary>
    /// <param name="element">
    /// The <see cref="RemoteWebElement"/>.
    /// </param>
    /// <returns>
    /// The <see cref="MenuBar"/>.
    /// </returns>
    public static implicit operator MenuBar(RemoteWebElement element)
    {
        return new MenuBar(element as WindowsElement);
    }

    /// <summary>
    /// Clicks on a child menu option with the specified item name.
    /// </summary>
    /// <param name="name">
    /// The name of the item to click.
    /// </param>
    /// <returns>
    /// The clicked <see cref="MenuBarItem"/>.
    /// </returns>
    /// <exception cref="NoSuchElementException">Thrown when no element matches the expected locator.</exception>
    /// <exception cref="InvalidElementStateException">Thrown when an element is not enabled.</exception>
    /// <exception cref="ElementNotVisibleException">Thrown when an element is not visible.</exception>
    /// <exception cref="StaleElementReferenceException">Thrown when an element is no longer valid in the document DOM.</exception>
    public virtual MenuBarItem ClickOption(string name)
    {
        MenuBarItem item = this.MenuItems.FirstOrDefault(
            element => element.GetName()
                .Equals(name, StringComparison.CurrentCultureIgnoreCase));

        if (item == null)
        {
            throw new NoSuchElementException($"Unable to find element by {name}");
        }

        item.Click();
        return item;
    }

    /// <summary>
    /// Clicks on a child menu option with the specified partial item name.
    /// </summary>
    /// <param name="name">
    /// The partial name of the item to click.
    /// </param>
    /// <returns>
    /// The clicked <see cref="MenuBarItem"/>.
    /// </returns>
    /// <exception cref="NoSuchElementException">Thrown when no element matches the expected locator.</exception>
    /// <exception cref="InvalidElementStateException">Thrown when an element is not enabled.</exception>
    /// <exception cref="ElementNotVisibleException">Thrown when an element is not visible.</exception>
    /// <exception cref="StaleElementReferenceException">Thrown when an element is no longer valid in the document DOM.</exception>
    public virtual MenuBarItem ClickOptionByPartialName(string name)
    {
        MenuBarItem item = this.MenuItems.FirstOrDefault(
            element => element.GetName()
                .Contains(name, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase));

        if (item == null)
        {
            throw new NoSuchElementException($"Unable to find element by {name}");
        }

        item.Click();
        return item;
    }
}