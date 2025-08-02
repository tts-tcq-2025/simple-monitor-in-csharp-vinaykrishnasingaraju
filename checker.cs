using System;

namespace paradigm_shift_csharp
{
    class Checker
    {
        const float MIN_TEMP = 0;
        const float MAX_TEMP = 45;

        const float MIN_SOC = 20;
        const float MAX_SOC = 80;

        const float MAX_CHARGE_RATE = 0.8f;

        enum BreachType { NORMAL, TOO_LOW, TOO_HIGH }

        static BreachType GetBreachType(float value, float min, float max)
        {
            if (value < min) return BreachType.TOO_LOW;
            if (value > max) return BreachType.TOO_HIGH;
            return BreachType.NORMAL;
        }

        static bool IsBatteryHealthy(float temperature, float soc, float chargeRate)
        {
            bool allOk = true;

            BreachType tempStatus = GetBreachType(temperature, MIN_TEMP, MAX_TEMP);
            BreachType socStatus = GetBreachType(soc, MIN_SOC, MAX_SOC);
            BreachType chargeStatus = GetBreachType(chargeRate, 0, MAX_CHARGE_RATE);

            allOk &= ReportIfNotNormal("Temperature", tempStatus);
            allOk &= ReportIfNotNormal("State of Charge", socStatus);
            allOk &= ReportIfNotNormal("Charge Rate", chargeStatus);

            return allOk;
        }

        static bool ReportIfNotNormal(string paramName, BreachType status)
        {
            if (status == BreachType.NORMAL) return true;

            string condition = status == BreachType.TOO_LOW ? "too low" : "too high";
            Console.WriteLine($"{paramName} is {condition}.");
            return false;
        }

        static void ExpectTrue(bool result)
        {
            if (result) Console.WriteLine("Test Passed: Expected true, got true");
            else Console.WriteLine("Test Failed: Expected true, got false");
        }

        static void ExpectFalse(bool result)
        {
            if (!result) Console.WriteLine("Test Passed: Expected false, got false");
            else Console.WriteLine("Test Failed: Expected false, got true");
        }

        static int Main()
        {
            Console.WriteLine("Running battery health checks...\n");

            ExpectTrue(IsBatteryHealthy(25, 70, 0.7f));

           
            ExpectFalse(IsBatteryHealthy(50, 85, 0.0f));

           
            ExpectFalse(IsBatteryHealthy(-5, 60, 0.5f));

            
            ExpectFalse(IsBatteryHealthy(25, 10, 0.5f));

           
            ExpectFalse(IsBatteryHealthy(25, 60, 1.0f));

          
            ExpectFalse(IsBatteryHealthy(-10, 5, 1.5f));

            Console.WriteLine("\nAll tests completed.");
            return 0;
        }
    }
}
