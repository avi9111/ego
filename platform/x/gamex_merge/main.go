package main

import (
	"os"

	"runtime"

	"taiyouxi/platform/planx/util/config"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/version"
	"taiyouxi/platform/x/gamex_merge/cmds"
	_ "taiyouxi/platform/x/gamex_merge/cmds/allinone"

	"github.com/codegangsta/cli"
)

func main() {
	defer logs.Close()
	app := cli.NewApp()
	app.Version = version.GetVersion()
	app.Name = "gamex_merge"
	app.Author = "ZhangZhen"
	app.Email = "zhangzhen@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)

	logxml := config.NewConfigPath("log.xml")
	logs.LoadLogConfig(logxml)
	logs.Info("GOMAXPROCS is %d", runtime.GOMAXPROCS(0))
	logs.Info("Version is %s", app.Version)

	app.Run(os.Args)

}
