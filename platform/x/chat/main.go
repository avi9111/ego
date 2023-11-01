package main

import (
	"os"

	//A visual interface to work with runtime profiling data from Go programs.
	//instead of pprof/http
	"fmt"

	_ "taiyouxi/platform/x/chat/cmds/allinone"

	_ "github.com/rakyll/gom/http"

	"taiyouxi/platform/planx/util"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/version"
	"taiyouxi/platform/x/chat/cmds"

	"github.com/codegangsta/cli"
)

const (
	logConf = "conf/log.xml"
)

/*
chattown配套机器人工具请使用 titools/chatroomrobot/client
*/
func main() {
	defer logs.Close()
	logs.LoadLogConfig(logConf)

	util.PProfStart()

	app := cli.NewApp()

	app.Version = version.GetVersion()
	app.Name = "Chat"
	app.Usage = fmt.Sprintf("Chat Http Api Server. version: %s", version.GetVersion())
	app.Author = "YZH"
	app.Email = "yinzehong@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)

	app.Run(os.Args)
}
