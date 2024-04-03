using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace EcommerceAutomation;

public class ExtentReporting
{
    private ExtentReports _extentReports;
    private ExtentTest _extentTest;

    private ExtentReports StartReporting()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "../../../reports");

        if (_extentReports == null)
        {
            Directory.CreateDirectory(path);

            _extentReports = new ExtentReports();
            ExtentSparkReporter spark = new ExtentSparkReporter($"{path}/Report_{Guid.NewGuid()}.html");
            Console.WriteLine(TestContext.CurrentContext.Test.Name);

            _extentReports.AttachReporter(spark);

        }

        return _extentReports;
    }

    public void CreateTest(string testName)
    {
        _extentTest = StartReporting().CreateTest(testName);
        LogInfo("Test Start");
    }

    public void EndReporting()
    {
        StartReporting().Flush();
    }

    public void LogFail(string info) => _extentTest.Fail(info);

    public void LogPass(string info) => _extentTest.Pass(info);

    public void LogInfo(string info) => _extentTest.Info(info);

    public void LogScreenshot(string info, string image) => _extentTest.Info(info, MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
}