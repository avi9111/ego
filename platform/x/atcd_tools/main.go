package main

import (
	"os"

	_ "taiyouxi/platform/x/atcd_tools/cmds/allinone"

	"taiyouxi/platform/planx/util/config"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/x/atcd_tools/cmds"

	"github.com/codegangsta/cli"
)

func main() {
	defer logs.Close()
	app := cli.NewApp()

	app.Version = ""
	app.Name = "atcd_tool"
	app.Usage = "Atcd Config Tool web server"
	app.Author = "FanYang"
	app.Email = "fanyang@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)

	logxml := config.NewConfigPath("log.xml")
	logs.LoadLogConfig(logxml)

	app.Run(os.Args)
}
