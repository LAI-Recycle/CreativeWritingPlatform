# 一杯豆漿 - 創意寫作平台

一個提供用戶輕鬆寫作、分享創意的平台。用戶可以發表文章、收藏喜歡的作品、留下評論，並管理個人頁面。

[CakeResume 作品集介紹](https://www.cakeresume.com/portfolios/165659)

---

## 開發工具

| 類別 | 技術 |
|------|------|
| Backend | ASP.NET MVC 5.2.9 (.NET Framework 4.7.2) |
| Database | MongoDB Atlas (MongoDB.Driver 2.24.0) |
| Frontend | Razor、Bootstrap 5.2.3、jQuery 3.4.1 |

---

## 功能說明

- **會員系統** — 註冊、登入、登出，支援訪客 / 一般會員 / 管理員三種角色
- **文章管理** — 新增、編輯、閱讀文章，支援依時間、愛心數、瀏覽數、標題排序
- **互動功能** — 對文章按愛心、加入收藏，瀏覽數自動累計
- **評論系統** — 在文章下方留言，支援對評論按愛心
- **個人頁面** — 查看與編輯個人資料、瀏覽自己的文章與收藏

---

## 資料庫設計

![資料庫設計](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/fb2fc784-c920-44d5-afd9-7d3e9d63b0ed)

**Collections：**

| Collection | 主要欄位 |
|------------|---------|
| Member | MemberID、Name、Account、Password（SHA256）、Authentication（角色） |
| Article | ArticleId、Title、AuthorID、Content、FavoriteNumber、ClickNumber、DisplayStatus |
| Collect | CollectID、MemberID、ArticleId |
| Comment | CommentID、ArticleId、MemberID、CommentContent、FavoriteNumber、DisplayStatus |

---

## 快速開始

### 前置需求

- Visual Studio 2019 或 2022
- MongoDB Atlas 帳號（或自架 MongoDB）

### 步驟

**1. 設定資料庫連線**

將 `Web.example.config` 複製並改名為 `Web.config`，填入你的 MongoDB 帳密：

```xml
<add key="Username" value="你的帳號" />
<add key="Password" value="你的密碼" />
```

**2. 開啟專案**

以 Visual Studio 開啟 `NotX.sln`

**3. 還原 NuGet 套件**

Visual Studio 通常會自動還原，或在 Package Manager Console 執行：

```
Update-Package -reinstall
```

**4. 執行**

按 `F5`，預設網址為 `https://localhost:44377`

---

## 專案結構

```
NotX/
├── Controllers/    # MVC 控制器（Article、Member、User、Comment、Collect 等）
├── Models/         # 資料模型與 MongoDB 操作邏輯
├── Views/          # Razor CSHTML 頁面
├── Scripts/        # 前端 JavaScript（AJAX 互動）
├── Filters/        # 自訂授權 Filter
├── Web.example.config  # 設定範本（需改名為 Web.config）
└── Global.asax     # 應用程式生命週期設定
```

---

## 畫面截圖

**首頁**

![首頁](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/be2ff88d-b657-41e7-91c7-977379c99469)

**文章列表（可選擇排序方式）**

![文章列表](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/45ea6d07-234e-4b8b-9d18-581a2f52c90f)

**撰寫文章**

![撰寫文章](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/357b75fc-be83-43b1-9042-1384204626d7)

**文章閱讀（愛心、收藏、瀏覽數）**

![文章閱讀](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/76fc0eeb-2f05-41ea-a175-6e5a87fb49de)

**個人資料**

![個人資料](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/7889528e-5b37-4c0b-9e8a-5058d58371f8)

**我的文章**

![我的文章](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/772b7b40-ecca-4772-adf3-fdcf2c065ab4)

**我的收藏**

![我的收藏](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/259410a4-d78a-4624-8922-89bfa53ecace)
