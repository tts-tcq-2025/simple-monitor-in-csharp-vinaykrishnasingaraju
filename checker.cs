using System;
using System.Collections.Generic;

namespace paradigm_shift_csharp
{
    class Checker
    {
        enum BreachType { NORMAL, TOO_LOW, TOO_HIGH }

        struct Parameter
        {
            public string Name;
            public float Value;
            public float Min;
            public float Max;
        }

        static BreachType InferBreach(float value, float min, float max)
        {
            if (value < min) return BreachType.TOO_LOW;
            if (value > max) return BreachType.TOO_HIGH;
            return BreachType.NORMAL;
        }

        static BreachType CheckParameter(Parameter parameter)
        {
            return InferBreach(parameter.Value, parameter.Min, parameter.Max);
        }

        static bool BatteryIsOk(float temperature, float soc, float chargeRate)
        {
            var checks = new List<Parameter>
            {
                new Parameter { Name = "Temperature", Value = temperature, Min = 0, Max = 45 },
                new Parameter { Name = "State of Charge", Value = soc, Min = 20, Max = 80 },
                new Parameter { Name = "Charge Rate", Value = chargeRate, Min = 0, Max = 0.8f }
            };

            bool allOk = true;
            foreach (var param in checks)
            {
                var result = CheckParameter(param);
                if (result != BreachType.NORMAL)
                {
                    ReportBreach(param.Name, result);
                    allOk = false;
                }
            }
            return allOk;
        }

        static void ReportBreach(string parameterName, BreachType breach)
        {
            string direction = breach == BreachType.TOO_LOW ? "low" : "high";
            Console.WriteLine($"{parameterName} is too {direction}!");
        }

        static void ExpectTrue(bool expression)
        {
            if (!expression)
            {
                Console.WriteLine("Expected true, but got false");
                Environment.Exit(1);
            }
        }

        static void ExpectFalse(bool expression)
        {
            if (expression)
            {
                Console.WriteLine("Expected false, but got true");
                Environment.Exit(1);
            }
        }

        static int Main()
        {
            // Normal case
            ExpectTrue(BatteryIsOk(25, 70, 0.7f));
            // High temperature and high SOC
            ExpectFalse(BatteryIsOk(50, 85, 0.0f));
            // Low temperature
            ExpectFalse(BatteryIsOk(-1, 50, 0.5f));
            // Low SOC
            ExpectFalse(BatteryIsOk(25, 10, 0.5f));
            // High Charge Rate
            ExpectFalse(BatteryIsOk(25, 50, 0.9f));
            // Multiple breaches
            ExpectFalse(BatteryIsOk(-5, 10, 1.0f));

            Console.WriteLine("All tests passed.");
            return 0;
        }
    }
}
