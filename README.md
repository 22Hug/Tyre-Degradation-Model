tire degradation model is basically a digital race engineer 
that calculates how much slower a car gets as its rubber wears out. 
Instead of just guessing, the code uses a estimated physical logic, 
That recognizes that the hotter and more worn a tire gets, 
the faster it begins to fall apart


//Estimated simulation of tyre degradation over a stint


public class Tyre
{
    public string Compound {get; set; }
    public float InitialGrip {get; set; }
    public float DecayRate { get; set; }
    public float ThermalSensitivity { get; set; }

    public Tyre(string name, float grip, float decay, float heat)
    {
        Compound = name;
        InitialGrip = grip;
        DecayRate = decay;
        ThermalSensitivity = heat;
    }
}

class RaceSim
{
    static void Main(string[] args)
    {
        Console.Write("Enter Base Lap Time(s): ");
        if (!float.TryParse(Console.ReadLine(), out float baseLapTime))
        {
            return;
        }

        Console.Write("Enter num of laps: ");
        if (!int.TryParse(Console.ReadLine(), out int totalLaps))
        {
            return;
        }

        var tyreLibrary = new List<Tyre>
        {
            new Tyre("Soft", 100f, 1.1f, 1.1f),
            new Tyre("Medium", 90f, 1.06f, 1.06f),
            new Tyre("Hard", 80f, 0.8f, 1.03f)
        };

        Sim(tyreLibrary, baseLapTime, totalLaps);
    }

    static void Sim(List<Tyre> tyres, float baseTime, int laps)
    {
        foreach (var tyre in tyres)
        {
            Console.WriteLine($"\nTyre Compound: {tyre.Compound}");
            float currentGrip = tyre.InitialGrip;
            float currentDecay = tyre.DecayRate;
            double cumulativeTime = 0;

            for (int i = 1; i <= laps; i++)
            {
                float gripDeficit = (100 - currentGrip) / 100;
                float delta = gripDeficit * 2.0f;
                float lapTime = baseTime + delta;

                cumulativeTime = cumulativeTime + lapTime;

                Console.WriteLine($"Lap {i:D2} | Grip: {currentGrip:F2}% | Time: {lapTime:F3}s");

                currentGrip = currentGrip - currentDecay;
                currentDecay = currentDecay * tyre.ThermalSensitivity; 

                if (currentGrip < 0) currentGrip = 0;
            }

            Console.WriteLine($"Total Duration: {cumulativeTime:F3}s");
        }
    }
}
