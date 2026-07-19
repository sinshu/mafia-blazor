# Mafia for the web

An old platform game ported to the browser with [KNI Engine](https://github.com/kniEngine/kni) and Blazor WebAssembly.

## Run locally

Requirements: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```powershell
dotnet run
```

Open the URL printed in the terminal. The game must be served over HTTP; opening a generated HTML file directly does not work.

## Controls

- Arrow keys: move and select
- `A`, `S`, `Z`, left Shift, left Ctrl, Enter, or Space: jump / confirm
- Escape: back

## Publish

```powershell
dotnet publish -c Release
```

Deploy the contents of `bin/Release/net8.0/publish/wwwroot` to any static web host. The host must serve unknown file extensions such as `.wasm`, `.dll`, `.dat`, `.json`, `.xnb`, and `.stg` without rewriting them to `index.html`.

For a smaller production download, optionally install the WebAssembly build tools once with `dotnet workload install wasm-tools` before publishing.

Source assets live in `Content`. They are compiled or copied into `wwwroot/Content` automatically during the build.

## GitHub Pages

Every push to `main` is built and deployed automatically by GitHub Actions.

One-time repository setting: select **Settings > Pages > Build and deployment > Source > GitHub Actions**.

The published game is available at:

https://sinshu.github.io/mafia-blazor/
