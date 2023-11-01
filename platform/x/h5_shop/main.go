package main

import (
	"taiyouxi/platform/planx/util/etcd"
	"taiyouxi/platform/planx/util/logs"
	authConfig "taiyouxi/platform/x/auth/config"
	h5Config "taiyouxi/platform/x/h5_shop/config"
	"taiyouxi/platform/x/h5_shop/sdk_shop"

	"github.com/gin-gonic/gin"

	"taiyouxi/platform/x/gift_sender/config"
)

func main() {
	h5Config.LoadConfig("conf/config.toml")
	logs.Debug("%v", h5Config.CommonConfig)
	err := etcd.InitClient(h5Config.CommonConfig.EtcdEndPoint)
	if err != nil {
		logs.Error("etcd InitClient err %s", err.Error())
		logs.Critical("\n")
		logs.Close()
		return
	}
	authConfig.Cfg.Runmode = "test"
	authConfig.Cfg.EtcdRoot = h5Config.CommonConfig.EtcdRoot
	config.CommonConfig.EtcdRoot = h5Config.CommonConfig.EtcdRoot
	logs.Debug("load from h5shop config %v", h5Config.CommonConfig)
	gin := gin.Default()
	gin.POST("user/costdiamond.html", sdk_shop.CostDiamond)
	gin.Run(h5Config.CommonConfig.Port)
}
