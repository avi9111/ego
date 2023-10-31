package main

import (
	"fmt"
	"os"

	//_ "vcs.taiyouxi.net/platform/x/auth/cmds/allinone"
	//_ "vcs.taiyouxi.net/platform/x/auth/cmds/auth"
	//_ "vcs.taiyouxi.net/platform/x/auth/cmds/login"
	//_ "vcs.taiyouxi.net/platform/x/auth/cmds/verupdateurl"

//	"github.com/codegangsta/cli"
	//"vcs.taiyouxi.net/platform/planx/util/logs"
	//"vcs.taiyouxi.net/platform/planx/version"
	//"vcs.taiyouxi.net/platform/x/auth/cmds"
	
	//"github.com/avi9111/ego/a"
	"taiyouxi/a"
	
	"taiyouxi/platform/x/ximport"
	//"taiyouxi/platform/x/auth/cmds"
	"auth/cmds"
	"github.com/urfave/cli"
)

/*
Auth系统使用Gin作为API访问框架;
Gin可参考：https://blog.csdn.net/guo_zhen_qian/article/details/122654286
*/
func main() {
	//defer logs.Close()
	app := cli.NewApp()

	//app.Version = version.GetVersion()
	var ver = "0.1.0"
	app.Version = ver
	app.Name = "AuthApix"
	app.Usage = fmt.Sprintf("Auth Http Api Server. version: %s", ver)
	app.Author = "YZH"
	app.Email = "yinzehong@taiyouxi.cn"

	cmds.InitCommands(&app.Commands)
	
	//ff.GoFuck()
	//a.GoFuck()

	a.Gogo()
	a.Gofuck()
	a.F2()

	//x.logXImport()
	fmt.Println("?????")

	app.Run(os.Args)
}
