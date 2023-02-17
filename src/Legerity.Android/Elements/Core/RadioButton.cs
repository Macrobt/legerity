namespace Legerity.Android.Elements.Core;

using Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

/// <summary>
/// Defines a <see cref="AndroidElement"/> wrapper for the core Android RadioButton control.
/// </summary>
public class RadioButton : Button
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RadioButton"/> class.
    /// </summary>
    /// <param name="element">
    /// The <see cref="AndroidElement"/> reference.
    /// </param>
    public RadioButton(AndroidElement element)
        : base(element)
    {
    }

    /// <summary>
    /// Gets a value indicating whether the radio button is selected.
    /// </summary>
    /// <exception cref="StaleElementReferenceException">Thrown when an element is no longer valid in the document DOM.</exception>
    public virtual bool IsSelected => this.GetCheckedState();

    /// <summary>
    /// Allows conversion of a <see cref="AndroidElement"/> to the <see cref="RadioButton"/> without direct casting.
    /// </summary>
    /// <param name="element">
    /// The <see cref="AndroidElement"/>.
    /// </param>
    /// <returns>
    /// The <see cref="RadioButton"/>.
    /// </returns>
    public static implicit operator RadioButton(AndroidElement element)
    {
        return new RadioButton(element);
    }

    /// <summary>
    /// Allows conversion of a <see cref="AppiumWebElement"/> to the <see cref="RadioButton"/> without direct casting.
    /// </summary>
    /// <param name="element">
    /// The <see cref="AppiumWebElement"/>.
    /// </param>
    /// <returns>
    /// The <see cref="RadioButton"/>.
    /// </returns>
    public static implicit operator RadioButton(AppiumWebElement element)
    {
        return new RadioButton(element as AndroidElement);
    }

    /// <summary>
    /// Allows conversion of a <see cref="RemoteWebElement"/> to the <see cref="RadioButton"/> without direct casting.
    /// </summary>
    /// <param name="element">
    /// The <see cref="RemoteWebElement"/>.
    /// </param>
    /// <returns>
    /// The <see cref="RadioButton"/>.
    /// </returns>
    public static implicit operator RadioButton(RemoteWebElement element)
    {
        return new RadioButton(element as AndroidElement);
    }
}