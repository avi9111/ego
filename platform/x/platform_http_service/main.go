package main

import (
	"taiyouxi/platform/planx/util"
	pConfig "taiyouxi/platform/planx/util/config"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/x/platform_http_service/config"
	"taiyouxi/platform/x/platform_http_service/db"
	gin2 "taiyouxi/platform/x/platform_http_service/gin"

	"github.com/gin-gonic/gin"
)

func main() {
	defer logs.Close()

	logxml := pConfig.NewConfigPath("log.xml")
	logs.LoadLogConfig(logxml)

	config.LoadConfig("config.toml")
	config.InitEtcd(config.CommonConfig.EtcdCfg.EtcdEndPoint)

	config.LoadGameData("")

	db.InitDynamoDB()

	waiter := util.WaitGroupWrapper{}
	for _, cfg := range config.CommonConfig.PlatformConfigArray {
		waiter.Wrap(func() {
			gin := gin.Default()
			plat := gin2.NewPlatform(cfg)
			plat.RegHandler(gin)
			gin.Run(cfg.Host)
		})
	}
	waiter.Wait()
}
