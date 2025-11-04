
## What we did:

In order to get the program to compile and sucessfully run, we needed to update the project file (`WebCam-Project.csproj`) to include the necessary OpenCV dependencies. Specifically, we imported the OpenCVSharp4 package along with the Windows runtime for our windows machines:

```
...

  <ItemGroup>
    <PackageReference Include="OpenCvSharp4" Version="4.10.0.20240616" />
    <PackageReference Include="OpenCvSharp4.runtime.win" Version="4.10.0.20240616" />
  </ItemGroup>

...
```

