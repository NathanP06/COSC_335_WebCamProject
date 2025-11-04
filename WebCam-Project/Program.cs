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
                break;

            // Apply the selected filter dynamically
            Mat displayFrame = FilterManager.ApplyFilter(frame, activeFilter);
            window.ShowImage(displayFrame);

            // Wait for a key press
            int key = Cv2.WaitKey(1);

            // Handle key presses for filter selection and image capture
            if (key == 'r') activeFilter = "none";
            if (key == 'g') activeFilter = "grayscale";
            if (key == 'b') activeFilter = "blur";

            if (key == 'c')
            {
                string folderPath = @"C:\Users\natha\OneDrive\Documents\Coding Projects\C# Projects\COSC_335_WebCamProject\CapturedImages";

                //Get the current date & time plus filter name for the filename
                string timestamp = DateTime.Now.ToString("MM_dd_yyyy-HHmmss");
                string filterName = activeFilter == "none" ? "original" : activeFilter;


                string fileName = $"{timestamp}-{filterName}.jpg";
                string fullPath = System.IO.Path.Combine(folderPath, fileName);

                // Save the current frame (what is displayed) as an image
                Cv2.ImWrite(fullPath, displayFrame);
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
}