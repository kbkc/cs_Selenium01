//Программа создает почтовые ящики на хостинге Hetzner.
//Список ящиков - файл mails.csv
//Настройки хостинга - файл settings.csv
//(имена файлов прописаны в datas_m.cs)
//Работает с браузером Firefox.
//Используется selenium driver и geckodriver.exe


using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace cs_Selenium01
{
    class Program
    {
        static void Main(string[] args)
        {
          // datas_m.mails.ForEach(delegate List<string>)

            foreach (List<string> ss in datas_m.mails)
            {
                Console.WriteLine("{1}  \t\t\t pass: {0}",ss[1],ss[2]);
            }
            foreach (List<string> ss in datas_m.settings)
            {
                 Console.WriteLine(String.Join(";",ss.ToArray()));
            }

            Console.WriteLine("\nThese addresses were added. Proceed (Y) ?");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                run_in_browser();
                Console.WriteLine("\nJob's done.\nYou can close the browser.\nKey to bye.");
            } 
            Console.WriteLine("\nBye. Press key.");
            Console.ReadKey();
        }

        static void run_in_browser()
        {
            IWebDriver browser = new FirefoxDriver();
            //Firefox's proxy driver executable is in a folder already
            //  on the host system's PATH environment variable.
            browser.Navigate().GoToUrl(datas_m.settings[0][0]);

            // login to site
            browser.FindElement(By.Id("login_user_inputbox")).SendKeys(datas_m.settings[0][1]);
            browser.FindElement(By.Id("login_pass_inputbox")).SendKeys(datas_m.settings[0][2]);
            browser.FindElement(By.XPath("//input[@type='submit']")).Click();

            // wait, while page opens
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

           // browser.FindElement(By.XPath("//html//body//div[4]//form//div//div//div[1]//dl//dt[2]//a")).Click();
           // IJavaScriptExecutor js = browser as IJavaScriptExecutor;

            string site_url; // = datas_m.settings[0][0];

            // open site frame
            // 1stcollection.com   "//html//body//div[4]//form//div//div//div[1]//dl//dt[2]//a"
            // reformnails.com     @"/html/body/div[4]/form/div/div/div[1]/dl/dt[9]/a"
            site_url = browser.FindElement(By.XPath(@"/html/body/div[4]/form/div/div/div[1]/dl/dt[9]/a")).GetAttribute("href");
            browser.Navigate().GoToUrl(site_url);


            // wait, while page opens and expatd mail area
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            browser.FindElement(By.XPath("//a[contains(text(), 'Email')]")).Click();

            // goto mailbox list
            site_url = browser.FindElement(By.XPath(@"/html/body/div[3]/div[2]/div[4]/dl/dd[1]/a")).GetAttribute("href");
            browser.Navigate().GoToUrl(site_url);

            // create mail boxes cycle
            foreach (List<string> s in datas_m.mails)
            {
                // new mail click
                //  browser.FindElement(By.XPath(@"/html/body/div[4]/div[1]/div[1]/a[1]")).Click();
                browser.FindElement(By.XPath("//a[contains(text(), 'New mailbox')]")).Click();

                // create new mailbox
                browser.FindElement(By.Name("localaddress")).SendKeys(s[0]);
                browser.FindElement(By.Name("password")).SendKeys(s[1]);
                browser.FindElement(By.Name("password_repeat")).SendKeys(s[1]);
                browser.FindElement(By.XPath("//input[@name='save']")).Click();

                Console.WriteLine("\nadded box:\t{0}@reformnails.com ", s[0]);

                // goto mailbox list
                site_url = browser.FindElement(By.XPath(@"/html/body/div[3]/div[2]/div[4]/dl/dd[1]/a")).GetAttribute("href");
                browser.Navigate().GoToUrl(site_url);
            }
            //  browser.Close();
        }
    }
}
