namespace Tyre_Degradation_Model
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Estimated simulation of tyre degradation over a stint

            Console.Write("Enter base lap time (s): ");
            float LapTime = float.Parse(Console.ReadLine());

            Console.Write("Enter number of laps: ");
            int laps = Convert.ToInt32(Console.ReadLine());

            string[] TyreNames = { "Soft", "Medium", "Hard", "Wet" };
            float[] StartGrip = { 100, 90, 80, 70 };
            float[] DegradeRate = { 1.1f, 1.06f, 0.8f, 0.5f };
            float[] Overheat = { 1.1f, 1.06f, 1.03f, 1.01f };

            for (int t = 0; t < 4; t++)
            {
                Console.WriteLine(TyreNames[t] + " Tyre");

                float CurrentGrip = StartGrip[t];
                float CurrentDegrade = DegradeRate[t];
                double TotalTime = 0;

                for (int lap = 1; lap <= laps; lap++)
                {
                    float GripLoss = (100 - CurrentGrip) / 100;
                    float Time = LapTime + (GripLoss * 2.0f);

                    TotalTime += Time;

                    Console.WriteLine("Lap " + lap +
                                      " | Grip: " + CurrentGrip.ToString("f2") +
                                      "% | Lap Time: " + Time.ToString("f3"));

                    CurrentGrip = CurrentGrip - CurrentDegrade;
                    CurrentDegrade = CurrentDegrade * Overheat[t];

                    if (CurrentGrip < 0)
                    {
                        CurrentGrip = 0;
                    }
                }

                Console.WriteLine("Total Stint Time: " + TotalTime.ToString("f3") + " seconds");
            }
        }
    }
}
