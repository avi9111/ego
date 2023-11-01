package main

import (
	"fmt"
	"os"

	_ "taiyouxi/platform/x/clientlog/cmds/allinone"

	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/version"
	"taiyouxi/platform/x/clientlog/cmds"

	"github.com/codegangsta/cli"
)

/*
Auth系统使用Gin作为API访问框架
*/
func main() {
	defer logs.Close()
	app := cli.NewApp()

	app.Version = version.GetVersion()
	app.Name = "ClientLog"
	app.Usage = fmt.Sprintf("ClientLog Server. version: %s", version.GetVersion())
	app.Author = "YZH"
	app.Email = "yinzehong@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)

	app.Run(os.Args)
}
