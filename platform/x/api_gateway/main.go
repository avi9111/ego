package main

import (
	"fmt"
	"os"

	"runtime"

	"taiyouxi/platform/planx/util/config"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/version"
	"taiyouxi/platform/x/api_gateway/cmds"
	_ "taiyouxi/platform/x/api_gateway/cmds/pay"

	"github.com/codegangsta/cli"
)

func main() {
	defer logs.Close()

	app := cli.NewApp()
	app.Version = version.GetVersion()
	app.Name = "api_gateway"
	app.Usage = fmt.Sprintf("Ticore game company api gateway server. version:%s", version.GetVersion())
	app.Author = "ZhangZhen"
	app.Email = "zhangzhen@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)

	logxml := config.NewConfigPath("log.xml")
	logs.LoadLogConfig(logxml)
	logs.Info("GOMAXPROCS is %d", runtime.GOMAXPROCS(0))

	app.Run(os.Args)
}
