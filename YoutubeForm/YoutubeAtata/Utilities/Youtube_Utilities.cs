using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Atata;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using OpenQA.Selenium.Remote;

namespace YoutubeAtata.Utilities
{
    public class WebDriverSingleton
    {

        public static IWebDriver driver;

        public static IWebDriver getInstance()
        {
            if (driver == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("incognito");
                //var Instance = new ChromeDriver(options);
                driver = new ChromeDriver(options);
            }
            return driver;
        }

    }
    public class Youtube_Utilities
    {
        //IWebDriver driver = new ChromeDriver(Ops);
        IWebDriver driver = WebDriverSingleton.getInstance();
        //chromeOptions.AddArgument("incognito");
        //IWebDriver driver = new ChromeDriver(ChromeOptions.AddArgument("incognito"));
        public void OpenYoutubeURL(string URL)
        {
            //var chromeOptions = new ChromeOptions();
            ////chromeOptions.AddArguments("no-sandbox", "headless", "disable-gpu");
            //chromeOptions.AddArgument("incognito");
            //var driver = new ChromeDriver(chromeOptions);
            //var options = new ChromeOptions();
            //options.AddArgument("incognito");
            //var capabilities = options.ToCapabilities();
            //var driver = new RemoteWebDriver(new Uri(gridHubURL), capabilities);
            //driver.Manage().Window.Maximize();
            Console.WriteLine("Start go to URL");
            driver.Navigate().GoToUrl(URL);
        }
        public void SearchYoutubeFunction(string VideoName, string User)
        {
            string YoutubeSearchBoxXpath = "//input[@id='search']";
            //string YoutubeVideo = "//h3[@class='title-and-badge style-scope ytd-video-renderer']";
            InputTextIntoElement(YoutubeSearchBoxXpath, VideoName);
            ClickElement("//button[@id='search-icon-legacy']", 500);
            Console.WriteLine("Start find video");
            IList<IWebElement> videoresult = driver.FindElements(By.XPath("(//div[@id='title-wrapper']//a[contains(text(),'" + VideoName + "')])"));
            for (int i = 1; i <= videoresult.Count; i++)
            {
                string YoutubeVideoCount = GetElementText("(//h3[@class='title-and-badge style-scope ytd-video-renderer'])[" + i + "]");
                string text = GetElementText("(//div[@id='byline-inner-container'])[" + i + "]//a");
                if (YoutubeVideoCount.Contains(VideoName))
                {
                    if (text.Equals(User))
                    {
                        Console.WriteLine("Start to click into video");
                        string video = "(//div[@id='title-wrapper']//a[contains(text(),'" + VideoName + "')])[" + i + "]";
                        ClickElement(video, 500);
                        break;
                    }
                }
            }
        }
        public void CountDownTimer(int timer)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(timer))
            {
                driver.Navigate().Refresh();
            }
            s.Stop();
        }
        public void CloseWebDriver()
        {
            driver.Close();
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = " taskkill /im chromedriver.exe /f";
            process.StartInfo = startInfo;
            process.Start();
        }






        #region Utilities
        public void SelectDropdownList<T, TOwner>(Select<T, TOwner> control, string defaultValue, T option)
                where T : class
                where TOwner : PageObject<TOwner>
        {
            if (defaultValue != "") { control.Should.Equals(defaultValue); }
            control.Set(option).Should.Equals(option);
        }
        public void ScrollToBottom()
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }
        public void ScrollMiddle()
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(50, document.body.scrollHeight);");
        }
        public void ScrollTop()
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0);");
        }

        public bool IsElementPresent(string XPath)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            try
            {
                driver.FindElement(By.XPath(XPath));
                Console.WriteLine("Element {0} find by FindElement", XPath);
                return true;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Cannot find Element {0} find by FindElement", XPath);
                return false;
            }
        }
        public void HoverToElement(string Xpath)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }
        public void Compare(dynamic Actual, dynamic Expected)
        {
            try
            {
                if (Actual.Equals(Expected))
                {
                    Console.WriteLine(Actual + " Equals to " + Expected);
                }
                else
                {
                    string msg = Actual + " Not Equals to " + Expected;
                    throw new Exception(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is : {0}", e);
            }
        }
        public void CompareText(string Actual, string Expected)
        {
            try
            {
                if (Actual.Equals(Expected))
                {
                    Console.WriteLine(Actual + " Equals to " + Expected);
                }
                else
                {
                    string msg = Actual + " Not Equals to " + Expected;
                    throw new Exception(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is : {0}", e);
            }
        }
        public bool IsElementDisplayed(string Xpath)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            bool value = driver.FindElement(By.XPath(Xpath)).Displayed;
            try
            {
                if (value == true)
                {
                    Console.WriteLine("Element {0} is displayed ", Xpath);
                    return value = true;
                }
                else
                {
                    Console.WriteLine("Element {0} is not displayed ", Xpath);
                    return value = false;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element {0} not displays : ", Xpath);
            }
            return value;
        }
        public void WaitForPageLoad()
        {
            IWebElement page = null;
            //IWebDriver driver = AtataContext.Current.Driver;
            if (page != null)
            {
                var waitForCurrentPageToStale = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitForCurrentPageToStale.Until(ExpectedConditions.StalenessOf(page));
            }

            var waitForDocumentReady = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waitForDocumentReady.Until((wdriver) => (driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));

            page = driver.FindElement(By.TagName("html"));
        }
        public void DrawCanvas(string Xpath)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            IWebElement canvas = driver.FindElement(By.XPath(Xpath));
            string pencil = "//div[@class='viewer-tool viewer-tool-draw active']";
            ClickElement(pencil, 10);
            var size = canvas.Size;
            Actions builder = new Actions(driver);
            builder
            .ClickAndHold(canvas)
            .MoveByOffset(0, 200)
           .MoveByOffset(0, 200).
            MoveByOffset(-200, 0).
            MoveByOffset(-100, 0).Release(canvas).Build();
            builder.Perform();
        }
        public void ClickElement(string Xpath, int milisecond)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
            IWebElement element = driver.FindElement(By.XPath(Xpath));
            try
            {
                element.Click();
                Thread.Sleep(milisecond);
                Console.WriteLine("Click successful to {0}", element);
            }
            catch (ElementClickInterceptedException e)
            {
                Console.WriteLine("Element not click :{0}", e);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + element + " was not found in DOM "
                        + e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Element " + element + " was not clickable "
                        + e.StackTrace.ToString());
            }
        }
        public void RightClick(string Xpath)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
            try
            {
                Actions action = new Actions(driver).ContextClick(element);
                action.Build().Perform();
                Console.WriteLine("Successfully Right clicked on the element");
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element is not attached to the page document "
                        + e.StackTrace.ToString());
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + element + " was not found in DOM "
                        + e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Element " + element + " was not clickable "
                        + e.StackTrace.ToString());
            }
        }
        public string GetElementText(string Xpath)
        {
            string Text = "";
            //IWebDriver driver = AtataContext.Current.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                //IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                IWebElement element = driver.FindElement(By.XPath(Xpath));
                Text = element.Text;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Get Element Text with Exception : {0}", e);
            }
            return Text;
        }
        public void InputTextIntoElement(string Xpath, string InputText)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                element.SendKeys(InputText);
                Console.WriteLine("Input {0} into element {1} successfully", InputText, element);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Input text into Element: {0}", e);
            }
        }
        public void ClearTextInTextBox(string Xpath)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                element.Clear();
                Console.WriteLine("Clear text in {0} successfully", element);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Clear text in Textbox: {0}", e);
            }
        }

        public void SlideOnSlider(string SliderHandle, int position)
        {
            //IWebDriver driver = AtataContext.Current.Driver;
            try
            {
                IWebElement slider = driver.FindElement(By.XPath(SliderHandle));
                Actions move = new Actions(driver);
                new Actions(driver).DragAndDropToOffset(slider, position, 0).Build().Perform();
                Console.WriteLine("Slide to element {0} successful", SliderHandle);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Console.WriteLine("Slide exception is : {0}", e);
            }
        }
        #endregion
    }
}
