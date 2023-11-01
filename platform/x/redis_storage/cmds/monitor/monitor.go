package monitor

import (
	"time"

	"github.com/codegangsta/cli"

	"taiyouxi/platform/x/redis_storage/cmds"
	"taiyouxi/platform/x/redis_storage/cmds/helper"
	"taiyouxi/platform/x/redis_storage/command"
	"taiyouxi/platform/x/redis_storage/config"

	"taiyouxi/platform/planx/util"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/util/signalhandler"
	"taiyouxi/platform/planx/util/storehelper"
)

const ()

func init() {
	logs.Trace("monitor cmd loaded")
	cmds.Register(&cli.Command{
		Name:   "monitor",
		Usage:  "启动监听Redis修改同步模式",
		Action: Start,
		Flags: []cli.Flag{
			cli.StringFlag{
				Name:  "config, c",
				Value: "onland.toml",
				Usage: "Onland Configuration toml config, in {CWD}/conf/ or {AppPath}/conf",
			},
		},
	})
}

func Start(c *cli.Context) {
	config.ReadConfig(c)

	onlandStores := helper.InitOnLandStores()
	redisMonitor := helper.InitMonitor(onlandStores)
	helper.InitBackends(func(s storehelper.IStore) {
		onlandStores.Add(s)
	})

	cmd := command.NewCmdService(config.CommonCfg.Command_Addr)
	onlandStores.Register(cmd)

	// 启动
	go onlandStores.Start()
	go redisMonitor.Start()
	go cmd.Start()

	var waitGroup util.WaitGroupWrapper

	signalhandler.SignalKillHandler(redisMonitor)
	signalhandler.SignalKillHandler(onlandStores)
	signalhandler.SignalKillHandler(cmd)

	//handle kill signal
	waitGroup.Wrap(func() { signalhandler.SignalKillHandle() })
	waitGroup.Wait()

	logs.Close()
	time.Sleep(1 * time.Second)

}
