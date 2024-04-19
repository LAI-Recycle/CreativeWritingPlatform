# **一杯豆漿 - 創意寫作平台 Creative writing platform**

## 簡介
一杯豆漿是一個紀錄創意的寫作平台，旨在提供用戶一個輕鬆寫作記錄的環境。


## 發想
這個平台的發想來自對於創意寫作的熱愛和追求。致力打造能讓用戶分享創造力跟想法的平台，讓每一個人都能夠成為自己的故事大師。


## 資料關係設計

![20240400一杯豆漿創作平台資料庫設計 (1)](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/fb2fc784-c920-44d5-afd9-7d3e9d63b0ed)

## 初始化注意
在使用之前，注意更改Web.config文件並填寫相應的用戶名和密碼等資料，這步驟是確保平台資料庫可以使用。

1. 修改Web.example.config其中的內容，填入自己的MongoDB連線資料
```
<add key="Username" value=" YourUsername " />
<add key="Password" value=" YourPassword " />
```
2. 更改Web.example.config成Web.config

## 畫面呈現

開啟畫面
![0 開啟畫面](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/be2ff88d-b657-41e7-91c7-977379c99469)

當用戶登入後，他們可以瀏覽其他用戶發佈的文章，並選擇喜歡的排序方式。
![3 選擇想要關注的排列方式](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/45ea6d07-234e-4b8b-9d18-581a2f52c90f)

用戶可以自由地撰寫和修改自己的文章內容。
![4 自己寫文章](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/357b75fc-be83-43b1-9042-1384204626d7)

平台會統計文章的觀看數，並允許用戶對文章進行愛心和收藏操作。
![5 看到別人寫的文章3](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/76fc0eeb-2f05-41ea-a175-6e5a87fb49de)


用戶可以查看和修改自己的個人資訊和設置。
![6 個人資料](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/7889528e-5b37-4c0b-9e8a-5058d58371f8)

在個人資訊頁面，您可以輕鬆地查看您自己的文章

![7 自己文章可以在自己的介面找到](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/772b7b40-ecca-4772-adf3-fdcf2c065ab4)


同樣地，您也可以找到自己收藏的文章，方便您隨時查閱您感興趣的內容。
![8 找到自己收藏的文章](https://github.com/LAI-Recycle/Creative-Writing-Platform/assets/77723979/259410a4-d78a-4624-8922-89bfa53ecace)



## 開發工具
- Microsoft.AspNet.Mvc 5.2.9
- jQuery 3.4.1
- Bootstrap 5.2.3
- MongoDB.Driver 2.24.0
