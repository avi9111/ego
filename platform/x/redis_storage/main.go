package main

import (
	"fmt"
	"os"

	_ "taiyouxi/platform/x/redis_storage/cmds" //放在这里试图让log配置尽早生效
	_ "taiyouxi/platform/x/redis_storage/cmds/allinone"
	_ "taiyouxi/platform/x/redis_storage/cmds/monitor"
	_ "taiyouxi/platform/x/redis_storage/cmds/onlandall"
	_ "taiyouxi/platform/x/redis_storage/cmds/onlandone"
	_ "taiyouxi/platform/x/redis_storage/cmds/restoreall"
	_ "taiyouxi/platform/x/redis_storage/cmds/restoreone"
	_ "taiyouxi/platform/x/redis_storage/cmds/warm"

	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/version"
	"taiyouxi/platform/x/redis_storage/cmds"

	"github.com/codegangsta/cli"
)

//2016.5.18
// 测试了onlandall/onlandone LevelDB/RedisBiLog/Stdout有效的
// 测试了monitor模式下sync/sync_all LevelDB/RedisBiLog/Stdout是有效的

// readme 请参考项目总文档区RedisStorage/redis_storage_readme.md
func main() {
	defer logs.Close()

	app := cli.NewApp()
	app.Version = version.GetVersion()
	app.Name = "redis storage"
	app.Usage = fmt.Sprintf("Ticore game company redis onland server. version:%s", version.GetVersion())
	app.Author = "YinZeHong"
	app.Email = "yinzehong@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)

	app.Run(os.Args)
}
