namespace GreenkingTest.Api.Utils;

public class RegistrationFeeHelper : IRegistrationFeeHelper
{
    private const int FeeForOneYearOrLess = 500;
    private const int FeeForTwoToThreeYears = 250;
    private const int FeeForFourToFiveYears = 100;
    private const int FeeForSixToNineYears = 50;

    public int GetRegistrationFee(int? yearsOfExperience)
    {
        return yearsOfExperience switch
        {
            <= 1 => FeeForOneYearOrLess,
            >= 2 and <= 3 => FeeForTwoToThreeYears,
            >= 4 and <= 5 => FeeForFourToFiveYears,
            >= 6 and <= 9 => FeeForSixToNineYears,
            _ => 0
        };
    }
}