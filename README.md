# COSC 335 — WebCam Project

This is a small .NET console application used for capturing frames from a webcam and saving captured images to the repository's `CapturedImages/` folder. The project uses OpenCvSharp (OpenCV bindings for .NET) for camera access and image handling.

## Repository layout (relevant)

- `WebCam-Project/` — main C# project (`WebCam-Project.csproj`) and `Program.cs`.
- `CapturedImages/` — output folder where captured images are saved by the program.

## Prerequisites

- .NET SDK 9.0 (or a compatible .NET 9 install). Verify with `dotnet --version`.
- Windows OS for the provided native runtimes (the project references `OpenCvSharp4.runtime.win`).
- Visual C++ Redistributable may be required for native OpenCV DLLs — install the appropriate x64/x86 runtime if you see native DLL load errors.

The project includes NuGet package references for `OpenCvSharp4` and `OpenCvSharp4.runtime.win` in the project file so that native OpenCV binaries are restored at build time.

#### Installing/Adding NuGet Package (OpenCvSharp4)

In order to get the program to compile and sucessfully run, we needed to update the project file (`WebCam-Project.csproj`) to include the necessary OpenCV dependencies. Specifically, we imported the OpenCVSharp4 package along with the Windows runtime for our windows machines:

```
...

  <ItemGroup>
    <PackageReference Include="OpenCvSharp4" Version="4.10.0.20240616" />
    <PackageReference Include="OpenCvSharp4.runtime.win" Version="4.10.0.20240616" />
  </ItemGroup>

...
```

## Build and run (PowerShell on Windows)

1. From the repository root, restore and build:

```powershell
dotnet restore "WebCam-Project\WebCam-Project.csproj"
dotnet build "WebCam-Project\WebCam-Project.csproj" -c Debug
```

2. Run the app:

```powershell
dotnet run --project "WebCam-Project\WebCam-Project.csproj" -c Debug
```

When running, the app will open your default webcam (if available) and save captured images to the `CapturedImages/` folder. See the console for applicable instructions to utilize different application functions.

## Captured images

- Captured images are saved into the repository folder `CapturedImages/` through an absolute path reference. Check that folder after running the program to verify output. Files are named based upon the date, timestamp, and activated filter (if no filter is activated, it is set to "None").

## Troubleshooting

- DllNotFoundException or missing native OpenCV DLLs:
  - Ensure the `OpenCvSharp4.runtime.win` package is restored (`dotnet restore`).
  - Confirm you run the app with the correct architecture (x64 vs x86). The native DLLs live under `bin\Debug\net9.0\runtimes\win-x64\native` (or `win-x86`). If your OS/process is x64, use the x64 runtimes.
  - If needed, install the Visual C++ Redistributable for Visual Studio (matching architecture).

- No webcam detected / camera not accessible:
  - Make sure no other applications are using the camera.
  - Confirm Windows privacy settings permit apps to access the camera.

## Author

- NathanP06

- Note: Help from Github Copilot Chat and ChatGPT were used to understand, code, and create descriptions for this project.


