using OpenCvSharp; // Import the OpenCV library for image/video processing
using System;      // Import basic .NET functionality like Console I/O

class Program
{
    static void Main(string[] args)
    {
        // Initialize the webcam (0 = default camera)
        using var capture = new VideoCapture(0);

        // Create a window to display the webcam feed
        using var window = new Window("Live Webcam Feed");

        // Check if the webcam is accessible
        if (!capture.IsOpened())
        {
            Console.WriteLine("Error: Unable to access the webcam.");
            return; // Exit the program if webcam is unavailable
        }

        // Create a Mat (matrix) object to store each video frame
        var frame = new Mat();

        // Creates a string to hold the filter type
        string activeFilter = "none";

        // User instructions
        Console.WriteLine("Press 'c' to capture an image.");
        Console.WriteLine("Press 'g' to apply grey-scale.");
        Console.WriteLine("Press 'q' to quit.");

        // Main loop for live video streaming
        while (true)
        {
            // Read the current frame from the webcam
            capture.Read(frame);

            // If no frame is captured, break the loop
            if (frame.Empty())
                break;

            // Apply the selected filter dynamically
            Mat displayFrame = FilterManager.ApplyFilter(frame, activeFilter);
            window.ShowImage(displayFrame);

            int key = Cv2.WaitKey(1);

            if (key == 'r') activeFilter = "none";
            if (key == 'g') activeFilter = "grayscale";

            if (key == 'c')
            {
                // Save the current frame as an image
                Cv2.ImWrite("captured.jpg", frame);
                Console.WriteLine("Image captured!");
            }

            if (key == 'q') break;
        }
    }
}

class FilterManager
{
    // Routes to the appropriate filter
    public static Mat ApplyFilter(Mat frame, string filterType)
    {
        switch (filterType)
        {
            case "grayscale":
                return ApplyGrayscale(frame);
            default:
                return frame.Clone();
        }
    }

    // Grayscale filter
    public static Mat ApplyGrayscale(Mat frame)
    {
        var result = new Mat();
        Cv2.CvtColor(frame, result, ColorConversionCodes.BGR2GRAY);
        return result;
    }
}