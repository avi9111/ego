package allinone

import (
	"time"

	"github.com/codegangsta/cli"

	"taiyouxi/platform/x/redis_storage/cmds"
	"taiyouxi/platform/x/redis_storage/cmds/helper"
	"taiyouxi/platform/x/redis_storage/command"
	"taiyouxi/platform/x/redis_storage/config"

	"taiyouxi/platform/planx/util"
	"taiyouxi/platform/planx/util/etcd"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/util/signalhandler"
	"taiyouxi/platform/planx/util/storehelper"
)

func init() {
	logs.Trace("allinone cmd loaded")
	cmds.Register(&cli.Command{
		Name:   "allinone",
		Usage:  "开启所有功能",
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
	err := etcd.InitClient(config.CommonCfg.EtcdEndpoint)
	if err != nil {
		panic(err)
	}

	onlandStores := helper.InitOnLandStores()
	redisMonitor := helper.InitMonitor(onlandStores)
	restorer := helper.InitRestore()
	if restorer == nil {
		return
	}
	helper.InitBackends(func(s storehelper.IStore) {
		onlandStores.Add(s)
	})

	cmd := command.NewCmdService(config.CommonCfg.Command_Addr)
	onlandStores.Register(cmd)
	restorer.Register(cmd)

	// 启动
	go onlandStores.Start()
	go redisMonitor.Start()
	go restorer.Start()
	go cmd.Start()

	var waitGroup util.WaitGroupWrapper

	signalhandler.SignalKillHandler(redisMonitor)
	signalhandler.SignalKillHandler(onlandStores)
	signalhandler.SignalKillHandler(restorer)
	signalhandler.SignalKillHandler(cmd)

	//handle kill signal
	waitGroup.Wrap(func() { signalhandler.SignalKillHandle() })
	waitGroup.Wait()

	logs.Close()
	time.Sleep(1 * time.Second)

}
