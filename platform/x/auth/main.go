package main

import (
	"fmt"
	"os"

	_ "taiyouxi/platform/x/auth/cmds/allinone" //_ 下划线引入，会引入调用 init（）;另外路径上的所有 import 应该都会引入(allinone 几乎没有import也有大量import)
	"taiyouxi/platform/x/auth/ximport"

	"taiyouxi/platform/x/chat/cmds"

	//_ "vcs.taiyouxi.net/platform/x/auth/cmds/auth"
	//_ "vcs.taiyouxi.net/platform/x/auth/cmds/login"
	//_ "vcs.taiyouxi.net/platform/x/auth/cmds/verupdateurl"

	//	"github.com/codegangsta/cli"
	//"vcs.taiyouxi.net/platform/planx/util/logs"
	//"vcs.taiyouxi.net/platform/planx/version"
	//"vcs.taiyouxi.net/platform/x/auth/cmds"

	//"github.com/avi9111/ego/a"
	"taiyouxi/a"

	//"taiyouxi/platform/x/auth/cmds"
	//"auth/cmds"
	"github.com/urfave/cli"
)

/*
Auth系统使用Gin作为API访问框架;
Gin可参考：https://blog.csdn.net/guo_zhen_qian/article/details/122654286
*/
func main() {
	//测试 ximport 的 import
	ximport.LogXImport()

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
	//测试(无用，单向调用，增加cmds数组？？)
	cmds.Register(&cli.Command{
		Name:   "bot",
		Usage:  "run a bot",
		Action: BotStart,
		Flags: []cli.Flag{
			cli.StringFlag{
				Name:  "rpc, r",
				Value: "127.0.0.1:8668",
				Usage: "gate server rpc ip:port",
			},
			cli.StringFlag{
				Name:  "server",
				Value: "127.0.0.1:8667",
				Usage: "gate server ip:port",
			},
			cli.Float64Flag{
				Name:  "speed, s",
				Value: 1.0,
				Usage: "2.0 2xtimes faster, 0 fixed 100ms",
			},
		},
	})
	//测试2
	app.Commands = []cli.Command{
		{
			Name:        "describeit",
			Aliases:     []string{"d"},
			Usage:       "use it to see a description",
			Description: "This is how we describe describeit the function",
			Action: func(c *cli.Context) error {
				fmt.Printf("i like to describe things")
				return nil
			},
		},
	}

	//ff.GoFuck()
	//a.GoFuck()

	a.Gogo()
	a.Gofuck()
	a.F2()

	//x.logXImport()
	fmt.Println("?????")

	app.Run(os.Args)
}

// 测试
func BotStart(c *cli.Context) {
	fmt.Println("botStart()")
}
