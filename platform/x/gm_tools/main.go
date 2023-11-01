package main

import (
	"fmt"
	_ "net/http/pprof"
	"os"

	_ "taiyouxi/platform/x/gm_tools/cmds/allinone"

	"taiyouxi/platform/planx/util/config"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/version"
	"taiyouxi/platform/x/gm_tools/cmds"

	"github.com/codegangsta/cli"
)

func main() {
	defer logs.Close()

	app := cli.NewApp()

	app.Version = version.GetVersion()
	app.Name = "GM_Tools"
	app.Usage = fmt.Sprintf("GM Tool web server: version:%s", version.GetVersion())
	app.Author = "FanYang"
	app.Email = "fanyang@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)

	logxml := config.NewConfigPath("log.xml")
	logs.LoadLogConfig(logxml)

	app.Run(os.Args)
}
