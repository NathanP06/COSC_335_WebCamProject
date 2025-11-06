using OpenCvSharp;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the webcam (0 = default camera)
        using var capture = new VideoCapture(0);

        // Create a window to display the webcam feed
        using var window = new Window("Live Webcam Feed");

        // Close the program if the webcam cannot be accessed
        if (!capture.IsOpened())
        {
            Console.WriteLine("Error: Unable to access the webcam.");
            return;
        }

        // Create a single matrix object to store each video frame (overwrites previous)
        var frame = new Mat();

        // Creates a string to hold the filter type
        string activeFilter = "none";

        // User instructions
        Console.WriteLine("Press 'c' to capture an image.");
        Console.WriteLine("Press 'g' to apply grey-scale.");
        Console.WriteLine("Press 'b' to apply blur.");
        Console.WriteLine("Press 'r' to remove & reset filters.");
        Console.WriteLine("Press 'q' to quit.");

        // Main loop for live video streaming
        while (true)
        {
            // Read the current frame from the webcam
            capture.Read(frame);

            // If no frame is captured, break the loop
            if (frame.Empty())
            {
                Console.WriteLine("Error: No frame captured from webcam, quitting program.");
                break;
            }
            
            // Apply the selected filter dynamically
            Mat displayFrame = FilterManager.ApplyFilter(frame, activeFilter);
            window.ShowImage(displayFrame);

            // Detects key presses
            int key = Cv2.WaitKey(1);

            // Handle key presses for filter selection and image capture
            if (key == 'r') activeFilter = "none";
            if (key == 'g') activeFilter = "grayscale";
            if (key == 'b') activeFilter = "blur";
            if (key == 'c') CaptureImage(displayFrame, activeFilter);
            if (key == 'q') break;
        }
    }

    static void CaptureImage(Mat frame, string filterType)
    {
        // Absolute folder path for image capture (location to be changed as needed)
        string folderPath = @"C:\Users\natha\OneDrive\Documents\Coding Projects\C# Projects\COSC_335_WebCamProject\CapturedImages";

        // Get the current date, time, and filter name for the filename
        string timestamp = DateTime.Now.ToString("MM_dd_yyyy-HHmmss");
        string filterName = filterType == "none" ? "original" : filterType;

        string fileName = $"{timestamp}-{filterName}.jpg";
        string fullPath = System.IO.Path.Combine(folderPath, fileName);

        // Save the current frame as an image to the specified file path
        Cv2.ImWrite(fullPath, frame);
        Console.WriteLine("Image captured!");
    }
}

class FilterManager
{
    // Method to apply the selected filter to the webcam
    public static Mat ApplyFilter(Mat frame, string filterType)
    {
        // Determine which filter to apply (similar to an "if" tree)
        switch (filterType)
        {
            case "grayscale":
                return ApplyGrayscale(frame);
            case "blur":
                return ApplyBlur(frame);
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

    // Blur filter
    public static Mat ApplyBlur(Mat frame)
    {
        var result = new Mat();
        Cv2.GaussianBlur(frame, result, new Size(15, 15), 0);
        return result;
    }

    // Additional filters can be added here
}