What we did:

In order to get the program to run, we needed to update the `WebCam-Project.csproj` file, adding in the following code:

```
  <ItemGroup>
    <PackageReference Include="OpenCvSharp4" Version="4.10.0.20240616" />
    <PackageReference Include="OpenCvSharp4.runtime.win" Version="4.10.0.20240616" />
  </ItemGroup>
```

