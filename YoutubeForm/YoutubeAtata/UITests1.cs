using System;
using Atata;
using NUnit.Framework;
using YoutubeAtata.Utilities;

namespace YoutubeAtata
{
    public class UITests1 : UITestFixture
    {
        Youtube_Utilities t = new Youtube_Utilities();
        public void YoutubeTest(string URL,string VideoName,string User)
        {
            Console.WriteLine("Start Youtube Test");
            t.OpenYoutubeURL(URL);
            t.SearchYoutubeFunction(VideoName, User);
            t.CountDownTimer(300000);
            //t.CloseWebDriver();
        }
    }
}
