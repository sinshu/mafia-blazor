# Mafia ブラウザ版

昔制作したプラットフォームゲームを、[KNI Engine](https://github.com/kniEngine/kni)とBlazor WebAssemblyでブラウザ向けに移植したものです。

## 🎮 ブラウザで遊ぶ

### [ゲームを起動する](https://sinshu.github.io/mafia-blazor/)

ダウンロードやインストールは不要です。PCのブラウザからそのまま遊べます。

## 操作方法

- 矢印キー：移動・選択
- `A`、`S`、`Z`、左Shift、左Ctrl、Enter、Space：ジャンプ・決定
- Escape：戻る

## ローカルで起動する

事前に[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)をインストールしてください。

```powershell
dotnet run --project src/Mafia/Mafia.csproj
```

ターミナルに表示されたURLをブラウザで開きます。生成されたHTMLファイルを直接開いても動作しないため、必ずHTTPサーバー経由で起動してください。

## 公開用ファイルを生成する

```powershell
dotnet publish src/Mafia/Mafia.csproj -c Release
```

生成された`src/Mafia/bin/Release/net8.0/publish/wwwroot`の中身を静的Webホスティングサービスへ配置します。ホスティング側では、`.wasm`、`.dll`、`.dat`、`.json`、`.xnb`、`.stg`などの拡張子を持つファイルを`index.html`へ書き換えず、そのまま配信できる必要があります。

配信サイズを小さくしたい場合は、公開前に一度だけ次のコマンドでWebAssemblyビルドツールをインストールできます。

```powershell
dotnet workload install wasm-tools
```

素材ファイルは`src/Mafia/Content`にあります。ビルド時に自動でコンパイルまたはコピーされ、`wwwroot/Content`へ配置されます。

## GitHub Pagesへの公開

`main`ブランチへpushすると、GitHub Actionsによって自動的にビルド・公開されます。

リポジトリで初回のみ、GitHubの「Settings」→「Pages」→「Build and deployment」→「Source」を「GitHub Actions」に設定する必要があります。
