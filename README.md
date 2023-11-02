# ego
无双游戏的 go 服务器

始于2016年，一个适合入门学习的服务器代码，原团队的设计和研发文档写得很全+仔细

# 目录结构
a/ 测试目录

platform/ 主目录

# 里程碑

- (*2023/11/2)发现 debug 方法，只要安装go-dev,VsCode 直接按 DeBug Icon 会自动提示是否安装
![vscode go debug](images/vsCode自动询问是否安装go-dev.png)

- 建立router(路由)

基本登录等联调

业务逻辑（开房间）

- 业务逻辑（战斗）ol



# 当前启动方法（Windows

<缺>

项目需要多个安装。。。。

![go get xxxx](images/屏幕截图%202023-11-01%20014720.png)


# 开发过程

| 操作 | 结果
| ------------------ | ---------- |
|修改了下客户端，登录时会有这个 Warn|[RCD] Auth Step1 - Auth usrname/pwd [URL]: http://192.168.0.124:8080/auth/v1/user/login?name=YXZpOTExMQ==&passwd=C4RlHXlZtMtG0jlx2oD9m-==|
|登录后马上回请求 sharelist|[RCD] Auth Step2 - Start, get shard list. [URL]: http://127.0.0.1:8081/login/v2/shards/0?at=a4d56741-2d41-4c54-b2ad-a775c2aab740|
|（修复从服务器获取shard),选择服务器后| [RCD] Auth Step3, enter shard. [URL]: http://127.0.0.1:8081/login/v1/getgate?at=127d2542-6e2c-4553-8837-04c640a48efd&sn=test Server2&ver=0.0.0 |