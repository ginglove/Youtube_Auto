using Atata;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System;
using OpenQA.Selenium;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using OpenQA.Selenium.Interactions;
using YoutubeAtata.Configs;
namespace YoutubeAtata
{
    [TestFixture]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp()
        {
            string filePath = "Configs/Atata";
            // Find information about AtataContext set-up on https://atata-framework.github.io/getting-started/#set-up.
            AtataContext.Configure().
                ApplyJsonConfig<AppConfig>(filePath)
                //UseChrome().
                //    WithArguments("start-maximized").
                //UseBaseUrl("SITE_URL").
                .AddNUnitTestContextLogging()
                .AddScreenshotFileSaving()
                .WithFolderPath(() =>
                {
                    string screenshotFileOutput = AppConfig.Current.ScreenShotFileOutput;
                    string folderPath =
                        $@"Outputs\{AtataContext.BuildStart:yyyy-MM-dd HH_mm_ss}\{AtataContext.Current.TestName}";

                    folderPath = Path.Combine(screenshotFileOutput, folderPath);

                    return folderPath;
                })
                .WithFileName(screenshotInfo =>
                {
                    string fileName =
                        $"{screenshotInfo.Number:D2}-{AtataContext.Current.TestName}{screenshotInfo.Title?.Prepend("-")}";

                    return fileName;
                })
                .UseChrome()
                .WithArguments(AppConfig.Current.Drivers.First(d => d.Type == "chrome").Options.Arguments)
                .WithArguments("--disable-notifications", "--disable-popup-blocking", "--disable-extensions")
                .WithFixOfCommandExecutionDelay()
                .WithLocalDriverPath()
                .Build();
        }

        [TearDown]
        public void TearDown()
        {
            if (AtataContext.Current != null)
            {
                AtataContext.Current.CleanUp();
            }

        }
    }
}
