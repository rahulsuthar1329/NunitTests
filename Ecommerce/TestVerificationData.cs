namespace TestData;

public class TestVerificationData
{
    public const string ProductSize = "M";
    public const string TeeId328 = "TEE#328";
    public const string HoodieId33 = "RDKLU#33";
    public const string BrandName = "RDKLU";
    public const string CustomerAddress = "Bhakhrava Jaav, Bali";
    public const string CustomerCity = "Jaipur";
    public const string CustomerState = "Rajasthan";
    public const string CustomerEmail = "rksuthar1169@gmail.com";
    public const string CustomerFirstName = "Rahul";
    public const string CustomerLastName = "Suthar";
    public const string CustomerPostalCode = "302021";
    public const string CustomerPhoneNo = "9587567573";

    public static string GenerateRandomNumber(int range) => new string(Enumerable.Range(0, range).Select(i => (char)new Random().Next(48, 58)).ToArray());
}