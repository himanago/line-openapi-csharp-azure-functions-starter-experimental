# LINE OpenAPI C# Azure Functions Starter

GitHub Codespaces で開くだけで、最新の LINE OpenAPI に従った SDK をもとに開発ができるテンプレートです。

（実用的ではなく、実験的なものです）


## 使い方

### 1. GitHub Codespaces で開く

Use this template > Open in a codespace から開けます。

### 2. 準備完了するまで待つ

Codespace の画面が表示されたあと、いくつかのコマンドが実行されます。

openapi-generator-cli により LINE OpenAPI の定義から SDK を `sdk` フォルダに生成するので、出来上がりを待ちます。


### 3. SDK ソースコードの修正

以下のファイルのクラス定義で、クラスについている `[JsonConverter(typeof(JsonSubtypes), "Type")]` の行を消す or コメントアウトします。

- sdk/src/LineOpenApi.Webhook/Model/AccountLinkEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/ActivatedEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/AllMentionee.cs
- sdk/src/LineOpenApi.Webhook/Model/AttachedModuleContent.cs
- sdk/src/LineOpenApi.Webhook/Model/AudioMessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/BeaconEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/BotResumedEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/BotSuspendedEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/DeactivatedEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/DetachedModuleContent.cs
- sdk/src/LineOpenApi.Webhook/Model/FileMessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/FollowEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/GroupSource.cs
- sdk/src/LineOpenApi.Webhook/Model/ImageMessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/JoinEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/LeaveEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/LinkThingsContent.cs
- sdk/src/LineOpenApi.Webhook/Model/LocationMessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/MemberJoinedEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/MemberLeftEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/MessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/MessageEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/ModuleEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/PostbackEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/RoomSource.cs
- sdk/src/LineOpenApi.Webhook/Model/ScenarioResultThingsContent.cs
- sdk/src/LineOpenApi.Webhook/Model/StickerMessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/TextMessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/ThingsContent.cs
- sdk/src/LineOpenApi.Webhook/Model/ThingsEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/UnfollowEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/UnlinkThingsContent.cs
- sdk/src/LineOpenApi.Webhook/Model/UnsendEvent.cs
- sdk/src/LineOpenApi.Webhook/Model/UserMentionee.cs
- sdk/src/LineOpenApi.Webhook/Model/UserSource.cs
- sdk/src/LineOpenApi.Webhook/Model/VideoMessageContent.cs
- sdk/src/LineOpenApi.Webhook/Model/VideoPlayCompleteEvent.cs

※ ReplyFnction 記載のオウム返しを動かすだけなら `MessageEvent.cs` と `TextMessageContent.cs` だけで大丈夫です。

### 4. Azure に Function App のリソースを作成

Azure Portal もしくは Codespace の Azure Functions 拡張機能から作成します。

ランタイムは `.NET 6` にしてください。

### 5. 作成した Function App にデプロイ

`LineBotFunctions` フォルダの右クリック > Deploy to Function App... からデプロイします。

### 6. LINE Messagng API との接続

LINE Developers のドキュメント、[Messaging APIを始めよう](https://developers.line.biz/ja/docs/messaging-api/getting-started/) を参考に、Messaging API チャネルを作成します。
​
Webhook URL として、さきほどデプロイした Azure Functions の `ReplyFunction` の URL を設定します。

チャネルアクセストークンを発行し、Azure Functions のアプリケーション設定に `ChannelAccessToken` というキーで登録します。

LINE に友だち追加すれば、オウム返ししてくれます。

あとは、好きな機能を実装していきます。