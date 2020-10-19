using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace AgileWay
{
    //Automation - Regression Scenarios 1 - Flight Scenarios
    //Test Case 1: Book Return flight
    //Test Case 2: Book One way flight

    [TestClass]
    public class TravelAgileWayAutomation
    {
        IWebDriver driver;

        //Regression Test Case 1: Login to Travel site        

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\C:\\Users\\Esheku\\Desktop\\CHROME\\InputData.csv", "InputData#csv", DataAccessMethod.Sequential)]
        public void SignInTravelAgileWay()
        {
            //Data Output


            using (StreamWriter writer = new StreamWriter("C:\\Users\\Esheku\\Desktop\\CHROME\\DataOutput.txt"))
            {
                writer.WriteLine("Test Automation-Start: Login to TravelSite");

                driver = new ChromeDriver(@"C:\Users\Esheku\Desktop\CHROME");

                driver.Manage().Window.Maximize();

                writer.WriteLine("Please enter url to login");

                //step 1: open travel.agileway.net
                driver.Url = TestContext.DataRow["url"].ToString();

                writer.WriteLine("The url entered as: " + driver.Url + "----PASS");

                string UserNameText = TestContext.DataRow["username"].ToString();

                string PasswordText = TestContext.DataRow["password"].ToString();

                //step 2: Enter user username
                driver.FindElement(By.Id("username")).SendKeys(UserNameText);

                writer.WriteLine("Username entered as: " + UserNameText + "----PASS");

                //Step 3: Enter pwd
                driver.FindElement(By.Id("password")).SendKeys(PasswordText);

                writer.WriteLine("Password entered as: " + PasswordText + "----PASS");

                //Step 4: Click Login button
                driver.FindElement(By.Name("commit")).Click();

                string ExpectedUrl = "travel.agileway.net/flights/start";

                if (driver.Url.Contains(ExpectedUrl))
                {
                    Console.WriteLine("Yes, travel site signed in successfully");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C:\\Users\\Esheku\\Desktop\\CHROME\\PASSSignInTravelSite.jpeg");
                }
                else
                {
                    Console.WriteLine("No, travel site Not signed in successfully");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C:\\Users\\Esheku\\Desktop\\CHROME\\FailedSignInTravelSite.jpeg");
                    Assert.Fail("ERROR; travel site Not signed in successfully");
                }
                writer.WriteLine("Scenario 1: Login to Travel site - Successfull");
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\C:\\Users\\Esheku\\Desktop\\CHROME\\InputData.csv", "InputData#csv", DataAccessMethod.Sequential)]
        public void EnterFlightDetails()
        {
            using (StreamWriter writer = new StreamWriter("C:\\Users\\Esheku\\Desktop\\CHROME\\FlightBookingTestResult.txt"))
            {
                string FirstnameText = TestContext.DataRow["firstname"].ToString();

                string LastnameText = TestContext.DataRow["lastname"].ToString();

                string CardNumberText = TestContext.DataRow["cardnumber"].ToString();


                writer.WriteLine("Start Automation - Enter Booking details");

                //first do sign in

                //var signIn = new SignInTravelAgileWay();
                this.SignInTravelAgileWay();

                writer.WriteLine("Login successfully completed");

                //string ExpectFlightURL = "travel.agileway.net/flights/edit";

                //enter flight details
                writer.WriteLine("Now entering flight details");

                // Step 5: Click on origin

                var origin = driver.FindElement(By.Name("fromPort"));

                var SelectOrigin = new SelectElement(origin);
                SelectOrigin.SelectByText("New York");

                writer.WriteLine("Origin city click succesfully");

                // Step 6: Destination

                var destination = driver.FindElement(By.Name("toPort"));

                var Selectdestination = new SelectElement(destination);
                Selectdestination.SelectByText("Sydney");

                writer.WriteLine("destination city click successfully");


                //Step 7: departing day
                var day = driver.FindElement(By.Id("departDay"));

                var Selectday = new SelectElement(day);
                Selectday.SelectByIndex(04);

                writer.WriteLine("departing day click successfully");

                //step 8: departing month
                var month = driver.FindElement(By.Id("departMonth"));

                var Selectmonth = new SelectElement(month);
                Selectmonth.SelectByValue("032016");

                writer.WriteLine("departing month click successfully");

                //step 9 returning day

                var Returnday = driver.FindElement(By.Id("returnDay"));

                var SelectReturnday = new SelectElement(Returnday);
                SelectReturnday.SelectByIndex(03);

                writer.WriteLine("returning day click successfully");

                //10 return month
                var returnMonth = driver.FindElement(By.Id("returnMonth"));

                var SelectReturnmonth = new SelectElement(returnMonth);
                SelectReturnmonth.SelectByValue("092016");

                writer.WriteLine("returning month click successfully");




                // 11 continue
                driver.FindElement(By.XPath("//*[@id='container']/form/input")).Click();

                string Url = "https://travel.agileway.net/flights/select_date?tripType=return&fromPort=New+York&toPort=Sydney&departDay=05&departMonth=032016&returnDay=04&returnMonth=092016";

                if (driver.Url.Contains(Url))
                {
                    Console.WriteLine("flight details open successfully");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C:\\Users\\Esheku\\Desktop\\CHROME\\PASSSignFlightDetails.jpeg");
                    writer.WriteLine("passenger details open successfully");
                }

                else
                {
                    Console.WriteLine("flight details did not open successfully");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C:\\Users\\Esheku\\Desktop\\CHROME\\FailedFlightDetails.jpeg");

                    Assert.Fail("passenger details did not successfully; Error Page");
                    writer.WriteLine("passenger details did not open successfully; Error Page");
                }




                // step 12 enter passenger details

                writer.WriteLine("enter passenger details");

                driver.FindElement(By.Name("passengerFirstName")).SendKeys("FirstnameText");

                writer.WriteLine("Firstname entered successfully");

                driver.FindElement(By.Name("passengerLastName")).SendKeys("LastnameText");

                writer.WriteLine("Lastname enteredsuccessfully");

                // step 13 click next

                driver.FindElement(By.XPath("//*[@id='container']/form/input[3]")).Click();

                string UrlLink = "https://travel.agileway.net/flights/passenger";

                if (driver.Url.Contains(UrlLink))
                {
                    Console.WriteLine("payment open successfully");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C:\\Users\\Esheku\\Desktop\\CHROME\\PAymentOpenSucessfully.jpeg");
                    writer.WriteLine("payment open successfully");

                }
                else
                {
                    Console.WriteLine("payment did not open successfully");

                    Assert.Fail("payment did not successfully; Error Page");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C:\\Users\\Esheku\\Desktop\\CHROME\\FailedPayment.jpeg");
                    writer.WriteLine("payment did not open successfully");

                }

                writer.WriteLine("enter card details");
                // Step: 14 click card type
                driver.FindElement(By.XPath("//input[contains(@value,'master')]")).Click();

                // step:15 click card number
                //driver.FindElement(By.Name("card_number")).SendKeys("234567890");
                driver.FindElement(By.Name("card_number")).SendKeys("cardnumber");


                // step: 16 click on expiry date
                var ExpiryMonth = driver.FindElement(By.Name("expiry_month"));
                var SelectExpiryMonth = new SelectElement(ExpiryMonth);
                SelectExpiryMonth.SelectByValue("06");



                // step: 17 click on expiry date
                var ExpiryYear = driver.FindElement(By.Name("expiry_year"));
                var SelectExpiryyear = new SelectElement(ExpiryYear);
                SelectExpiryyear.SelectByValue("2018");


                // step 18: click paynow
                driver.FindElement(By.XPath("//input[contains(@value,'Pay now')]")).Click();

                writer.WriteLine("enter card details entered successfully");


            }
        }
        //Regression Test Case 2: One Way Flight - P1
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\C:\\Users\\vipul\\Documents\\AutomationSept20\\InputData.csv", "InputData#csv", DataAccessMethod.Sequential)]
        public void OneWayFlightTest()
        {
            //


        }

        //Negative testing scenario
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\C:\\Users\\vipul\\Documents\\AutomationSept20\\InputData.csv", "InputData#csv", DataAccessMethod.Sequential)]
        public void LoginTestNagativeScenario()
        {

        }
        //before last two curley brackets

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

    }

    internal class SignInTravelAgileWay
    {
        public SignInTravelAgileWay()
        {
        }
    }

    internal class EnterFlightDetails
    {
        public EnterFlightDetails()
        {
        }
    }
}


