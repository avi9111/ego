package monitor

import (
	"time"

	"github.com/codegangsta/cli"

	redisStorageApi "taiyouxi/platform/x/redis_storage/api"
	"taiyouxi/platform/x/redis_storage/cmds"
	"taiyouxi/platform/x/redis_storage/config"

	"fmt"

	"taiyouxi/platform/planx/util"
	"taiyouxi/platform/planx/util/etcd"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/util/signalhandler"
)

const ()

func init() {
	logs.Trace("monitor cmd loaded")
	cmds.Register(&cli.Command{
		Name:   "warm",
		Usage:  "启动冷热切换Api模式",
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

	apiService := redisStorageApi.NewAPIService()
	apiService.Init(
		config.CommonCfg.EtcdRoot,
		config.CommonCfg.Api_Addr,
		fmt.Sprintf("%d", config.CommonCfg.ShardId[0]))

	// 启动
	go apiService.Start()

	var waitGroup util.WaitGroupWrapper

	signalhandler.SignalKillHandler(apiService)

	//handle kill signal
	waitGroup.Wrap(func() { signalhandler.SignalKillHandle() })
	waitGroup.Wait()

	logs.Close()
	time.Sleep(1 * time.Second)

}
