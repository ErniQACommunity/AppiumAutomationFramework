﻿using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Factory.SetUp
{
    /// <summary>
    /// Initialize the Appium Driver.
    /// </summary>
    public static class SetUpWebDriver
    {
        private const string AndroidApplicationPath = @"\Factory.SetUp\binaries\mylist.apk";

        /// <summary>
        /// Appium Driver.
        /// </summary>
        public static AppiumDriver<AndroidElement> AppiumDriver { get; private set; }

        /// <summary>
        /// See Wiki to set up the desired capabilities.
        /// <seealso cref="https://github.com/appium/appium/blob/master/docs/en/writing-running-appium/caps.md"/>
        /// </summary>
        public static AppiumDriver<AndroidElement> SetUpAppiumDriver()
        {
            if (AppiumDriver != null)
            {
                return AppiumDriver;
            }

            var appFullPath = Directory.GetParent(Directory.GetCurrentDirectory()) + AndroidApplicationPath;

            // Set up capabilities.
            // See Appium Capabilities wiki.
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("platformVersion", "7.0");
            capabilities.SetCapability("fullReset", "True");
            capabilities.SetCapability("app", appFullPath);

            // To see the device name with the cmd console check adb devices -l
            capabilities.SetCapability("deviceName", "generic_x86");

            AppiumDriver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);

            return AppiumDriver;
        }

        /// <summary>
        /// See Wiki to set up the desired capabilities.
        /// <seealso cref="https://github.com/appium/appium/blob/master/docs/en/writing-running-appium/caps.md"/>
        /// </summary>
        public static AppiumDriver<AndroidElement> SetUpAppiumLocalDriver()
        {
            if (AppiumDriver != null)
            {
                return AppiumDriver;
            }

            var appFullPath = Directory.GetParent(Directory.GetCurrentDirectory()) + AndroidApplicationPath;

            // Set up capabilities.
            // See Appium Capabilities wiki.
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("platformVersion", "6.0.1");
            capabilities.SetCapability("fullReset", "True");
            capabilities.SetCapability("app", appFullPath);

            // To see the device name with the cmd console check adb devices -l
            capabilities.SetCapability("deviceName", "trlte");

            AppiumDriver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities, TimeSpan.FromSeconds(600));

            return AppiumDriver;
        }

        /// <summary>
        /// Closes the android driver.
        /// </summary>
        public static void CloseAndroidDriver()
        {
            AppiumDriver?.Dispose();
            AppiumDriver = null;
        }

        /// <summary>
        /// Makes the screenshot.
        /// </summary>
        /// <param name="scenario">The scenario.</param>
        public static void MakeScreenshot(string scenario)
        {
            var screenshot = ((ITakesScreenshot) AppiumDriver).GetScreenshot();
            var dateTime = $"{DateTime.Now.ToString("d-M-yyyy HH-mm-ss", CultureInfo.InvariantCulture)}_{scenario}.jpeg";
            screenshot.SaveAsFile(dateTime, ImageFormat.Jpeg);
        }
    }
}
