using System;

namespace paradigm_shift_csharp
{
    class Checker
    {
        const float MIN_TEMP = 0;   
        const float MAX_TEMP = 45;    

        const float MIN_SOC = 20;     
        const float MAX_SOC = 80;     

        const float MIN_CHARGERATE = 0;
        const float MAX_CHARGERATE = 0.8f;

        static bool IsSafeRange(float value, float min, float max)
        {
            return ((value >= min) && (value <= max));  
        }

        static bool IsBatteryHealthy(float temperature, float soc, float chargeRate)
        {
            return IsSafeRange(temperature, MIN_TEMP, MAX_TEMP)
                && IsSafeRange(soc, MIN_SOC, MAX_SOC)
                && IsSafeRange(chargeRate, MIN_CHARGERATE, MAX_CHARGERATE);
        }

        static void ExpectTrue(bool expression)
        {
            if (expression)
            {
                Console.WriteLine("Expected true, got true");
            }
            else
            {
                Console.WriteLine("Expected true, but got false");
            }
        }

        static void ExpectFalse(bool expression)
        {
            if (!expression)
            {
                Console.WriteLine("Expected false, got false");
            }
            else
            {
                Console.WriteLine("Expected false, got true");
            }
            
        }

        static int Main()
        {
            ExpectTrue(IsBatteryHealthy(25, 70, 0.7f));
            ExpectFalse(IsBatteryHealthy(50, 85, 0.0f));

            return 0;
        }
    }
}
