package main

import (
	"fmt"
	"os"

	"taiyouxi/platform/x/auth/cmds"
	_ "taiyouxi/platform/x/auth/cmds/allinone" //_ 下划线引入，会引入调用 init（）;另外路径上的所有 import 应该都会引入(allinone 几乎没有import也有大量import)
	"taiyouxi/platform/x/tiprotogen/log"
	"taiyouxi/platform/x/ximport"

	//"taiyouxi/platform/x/chat/cmds"//这个cmds 会截胡，不能打开 auth/cmds/////

	//_ "taiyouxi/platform/x/auth/cmds/auth"
	_ "taiyouxi/platform/x/auth/cmds/login"
	//_ "taiyouxi/platform/x/auth/cmds/verupdateurl"

	//	"github.com/codegangsta/cli"
	//"taiyouxi/platform/planx/util/logs"
	//"taiyouxi/platform/planx/version"
	//"taiyouxi/platform/x/auth/cmds"

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
	log.Trace("main cmd count=%d", cmds.GetCmdCount())
	cmds.InitCommands(&app.Commands)

	log.Trace("main.go main function()")

	// //测试(无用，单向调用，增加cmds数组？？)
	// cmds.Register(&cli.Command{
	// 	Name:   "bot",
	// 	Usage:  "run a bot",
	// 	Action: BotStart,
	// 	Flags: []cli.Flag{
	// 		cli.StringFlag{
	// 			Name:  "rpc, r",
	// 			Value: "127.0.0.1:8668",
	// 			Usage: "gate server rpc ip:port",
	// 		},
	// 		cli.StringFlag{
	// 			Name:  "server",
	// 			Value: "127.0.0.1:8667",
	// 			Usage: "gate server ip:port",
	// 		},
	// 		cli.Float64Flag{
	// 			Name:  "speed, s",
	// 			Value: 1.0,
	// 			Usage: "2.0 2xtimes faster, 0 fixed 100ms",
	// 		},
	// 	},
	// })

	// //测试2
	// app.Commands = []cli.Command{
	// 	{
	// 		Name:        "describeit",
	// 		Aliases:     []string{"d"},
	// 		Usage:       "use it to see a description",
	// 		Description: "This is how we describe describeit the function",
	// 		Action: func(c *cli.Context) error {
	// 			fmt.Printf("i like to describe things")
	// 			return nil
	// 		},
	// 	},
	// }

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
