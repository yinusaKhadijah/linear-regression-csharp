using System.Diagnostics.Metrics;

namespace ConsoleApp5
{
    internal class Program

    {
        static void Main(string[] args)

        {
            // Controls whether the program repeats
            // The loop continues as long as the user types "yes".
            string userInput = "yes";
            do
            {
                int size;

                // Ask the user how many entries they want to make
                Console.WriteLine("How many entries will be made?");
                size = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("You must enter at least 2 points, but you can type 'stop' at any time if you want to finish early.");

                // Ensures the user inputs at least two points
                // Regression cannot be calculated with fewer than 2 points
                while (size < 2)
                {
                    Console.WriteLine("Please input at least 2 entries...");
                    Console.WriteLine("How many entries will be made?");

                    size = Convert.ToInt32(Console.ReadLine());
                }

                // Arrays to store x and y values
                double[] x = new double[size];
                double[] y = new double[size];

                int index = 0;  //Tracks how many valid points were entered

                for (int i = 0; i < size; i++)

                {
                    Console.WriteLine("--- Point " + (index + 1) + " ---");
                    // Ask for x value
                    Console.Write("Enter x value: ");
                    string xInput = Console.ReadLine();

                    // Allow the user to stop early by typing 'stop'.
                    if (xInput.ToLower() == "stop")
                    {
                        break;
                    }

                    // Checking x input
                    double xValue;
                    while (!double.TryParse(xInput, out xValue))
                    {
                        Console.WriteLine("Invalid input. Please enter a number or type 'stop' to finish early.");
                        Console.Write("Enter x value: ");
                        xInput = Console.ReadLine();

                        if (xInput.ToLower() == "stop")
                        {
                            break;
                        }
                    }
                    if (xInput.ToLower() == "stop")
                        {
                            break;
                        }
                    

                    x[index] = xValue;

                    // Ask for y value
                    Console.Write("Enter y value: ");
                    string yInput = Console.ReadLine();

                    // Allow stopping during y input as well.
                    if (yInput.ToLower() == "stop")
                    {
                        break;
                    }

                    // Checking y input
                    double yValue;
                    while (!double.TryParse (yInput, out yValue))
                    {
                        Console.WriteLine("Invalid input. Please enter a number or type 'stop' to finish early."); 
                        Console.Write("Enter y value: ");
                        yInput = Console.ReadLine();

                        if (yInput.ToLower() == "stop")
                        {
                            break;
                        }
                    }

                    if (yInput.ToLower() == "stop")
                    {
                        break;
                    }
                    
                    y[index] = yValue;

                    index++;
                }
                // Must have at least 2 dimensional points to continue.
                if (index < 2)
                {
                    Console.WriteLine("\nYou must input at least 2-dimensional points.");
                    Console.WriteLine("Restarting...\n");
                    continue;   //Restart entire loop
                }

                // Calculate Total
                double totalX = 0, totalY = 0;

                for (int i = 0; i < index; i++)
                {
                    totalX += x[i];
                    totalY += y[i];
                }

                // Calculate Mean
                double xmean = Math.Round(totalX / index, 2);
                double ymean = Math.Round(totalY / index, 2);

                // Arrays to store regression calculation
                double[] xyMean = new double[index];    
                double[] xMeanSquare = new double[index];   
                double[] yMeanSquare = new double[index];   

                for (int i = 0; i < index; i++)
                {
                    double dx = x[i] - xmean;   // x - xmean
                    double dy = y[i] - ymean;   // y - ymean

                    xyMean[i] = dx * dy;    // (x-xmean)(y-ymean)
                    xMeanSquare[i] = dx * dx;   // (x-xmean)^2
                    yMeanSquare[i] = dy * dy;   // (y-ymean)^2
                }

                // Sum each column
                double xySum = Math.Round(xyMean.Sum(), 2);
                double xSquareSum = Math.Round(xMeanSquare.Sum(), 2);
                double ySquareSum = Math.Round(yMeanSquare.Sum(), 2);

                // Print Regression table
                Console.WriteLine("\nRegression Table:");
                Console.WriteLine($"{"x",10} {"y",10} {"(x-xmean)(y-ymean)",20} {"(x-xmean)^2",15} {"(y-ymean)^2",15}");

                // Print each row of the table
                for (int i = 0; i < index; i++)
                {
                    Console.WriteLine($"{x[i],10} {y[i],10} {xyMean[i],20:F2} {xMeanSquare[i],15:F2} {yMeanSquare[i],15:F2}");
                }

                Console.WriteLine(" ");
                Console.WriteLine($"{"xmean",10} {"ymean",10} {"sum1",20} {"sum2",15} {"sum3",15}");
                Console.WriteLine($"{xmean,10} {ymean,10} {xySum,20} {xSquareSum,15} {ySquareSum,15}");


                // Calculate Regression Values
                double slope = Math.Round(xySum / xSquareSum, 2);
                double c = Math.Round((ymean - slope) * xmean, 2);
                double correlation = Math.Round(xySum / (Math.Sqrt(xSquareSum * ySquareSum)), 2);

                //Print Results
                Console.WriteLine("\nSlope(m) = " + slope);
                 
                Console.WriteLine("Intercept(c) = " + c);

                Console.WriteLine("y = " + slope + "x + (" + c + ")");

                Console.WriteLine("Correlation(r) = " + correlation);

                //Ask user if they want to repeat
                //The user can input any other thing aside from yes if they are not making another regression table (no).

                Console.WriteLine("\nDo you want to make another input?\nInput 'yes' if true ");
                userInput = Console.ReadLine().ToLower();

            } while (userInput == "yes");

            Console.WriteLine("Bye!");
        }

    }

}